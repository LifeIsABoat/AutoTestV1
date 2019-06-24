#!/usr/bin/env python
# -*- coding: utf-8 -*-


from Resource import *
from Logger import Logger
import Environment
import MachineWait
import MachineClicker
import MachineParser
import time


class FunctionTransfer(object):
    """Function Layer Transfer Series KeyWork Implemention Class

    move_to is to transfer to target screen from current screen.
    back_previous_level is to transfer to previous level screen.
    back_home is to tansfer to home screen.
    """

    def __init__(self):
        """constructor for FunctionTransfer
        """
        super(FunctionTransfer, self).__init__()
        self.__logger = Logger("FunctionTransfer").get()
        # self.update_params(Environment.Environment().function_info_dict)

    def update_params(self, function_info_dict):
        """update params func

        transfer is dependent on Machine Layer's clicker/wait,
                                 Resource's home_key/clear_key.
        home_key:home key name
        clear_key:back key name

        Arguments:
            function_info_dict {dic} -- function info dict

        Raises:
            ValueError -- could not get value in function info given.
        """
        self.__machine_clicker = MachineClicker.MachineClicker()
        self.__machine_wait = MachineWait.MachineWait()
        self.__machine_parser = MachineParser.MachineParser()
        if "home_key" in function_info_dict:
            self.__home_key = function_info_dict["home_key"]
        else:
            raise ValueError('home_key not exist')
        if "clear_key" in function_info_dict:
            self.__clear_key = function_info_dict["clear_key"]
        else:
            raise ValueError('clear_key not exist')

        if "home_screen" in function_info_dict:
            self.__home_screen = function_info_dict["home_screen"]
        else:
            raise ValueError('home_screen not exist')
        if "home_similarity" in function_info_dict:
            self.__home_similarity = function_info_dict["home_similarity"]
        else:
            raise ValueError('home_similarity not exist')
        if "times_wait_for_home" in function_info_dict:
            self.__times_wait_for_home = \
                function_info_dict["times_wait_for_home"]
        else:
            raise ValueError('times_wait_for_home not exist')

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
    def move_to(self, path):
        """Move to the next screen from current screen

        Example : path -- ["Settings", "All Settings", "Initial Setup",
            "Reset", "Machine Reset"]

        Arguments:
            path {list} -- the path of the screen transference

        Returns:
            bool -- true means success
        """
        if len(path) == 1 and isinstance(path[0], list):
            path = path[0]

        self.__logger.info("Move to [%s] start.", path)
        for word in path:
            if not self.__machine_clicker.click(word):
                self.__logger.warning(
                    "Move to [%s] failed when click [%s]", path, word)
                return False
        self.__logger.info("Move to [%s] succeed.", path)
        return True

    @update_params_dec
    def move_to_from_home(self, path):
        """Move to the next screen from home screen

        Example : path -- ["Settings", "All Settings", "Initial Setup",
            "Reset", "Machine Reset"]

        Arguments:
            path {list} -- the path of the screen transference

        Returns:
            bool -- true means success
        """
        if len(path) == 1 and isinstance(path[0], list):
            path = path[0]

        self.__logger.info("Move to [%s] from home start.", path)
        if self.back_home() is False:
            self.__logger.warning(
                "Move to [%s] from home failed by back home failed.", path)
            return False
        if self.move_to(path) is False:
            self.__logger.warning("Move to [%s] from home failed by\
 move to [%s] failed.", path, path)
            return False
        self.__logger.info("Move to [%s] from home succeed.", path)
        return True

    @update_params_dec
    def back_previous_level(self):
        """Back to the pre screen.

        Returns:
            bool -- true means success
        """
        self.__logger.info("Back to previous level start.")
        if not self.__machine_clicker.click_key_board(self.__clear_key):
            self.__logger.info("Back to previous level failed by\
 click [%s] failed.", self.__clear_key)
            return False
        self.__logger.info("Back to previous level succeed.")
        return True

    @update_params_dec
    def back_home(self):
        """Back Home screen

        Returns:
            bool -- true means success
        """
        self.__logger.info("Back home start.")

        ret = False
        if self.__machine_clicker.click_key_board(self.__home_key):
            # back home by click home key
            if self.__check_home():
                ret = True
            # back home by wait 60s.
            # else:
            #     self.__logger.info("Info:go home failed, wait times 60s")
            #     self.__machine_wait.wait(self.__times_wait_for_home)
            #     if self.__check_home():
            #         self.__logger.info(
            #             "Info:after wait times 60s, go home succeed")
            #         ret = True
            #     else:
            #         self.__logger.info(
            #             "Info:after wait times 60s, go home failed")
            #         ret = False

        if ret is True:
            self.__logger.info("Back home succeed.")
        else:
            self.__logger.warning("Back home failed")
        return ret

    def __check_home(self):
        """Check whether the current screen is Home screen.

        Returns:
            bool -- true means that back to home
        """
        self.__logger.info("Check home start.")
        if self.__machine_parser.find_by_hit(self.__home_screen,
                                             self.__home_similarity) is None:
            self.__logger.warning("Check home failed.")
            return False
        self.__logger.info("Check home succeed.")
        return True


if __name__ == '__main__':
    print "test start:"
    Environment.Environment(function_info_dict, machine_info_dict)
    function_transfer = FunctionTransfer()

