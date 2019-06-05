#coding=utf-8
import unittest
from suds.client import Client


class WcfTest(object):
    def __init__(self,address = None):
        if address:
            self.client = Client(address)
        pass

    def CommonCommand(self,action,param = None):
        if(param == None):
            param = tuple()
        ArrayOfString = self.client.factory.create('ns1:ArrayOfstring')
        ArrayOfString.string = param
        return self.client.service.CommonCommand(action,ArrayOfString)

    def NodeCommand(self,action,param = None):
        if(param == None):
            param = tuple()
        ArrayOfString = self.client.factory.create('ns1:ArrayOfstring')
        ArrayOfString.string = param
        return self.client.service.NodeCommand(action,ArrayOfString)
        




if __name__ == '__main__':
    


    test = WcfTest('http://APBSH0809:8003/Service')
    test.CommonCommand('SetMode',('RSP'))
    test.CommonCommand('GetMode',None)
    test.CommonCommand('SetPrinterIP',('10.244.4.170'))
    test.CommonCommand('GetCommIF',None)
    test.CommonCommand('Open',None)
    test.CommonCommand('GoToRoot',None)
    #test.NodeCommand('GoTo',('Fax','TreeItem','0'))

    '''
    client = Client('http://APBSH0809:8003/Service')
    print client
    ArrayOfString = client.factory.create('ns1:ArrayOfstring')
    ArrayOfString.string = ('RSP')
    print client.service.CommonCommand("SetMode",ArrayOfString)
    ArrayOfString.string = tuple()
    print client.service.CommonCommand("GetMode",ArrayOfString)
    ArrayOfString.string = ['10.244.4.170']
    print client.service.CommonCommand("SetPrinterIP",ArrayOfString)
    ArrayOfString.string = []
    print client.service.CommonCommand("GetCommIF",ArrayOfString)
    ArrayOfString.string = []
    print client.service.CommonCommand("Open",ArrayOfString)
    ArrayOfString.string = []
    print client.service.CommonCommand("GoToRoot",ArrayOfString)
    ArrayOfString.string = ['Fax','TreeItem','0']
    print client.service.NodeCommand("GoTo",ArrayOfString)
'''
