*** Settings ***  
Resource        ../Variables/ScanProfileHTTP(S).txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GetNo.4-10-1
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path1}
    ${current_text}    GetSubNodeValue    ${Index1}
    Should Be Equal    ${current_text}    ${Value1}
    [Teardown]    DoTearDown
GetNo.4-10-2
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path2}
    ${current_text}    GetSubNodeValue    ${Index2}
    Should Be Equal    ${current_text}    ${Value2}
    [Teardown]    DoTearDown
GetNo.4-10-3
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path3}
    ${current_text}    GetSubNodeValue    ${Index3}
    Should Be Equal    ${current_text}    ${Value3}
    [Teardown]    DoTearDown

GetNo.4-10-5
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path5}
    ${current_text}    GetSubNodeValue    ${Index5}
    Should Be Equal    ${current_text}    ${Value5}
    [Teardown]    DoTearDown
GetNo.4-10-6
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path6}
    ${current_text}    GetSubNodeValue    ${Index6}
    Should Be Equal    ${current_text}    ${Value6}
    [Teardown]    DoTearDown

GetNo.4-10-7
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path7}
    ${current_text}    GetSubNodeValue    ${Index7}
    Should Be Equal    ${current_text}    ${Value7}
    [Teardown]    DoTearDown
GetNo.4-10-9
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path9}
    ${current_text}    GetSubNodeValue    ${Index9}
    Should Be Equal    ${current_text}    ${Value9}
    [Teardown]    DoTearDown
    
GetNo.4-10-10
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path10}
    ${current_text}    GetSubNodeValue    ${Index10}
    Should Be Equal    ${current_text}    ${Value10}
    [Teardown]    DoTearDown
GetNo.4-10-11
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path11}
    ${current_text}    GetSubNodeValue    ${Index11}
    Should Be Equal    ${current_text}    ${Value11}
    [Teardown]    DoTearDown
GetNo.4-10-12
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path12}
    ${current_text}    GetSubNodeValue    ${Index12}
    Should Be Equal    ${current_text}    ${Value12}
    [Teardown]    DoTearDown

GetNo.4-10-13
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path13}
    ${current_text}    GetSubNodeValue    ${Index13}
    Should Be Equal    ${current_text}    ${Value13}
    [Teardown]    DoTearDown
GetNo.4-10-14
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path14}
    ${current_text}    GetSubNodeValue    ${Index14}
    Should Be Equal    ${current_text}    ${Value14}
    [Teardown]    DoTearDown
GetNo.4-10-15
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path15}
    ${current_text}    GetSubNodeValue    ${Index15}
    Should Be Equal    ${current_text}    ${Value15}
    [Teardown]    DoTearDown

GetNo.4-10-16
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path16}
    ${current_text}    GetSubNodeValue    ${Index16}
    Should Be Equal    ${current_text}    ${Value16}
    [Teardown]    DoTearDown
GetNo.4-10-17
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToPath    ${Path17}
    ${current_text}    GetSubNodeValue    ${Index17}
    Should Be Equal    ${current_text}    ${Value17}
    [Teardown]    DoTearDown
