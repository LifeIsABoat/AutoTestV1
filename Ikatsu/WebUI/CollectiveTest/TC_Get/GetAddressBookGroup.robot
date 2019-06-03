*** Settings ***  
Resource        ../Variables/AddressBookGroup.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service
*** Test Cases ***
GetNo.3-3-1
    [Setup]    DoSetUp
    GoToPath    ${GroupPath1}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row1[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member1[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown
GetNo.3-3-2
    [Setup]    DoSetUp
    GoToPath    ${GroupPath1}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row1[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member1[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown
GetNo.3-3-4
    [Setup]    DoSetUp
    GoToPath    ${GroupPath1}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row1[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member1[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown

GetNo.3-3-7
    [Setup]    DoSetUp
    GoToPath    ${GroupPath2}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row2[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member2[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown
GetNo.3-3-8
    [Setup]    DoSetUp
    GoToPath    ${GroupPath2}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row2[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member2[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown
GetNo.3-3-10
    [Setup]    DoSetUp
    GoToPath    ${GroupPath2}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row2[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member2[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown

Get最小最大以外の任意Address Book Setup Group-G3
    [Setup]    DoSetUp
    GoToPath    ${GroupPath3}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row3[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member3[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown
