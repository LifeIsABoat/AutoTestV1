*** Settings ***  
Resource        ../Variables/ScanProfileHTTP(S).txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GoToProfile(HTTP(S))
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
SetNo.4-10-1
    GoToPageRoot
    GoToPath    ${Path1}
    SetSubNodeValue    ${Value1}    ${Index1}
SetNo.4-10-2
    GoToPageRoot
    GoToPath    ${Path2}
    SetSubNodeValue    ${Value2}    ${Index2}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-3
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path3}
    SetSubNodeValue    ${Value3}    ${Index3}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-5
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path5}
    SetSubNodeValue    ${Value5}    ${Index5}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-6
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path6}
    SetSubNodeValue    ${Value6}    ${Index6}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-7
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path7}
    SetSubNodeValue    ${Value7}    ${Index7}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-9
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path9}
    SetSubNodeValue    ${Value9}    ${Index9}
    PushOK
    [Teardown]    DoTearDown
    
SetNo.4-10-10
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path10}
    SetSubNodeValue    ${Value10}    ${Index10}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-11
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path11}
    SetSubNodeValue    ${Value11}    ${Index11}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-12
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path12}
    SetSubNodeValue    ${Value12}    ${Index12}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-13
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path13}
    SetSubNodeValue    ${Value13}    ${Index13}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-14
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path14}
    SetSubNodeValue    ${Value14}    ${Index14}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-15
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path15}
    SetSubNodeValue    ${Value15}    ${Index15}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-16
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path16}
    SetSubNodeValue    ${Value16}    ${Index16}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-10-17
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path17}
    SetSubNodeValue    ${Value17}    ${Index17}
    PushOK
    [Teardown]    DoTearDown
