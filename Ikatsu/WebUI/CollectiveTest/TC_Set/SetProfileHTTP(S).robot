*** Settings ***  
Resource        ../Variables/ScanProfileHTTP(S).txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetNo.4-10-18
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-19
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-20
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row3[0]}    ${Head[${idx}]}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-21
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row4[0]}    ${Head[${idx}]}    ${Row4[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-22
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row5[0]}    ${Head[${idx}]}    ${Row5[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-23
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row6[0]}    ${Head[${idx}]}    ${Row6[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-24
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row1[0]}    ${Head[${idx}]}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-25
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row2[0]}    ${Head[${idx}]}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-26
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row3[0]}    ${Head[${idx}]}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-27
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row4[0]}    ${Head[${idx}]}    ${Row4[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-28
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row5[0]}    ${Head[${idx}]}    ${Row5[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.4-10-29
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    SetByName    ${Row6[0]}    ${Head[${idx}]}    ${Row6[${idx}]}
    PushOK
    [Teardown]    DoTearDown


SetNo.4-10-30
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${BodyPath}
    SetSubNodeValue    ${BodyValue}    ${BodyIndex}
    PushOK
    [Teardown]    DoTearDown
    
SetNo.4-10-31
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${AuthenticationSettingPath}
    GoToName    ${UsernamePath}
    SetSubNodeValue    ${UsernameValue}    ${UsernameIndex}
    PushOK
    [Teardown]    DoTearDown
