#!/usr/bin/env python
# -*- coding: utf-8 -*-
# @Link    :
# @Version : V0.1

import requests
import json
from Rectangle import Rectangle
from ClientMachineRest import ClientMachineRest
from Logger import Logger


class ClientImageMatcherRest(object):
    """Image Matcher Client

    To Calling Rest Service that Implement by opencv
    """

    def __init__(self, base_url):
        """Constructor for Image Matcher Client

        Image Matcher bindings base_url to connetct to rest server

        Arguments:
            base_url {str} -- rest server url
        """
        super(ClientImageMatcherRest, self).__init__()
        self.__base_url = base_url
        self.__addi_url = r'ImageMatcher'
        self.__logger = Logger("ClientImageMatcherRest").get()

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
    def Match(self, machine_id, label):
        """Match Icon in current machine screen

        machine by both Template and Feature to calling rest service

        Arguments:
            machine_id {str} -- machine_id
            label {str} -- icon label to match

        Returns:
            list<Rectangle> -- regions matched
        """
        rest_url = self.__base_url + r'/' + self.__addi_url + \
            r'?MachineID=' + machine_id + r'&label=' + label
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            raw_list = json.loads(response.text)
            if raw_list is None:
                return None
            for i in range(0, len(raw_list)):
                raw_list[i] = Rectangle.json_hock(raw_list[i])
            return raw_list
        self.__logger.error(
            "Error:Requested failed in Icon Match. Response:%s:%s",
            response.status_code, response.text)
        raise RuntimeError("Requested failed in Icon Match.")

    @multiple_connection_server_dec
    def TemplateMatch(self, machine_id, label, similarity=0.95):
        """Match Icon in current machine screen

        machine by Template to calling rest server

        Arguments:
            machine_id {str} -- machine_id
            label {str} -- icon label to match
            similarity {double} -- The minimum similarity of template Match.

        Returns:
            list<Rectangle> -- regions matched
        """
        rest_url = self.__base_url + r'/' + self.__addi_url + r'/Template' + \
            r'?MachineID=' + machine_id + r'&label=' + \
            label + r'&Min_similarity=' + str(similarity)
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            raw_list = json.loads(response.text)
            if raw_list is None:
                return None
            for i in range(0, len(raw_list)):
                raw_list[i] = Rectangle.json_hock(raw_list[i])
            return raw_list
        self.__logger.error(
            "Error:Requested failed in Icon Template Match. Response:%s:%s",
            response.status_code, response.text)
        raise RuntimeError("Requested failed in Icon Template Match.")

    @multiple_connection_server_dec
    def FeatureMatch(self, machine_id, label):
        """Match Icon in current machine screen

        machine by Feature to calling rest server

        Arguments:
            machine_id {str} -- machine_id
            label {str} -- icon label to match

        Returns:
            list<Rectangle> -- regions matched
        """
        rest_url = self.__base_url + r'/' + self.__addi_url + r'/Feature' + \
            r'?MachineID=' + machine_id + r'&label=' + label
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            raw_list = json.loads(response.text)
            if raw_list is None:
                return None
            for i in range(0, len(raw_list)):
                raw_list[i] = Rectangle.json_hock(raw_list[i])
            return raw_list
        self.__logger.error(
            "Error:Requested failed in Icon Feature Match. Response:%s:%s",
            response.status_code, response.text)
        raise RuntimeError("Requested failed in Icon Feature Match.")


if __name__ == '__main__':
    machine = ClientMachineRest("http://Local:64001")
    machines_info = machine.get_machine_info()
    machine_id = machines_info.get("machineID")
    match = ClientImageMatcherRest("http://Local:64005")
    print match.TemplateMatch(machine_id, "_left", 0.6)
