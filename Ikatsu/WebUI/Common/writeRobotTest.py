# -*- coding: utf-8 -*-
import  xdrlib ,sys
import  xlrd
import  sys
import  re
import  linecache
reload(sys)
sys.setdefaultencoding('utf-8')

def list_of_groups(init_list, childern_list_len):
    list_of_group = zip(*(iter(init_list),) *childern_list_len)
    end_list = [list(i) for i in list_of_group]
    count = len(init_list) % childern_list_len
    end_list.append(init_list[-count:]) if count !=0 else end_list
    return end_list

def main():
    tab = ' ' * 4
    robotStr = '''*** Settings ***
Resource        Variables.txt
Library         ..\\\Python\\\control.py    http://127.0.0.1:8003/Service
        '''

    with open('TestOption.robot','w') as f:
        f.write(robotStr)
        f.write('\n*** Test Cases ***' + '\n')
        f.write('%s'%('MachineA_EWSTestPrepare') + '\n')
        f.write(tab + 'SetMode    EWS' + '\n')
        f.write(tab + 'SetPrinterIP    10.244.3.3' + '\n')
        f.write(tab + 'SetPassword    initpass' + '\n')

        fileStr = 'TestOptionValue.txt'
        file = open(fileStr)
        linesLenth = len(file.readlines())
        print len(file.readlines())
        rangeLists = [i for i in range(2,linesLenth + 1)]
        print(list_of_groups(rangeLists,4))
        print(linecache.getline(fileStr,3))
        for one_list in list_of_groups(rangeLists,4):
            caseNo = linecache.getline(fileStr,one_list[0])
            f.write('SetMachineA_EWSValues-No.'+ caseNo.split()[1] + '\n')
            f.write(tab + '[Setup]    DoSetup' + '\n')
            GoToPath = linecache.getline(fileStr,one_list[1])
            f.write(tab + 'GoToPath' + tab + GoToPath.split()[0] + '\n')
            Option_Value = linecache.getline(fileStr,one_list[2])
            Option_Index = linecache.getline(fileStr,one_list[3])
            f.write(tab + 'SetSubNodeValue' + tab + Option_Value.split()[0] + tab + Option_Index.split()[0] + '\n')
            f.write(tab + 'PushOK' + '\n')
            f.write(tab + '[Teardown]    DoTeardown' + '\n')

        f.write('\n' + '导出一括设定' + '\n')
        f.write(tab + '导出一括设定' + '\n' + '\n')

        f.write('\n' + '导入一括设定' + '\n')
        f.write(tab + '导入一括设定' + '\n' + '\n')

        f.write('%s'%('MachineB_EWSTestPrepare') + '\n')
        f.write(tab + 'SetMode    EWS' + '\n')
        f.write(tab + 'SetPrinterIP    10.244.3.3' + '\n')
        f.write(tab + 'SetPassword    initpass' + '\n')
        for one_list in list_of_groups(rangeLists,4):
            caseNo = linecache.getline(fileStr,one_list[0])
            f.write('GetMachineB_EWSValues-No.'+ caseNo.split()[1] + '\n')
            f.write(tab + '[Setup]    DoSetup' + '\n')
            GoToPath = linecache.getline(fileStr,one_list[1])
            f.write(tab + 'GoToPath' + tab + GoToPath.split()[0] + '\n')
            Option_Value = linecache.getline(fileStr,one_list[2])
            Option_Index = linecache.getline(fileStr,one_list[3])
            f.write(tab + '${current_text}    ' +  'GetSubNodeValue' + tab + Option_Index.split()[0] + '\n')
            f.write(tab + 'Should Be Equal    ${current_text}    ' + tab + Option_Index.split()[0] + '\n')
            f.write(tab + '[Teardown]    DoTeardown' + '\n')

        file.close()


if __name__=="__main__":
    main()
