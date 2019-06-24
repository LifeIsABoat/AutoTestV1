#!/usr/bin/env python
# -*- coding: utf-8 -*-


import requests
import json
import time
from PIL import Image
from StringIO import StringIO
from enum import Enum
from Point import Point
from Logger import Logger


class ClientMachineRest(object):
    """MachineRestClient to Access Restful Machine Manager Server

    note : Not using MachineID to interact with Machine Server.

    get_machines_info is used to list Machines that valid in machine Server,
    get_machine_info is used to get a single machine's info,
    get_io_info is used to get io info from a single machine,
    io_write is used to send cmd to a single machine's io,
    io_read is used to read log from a single machine's io,
    get_keyboard_info is used to get keyboard info of a single machine,
    keyboard_push is used to push key on a single machine's keyboard,
    keyboard_release is used to release key on a single machine's keyboard,
    keyboard_click is used to click key on a single machine's keyboard,
    get_touchpanel_info is use to get touch panel's info of a single machine,
    touchpanel_push is use to push touch panel on a single machine,
    touchpanel_release is use to release touch panel on a single machine,
    touchpanel_click is use to click touch panel on a single machine,
    get_screen_info is use to get screen's info of a single machine,
    screen_shot is used to capture current screen of single machine.
    """

    def __init__(self, base_url):
        # """Machine Client Constructor

        # A machine client object bindings manager"s base_url.

        # Arguments:
        #     base_url {str} -- machine manager server url
        # """
        super(ClientMachineRest, self).__init__()
        self.__base_url = base_url
        self.__addi_url = r'MachineManager'
        self.__logger = Logger("ClientMachineRest").get()

    def multiple_connection_server_dec(func):
        """A decorator (Try to connect to the server multiple times.)

        Arguments:
        func {function} -- a function what you want to do

        Returns:
        function -- The function that is currently called.
        """
        def execute(*args, **kw):
            """decorator function

            Arguments:
            *args {[type]} -- args[0] --> self
            **kw {[type]} -- [description]

            Returns:
            function -- The function that is currently called.
            """
            # args[0].__logger.info("Start connecting to the server")

            # Try to connect to the server.
            for i in xrange(0, 3):
                try:
                    # args[0].__logger.info("Connection times : [%d]", i)
                    return func(*args, **kw)
                except RuntimeError:
                    args[0].__logger.warning(
                        "%d-connect to the server failed.", i)
            args[0].__logger.info("Multiple connection server failed.")
            return func(*args, **kw)
        return execute

    @multiple_connection_server_dec
    def get_machine_info(self):
        """Get Machine Info by RestAPI

        Sample API:http://Apbsh0796:65001/MachineManager/Inf

        Returns:
            dic -- machine info.
                samples:{
                    "machineName":"ECL3.7_BvBoard",
                    "configFile":"D:\\SVN\\TestEngine\\GUITestEngine\\MachineManager\\input\\ECL37_bvboard.xml",
                    "machineType":"MFCGUI",
                    "ioType":"IOSocket",
                    "keyBoardType":"KeyBoardIO",
                    "touchPanelType":"TouchPanelIO",
                    "screenType":"ScreenSocket"
                }
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/' + r'/Inf'
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the Machine information. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while getting the Machine information.")

    @multiple_connection_server_dec
    def get_io_info(self):
        """Get IO Info by Rest API

        Sample API:http://localhost:65001/MachineManager/IO/Inf

        Returns:
            dic -- io info.
                samples:{
                    "type":"IOSocket"
                }
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/IO/Inf'
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while\
 getting the IO information. Response:%s:%s",
            response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while getting the IO information.")

    @multiple_connection_server_dec
    def io_write(self, string):
        """write IO by Rest API

        Sample API:http://localhost:65001/MachineManager/IO/IF

        Arguments:
            string {str} -- content to send

        Returns:
            bool -- true means succeed
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/IO/IF'
        post_dict = {}
        post_dict['content'] = string
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while\
 IO write to manchie.  Response:%s:%s", response.status_code, response.text)
        raise RuntimeError("The requested failed while IO write to manchie.")

    @multiple_connection_server_dec
    def io_read(self):
        """Read IO by Rest API

        Sample API:http://localhost:65001/MachineManager/IO/IF

        Returns:
            str -- content io read
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/IO/IF'
        response = requests.get(rest_url)
        print response.status_code
        if response.status_code == requests.codes.ok:
            return response.text
        self.__logger.error(
            "Error:The requested failed while\
 reading the IO information. Response:%s:%s",
            response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while reading the IO information.")

    @multiple_connection_server_dec
    def get_keyboard_info(self):
        """Get IO Info by Rest API

         Sample API:http://localhost:65001/MachineManager/KeyBoard/Inf

         Returns:
             dic -- keyBoard info got.
                 sample:{
                     "type":"KeyBoardIO",
                     "keyList":[
                         "IGETA_KEY","KOME_KEY","CLEAR_KEY","TEN0_KEY",
                         "TEN1_KEY","TEN2_KEY","TEN3_KEY","TEN4_KEY",
                         "TEN5_KEY","TEN6_KEY","TEN7_KEY","TEN8_KEY",
                         "TEN9_KEY","STOP_KEY","HOME_KEY"
                     ]
                 }
         """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/KeyBoard/Inf'
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the KeyBoard information. Response:%s:%s",
            response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while getting the KeyBoard information.")

    class __KeyBoardCmd(Enum):
        push = 0
        release = 1
        click = 2

    @multiple_connection_server_dec
    def keyboard_push(self, key_name):
        """Push KeyBoard by Rest API

        Sample API:http://localhost:65001/MachineManager/KeyBoard/IF

        Arguments:
            key_name {str} -- key to push

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/KeyBoard/IF'
        post_dict = {}
        post_dict['cmdType'] = self._KeyBoardCmd.push
        post_dict['keyCode'] = key_name
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while pushing\
 the machine KeyBoard. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while pushing the machine KeyBoard.")

    @multiple_connection_server_dec
    def keyboard_release(self, key_name):
        """Release KeyBoard by Rest API

        Sample API:http://localhost:65001/MachineManager/KeyBoard/IF

        Arguments:
            key_name {str} -- key to release

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/KeyBoard/IF'
        post_dict = {}
        post_dict['cmdType'] = self._KeyBoardCmd.release
        post_dict['keyCode'] = key_name
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while releasing\
 machine KeyBoard. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while releasing machine KeyBoard.")

    @multiple_connection_server_dec
    def keyboard_click(self, key_name):
        """Click KeyBoard by Rest API

        Sample API:http://localhost:65001/MachineManager/KeyBoard/IF

        Arguments:
            key_name {str} -- key to click

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/KeyBoard/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__KeyBoardCmd.click
        post_dict['keyCode'] = key_name
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while clicking\
 on machine KeyBoard. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while clicked on machine KeyBoard.")

    @multiple_connection_server_dec
    def get_touchpanel_info(self):
        """Get TouchPanel Info by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/Inf

        Returns:
            dic -- touchpanel info got.
            sample:{
                "type":"TouchPanelIO",
                "size":{
                    "height":240,
                    "width":432
                }
            }
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/TouchPanel/Inf'
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the touchpanel information. Response:%s:%s",
            response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while getting the touchpanel information.")

    class __TouchPanelCmd(Enum):
        push = 0
        release = 1
        click = 2
        move = 3

    @multiple_connection_server_dec
    def touchpanel_push(self, position):
        """Push TouchPanel by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/IF

        Arguments:
            position {Point} -- position to push

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/TouchPanel/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__TouchPanelCmd.push
        post_dict['position'] = position
        post_json = json.dumps(
            post_dict, default=Point.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while pushing\
 touchpanel. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError("The requested failed while pushing touchpanel.")

    @multiple_connection_server_dec
    def touchpanel_release(self, position):
        """Release TouchPanel by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/IF

        Arguments:
            position {Point} -- position to release

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/TouchPanel/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__TouchPanelCmd.release
        post_dict['position'] = position
        post_json = json.dumps(
            post_dict, default=Point.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while releasing\
 touchpanel. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while releasing touchpanel.")

    @multiple_connection_server_dec
    def touchpanel_click(self, position):
        """Click TouchPanel by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/IF

        Arguments:
            position {Point} -- position to click

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/TouchPanel/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__TouchPanelCmd.click
        post_dict['position'] = position
        post_json = json.dumps(
            post_dict, default=Point.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while clicking\
 touchpanel. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError("The requested failed while clicking touchpanel.")

    @multiple_connection_server_dec
    def get_screen_info(self):
        """Get Screen Info by Rest API

        Sample API:http://localhost:65001/MachineManager/Screen/Inf

        Returns:
            dic -- screen info got.
            sample:{
                "type":"ScreenSocket",
                "size":{
                    "height":240,
                    "width":432
                }
            }
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/Screen/Inf'
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the screen information. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while getting the screen information.")

    @multiple_connection_server_dec
    def screen_shot(self):
        """Get Machine Screen Image by RestAPI

        Sample API:http://Apbsh0796:65001/MachineManager/Screen/IF
        Return {Image} -- screen image

        Returns:
            Image -- image of current screen
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/Screen/IF'
        response = requests.get(rest_url, stream=True)
        if response.status_code == requests.codes.ok:
            return Image.open(StringIO(response.content))
        self.__logger.error(
            "Error:The requested failed while getting\
 the screen image. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed while getting the screen image.")

    @multiple_connection_server_dec
    def wait_screen_change(self, time_out):
        """Wait Machine Screen Change by RestAPI

        Sample API:
            http://Apbsh0796:65001/MachineManager/Screen/WaitSteady?TimeOut=2

        Returns:
            True -- true means success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/Screen/WaitSteady?TimeOut=' + str(time_out)
        response = requests.get(rest_url, stream=True)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while waiting\
                screen Change. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while waiting screen change.")


if __name__ == '__main__':
    machine = ClientMachineRest("http://localhost:65001")
    print "test start: "
    # Machine
    print 'MachineInfo:'
    print machine.get_machine_info()
    # IO
    # print machine.get_io_info()
    # print machine.io_write('®')
    print machine.io_write('|s2')
    time.sleep(0.3)
    print machine.io_read()
    # # KeyBoard
    # print machine.get_keyboard_info()
    # # print machine.keyboard_push(machine_id, 'HOME_KEY') #todo
    # # print machine.keyboard_release(machine_id, 'HOME_KEY') #todo
    # print machine.keyboard_click('HOME_KEY')
    # time.sleep(1)
    # # TouchPanelb
    # print machine.get_touchpanel_info()
    # print machine.touchpanel_push(Point(320, 211))
    # time.sleep(1)
    # print machine.touchpanel_release(Point(320, 211))
    # time.sleep(1)
    # print machine.touchpanel_click(Point(320, 211))
    # time.sleep(1)
    # # Screen
    # print machine.get_screen_info()
    # image = machine.screen_shot()
    # image.show()

    # print machine.wait_screen_change("2")
