*** Settings ***  
Resource        ../Variables/AddressBook.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetNo.3-1-1
    [Setup]    DoSetUp
    GoToPath    ${addressPath1}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.3-1-3
    [Setup]    DoSetUp
    GoToPath    ${addressPath1}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.3-1-4
    [Setup]    DoSetUp
    GoToPath    ${addressPath1}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.3-1-6
    [Setup]    DoSetUp
    GoToPath    ${addressPath2}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.3-1-8
    [Setup]    DoSetUp
    GoToPath    ${addressPath2}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.3-1-9
    [Setup]    DoSetUp
    GoToPath    ${addressPath2}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown

Set最小最大以外の任意AddressのNo.23
    [Setup]    DoSetUp
    GoToPath    ${addressPath3}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row3[0]}    ${Head[${idx}]}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown

Set最小最大以外の任意AddressのNo.24
    [Setup]    DoSetUp
    GoToPath    ${addressPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row4[0]}    ${Head[${idx}]}    ${Row4[${idx}]}
    PushOK
    [Teardown]    DoTearDown
