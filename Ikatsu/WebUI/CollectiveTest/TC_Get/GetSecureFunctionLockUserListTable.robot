*** Settings ***  
Resource        ../Variables/SecureFunctionLock.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GetNo.2-2-1~No.2-2-5
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row1[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.2-2-6~No.2-2-10
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row3[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row3[${idx}]}
    [Teardown]    DoTearDown

GetNo.2-2-11~No.2-2-15
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row2[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row2[${idx}]}
    [Teardown]    DoTearDown

GetNo.2-2-16
    [Setup]    DoSetUp
    GoToPath    ${FrequencyPath}
    ${Result}    GetSubNodeValue    0
    Should Be Equal    ${Result}    @{FrequencyValue}
    [Teardown]    DoTearDown
GetNo.2-2-19.1
    [Setup]    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${Empty}    Text    0
    ${Result}    GetText
    Should Be Equal    ${Result}    @{TimeValue1}
    [Teardown]    DoTearDown
GetNo.2-2-19.2
    [Setup]    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${empty}    Text    1
    ${Result}    GetText
    Should Be Equal    ${Result}    @{TimeValue2}
    [Teardown]    DoTearDown
