*** Settings ***  
Resource        ../Variables/AddressBook.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GetNo.3-1-1
    [Setup]    DoSetUp
    GoToPath    ${addressPath1}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row1[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row1[1]}
    [Teardown]    DoTearDown
GetNo.3-1-3
    [Setup]    DoSetUp
    GoToPath    ${addressPath1}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row1[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row1[1]}
    [Teardown]    DoTearDown
GetNo.3-1-4
    [Setup]    DoSetUp
    GoToPath    ${addressPath1}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row1[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row1[1]}
    [Teardown]    DoTearDown

GetNo.3-1-6
    [Setup]    DoSetUp
    GoToPath    ${addressPath2}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row2[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row2[1]}
    [Teardown]    DoTearDown
GetNo.3-1-8
    [Setup]    DoSetUp
    GoToPath    ${addressPath2}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row2[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row2[1]}
    [Teardown]    DoTearDown
GetNo.3-1-9
    [Setup]    DoSetUp
    GoToPath    ${addressPath2}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row2[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row2[1]}
    [Teardown]    DoTearDown

Get最小最大以外の任意AddressのNo.23
    [Setup]    DoSetUp
    GoToPath    ${addressPath3}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row3[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row3[1]}
    [Teardown]    DoTearDown
Get最小最大以外の任意AddressのNo.24
    [Setup]    DoSetUp
    GoToPath    ${addressPath4}
    GoTo    ${Empty}    Table    0
    ${CurrentValue}    GetByName    ${Row4[0]}    ${Head[1]}
    Should Be Equal    ${CurrentValue}    ${Row4[1]}
    [Teardown]    DoTearDown
