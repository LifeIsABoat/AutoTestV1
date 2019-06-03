*** Settings ***  
Resource        ../Variables/AddressBookGroup.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service
*** Test Cases ***
SetNo.3-3-1
    [Setup]    DoSetUp
    GoToPath    ${GroupPath1}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    SetByName    ${Row1[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    :FOR    ${MembersToSelect}    IN    ${Row_Member1}
    \    SetByIdCol    ${MembersToSelect}    2    On
    PushOK
    [Teardown]    DoTearDown
SetNo.3-3-2
    [Setup]    DoSetUp
    GoToPath    ${GroupPath1}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    SetByName    ${Row1[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    :FOR    ${MembersToSelect}    IN    ${Row_Member1}
    \    SetByIdCol    ${MembersToSelect}    2    On
    PushOK
    [Teardown]    DoTearDown
SetNo.3-3-4
    [Setup]    DoSetUp
    GoToPath    ${GroupPath1}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    SetByName    ${Row1[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    :FOR    ${MembersToSelect}    IN    ${Row_Member1}
    \    SetByIdCol    ${MembersToSelect}    2    On
    PushOK
    [Teardown]    DoTearDown

SetNo.3-3-7
    [Setup]    DoSetUp
    GoToPath    ${GroupPath2}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    SetByName    ${Row2[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    :FOR    ${MembersToSelect}    IN    ${Row_Member2}
    \    SetByIdCol    ${MembersToSelect}    2    On
    PushOK
    [Teardown]    DoTearDown
SetNo.3-3-8
    [Setup]    DoSetUp
    GoToPath    ${GroupPath2}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    SetByName    ${Row2[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    :FOR    ${MembersToSelect}    IN    ${Row_Member2}
    \    SetByIdCol    ${MembersToSelect}    2    On
    PushOK
    [Teardown]    DoTearDown
SetNo.3-3-10
    [Setup]    DoSetUp
    GoToPath    ${GroupPath2}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    SetByName    ${Row2[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    :FOR    ${MembersToSelect}    IN    ${Row_Member2}
    \    SetByIdCol    ${MembersToSelect}    2    On
    PushOK
    [Teardown]    DoTearDown

Set最小最大以外の任意Address Book Setup Group-G3
    [Setup]    DoSetUp
    GoToPath    ${GroupPath3}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row3[0]}    ${Head[${idx}]}    ${Row3[${idx}]}
    SetByName    ${Row3[0]}    Members    Click
    Goto    Address\ Book    Label    0
    Goto    ${Empty}    Table    0
    :FOR    ${MembersToSelect}    IN    ${Row_Member3}
    \    SetByIdCol    ${MembersToSelect}    2    On
    PushOK
    [Teardown]    DoTearDown
