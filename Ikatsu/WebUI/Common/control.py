#coding=utf-8
from robot.libraries.BuiltIn import BuiltIn
import functools
from wcf import wcftest
import re
import time
import json

import sys
reload(sys)
sys.setdefaultencoding('utf8')

class control(object):
    def __init__(self,serveaddress):
        #self.builtIn = BuiltIn()#'http://APBSH0809:8003/Service'
        self.wcf = wcftest.WcfTest(str(serveaddress))
        assert(self.wcf != None)
        print 'new wcf test'

    def __del__(self):
        print 'del'

    def DoSuiteSetup(self):
        print 'DoSuiteSetup'
        #self.GetCommIF()
        #self.Open()

    def DoSuiteTeardown(self):
        print 'DoSuiteTeardown'
        #self.Close()

    def DoSetUp(self):
        print 'DoSetUp'
        #self.builtIn.set_library_search_order(self.__class__.__name__)
        self.Open()

    def DoTearDown(self):
        print 'DoTearDown'
        self.Close()

    def __parseResult(self,value):
        assert(value != None)
        ms = re.match(r"\[(.+?)\](.*)", str(value))
        print value
        if ms:
            operateRet = ms.group(1)
            ret = ms.group(2)
            #self.builtIn.should_be_equal(operateRet, 'OK')
            #assert(operateRet =='OK')
            if(operateRet == 'OK'):
                return ret
            else:
                raise Exception("operater NG:" + ret)
        else:
            raise Exception("can not parser operater result")

    def _Common(func):
        action = func.__name__
        @functools.wraps(func)
        def excute(self,*args,**kw):
            print 'do ' + action
            ret = self.wcf.CommonCommand(action,args)
            func(self,*args,**kw)
            return self.__parseResult(ret)

        return excute

    def _Node(func):
        action = func.__name__
        if(action=="SetValueSingle"):
            action = "SetValue"
        if(action=="GetValueSingle"):
            action = "GetValue"
        @functools.wraps(func)
        def excute(self,*args,**kw):
            ##action = func.__name__
            print 'do ' + action
            ret = self.wcf.NodeCommand(action,args)
            func(self,*args,**kw)
            return self.__parseResult(ret)

        return excute


#---------Node------------
    def GoToPath(self,Path):
        if Path == "":
            return

        print Path
        self.GoToName(Path)
        pass
        # splitPath = re.split('\/+',AllPaths)
        # for onePath in splitPath:
        #     self.GoToName(onePath)
        
        # self.SetSubNodeValue(Value,0)
            
    @_Node
    def GoToName(self,Name):
        pass

    @_Node
    def GetSubNodeValue(self,Index):
        pass

    @_Node
    def SetSubNodeValue(self,Value,Index):
        pass

#---------common------------
    @_Common
    def SetMode(self,mode):
        pass

    @_Common
    def SetPrinterIP(self,IP):
        pass

    @_Common
    def SetPassword(self,pw):
        pass

    @_Common
    def GetCommIF(self):
        pass

    @_Common
    def GoToRoot(self):
        pass

    def Open(self):
        #time.sleep(5)
        self.GetCommIF()
        print 'do ' + 'Open'
        ret = self.wcf.CommonCommand('Open')
        self.GoToRoot()
        return self.__parseResult(ret)

    @_Common
    def PushOK(self):
        pass

    @_Common
    def PushApply(self):
        pass

    @_Common
    def PushCancel(self):
        pass

    @_Common
    def Close(self):
        pass
    
    @_Common
    def GoToPageRoot(self):
        pass

