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
SetNo.2-1-1
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord   2   2   USER1
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-2
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    3     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-3
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    4     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-4
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    5     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-5
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    6     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-6
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    7     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-7
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    8     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-8
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    9     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-9
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    10     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-10
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    11     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-11
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    12     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-12&No.2-1-13
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    13     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    2    14    1
    PushOK
    [Teardown]    DoTearDown



SetNo.2-1-14
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord   25   2   USER100
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-15
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    3     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-16
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    4     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-17
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    5     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-18
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    6     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-19
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    7     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-20
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    8     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-21
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    9     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-22
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    10     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-23
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    11     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-24
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    12     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-25&No.2-1-26
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    13     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    25    14    1
    PushOK
    [Teardown]    DoTearDown



SetNo.2-1-27
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord   1   2   USER26
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-28
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    3     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-29
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    4     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-30
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    5     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-31
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    6     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-32
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    7     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-33
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    8     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-34
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    9     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-35
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    10     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-36
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    11     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-37
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    12     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-38&No.2-1-39
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    13     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    1    14    1
    PushOK
    [Teardown]    DoTearDown



SetNo.2-1-40
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord   1   2    Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-41
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    3     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-42
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    4     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-43
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    5     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-44
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    6     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-45
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    7     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-46
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    8     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-47
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    9     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-48
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    10     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-49
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    11     Off
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-50&No.2-1-51
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    12     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    1    13    1
    PushOK
    [Teardown]    DoTearDown
