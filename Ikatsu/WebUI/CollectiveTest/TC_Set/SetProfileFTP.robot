*** Settings ***  
Resource        ../Variables/ScanProfileFTP.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetScan_Profile_FTP_Setup
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
SetNo.4-6-1
    GoToPageRoot
    GoToPath    ${Path1}
    SetSubNodeValue    ${Value1}    ${Index1}
SetNo.4-6-2
    GoToPageRoot
    GoToPath    ${Path2}
    SetSubNodeValue    ${Value2}    ${Index2}
SetNo.4-6-3
    GoToPageRoot
    GoToPath    ${Path4}
    SetSubNodeValue    ${Value4}    ${Index4}
SetNo.4-6-5
    GoToPageRoot
    GoToPath    ${Path5}
    SetSubNodeValue    ${Value5}    ${Index5}
SetNo.4-6-6
    GoToPageRoot
    GoToPath    ${Path20}
    SetSubNodeValue    ${Value20}    ${Index20}
SetNo.4-6-7
    GoToPageRoot
    GoToPath    ${Path6}
    SetSubNodeValue    ${Value6}    ${Index6}
SetNo.4-6-8
    GoToPageRoot
    GoToPath    ${Path7}
    SetSubNodeValue    ${Value7}    ${Index7}
SetNo.4-6-9
    GoToPageRoot
    GoToPath    ${Path8}
    SetSubNodeValue    ${Value8}    ${Index8}
SetNo.4-6-10
    GoToPageRoot
    GoToPath    ${Path9}
    SetSubNodeValue    ${Value9}    ${Index9}
SetNo.4-6-11
    GoToPageRoot
    GoToPath    ${Path10}
    SetSubNodeValue    ${Value10}    ${Index10}
SetNo.4-6-13
    GoToPageRoot
    GoToPath    ${Path12}
    SetSubNodeValue    ${Value12}    ${Index12}
SetNo.4-6-14
    GoToPageRoot
    GoToPath    ${Path13}
    SetSubNodeValue    ${Value13}    ${Index13}
SetNo.4-6-15
    GoToPageRoot
    GoToPath    ${Path14}
    SetSubNodeValue    ${Value14}    ${Index14}
SetNo.4-6-16
    GoToPageRoot
    GoToPath    ${Path15}
    SetSubNodeValue    ${Value15}    ${Index15}
SetNo.4-6-17
    GoToPageRoot
    GoToPath    ${Path16}
    SetSubNodeValue    ${Value16}    ${Index16}
SetNo.4-6-18
    GoToPageRoot
    GoToPath    ${Path17}
    SetSubNodeValue    ${Value17}    ${Index17}
SetNo.4-6-19
    GoToPageRoot
    GoToPath    ${Path18}
    SetSubNodeValue    ${Value18}    ${Index18}
SetNo.4-6-20
    GoToPageRoot
    GoToPath    ${Path19}
    SetSubNodeValue    ${Value19}    ${Index19}
SetNo.4-6-21
    GoToPageRoot
    GoToPath    ${Path3}
    SetSubNodeValue    ${Value3}    ${Index3}
SetScan_Profile_FTP_Down
    PushOK
    [Teardown]    DoTearDown
