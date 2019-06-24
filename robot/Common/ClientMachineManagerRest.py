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


class ClientMachineManagerRest(object):
    """MachineManagerRestClient to Access Restful Machine Manager Server

    note : using MachineID to interact with Machine Server.

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

        # A machine client object bindings manager"s base_url and machine id.

        # Arguments:
        #     base_url {str} -- machine manager server url
        #     machine_id {str} -- machine id to find specified machine
        # """
        super(ClientMachineManagerRest, self).__init__()
        self.__base_url = base_url
        self.__addi_url = r'MachineManager'
        self.__logger = Logger("ClientMachineManagerRest").get()

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
    def get_machines_info(self):
        """Enumerate All Machines Info

        Sample API:http://Apbsh0796:65001/MachineManager/Inf

        Arguments:
            machine_id {str} --  machine id to find specified machine

        Returns:
            dic -- machine infos.
                samples:{
                    "localhost:65001-ECL3.7_BvBoard-20180322021947124":{
                        "machineName":"ECL3.7_BvBoard",
                        "configFile":"D:\\SVN\\TestEngine\\GUITestEngine\\MachineManager\\input\\ECL37_bvboard.xml",
                        "machineType":"MFCGUI",
                        "ioType":"IOSocket",
                        "keyBoardType":"KeyBoardIO",
                        "touchPanelType":"TouchPanelIO",
                        "screenType":"ScreenSocket"
                    }
                    ...
                }
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + '/Inf'
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            raw_dic = json.loads(response.text)
            ret_dic = {}
            print raw_dic
            for kvp in raw_dic['getMachinesInfoResult']:
                ret_dic[kvp['Key']] = kvp['Value']
            return ret_dic
        self.__logger.error(
            "Error:The requested failed while getting\
 the Machine information list. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while getting\
             the Machine information list.")

    @multiple_connection_server_dec
    def get_machine_info(self, machine_id):
        """Get Machine Info by RestAPI

        Sample API:http://Apbsh0796:65001/MachineManager/{id}/Inf

        Arguments:
            machine_id {str} --  machine id to find specified machine

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
            self.__addi_url + r'/' + machine_id + r'/Inf'
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the Machine information. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while getting\
 the Machine information.")

    @multiple_connection_server_dec
    def get_io_info(self, machine_id):
        """Get IO Info by Rest API

        Sample API:http://localhost:65001/MachineManager/IO/Inf/{id}

        Arguments:
            machine_id {str} --  machine id to find specified machine

        Returns:
            dic -- io info.
                samples:{
                    "type":"IOSocket"
                }
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/IO/Inf/' + machine_id
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the IO information. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while getting the IO information.")

    @multiple_connection_server_dec
    def io_write(self, machine_id, string):
        """write IO by Rest API

        Sample API:http://localhost:65001/MachineManager/IO/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine
            string {str} -- content to send

        Returns:
            bool -- true means succeed
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/IO/IF'
        post_dict = {}
        post_dict['content'] = string
        post_dict['machineID'] = machine_id
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while\
 IO write to manchie. Response:%s",
            response.text)
        raise RuntimeError("The requested failed while IO write to manchie.")

    @multiple_connection_server_dec
    def io_read(self, machine_id):
        """Read IO by Rest API

        Sample API:http://localhost:65001/MachineManager/IO/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine

        Returns:
            str -- content io read
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/IO/IF/' + machine_id
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return response.text
        self.__logger.error(
            "Error:The requested failed while reading\
 the IO information. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while reading the IO information.")

    @multiple_connection_server_dec
    def get_keyboard_info(self, machine_id):
        """Get IO Info by Rest API

         Sample API:http://localhost:65001/MachineManager/KeyBoard/Inf/{id}

         Arguments:
             machine_id {str} --  machine id to find specified machine

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
            self.__addi_url + r'/KeyBoard/Inf/' + machine_id
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the KeyBoard information. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while getting the KeyBoard information.")

    class __KeyBoardCmd(Enum):
        push = 0
        release = 1
        click = 2

    @multiple_connection_server_dec
    def keyboard_push(self, machine_id, key_name):
        """Push KeyBoard by Rest API

        Sample API:http://localhost:65001/MachineManager/KeyBoard/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine
            key_name {str} -- key to push

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/KeyBoard/IF'
        post_dict = {}
        post_dict['cmdType'] = self._KeyBoardCmd.push
        post_dict['machineID'] = machine_id
        post_dict['keyCode'] = key_name
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while pushing\
 the machine KeyBoard. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while pushing the machine KeyBoard.")

    @multiple_connection_server_dec
    def keyboard_release(self, machine_id, key_name):
        """Release KeyBoard by Rest API

        Sample API:http://localhost:65001/MachineManager/KeyBoard/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine
            key_name {str} -- key to release

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/KeyBoard/IF'
        post_dict = {}
        post_dict['cmdType'] = self._KeyBoardCmd.release
        post_dict['machineID'] = machine_id
        post_dict['keyCode'] = key_name
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while releasing\
 machine KeyBoard. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while releasing machine KeyBoard.")

    @multiple_connection_server_dec
    def keyboard_click(self, machine_id, key_name):
        """Click KeyBoard by Rest API

        Sample API:http://localhost:65001/MachineManager/KeyBoard/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine
            key_name {str} -- key to click

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/KeyBoard/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__KeyBoardCmd.click
        post_dict['machineID'] = machine_id
        post_dict['keyCode'] = key_name
        post_json = json.dumps(
            post_dict, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while clicking\
 on machine KeyBoard. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while clicked on machine KeyBoard.")

    @multiple_connection_server_dec
    def get_touchpanel_info(self, machine_id):
        """Get TouchPanel Info by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/Inf/{id}

        Arguments:
         machine_id {str} --  machine id to find specified machine

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
            self.__addi_url + r'/TouchPanel/Inf/' + machine_id
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the touchpanel information. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while getting\
 the touchpanel information.")

    class __TouchPanelCmd(Enum):
        push = 0
        release = 1
        click = 2
        move = 3

    @multiple_connection_server_dec
    def touchpanel_push(self, machine_id, position):
        """Push TouchPanel by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine
            position {Point} -- position to push

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/TouchPanel/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__TouchPanelCmd.push
        post_dict['machineID'] = machine_id
        post_dict['position'] = position
        post_json = json.dumps(
            post_dict, default=Point.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while pushing\
 touchpanel. Response:%s", response.text)
        raise RuntimeError("The requested failed while pushing touchpanel.")

    @multiple_connection_server_dec
    def touchpanel_release(self, machine_id, position):
        """Release TouchPanel by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine
            position {Point} -- position to release

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/TouchPanel/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__TouchPanelCmd.release
        post_dict['machineID'] = machine_id
        post_dict['position'] = position
        post_json = json.dumps(
            post_dict, default=Point.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while releasing\
 touchpanel. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while releasing touchpanel.")

    @multiple_connection_server_dec
    def touchpanel_click(self, machine_id, position):
        """Click TouchPanel by Rest API

        Sample API:http://localhost:65001/MachineManager/TouchPanel/IF

        Arguments:
            machine_id {str} --  machine id to find specified machine
            position {Point} -- position to click

        Returns:
            bool -- true mean success
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/TouchPanel/IF'
        post_dict = {}
        post_dict['cmdType'] = self.__TouchPanelCmd.click
        post_dict['machineID'] = machine_id
        post_dict['position'] = position
        post_json = json.dumps(
            post_dict, default=Point.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return True
        self.__logger.error(
            "Error:The requested failed while clicking\
 touchpanel. Response:%s", response.text)
        raise RuntimeError("The requested failed while clicking touchpanel.")

    @multiple_connection_server_dec
    def get_screen_info(self, machine_id):
        """Get Screen Info by Rest API

        Sample API:http://localhost:65001/MachineManager/Screen/Inf/{id}

        Arguments:
         machine_id {str} --  machine id to find specified machine

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
            self.__addi_url + r'/Screen/Inf/' + machine_id
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.error(
            "Error:The requested failed while getting\
 the screen information. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while getting the screen information.")

    @multiple_connection_server_dec
    def screen_shot(self, machine_id):
        """Get Machine Screen Image by RestAPI

        Sample API:http://Apbsh0796:65001/MachineManager/Screen/IF/{id}
        Return {Image} -- screen image

        Arguments:
            machine_id {str} --  machine id to find specified machine

        Returns:
            Image -- image of current screen
        """
        rest_url = self.__base_url + r'/' + \
            self.__addi_url + r'/Screen/IF/' + machine_id
        response = requests.get(rest_url, stream=True)
        if response.status_code == requests.codes.ok:
            return Image.open(StringIO(response.content))
        self.__logger.error(
            "Error:The requested failed while getting\
 the screen image. Response:%s", response.text)
        raise RuntimeError(
            "The requested failed while getting the screen image.")


if __name__ == '__main__':
    machine = ClientMachineManagerRest("http://localhost:65001")
    machines_info = machine.get_machines_info()
    if machines_info is not None:
        machine_ids = machines_info.keys()
        if len(machine_ids) is not 0:
            # Machine
            machine_id = machine_ids[0]
            print 'MachineInfo:',
            print machine.get_machine_info(machine_id)
            # IO
            print machine.get_io_info(machine_id)
            print machine.io_write(machine_id, '|s2')
            time.sleep(0.3)
            print machine.io_read(machine_id)
            # KeyBoard
            print machine.get_keyboard_info(machine_id)
            # print machine.keyboard_push(machine_id, 'HOME_KEY') #todo
            # print machine.keyboard_release(machine_id, 'HOME_KEY') #todo
            print machine.keyboard_click(machine_id, 'HOME_KEY')
            time.sleep(1)
            # TouchPanel
            print machine.get_touchpanel_info(machine_id)
            print machine.touchpanel_push(machine_id, Point(320, 211))
            time.sleep(1)
            print machine.touchpanel_release(machine_id, Point(320, 211))
            time.sleep(1)
            print machine.touchpanel_click(machine_id, Point(320, 211))
            time.sleep(1)
            # Screen
            print machine.get_screen_info(machine_id)
            image = machine.screen_shot(machine_id)
            image.show()
        else:
            print 'Get Fist Machine ID Failed.'
    else:
        print 'Get Machines Info Failed.'
