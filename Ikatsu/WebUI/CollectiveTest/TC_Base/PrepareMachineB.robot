*** Settings ***  
Resource        ../BaseInfo.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service


*** Test Cases ***
PrepareMachineB
    SetMode    EWS
    SetPrinterIP    ${MachineB_IP}
    SetPassword    ${MachineB_PWD}
