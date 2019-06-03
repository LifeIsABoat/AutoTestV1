*** Settings ***  
Resource        ../BaseInfo.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service


*** Test Cases ***
PrepareMachineA
    SetMode    EWS
    SetPrinterIP    ${MachineA_IP}
    SetPassword    ${MachineA_PWD}
