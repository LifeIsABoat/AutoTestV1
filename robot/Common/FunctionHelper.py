#!/usr/bin/env python
# -*- coding: utf-8 -*-


from Resource import *
from Logger import Logger
import time
import Environment
import MachineClicker
import MachineWait
import FunctionTransfer
import FunctionSetter
import FunctionDetector


class FunctionHelper(object):
    """Function Layer Helper Series KeyWork Implemention Class

    all_settings_reset is to reset machine.
    stop_operation is to stop current opertion in machine.
    run_option_condition is to do internal option setting contion.
    detect_language is to detect machine current language setting.
    switch_language is to switch machine language setting to target.
    get_wiredlan_ip is to get machine wired lan ip address.
    get_wlan_ip to get machine wlan ip address.
    """

    def __init__(self):
        """constructor for FunctionHelper
        """
        super(FunctionHelper, self).__init__()
        # self.update_params(Environment.Environment().function_info_dict)
        self.__logger = Logger("FunctionHelper").get()

    def update_params(self, function_info_dict):
        """update params func

        helper is dependent on Machine Layer's clicker/wait,
                               Function Layer's transfer/setter,
                               Resource's reset_path/reset_ok/reset_ok_time
                                          /reset_wait_time/reset_after_path/stop_key
        reset_path:Reset Menu Path
        reset_ok:OK's string,it might be '是'/'はい' and so on.
        reset_ok_time:time interval when push ok
        reset_wait_time:time to wait machine reset finished
        reset_after_path:path to click after machine reset
        stop_key:stop key name

        Arguments:
            function_info_dict {dic} -- funtion info dict

        Raises:
            ValueError -- could not get value in function info given.
        """
        if "reset_path" not in function_info_dict or\
           "reset_ok" not in function_info_dict or\
           "reset_ok_time" not in function_info_dict or\
           "reset_wait_time" not in function_info_dict or\
           "reset_after_path" not in function_info_dict:
            raise ValueError('reset param parse failed')
        self.__reset_path = function_info_dict["reset_path"]
        self.__reset_ok = function_info_dict["reset_ok"]
        self.__reset_ok_time = function_info_dict["reset_ok_time"]
        self.__reset_wait_time = function_info_dict["reset_wait_time"]
        self.__reset_after_path = function_info_dict["reset_after_path"]

        if "stop_key" not in function_info_dict:
            raise ValueError('stop_key not exist')
        self.__stop_key = function_info_dict["stop_key"]

        if "language_path" not in function_info_dict:
            raise ValueError('language_path not exist')
        self.__language_path = function_info_dict["language_path"]

        if "ethernet_ip_path" not in function_info_dict:
            raise ValueError('ethernet_ip_path not exist')
        self.__ethernet_ip_path = function_info_dict["ethernet_ip_path"]

        if "wifi_ip_path" not in function_info_dict:
            raise ValueError('wifi_ip_path not exist')
        self.__wifi_ip_path = function_info_dict["wifi_ip_path"]
        self.__machine_clicker = MachineClicker.MachineClicker()
        self.__machine_wait = MachineWait.MachineWait()
        self.__function_transfer = FunctionTransfer.FunctionTransfer()
        self.__function_setter = FunctionSetter.FunctionSetter()
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
    def all_settings_reset(self):
        """Restart the Machine.

        Returns:
            bool -- true means success
        """
        self.__logger.info("All settings reset start.")
        if not self.__function_transfer.move_to_from_home(self.__reset_path):
            self.__logger.warning("All settings reset failed by\
 move to reset path [%s] failed.", self.__reset_path)
            return False
        region = self.__machine_clicker.exist_in_screen(self.__reset_ok)
        if region is None:
            self.__logger.warning(
                "All settings reset failed by\
 no such a [%s] in screen.", self.__reset_ok)
            return False
        position = region.center()
        if not self.__machine_clicker.touchpanel_push(position):
            self.__logger.warning(
                "All settings reset failed by tp push failed.")
            return False
        time.sleep(self.__reset_ok_time)
        if not self.__machine_clicker.touchpanel_release(position):
            self.__logger.warning(
                "All settings reset failed by tp release failed.")
            return False
        time.sleep(self.__reset_wait_time)
        if not self.__machine_wait.wait_change(180000):
            self.__logger.warning(
                "All settings reset failed by wait screen change failed.")
            return False
        for target in self.__reset_after_path:
            ret, region = self.__machine_wait.wait_appear(target, 50)
            if ret is True:
                if not self.__machine_clicker.click_in_screen(region.center()):
                    self.__logger.warning("All settings reset failed\
 by tp click [%s] failed.", region.center())
                    return False
            else:
                self.__logger.warning("All settings reset failed by\
 wait [%s] appear failed.", targets)
                return False
        self.__logger.info("All settings reset succeed.")
        return True

    @update_params_dec
    def stop_operation(self):
        """Stop current operation.

        Stop current operation immediately, such as copy/scan and so on.

        Returns:
            bool -- true means success
        """
        self.__logger.info("Stop current operation start.")
        if not self.__machine_clicker.click_key_board(self.__stop_key):
            self.__logger.info("Stop current operation failed by\
 kb click [%s] failed.", self.__stop_key)
            return False
        self.__logger.info("Stop current operation succeed.")
        return True

    @update_params_dec
    def run_option_condition(self, path):
        """Run option setting condition.

        Run option setting condition automaticaly,
        by given option menu path and option name.

        Arguments:
            path {list} -- path with option
        Returns:
            bool -- true means success
        """
        if len(path) == 1 and isinstance(path[0], list):
            path = path[0]

        self.__logger.info("Run option setting condition start.")
        if not self.__function_transfer.move_to_from_home(path[:-1]):
            self.__logger.warning("Run option setting condition failed by\
 move to [%s] failed.", path[:-1])
            return False
        if not self.__function_setter.set_option(path[-1]):
            self.__logger.warning("Run option setting condition failed by\
 set option [%s] failed.", path[-1])
            return False
        self.__logger.info("Run option setting condition succeed.")
        return True

    @update_params_dec
    def input_option_condition(self, path):
        """Run option setting condition.

        Run option setting condition automaticaly,
        by given option menu path and option name.

        Arguments:
            path {list} -- path with option
        Returns:
            bool -- true means success
        """
        if len(path) == 1 and isinstance(path[0], list):
            path = path[0]

        self.__logger.info("Run option setting condition start.")
        if not self.__function_transfer.move_to_from_home(path[:-1]):
            self.__logger.warning("Run option setting condition failed by\
 move to [%s] failed.", path[:-1])
            return False
        if not self.__function_setter.input_option(path[-1]):
            self.__logger.warning("Run option setting condition failed by\
 set option [%s] failed.", path[-1])
            return False
        self.__logger.info("Run option setting condition succeed.")
        return True

    @update_params_dec
    def detect_language(self):
        """Detect Language

        Detect Language by Check Language Option Selected.
        Todo Todo Todo

        Returns:
            str -- language in screen
        """
        # todo to fix path list refer to masterlist
        self.__logger.info("Detect language start.")
        if not self.__function_transfer.move_to_from_home(
                self.__language_path):
            self.__logger.warning("Detect language failed by\
 move to [%s] failed.", self.__language_path)
            return None
        language_selected = self.__function_detector.get_option_selected()
        if language_selected is not None:
            self.__logger.info("Detect language succeed.")
            return language_selected
        else:
            self.__logger.warning("Detect language failed by\
 get option selected failed.")
            return None

    @update_params_dec
    def switch_language(self, language):
        """Switch Language to language str given

        Todo Todo Todo

        Arguments:
            language {str} -- language to change, must equls to option str.

        Returns:
            bool -- true means success
        """
        # todo to fix path list refer to masterlist
        self.__logger.info("Switch to language [%s] start.", language)
        if not self.__function_transfer.move_to_from_home(
                self.__language_path):
            self.__logger.warning("Switch to language [%s] failed by\
 move to [%s].", language, self.__language_path)
            return False
        if self.__function_setter.set_option(language) is False:
            self.__logger.warning(
                "Switch to language [%s] failed by set [%s].", language)
            return False
        else:
            self.__logger.info("Switch to language [%s] succeed.", language)
            return True

    @update_params_dec
    def get_wiredlan_ip(self):
        """Get Machine WiredLAN IP Address

        Get By Check Panel Menu Value

        Returns:
            str -- ip address
        """
        self.__logger.info("Get wiredlan ip start.")
        if not self.__function_transfer.move_to_from_home(
                self.__ethernet_ip_path[:-1]):
            self.__logger.warning("Get wiredlan ip failed by\
 move to [%s] failed.", self.__ethernet_ip_path[:-1])
            return None
        ip = self.__function_detector.get_option_value(
            self.__ethernet_ip_path[-1])
        if ip is None:
            self.__logger.warning(
                "Get wiredlan ip failed by get option [%s] value.",
                self.__ethernet_ip_path[-1])
        else:
            self.__logger.info("Get wiredlan ip succeed[%s].", ip)
        return ip

    @update_params_dec
    def get_wlan_ip(self):
        """Get Machine WLAN IP Address

        Get By Check Panel Menu Value

        Returns:
            str -- ip address
        """
        self.__logger.info("Get wlan ip start.")
        if not self.__function_transfer.move_to_from_home(
                self.__wifi_ip_path[:-1]):
            self.__logger.warning("Get wlan ip failed by\
 move to [%s] failed.", self.__wifi_ip_path)
            return None
        ip = self.__function_detector.get_option_value(
            self.__wifi_ip_path[-1])
        if ip is None:
            self.__logger.warning(
                "Get wlan ip failed by get option [%s] value.",
                self.__wifi_ip_path[-1])
        else:
            self.__logger.info("Get wlan ip succeed[%s].", ip)
        return ip


if __name__ == '__main__':
    print "test start:"
    Environment.Environment(function_info_dict, machine_info_dict)
    functionHelper = FunctionHelper()
    # print functionHelper.all_settings_reset()

    for x in xrange(0, 10):
        print functionHelper.all_settings_reset()
