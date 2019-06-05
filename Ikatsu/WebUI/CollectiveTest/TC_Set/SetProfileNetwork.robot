*** Settings ***  
Resource        ../Variables/ScanProfileNetwork.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetScan_Profile_Network_Setup
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
SetNo.4-8-1
    GoToPageRoot
    GoToPath    ${Path1}
    SetSubNodeValue    ${Value1}    ${Index1}
SetNo.4-8-2
    GoToPageRoot
    GoToPath    ${Path2}
    SetSubNodeValue    ${Value2}    ${Index2}
SetNo.4-8-3
    GoToPageRoot
    GoToPath    ${Path3}
    SetSubNodeValue    ${Value3}    ${Index3}
SetNo.4-8-4
    GoToPageRoot
    GoToPath    ${Path4}
    SetSubNodeValue    ${Value4}    ${Index4}
SetNo.4-8-5
    GoToPageRoot
    GoToPath    ${Path5}
    SetSubNodeValue    ${Value5}    ${Index5}
SetNo.4-8-6
    GoToPageRoot
    GoToPath    ${Path6}
    SetSubNodeValue    ${Value6}    ${Index6}
SetNo.4-8-8
    GoToPageRoot
    GoToPath    ${Path8}
    SetSubNodeValue    ${Value8}    ${Index8}
SetNo.4-8-9
    GoToPageRoot
    GoToPath    ${Path9}
    SetSubNodeValue    ${Value9}    ${Index9}
SetNo.4-8-10
    GoToPageRoot
    GoToPath    ${Path10}
    SetSubNodeValue    ${Value10}    ${Index10}
SetNo.4-8-11
    GoToPageRoot
    GoToPath    ${Path11}
    SetSubNodeValue    ${Value11}    ${Index11}
SetNo.4-8-12
    GoToPageRoot
    GoToPath    ${Path12}
    SetSubNodeValue    ${Value12}    ${Index12}
SetNo.4-8-13
    GoToPageRoot
    GoToPath    ${Path13}
    SetSubNodeValue    ${Value13}    ${Index13}
SetNo.4-8-14
    GoToPageRoot
    GoToPath    ${Path14}
    SetSubNodeValue    ${Value14}    ${Index14}
SetNo.4-8-15
    GoToPageRoot
    GoToPath    ${Path15}
    SetSubNodeValue    ${Value15}    ${Index15}
SetNo.4-8-16
    GoToPageRoot
    GoToPath    ${Path16}
    SetSubNodeValue    ${Value16}    ${Index16}
SetNo.4-8-17
    GoToPageRoot
    GoToPath    ${Path17}
    SetSubNodeValue    ${Value17}    ${Index17}
SetNo.4-8-18
    GoToPageRoot
    GoToPath    ${Path18}
    SetSubNodeValue    ${Value18}    ${Index18}

SetScan_Profile_Network_Down
    PushOK
    [Teardown]    DoTearDown
