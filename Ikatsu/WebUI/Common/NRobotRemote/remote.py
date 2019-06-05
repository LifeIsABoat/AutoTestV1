#coding=utf-8

from robot.libraries.Remote import Remote

class RemoteTest(object):
    def __init__(self,address = None):
        if address:
            self.client = Remote(address)
        pass

    def command(self,action,param = None):
        #print '[remote] do {action} with {param}'.format(action=action,param=param)
        print action
        #ret = self.client.run_keyword('COMMAND', [action,param], {})
        return True
