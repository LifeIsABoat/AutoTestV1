#!/usr/bin/env python
# -*- coding: utf-8 -*-


from Rectangle import Rectangle
from Point import Point
from Resource import *
from Logger import Logger
import Environment
import MachineClicker
import MachineParser


class FunctionSetter(object):
    """Function Layer Setter Series KeyWork Implemention Class

    set_option is to set the current Option item.
    input_optionL is to input option on the screen input box.
    set_window is to set window item on the screen.
    """

    def __init__(self):
        """constructor for FunctionSetter
        """
        super(FunctionSetter, self).__init__()
        self.__logger = Logger("FunctionSetter").get()
        # self.update_params(Environment.Environment().function_info_dict)

    def update_params(self, function_info_dict):
        """update params func

        setter is dependent on Machine Layer's clicker/parser,
            Resource's input_caps_lock_region/input_clear_region
                       /input_tab_region/input_ok_button/window_5_template
                       /window_11_template/window_ok_button

        input_caps_lock_region:Switch key region with uppercase
                               and lowercase letters.
        input_clear_region:Delete key region.
        input_tab_region:Switch key for different input keyboard.
        input_ok_button:OK button in the input screen
        window_5_template:There are 5 form in the form screen.
        window_11_template:There are 11 form in the form screen.
        window_ok_button:OK button in the form screen

        Arguments:
            function_info_dict {dict} -- funtion info dict

        Raises:
            ValueError -- could not get value in machine info
                          and parser info given.
        """
        self.__machine_clicker = MachineClicker.MachineClicker()
        self.__machine_parser = MachineParser.MachineParser()
        if "input_caps_lock_region" in function_info_dict:
            self.__input_caps_lock_region = Rectangle(
                function_info_dict["input_caps_lock_region"]).center()
        else:
            raise ValueError('input_caps_lock_region not exist')
        if "input_clear_region" in function_info_dict:
            self.__input_clear_region = Rectangle(
                function_info_dict["input_clear_region"]).center()
        else:
            raise ValueError('input_clear_region not exist')
        if "input_tab_region" in function_info_dict:
            self.__input_tab_region = function_info_dict["input_tab_region"]
        else:
            raise ValueError('input_tab_region not exist')
        if "input_ok_button" in function_info_dict:
            self.__input_ok_button = function_info_dict["input_ok_button"]
        else:
            raise ValueError('input_ok_button not exist')
        if "window_5_template" in function_info_dict:
            self.__window_5_template = function_info_dict["window_5_template"]
        else:
            raise ValueError('color_set_5 not exist')
        if "window_11_template" in function_info_dict:
            self.__window_11_template = \
                function_info_dict["window_11_template"]
        else:
            raise ValueError('window_11_template not exist')
        if "window_ok_button" in function_info_dict:
            self.__window_ok_button = function_info_dict["window_ok_button"]
        else:
            raise ValueError('window_ok_button not exist')
        if "key_board_dict" in function_info_dict:
            self.__key_board_dict = function_info_dict["key_board_dict"]
        else:
            raise ValueError('key_board_dict not exist')
        if "admin_password_regions" in function_info_dict:
            self.__admin_password_regions = function_info_dict["admin_password_regions"]

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
    def set_option(self, target):
        """Set the current Option item.

        Arguments:
            target {string} -- The Option item that needs to be set.

        Returns:
            bool -- true means success
        """
        self.__logger.info("Set option start.")
        ret = self.__machine_clicker.click(target)
        if ret is None:
            self.__logger.error("Set option failed by click fialed..")
        else:
            self.__logger.info("Set option succeed.")
        return ret

    @update_params_dec
    def input_option(self, target):
        """Input option on the screen input box

        Arguments:
            target {string} -- The string to be input.

        Returns:
            bool -- true means success
        """
        self.__logger.info("Input option [%s] start.", target)
        # Clear the input box
        for x in xrange(1, 16):
            if not self.__machine_clicker.click_in_screen(
                    self.__input_clear_region):
                self.__logger.warning("Input option [%s] failed by\
 click clear region [%s] fialed.", target, self.__input_clear_region)
                return False

        for character in target:

            if character == " ":
                if self.__machine_clicker.click_in_screen("Space"):
                    continue

            if character == "-":
                if self.__machine_clicker.click_in_screen("Pause"):
                    continue

            # print character
            # If the target exists, click.
            if self.__machine_clicker.click_in_screen(character):
                continue

            # Click the uppercase and lowercase key（A <=> a）.
            if self.__machine_clicker.click_in_screen(
                    self.__input_caps_lock_region):
                if self.__machine_clicker.click_in_screen(character):
                    continue

            if self.click_tab_button(character) is False:
                self.__logger.warning("Input option [%s] failed by\
 click [%s] fialed.", target, character)
                return False

        ret = self.__machine_clicker.click_in_screen(self.__input_ok_button)
        if ret is False:
            self.__logger.warning("Input option [%s] failed by\
 click [%s] fialed.", target, self.__input_ok_button)
        else:
            self.__logger.info("Input option [%s] succeed", target)
        return ret

    @update_params_dec
    def click_tab_button(self, character):
        """Click tab button to switch key board.

        the tab button region list is ECL([0,0,0,0])
            or BC4([[0,0,0,0],[0,0,0,0],[0,0,0,0]])

        Arguments:
            character {list} -- the tab button region list.
        """
        if isinstance(self.__input_tab_region[0], int):
            count = 0
            # Click the tab(A 1 @) button to change the screen key board.
            while count < 4:
                self.__machine_clicker.click_in_screen(
                    Rectangle(self.__input_tab_region).center())
                if self.__machine_clicker.click_in_screen(character):
                    break
                count += 1
            if count >= 4:
                return False
        elif isinstance(self.__input_tab_region[0], list):
            count = 0
            # Click the tab(A 1 @) button to change the screen key board.
            while count < 3:
                self.__machine_clicker.click_in_screen(
                    Rectangle(self.__input_tab_region[count]).center())
                if self.__machine_clicker.click_in_screen(character):
                    break
                count += 1
            self.__machine_clicker.click_in_screen(
                Rectangle(self.__input_tab_region[0]).center())
            if count >= 3:
                return False
        return True

    @update_params_dec
    def set_window(self, target):
        """Set window item on the screen

        Arguments:
            target {int} -- The value that need to be set.

        Returns:
            bool -- true means success
        """
        self.__logger.info("Set window [%s] start.", target)
        # Get all button region on the current screen
        all_region_list = self.__machine_parser.find_all_button_region()

        # Get the target area
        target_region_list = []
        #  filter region if width != height
        all_region_list = filter(
            lambda rect: rect.w == rect.h, all_region_list)
        #  sort all region list
        all_region_list = sorted(
            all_region_list, key=lambda rect: (rect.y, rect.x))
        #  get max count regions
        max_index = 0
        max_count = 0
        for i in xrange(0, len(all_region_list)):
            y_equal_list = filter(lambda rect: rect.y ==
                                  all_region_list[i].y, all_region_list)
            if len(y_equal_list) > max_count:
                max_count = len(y_equal_list)
                max_index = i
        #  get target_region_list by max cout of equal list
        if max_count > 0:
            target_region_list = all_region_list[
                max_index:max_index + max_count]

        is_clicked = False
        # The target area is 5.
        if len(target_region_list) is 5:
            for window_5_template in self.__window_5_template:
                # Find the index of the target in the template.
                if target in window_5_template:
                    self.__logger.info(
                        "Found target [%s] in template [%s].",
                        target, window_5_template)
                    index = window_5_template.index(target)
                    # Click on the target area according to the index.
                    if self.__machine_clicker.click_in_screen(
                            target_region_list[index]):
                        is_clicked = True
                        break
        # The target area is 11.
        elif len(target_region_list) is 11:
            for window_11_template in self.__window_11_template:
                # Find the index of the target in the template.
                if target in window_11_template:
                    self.__logger.info(
                        "Found target [%s] in template [%s].",
                        target, window_11_template)
                    index = window_11_template.index(target)
                    # Click on the target area according to the index.
                    if self.__machine_clicker.click_in_screen(
                            target_region_list[index]):
                        is_clicked = True
                        break
        if not is_clicked:
            self.__logger.warning(
                "Got unexpected window option target.")
            return False
        return True
        # ret = self.__machine_clicker.click_in_screen(self.__window_ok_button)
        # if ret is False:
        #     self.__logger.warning("Set window [%s] failed by click [%s]",
        #                           target, self.__window_ok_button)
        # else:
        #     self.__logger.warning("Set window [%s] succeed")
        # return ret

    @update_params_dec
    def input_key_value(self, target):
        """Click key board to input

        Arguments:
            target {stirng} -- key value name

        Returns:
            bool -- True means success
        """
        self.__logger.info("Input key value [%s] start.", target)
        for character in target:
            if character in self.__key_board_dict:
                if self.__machine_clicker.click_key_board(
                        self.__key_board_dict.get(character)) is False:
                    self.__logger.error(
                        "Click key value [%s] failed when input [%s].",
                        character, target)
                    return False
            else:
                self.__logger.warning(
                    "Key [%s] is not exist in key_board_dict of Resource.",
                    character)
                return False
        return True

    @update_params_dec
    def input_admin_password(self):
        """Input option on the screen input box

        Arguments:
            target {string} -- The string to be input.

        Returns:
            bool -- true means success
        """
        for region in self.__admin_password_regions:
            if self.__machine_clicker.click_in_screen(Rectangle(region).center()):
                self.__logger.info(
                    "Click [%s] succeed in input admin password function", region)
            else:
                self.__logger.warning(
                    "Click [%s] failed in input admin password function", region)

        ret = self.__machine_clicker.click_in_screen(self.__input_ok_button)
        if ret is False:
            self.__logger.warning("Input admin password succeed")
        else:
            self.__logger.info("Input admin password succeed")
        return ret


if __name__ == '__main__':
    print "test start: "
    Environment.Environment(function_info_dict, machine_info_dict)
    setter = FunctionSetter()
    print setter.input_admin_password()
