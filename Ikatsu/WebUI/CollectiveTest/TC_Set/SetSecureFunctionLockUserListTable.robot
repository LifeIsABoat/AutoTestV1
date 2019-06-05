*** Settings ***  
Resource        ../Variables/SecureFunctionLock.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetNo.2-2-1~No.2-2-5
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row1[0]}    ${index}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-2-6~No.2-2-10
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row3[0]}    ${index}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-2-11~No.2-2-15
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row2[0]}    ${index}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-2-16
    [Setup]    DoSetUp
    GoToPath    ${FrequencyPath}
    SetSubNodeValue     ${FrequencyValue}       0
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-19.1
    [Setup]    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${Empty}    Text    0
    SetText     ${TimeValue1}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-19.2
    [Setup]    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${empty}    Text    1
    SetText     ${TimeValue2}
    PushOK
    [Teardown]    DoTearDown
