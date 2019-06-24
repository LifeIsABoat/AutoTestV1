#!/usr/bin/env python
# -*- coding: utf-8 -*-

from Resource import *


class Environment(object):
    """Environment for machine param management

    Class Environment is an singleton class.
    set_machine is to Set Machine　Info for Machine Layer.
    set_function is to Set Function　Info for Function Layer.
    set_all is to to Set Info for Both Machine & Function Layer.
    """
    def __new__(cls, function_info_dict=None, machine_info_dict=None):
        """Constructor for Environment Class

        Keyword Arguments:
            function_info_dict {dic} -- Function Info Dict (default: {None})
            machine_info_dict {dic} -- Machine Info Dict (default: {None})

        Returns:
            obj -- singleton instance
        """
        if not hasattr(cls, 'instance'):
            cls.instance = super(Environment, cls).__new__(cls)
        return cls.instance

    def __init__(self, function_info_dict=None, machine_info_dict=None):
        """Constructor for Environment Class

        Keyword Arguments:
            function_info_dict {dic} -- Function Info Dict (default: {None})
            machine_info_dict {dic} -- Machine Info Dict (default: {None})
        """
        if(function_info_dict is None and machine_info_dict is None):
            return
        super(Environment, self).__init__()
        self.set_all(function_info_dict, machine_info_dict)

    def set_machine(self, machine_info_dict):
        """Set Machine Info

        Arguments:
            machine_info_dict {dic} -- Machine Info Dict
        """
        self.machine_info_dict = machine_info_dict

    def set_function(self, function_info_dict):
        """Set Function Info

        Arguments:
            function_info_dict {dic} -- Function Info Dict
        """
        self.function_info_dict = function_info_dict

    def set_all(self, function_info_dict, machine_info_dict):
        """Set Both Function Info and Machine Info

        Arguments:
            function_info_dict {dic} -- Function Info Dict
            machine_info_dict {dic} -- Machine Info Dict
        """
        self.set_machine(machine_info_dict)
        self.set_function(function_info_dict)


if __name__ == '__main__':
    print "start"
    Environment(function_info_dict, machine_info_dict)
    print "done"
