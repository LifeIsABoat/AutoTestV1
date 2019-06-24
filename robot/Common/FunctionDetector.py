#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Resource import *
from Logger import Logger
import MachineClicker
import MachineParser
import Environment


class FunctionDetector(object):
    """Function Layer Detector Series KeyWork Implemention Class

    get_option_selected is to get the word of the selected button.
    get_input_value is to get the string in the input box.
    get_window_value is to get the value in the window setting.
    get_option_value is to get the set value of the options
                           in the current menu (bottom right)
    get_option_status is to get the status of target item in the menue.
    __my_sort is to sort the rectangle list.
    """

    def __init__(self):
        """constructor for FunctionDetector
        """
        super(FunctionDetector, self).__init__()
        # self.update_params(Environment.Environment().function_info_dict)
        self.__logger = Logger("FunctionDetector").get()

    def update_params(self, function_info_dict):
        """update params func

        Detector is dependent on Machine Layer's clicker/Parser,
                                Resource's input_box_region/window_value_region

        input_box_region:the coordinates of thr input box in the input screen.
        window_value_region:the coordinates of values in the form screen.

        Arguments:
            function_info_dict {dic} -- function info dict

        Raises:
            ValueError -- could not get value in function info given.
        """
        if "input_box_region" not in function_info_dict:
            raise ValueError('input_box_region not exist')
        self.__input_box_region = function_info_dict["input_box_region"]
        if "window_value_region" not in function_info_dict:
            raise ValueError('input_box_region not exist')
        self.__window_value_region = function_info_dict["window_value_region"]
        self.__machine_parser = MachineParser.MachineParser()
        self.__machine_clicker = MachineClicker.MachineClicker()

    def update_params_dec(func):
        """Decorator to update params after call target function

        Arguments:
            func {function} -- funtion to decoate

        Returns:
            function -- [func decorated]
        """
        def execute(*args, **kw):
            args[0].update_params(Environment.Environment().function_info_dict)
            return func(*args, **kw)
        return execute

    @update_params_dec
    def get_option_selected(self):
        """Gets the word of the selected button.

        Returns:
            string -- The word of the selected button.
        """
        self.__logger.info("Get option selected start.")
        selected_word = self.__machine_clicker.run_on_all_screens(
            self.__machine_parser.get_selected_button_word)
        if selected_word is None:
            self.__logger.warning("Get option selected failed.")
        self.__logger.info("Get option selected succeed.")
        return selected_word

    @update_params_dec
    def get_input_value(self):
        """Gets the string in the input box.

        Returns:
            string -- The string in the input box.
        """
        self.__logger.info("Get input value start.")
        input_value = self.__machine_parser.get_target_text_result(
            self.__input_box_region)
        self.__logger.info("Get input value succeed.")
        return input_value

    @update_params_dec
    def get_window_value(self):
        """Gets the value in the window setting.

        Returns:
            string -- The value that window sets.
        """
        self.__logger.info("Get input value start.")
        window_value = self.__machine_parser.get_target_text_result(
            self.__window_value_region)
        self.__logger.info("Get input value succeed.")
        return window_value

    @update_params_dec
    def get_option_value(self, item_name):
        """Gets the set value of the options in the current menu (bottom right)

        Arguments:
            item_name {string} -- Word in the upper left

        Returns:
            string -- Word in the bottom right
        """
        self.__logger.info("Get option [%s] value start.", item_name)
        if self.__machine_clicker.find(item_name) is not None:
            button_region_list = self.__my_sort(
                self.__machine_parser.find_all_button_region())
            if button_region_list is None:
                self.__logger.warning("Get option [%s] value failed by\
 button region detect failed.", item_name)
                return None
            text_region_list = self.__my_sort(
                self.__machine_parser.find_all_text_region())
            if text_region_list is None:
                self.__logger.warning("Get option [%s] value failed by\
 text region detect failed.", item_name)
                return None
            text_list = self.__machine_parser.get_all_text_result(
                text_region_list)
            if text_list is None or len(text_region_list) != len(text_list):
                self.__logger.error(
                    "Got different size between region and text.")
                raise RuntimeError(
                    "Got different size between region and text.")
            if item_name in text_list:
                for button_region in button_region_list:
                    index = text_list.index(item_name)
                    if button_region.is_contains(text_region_list[index]):
                        index += 1
                        if index > len(text_list) - 1:
                            break
                        if button_region.is_contains(text_region_list[index]):
                            self.__logger.info("Get option [%s] value [%s]\
 succeed.", item_name, text_list[index])
                            return text_list[index]
        self.__logger.warning("Get option [%s] value [%s] failed.", item_name)
        return None

    @update_params_dec
    def get_option_status(self, item_name):
        """Gets the status of target item in the menue.

        Arguments:
            item_name {str} -- target item name.

        Returns:
            str -- target button status.
        """
        self.__logger.info("Get option [%s] status start.", item_name)
        item_pos = self.__machine_clicker.find(item_name)
        if item_pos is None:
            self.__logger.warning(
                "Get option [%s] status fail by no such option exist.",
                item_name)
            return None

        button_region_list = self.__machine_parser.find_all_button_region()
        if button_region_list is None:
            self.__logger.warning("Get option [%s] status failed by\
 button region detect failed.", item_name)
            return None
        button_status_list = self.__machine_parser.get_all_button_status(
            button_region_list)
        if button_status_list is None\
                or len(button_region_list) != len(button_status_list):
            self.__logger.error(
                "Got different size between region and status.")
            raise RuntimeError(
                "Got different size between region and status.")
        for i in range(0, len(button_region_list)):
            if item_pos.is_insides(button_region_list[i]):
                self.__logger.info("Get option [%s] status [%s]\
 succeed.", item_name, button_status_list[i])
                return button_status_list[i]
        self.__logger.warning("Get option [%s] status failed by\
 button disappear, what's the fuck.", item_name)
        return None

    def __my_sort(self, rectangle_list):
        """Sort the rectangle list.

        Sort by the Y coordinate of the rectangle.

        Arguments:
            rectangle_list {list} -- the rectangle list.

        Returns:
            list -- the rectangle list.
        """
        if rectangle_list is None:
            return None
        for i in range(len(rectangle_list) - 1):
            for j in range(len(rectangle_list) - i - 1):
                if rectangle_list[j].y > rectangle_list[j + 1].y:
                    rectangle_list[j], rectangle_list[j + 1] = \
                        rectangle_list[j + 1], rectangle_list[j]
        return rectangle_list


if __name__ == '__main__':
    print "test start: "
    Environment.Environment(function_info_dict, machine_info_dict)
    detector = FunctionDetector()
    # print detector.get_option_selected()
    # print detector.get_input_value()
    print detector.get_option_status('to OCR')
    # print detector.get_window_value()
    # print detector.get_option_value("Check Paper")
