# -*- coding: utf-8 -*-
import  xdrlib
import  sys
import  xlrd
import  sys
import  re
import  linecache
reload(sys)
sys.setdefaultencoding('utf-8')

def main():
    fileStr = 'TestGroup.txt'
    file = open(fileStr)
    linesLenth = len(file.readlines())
    caseNoWordList = []
    for i in range(2,linesLenth + 1):
        mString = linecache.getline(fileStr,i)
        if mString.find('#')!=-1 and mString.find('CaseNo.')!=-1:
            mString = mString.replace('\n','')
            caseNoWordList.append(mString)
    file.close()

    with open(fileStr, 'r') as f:
        a = []
        valueList = []
        lines = f.readlines()
        for x in lines:
            if x.find('*** Variables ***')!=-1:
                continue
            if x.startswith('#'):
                a.extend([x.strip().split()[0], lines.index(x),])
        for i in caseNoWordList[:-1]:
            if i in a:
                c = a.index(i)
                valueList.append(''.join(lines[a[c+1]:a[c+3]]))
                print ''.join(lines[a[c+1]:a[c+3]])
        if caseNoWordList[-1] == a[-2]:
            valueList.append(''.join(lines[a[int(a.index(caseNoWordList[-1]) + 1)]:]))
            print ''.join(lines[a[int(a.index(caseNoWordList[-1]) + 1)]:])
        else:
            print ''.join(lines[a[int(a.index(caseNoWordList[-1])) + 1]:a[int(a.index(caseNoWordList[-1])) + 3]])

    tab = ' ' * 4
    robotStr = '''*** Settings ***
Resource        Variables.txt
Library         ..\\\Python\\\control.py    http://127.0.0.1:8003/Service
        '''

    with open('testCase.robot','w') as f:
        f.write(robotStr)
        f.write('\n*** Test Cases ***' + '\n')
        f.write('%s'%('EWSTestPrepare') + '\n')
        f.write(tab + 'SetMode    EWS' + '\n')
        f.write(tab + 'SetPrinterIP    10.244.3.3' + '\n')
        f.write(tab + 'SetPassword    initpass' + '\n')

        file = open('TestAddressBookVariables.txt')
        #linesLenth = len(file.readlines())
        for jk in range(0, len(caseNoWordList)):
            f.write('Set-'+ caseNoWordList[jk].replace('#','') + '\n')
            f.write(tab + '[Setup]    DoSetup' + '\n')
            rowsValue = valueList[jk].split('\n')[2].split()[0].replace('@','$')
            Row_PathValue = valueList[jk].split('\n')[3].split()[1] + ' ' +valueList[jk].split('\n')[3].split()[2]
            rows = re.findall(r'(\w*[A-Za-z0-9]+)\w*',rowsValue)[0]
            path = valueList[jk].split('\n')[1].split()[0].replace('@','$')
            Row_Member = valueList[jk].split('\n')[4].split()[0]
            f.write(tab + 'GoToPath' + tab + path + '\n')
            f.write(tab + 'GoTo    ${Empty}    Table    0' + '\n')
            f.write(tab + '${length}=    Get Length    ${Head}' + '\n')
            f.write(tab + ':FOR    ${idx}    IN RANGE    1    ${length}' + '\n')
            aid = '${' + rows + '[0]}'
            aValue = '${' + rows + '[${idx}]}'
            f.write(tab + '\    SetByName' + tab + aid + tab + '${Head[${idx}]}' + tab + aValue + '\n')
            f.write(tab + 'SetByName' + tab + aid + tab + 'Members    Click' + '\n')
            f.write(tab + 'Goto    ' + Row_PathValue + '    Label    0' + '\n')
            f.write(tab + 'Goto    ${Empty}    Table    0' + '\n')
            Row_Mem = re.findall(r'(\w*[A-Za-z0-9]+)\w*',Row_Member)[0]
            f.write(tab + ':FOR    ${MembersToSelect}    IN    ' + '${' + Row_Mem + '}' + '\n')
            f.write(tab + '\    SetByIdCol    ${MembersToSelect}    2    On' + '\n')
            f.write(tab + 'PushOK' + '\n')
            f.write(tab + '[Teardown]    DoTeardown' + '\n')


if __name__=="__main__":
    main()
