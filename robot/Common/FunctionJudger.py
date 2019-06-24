#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Resource import *
from Logger import Logger
import Environment
import MachineClicker
import MachineParser
import FunctionDetector


class FunctionJudger(object):
    """Function Layer Judger Series KeyWork Implemention Class

    is_exist is to check item(title/option...) exist.
    is_option_seleted is to check option given whether was selected.
    is_window_seleted is to check window given whether was selected.
    """

    def __init__(self):
        """constructor for FunctionJudger
        """
        super(FunctionJudger, self).__init__()
        # self.update_params(Environment.Environment().function_info_dict)
        self.__logger = Logger("FunctionJudger").get()

    def update_params(self, function_info_dict):
        """update params func

        judger is dependent on Machine Layer's paser,
                               Function Layer's detector.

        Arguments:
            function_info_dict {dict} -- funtion info dict
        """
        self.__machine_clicker = MachineClicker.MachineClicker()
        self.__machine_parser = MachineParser.MachineParser()
        self.__function_detector = FunctionDetector.FunctionDetector()

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
    def is_exist(self, item):
        """To Judge Whether item exsit in this menu

        Arguments:
            item {str} -- item value to judge

        Returns:
            bool -- true means exist
        """
        if self.__machine_clicker.find(item) is None:
            self.__logger.warning(
                "Item [%s] is not existed in current menu.", item)
            return False
        self.__logger.info("Item [%s] is existed in current menu.", item)
        return True

    @update_params_dec
    def is_option_selected(self, option):
        """To Judge Whether option was selected

        Arguments:
            option {str} -- option value to judge

        Returns:
            bool -- true means selected
        """
        if self.__function_detector.get_option_selected() == option:
            self.__logger.info(
                "Option [%s] is selected in current menu.", option)
            return True
        self.__logger.warning(
            "Option [%s] is not selected in current menu.", option)
        return False

    @update_params_dec
    def is_window_selected(self, window):
        """To Judge Whether window option was selected

        Arguments:
            window {str} -- window value to judge

        Returns:
            bool -- true means selected
        """
        if self.__function_detector.get_window_value() == window:
            self.__logger.info(
                "Window [%s] is selected in current menu.", window)
            return True
        self.__logger.info(
            "Window [%s] is not selected in current menu.", window)
        return False


if __name__ == '__main__':
    print "test start: "
    Environment.Environment(function_info_dict, machine_info_dict)
    functionJudger = FunctionJudger()
    print functionJudger.is_exist("xxx")
