#!/usr/bin/env python
# -*- coding: utf-8 -*-


import requests
import json
from ClientMachineRest import ClientMachineRest
from Rectangle import Rectangle
from Logger import Logger


class ClientScreenParserRest(object):
    """ScreenParser Client to Call Restful Service

    binding Rest base url when create
    """

    def __init__(self,
                 text_detector_url, text_recognizer_url,
                 button_detector_url, button_recognizer_url,
                 image_detector_url, image_recognizer_url):
        """Constructor of Screen Parser Client

        Screen Parser Client Bindings text_detect,text_recognize,
                                      button_detect,button_recognize,
                                      image_detect,image_recognize
        Arguments:
            text_detector_url {str} s-- base_url for text_detect
            text_recognizer_url {str} -- base_url for text_recognize
            button_detector_url {str} -- base_url for button_detect
            button_recognizer_url {str} -- base_url for button_recognize
            image_detector_url {str} -- base_url for image_detect
            image_recognizer_url {str} -- base_url for image_recognize
        """
        super(ClientScreenParserRest, self).__init__()
        self.__text_detector_url = text_detector_url
        self.__text_recognizer_url = text_recognizer_url
        self.__button_detector_url = button_detector_url
        self.__button_recognizer_url = button_recognizer_url
        self.__image_detector_url = image_detector_url
        self.__image_recognizer_url = image_recognizer_url
        self.__logger = Logger("ClientScreenParserRest").get()

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
    def text_detect(self, machine_id):
        """Text region detect handler of specific machine

        detect text region by restful service

        Arguments:
            machine_id {str} -- machine_id

        Returns:
            list<Rectange> -- regions detected
        """
        url_addi = r'Texts/Detection?Machine=' + machine_id
        rest_url = self.__text_detector_url + r'/' + url_addi
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            raw_list = json.loads(response.text)
            if raw_list is None:
                self.__logger.error(
                    "Error:The text regions detection failed. Response:%s",
                    response.text)
                raise RuntimeError(
                    "The requested failed in the text regions detection.")
            for i in range(0, len(raw_list)):
                raw_list[i] = Rectangle.json_hock(raw_list[i])
            return raw_list
        self.__logger.error(
            "Error:The requested failed in the text\
 regions detection. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed in the text regions detection.")

    @multiple_connection_server_dec
    def text_recognize(self, machine_id, regions):
        """Text content recognize handler of specific machine

        recognize text region by restful service at given regions

        Arguments:
            machine_id {string} -- machine_id
            regions {list<Rectangle>} -- regions to recognize

        Returns:
            list<str> -- contents recognized
        """
        url_addi = r'Texts/Recognition'
        rest_url = self.__text_recognizer_url + r'/' + url_addi
        post_dict = {}
        post_dict['rects'] = regions
        post_dict['id'] = machine_id
        post_json = json.dumps(
            post_dict, default=Rectangle.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.warning(
            "Warning:The requested failed in the text\
 recognition. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError("The requested failed in the text recognition.")

    @multiple_connection_server_dec
    def button_detect(self, machine_id):
        """Button region detect handler of specific machine

        detect button region by restful service

        Arguments:
            machine_id {str} -- machine_id

        Returns:
            list<Rectange> -- regions detected
        """
        url_addi = r'Buttons/Detection?Machine=' + machine_id
        rest_url = self.__button_detector_url + r'/' + url_addi
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            raw_list = json.loads(response.text)
            if raw_list is None:
                self.__logger.warning(
                    "Warning:The button regions detection\
 failed. Response:%s", response.text)
                raise RuntimeError(
                    "The requested failed in the button regions detection.")
            for i in range(0, len(raw_list)):
                raw_list[i] = Rectangle.json_hock(raw_list[i])
            return raw_list
        self.__logger.warning(
            "Warning:The requested failed in the button\
 regions detection. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "The requested failed in the button regions detection.")

    @multiple_connection_server_dec
    def button_recognize(self, machine_id, regions):
        """Button Status recognize handler of specific machine

        recognize button region by restful service at given regions

        Arguments:
            machine_id {string} -- machine_id
            regions {list<Rectangle>} -- regions to recognize

        Returns:
            list<str> -- statuses recognized
        """
        url_addi = r'Buttons/Recognition'
        rest_url = self.__button_recognizer_url + r'/' + url_addi
        post_dict = {}
        post_dict['rects'] = regions
        post_dict['id'] = machine_id
        post_json = json.dumps(
            post_dict, default=Rectangle.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.warning(
            "Warning:The requested failed in the button\
 recognition. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError("The requested failed in the button recognition.")

    @multiple_connection_server_dec
    def image_detect(self, machine_id):
        """Image region detect handler of specific machine

        detect image region by restful service

        Arguments:
            machine_id {str} -- machine_id

        Returns:
            list<Rectange> -- regions detected
        """
        url_addi = r'Image/Detection?Machine=' + machine_id
        rest_url = self.__image_detector_url + r'/' + url_addi
        response = requests.get(rest_url)
        if response.status_code == requests.codes.ok:
            raw_list = json.loads(response.text)
            if raw_list is None:
                self.__logger.warning(
                    "Warning:The image regions detection\
 failed. Response:%s", response.text)
                raise RuntimeError(
                    "Requested failed in the image regions detection.")
            for i in range(0, len(raw_list)):
                raw_list[i] = Rectangle.json_hock(raw_list[i])
            return raw_list
        self.__logger.warning("Warning:Requested failed in the image regions\
 detection. Response:%s:%s", response.status_code, response.text)
        raise RuntimeError(
            "Requested failed in the image regions detection.")

    @multiple_connection_server_dec
    def image_recognize(self, machine_id, regions):
        """Image Status recognize handler of specific machine

        recognize image region by restful service at given regions

        Arguments:
            machine_id {string} -- machine_id
            regions {list<Rectangle>} -- regions to recognize

        Returns:
            list<str> -- labels recognized
        """
        url_addi = r'Image/Recognition'
        rest_url = self.__image_recognizer_url + r'/' + url_addi
        post_dict = {}
        post_dict['rects'] = regions
        post_dict['id'] = machine_id
        post_json = json.dumps(
            post_dict, default=Rectangle.json_default, sort_keys=True)
        headers = {'Content-type': 'application/json'}
        response = requests.post(rest_url, data=post_json, headers=headers)
        if response.status_code == requests.codes.ok:
            return json.loads(response.text)
        self.__logger.warning(
            "Warning:Requested failed in the images\
 recognize.  Response:%s:%s", response.status_code, response.text)
        raise RuntimeError("Requested failed in the images recognize.")


if __name__ == '__main__':
    machine = ClientMachineRest("http://APBSH0675:64001")
    machines_info = machine.get_machine_info()
    machine_id = machines_info.get("machineID")
    print machine_id
    parser = ClientScreenParserRest(
        "http://Local:64002/LogScreenParser",
        #"http://localhost:65002/LogScreenParser",
        "http://Local:64004/TesseractScreenParser",
        "http://Local:64002/LogScreenParser",
        "http://Local:64003/FeatureScreenParser",
        "",
        "")
    # regions = []
    # regions.append(Rectangle([4,35,305,40]))
    # print regions
    # print parser.text_recognize(machine_id, regions)

    regions = parser.text_detect(machine_id)
    print regions
    print parser.text_recognize(machine_id, regions)
    # regions = parser.button_detect(machine_id)
    # print regions
    # print parser.button_recognize(machine_id, regions)
