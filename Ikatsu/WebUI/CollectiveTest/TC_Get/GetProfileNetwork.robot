*** Settings ***  
Resource        ../Variables/ScanProfileNetwork.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service


*** Test Cases ***
GetScan_Profile_Network_Setup
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click

GetNo.4-8-1
    GoToPageRoot
    GoToPath    ${Path1}
    ${current_text}    GetSubNodeValue    ${Index1}
    Should Be Equal    ${current_text}    ${Value1}
GetNo.4-8-2
    GoToPageRoot
    GoToPath    ${Path2}
    ${current_text}    GetSubNodeValue    ${Index2}
    Should Be Equal    ${current_text}    ${Value2}
GetNo.4-8-3
    GoToPageRoot
    GoToPath    ${Path3}
    ${current_text}    GetSubNodeValue    ${Index3}
    Should Be Equal    ${current_text}    ${Value3}
GetNo.4-8-4
    GoToPageRoot
    GoToPath    ${Path4}
    ${current_text}    GetSubNodeValue    ${Index4}
    Should Be Equal    ${current_text}    ${Value4}
GetNo.4-8-5
    GoToPageRoot
    GoToPath    ${Path5}
    ${current_text}    GetSubNodeValue    ${Index5}
    Should Be Equal    ${current_text}    ${Value5}
GetNo.4-8-6
    GoToPageRoot
    GoToPath    ${Path6}
    ${current_text}    GetSubNodeValue    ${Index6}
    Should Be Equal    ${current_text}    ${Value6}
GetNo.4-8-8
    GoToPageRoot
    GoToPath    ${Path8}
    ${current_text}    GetSubNodeValue    ${Index8}
    Should Be Equal    ${current_text}    ${Value8}
GetNo.4-8-9
    GoToPageRoot
    GoToPath    ${Path9}
    ${current_text}    GetSubNodeValue    ${Index9}
    Should Be Equal    ${current_text}    ${Value9}
GetNo.4-8-10
    GoToPageRoot
    GoToPath    ${Path10}
    ${current_text}    GetSubNodeValue    ${Index10}
    Should Be Equal    ${current_text}    ${Value10}
GetNo.4-8-11
    GoToPageRoot
    GoToPath    ${Path11}
    ${current_text}    GetSubNodeValue    ${Index11}
    Should Be Equal    ${current_text}    ${Value11}
GetNo.4-8-12
    GoToPageRoot
    GoToPath    ${Path12}
    ${current_text}    GetSubNodeValue    ${Index12}
    Should Be Equal    ${current_text}    ${Value12}
GetNo.4-8-13
    GoToPageRoot
    GoToPath    ${Path13}
    ${current_text}    GetSubNodeValue    ${Index13}
    Should Be Equal    ${current_text}    ${Value13}
GetNo.4-8-14
    GoToPageRoot
    GoToPath    ${Path14}
    ${current_text}    GetSubNodeValue    ${Index14}
    Should Be Equal    ${current_text}    ${Value14}
GetNo.4-8-15
    GoToPageRoot
    GoToPath    ${Path15}
    ${current_text}    GetSubNodeValue    ${Index15}
    Should Be Equal    ${current_text}    ${Value15}
GetNo.4-8-16
    GoToPageRoot
    GoToPath    ${Path16}
    ${current_text}    GetSubNodeValue    ${Index16}
    Should Be Equal    ${current_text}    ${Value16}
GetNo.4-8-17
    GoToPageRoot
    GoToPath    ${Path17}
    ${current_text}    GetSubNodeValue    ${Index17}
    Should Be Equal    ${current_text}    ${Value17}
GetNo.4-8-18
    GoToPageRoot
    GoToPath    ${Path18}
    ${current_text}    GetSubNodeValue    ${Index18}
    Should Be Equal    ${current_text}    ${Value18}
    [Teardown]    DoTearDown
