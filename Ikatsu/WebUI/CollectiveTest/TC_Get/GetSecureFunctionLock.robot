*** Settings ***  
Resource        ../Variables/SecureFunctionLock.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetSecureFunctionLockCondition
    [Setup]    DoSetUp
    GoToPath    ${SecureFunctionLockPath}
    SetSubNodeValue    ${SecureFunctionLockValue}    ${SecureFunctionLockIndex}
    PushOK
    [Teardown]    DoTearDown
GetNo.2-1-1
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2   2
    Should Be Equal    ${Result}    USER1
    [Teardown]    DoTearDown
GetNo.2-1-2
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    3
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-3
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    4
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-4
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    5
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-5
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    6
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-6
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    7
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-7
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    8
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-8
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    9
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-9
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    10
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-10
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    11
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-11
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    12
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-12&No.2-1-13
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    13
    Should Be Equal    ${Result}    On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    2    14
    Should Be Equal    ${Result}    1
    [Teardown]    DoTearDown


    
GetNo.2-1-14
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   2
    Should Be Equal    ${Result}    USER100
    [Teardown]    DoTearDown
GetNo.2-1-15
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   3
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-16
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   4
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-17
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   5
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-18
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   6
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-19
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   7
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-20
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   8
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-21
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   9
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-22
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   10
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-23
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   11
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-24
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   12
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-25&No.2-1-26
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   13
    Should Be Equal    ${Result}    On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    25   14
    Should Be Equal    ${Result}    1
    [Teardown]    DoTearDown



GetNo.2-1-27
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   2
    Should Be Equal    ${Result}    USER26
    [Teardown]    DoTearDown
GetNo.2-1-28
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   3
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-29
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   4
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-30
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   5
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-31
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   6
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-32
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   7
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-33
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   8
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-34
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   9
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-35
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   10
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-36
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   11
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-37
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   12
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-38&No.2-1-39
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1   13
    Should Be Equal    ${Result}    On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    14
    Should Be Equal    ${Result}    1
    [Teardown]    DoTearDown



GetNo.2-1-40
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    2
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-41
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    3
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-42
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    4
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-43
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    5
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-44
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    6
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-45
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    7
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-46
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    8
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-47
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    9
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-48
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    10
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-49
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    11
    Should Be Equal    ${Result}    Off
    [Teardown]    DoTearDown
GetNo.2-1-50&No.2-1-51
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    12
    Should Be Equal    ${Result}    On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    ${Result}    GetByCoord    1    13
    Should Be Equal    ${Result}    1
    [Teardown]    DoTearDown