#------control operate---------
    @_Node
    def SetByName(self,Id,Key,Value):
        pass
    
    @_Node
    def GetByName(self,Id,key):
        pass
    
    @_Node
    def SetByIdCol(self,Id,Col,Value):
        pass
    
    @_Node
    def GetByIdCol(self,Id,Col):
        pass

    @_Node
    def SetByCoord(self,Row,Col,Value):
        pass

    @_Node
    def GetByCoord(self,Row,Col):
        pass
    
    @_Node
    def GoTo(self,text,ControlType,Index):
        '''all control'''
        pass

    def SetImpactPaths(self,jsonText):
        if jsonText == "":
            return

        jsonText = str(jsonText).replace('\\ ', ' ').replace("\\#", '#')
        print jsonText
        ImpactPaths = json.loads(jsonText)
        for ImpactPath in ImpactPaths:
            self.SetImpactPath(ImpactPath.get('ImpactPath'),ImpactPath.get('ImpactItemChildValue'))

    @_Node
    def SetImpactPath(self,impactPath,childValue):
        '''all control'''
        pass

    @_Node
    def BeforeCheckSettings(self):
        '''all control'''
        pass

    @_Node
    def CheckSettings(self):
        '''all control'''
        pass

    @_Node
    def GetName(self):
        '''all control'''
        pass

    @_Node
    def GetSubList(self):
        '''all control'''
        pass

    @_Node
    def GetSubListNum(self):
        '''all control'''
        pass

    @_Node
    def GetErrorMessage(self):
        '''all control'''
        pass

    @_Node
    def PushOKErrorMessage(self):
        '''all control'''
        pass

    @_Node
    def Click(self):
        '''TreeItem,Radio,ComboBoxOption,Button,SliderOption,CheckBox'''
        pass

    @_Node
    def GetSelectItem(self):
        '''RadioBox'''
        pass

    @_Node
    def CheckIsSelectOnly(self):
        '''RadioBox'''
        pass

    @_Node
    def CheckIsSelect(self):
        '''Radio,ComboBoxOption,CheckBox,SliderOption'''
        pass

    @_Node
    def GetCurrentValue(self):
        '''ComboBox'''
        pass

    @_Node
    def SetText(self,text):
        '''Text,SpinBox'''
        pass

    @_Node
    def GetText(self):
        '''Text,SpinBox'''
        pass

    @_Node
    def Clear(self):
        '''Text,SpinBox,Slider'''
        pass

    @_Node
    def GetTextLength(self):
        '''Text,SpinBox'''
        pass

    @_Node
    def Select(self):
        '''CheckBox'''
        pass

    @_Node
    def ClickUp(self):
        '''SpinBox'''
        pass

    @_Node
    def ClickDowm(self):
        '''SpinBox'''
        pass

    @_Node
    def GetAddValue(self):
        '''SpinBox'''
        pass

    @_Node
    def GetReduceValue(self):
        '''SpinBox'''
        pass

    @_Node
    def SetSlideValue(self,value):
        '''Slider'''
        pass

    @_Node
    def GetValue(self):
        '''Slider'''
        pass

    @_Node
    def GetLeastLevel(self):
        '''Slider'''
        pass

    @_Node
    def SetIP(self,IPValue):
        '''IP'''
        pass

    @_Node
    def GetIP(self):
        '''IP'''
        pass

    @_Node
    def GetLink(self):
        '''Link'''
        pass
    
    @_Node
    def SetValueSingle(self, value):
        '''all control'''
        pass

    @_Node
    def GetValueSingle(self):
        '''all control'''
        pass

    @_Node
    def SetValue(self, row, col, type, value):
        '''table'''
        pass

    @_Node
    def GetValue(self, row, col, type):
        '''table'''
        pass

#----------doCondition--------------
    def TryClick(self):
        '''TreeItem,Radio,ComboBoxOption,Button,SliderOption'''
        try:
            self.Click()
        except Exception, e:
            print 'warning:' + e.message
        

    def TrySelect(self):
        '''CheckBox'''
        try:
            self.Select()
        except Exception, e:
            print 'warning:' + e.message


if __name__ == '__main__':
    t = control('http://APBSH0557:8003/Service')
    print t.SetMode('EWS')
    print t.SetPrinterIP('10.244.3.3')
    t.SetPassword('initpass')
    t.Open()
    t.GoToRoot()
    t.GoToPath('Fax/Fax/Setup Receive/Receive Mode','Manual')
