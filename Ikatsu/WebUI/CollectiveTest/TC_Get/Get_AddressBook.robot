*** Settings ***  
Resource        ../Variables/AddressBook.txt
Resource        ../Variables/AddressBookGroup.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GetetNo.3-1-1-No.3-1-5
    [Setup]    DoSetUp
    GoToPath    ${addressPath1}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row1[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row1[1]}
    [Teardown]    DoTearDown
GetNo.3-1-6-No.3-1-10
    [Setup]    DoSetUp
    GoToPath    ${addressPath2}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row2[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row2[1]}
    [Teardown]    DoTearDown
GetNo.3-1-11.1-No.3-1-16.1
    [Setup]    DoSetUp
    GoToPath    ${addressPath3}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row3[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row3[1]}
    [Teardown]    DoTearDown
GetNo.3-1-11.2-No.3-1-16.2
    [Setup]    DoSetUp
    GoToPath    ${addressPath4}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row4[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row4[1]}
    [Teardown]    DoTearDown


*** Test Cases ***
GetNo.3-3-1-No.3-3-5
    [Setup]    DoSetUp
    GoToPath    ${GroupPath1}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row1[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member1[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown

GetNo.3-3-6-No.3-3-10
    [Setup]    DoSetUp
    GoToPath    ${GroupPath2}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row2[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member2[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown

GetNo.3-3-11-No.3-3-16
    [Setup]    DoSetUp
    GoToPath    ${GroupPath3}
    GoTo    ${Empty}    Table    0
    SetByName    ${Row3[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    ${Result}    GetByIdCol    ${Row_Member3[0]}    2
    Should Be Equal    ${Result}    On
    [Teardown]    DoTearDown
