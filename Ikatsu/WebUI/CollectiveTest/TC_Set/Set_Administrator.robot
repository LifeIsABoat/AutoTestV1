*** Settings ***
Resource        ../Variables/Option.txt
Resource        ../Variables/SecureFunctionLock.txt
Resource        ../Variables/SolutionsApplication.txt
Resource        ../Variables/OptionExternaCardReader.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetSecureFunctionLockOn
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
SetNo.2-1-12
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    2    13     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    2    14    1
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-13
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
SetNo.2-1-25
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath2}
    GoTo    ${Empty}    Table    0
    SetByCoord    25    13     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    25    14    1
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-26
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
SetNo.2-1-38
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath3}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    13     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    1    14    1
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-39
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
SetNo.2-1-50
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    12     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    1    13    1
    PushOK
    [Teardown]    DoTearDown
SetNo.2-1-51
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath1}
    GoTo    ${Empty}    Table    0
    SetByCoord    1    12     On
    GoToPageRoot
    GoTo    ${Empty}    Table    0
    SetByCoord    1    13    1
    PushOK
    [Teardown]    DoTearDown


*** Test Cases ***
SetNo.2-2-1
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row1[0]}    ${index}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-2
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row1[0]}    ${index}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-3
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row1[0]}    ${index}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-4
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row1[0]}    ${index}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-5
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row1[0]}    ${index}    ${Row1[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-2-6
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row3[0]}    ${index}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-7
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row3[0]}    ${index}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-8
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row3[0]}    ${index}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-9
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row3[0]}    ${index}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-10
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row3[0]}    ${index}    ${Row3[${idx}]}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-2-11
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row2[0]}    ${index}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-12
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row2[0]}    ${index}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-13
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row2[0]}    ${index}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-14
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    SetByCoord    ${Row2[0]}    ${index}    ${Row2[${idx}]}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-2-15
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
SetNo.2-2-19
    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${Empty}    Text    0
    SetText     ${TimeValue1}
    PushOK
    DoTearDown
    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${empty}    Text    1
    SetText     ${TimeValue2}
    PushOK
    [Teardown]    DoTearDown


SetActiveDirectoryAuthenticationOn
    [Setup]    DoSetUp
    GoToPath    ${TestPath_Condition1}
    SetSubNodeValue    @{OptionValue_Condition1}    @{OptionIndex_Condition1}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-3}
    SetSubNodeValue    @{OptionValue_2-3-3}    @{OptionIndex_2-3-3}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-4}
    SetSubNodeValue    @{OptionValue_2-3-4}    @{OptionIndex_2-3-4}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-5}
    SetSubNodeValue    @{OptionValue_2-3-5}    @{OptionIndex_2-3-5}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-6}
    SetSubNodeValue    @{OptionValue_2-3-6}    @{OptionIndex_2-3-6}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-7}
    SetSubNodeValue    @{OptionValue_2-3-7}    @{OptionIndex_2-3-7}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-9}
    SetSubNodeValue    @{OptionValue_2-3-9}    @{OptionIndex_2-3-9}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-10}
    SetSubNodeValue    @{OptionValue_2-3-10}    @{OptionIndex_2-3-10}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-11}
    SetSubNodeValue    @{OptionValue_2-3-11}    @{OptionIndex_2-3-11}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-12
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-12}
    SetSubNodeValue    @{OptionValue_2-3-12}    @{OptionIndex_2-3-12}
    PushOK
    [Teardown]    DoTearDown

SetLDAPAuthenticationOn
    [Setup]    DoSetUp
    GoToPath    ${TestPath_Condition2}
    SetSubNodeValue    @{OptionValue_Condition2}    @{OptionIndex_Condition2}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-13
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-13}
    SetSubNodeValue    @{OptionValue_2-3-13}    @{OptionIndex_2-3-13}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-14
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-14}
    SetSubNodeValue    @{OptionValue_2-3-14}    @{OptionIndex_2-3-14}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-15
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-15}
    SetSubNodeValue    @{OptionValue_2-3-15}    @{OptionIndex_2-3-15}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-16
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-16}
    SetSubNodeValue    @{OptionValue_2-3-16}    @{OptionIndex_2-3-16}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-17
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-17}
    SetSubNodeValue    @{OptionValue_2-3-17}    @{OptionIndex_2-3-17}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-18
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-18}
    SetSubNodeValue    @{OptionValue_2-3-18}    @{OptionIndex_2-3-18}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-19
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-19}
    SetSubNodeValue    @{OptionValue_2-3-19}    @{OptionIndex_2-3-19}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-20
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-20}
    SetSubNodeValue    @{OptionValue_2-3-20}    @{OptionIndex_2-3-20}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-21
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-21}
    SetSubNodeValue    @{OptionValue_2-3-21}    @{OptionIndex_2-3-21}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-34
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-34}
    SetSubNodeValue    @{OptionValue_2-3-34}    @{OptionIndex_2-3-34}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-35
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-35}
    SetSubNodeValue    @{OptionValue_2-3-35}    @{OptionIndex_2-3-35}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-36
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-36}
    SetSubNodeValue    @{OptionValue_2-3-36}    @{OptionIndex_2-3-36}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-37
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-37}
    SetSubNodeValue    @{OptionValue_2-3-37}    @{OptionIndex_2-3-37}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-38
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-38}
    SetSubNodeValue    @{OptionValue_2-3-38}    @{OptionIndex_2-3-38}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-39
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-39}
    SetSubNodeValue    @{OptionValue_2-3-39}    @{OptionIndex_2-3-39}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-40
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-40}
    SetSubNodeValue    @{OptionValue_2-3-40}    @{OptionIndex_2-3-40}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-41
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-41}
    SetSubNodeValue    @{OptionValue_2-3-41}    @{OptionIndex_2-3-41}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-42
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-42}
    SetSubNodeValue    @{OptionValue_2-3-42}    @{OptionIndex_2-3-42}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-43
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-43}
    SetSubNodeValue    @{OptionValue_2-3-43}    @{OptionIndex_2-3-43}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-44
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-44}
    SetSubNodeValue    @{OptionValue_2-3-44}    @{OptionIndex_2-3-44}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-45
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-45}
    SetSubNodeValue    @{OptionValue_2-3-45}    @{OptionIndex_2-3-45}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-46
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-46}
    SetSubNodeValue    @{OptionValue_2-3-46}    @{OptionIndex_2-3-46}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-47
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-47}
    SetSubNodeValue    @{OptionValue_2-3-47}    @{OptionIndex_2-3-47}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-48
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-48}
    SetSubNodeValue    @{OptionValue_2-3-48}    @{OptionIndex_2-3-48}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-49
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-49}
    SetSubNodeValue    @{OptionValue_2-3-49}    @{OptionIndex_2-3-49}
    PushOK
    [Teardown]    DoTearDown


*** Test Cases ***
SetNo.2-3-50
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-51
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-52
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-53
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-54
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-55
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-56
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-57
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-58
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-59
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-60
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-61
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-62
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-63
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-64
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-65
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-66
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-67
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-68
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-69
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-70
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-71
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-72
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-73
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-74
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-75
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-76
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-77
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-78
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-79
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-80
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-81
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-82
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-83
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-84
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-85
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-86
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-87
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-88
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-89
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-90
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-91
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-92
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-93
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-94
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-95
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-96
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-97
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-98
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-99
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-100
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-101
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-102
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-103
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-104
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-105
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-106
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-107
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-108
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-109
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-110
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown


*** Test Cases ***
SetNo.2-3-111
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-111}
    SetSubNodeValue    @{OptionValue_2-3-111}    @{OptionIndex_2-3-111}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-112
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path1}
    SetSubNodeValue    ${Value1}    ${Index1}
SetNo.2-3-113
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path2}
    SetSubNodeValue    ${Value2}    ${Index2}
SetNo.2-3-114
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path3}
    SetSubNodeValue    ${Value3}    ${Index3}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-115
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path4}
    SetSubNodeValue    ${Value4}    ${Index4}
SetNo.2-3-116
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path5}
    SetSubNodeValue    ${Value5}    ${Index5}
SetNo.2-3-117
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path6}
    SetSubNodeValue    ${Value6}    ${Index6}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-118
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path7}
    SetSubNodeValue    ${Value7}    ${Index7}
SetNo.2-3-119
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path8}
    SetSubNodeValue    ${Value8}    ${Index8}
SetNo.2-3-120
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path9}
    SetSubNodeValue    ${Value9}    ${Index9}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-121
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path10}
    SetSubNodeValue    ${Value10}    ${Index10}
SetNo.2-3-122
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path11}
    SetSubNodeValue    ${Value11}    ${Index11}
SetNo.2-3-123
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path12}
    SetSubNodeValue    ${Value12}    ${Index12}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-124
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path13}
    SetSubNodeValue    ${Value13}    ${Index13}
SetNo.2-3-125
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path14}
    SetSubNodeValue    ${Value14}    ${Index14}
SetNo.2-3-126
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path15}
    SetSubNodeValue    ${Value15}    ${Index15}
    PushOK
    [Teardown]    DoTearDown
   

SetNo.2-3-142
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-142}
    SetValueSingle    @{OptionValue_2-3-142}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-137
    DoSetUp
    GoToPath    ${TestPath_2-3-137-1}
    SetSubNodeValue    @{OptionValue_2-3-137-1}    @{OptionIndex_2-3-137-1}
    PushOK
    DoTearDown
    DoSetUp
    GoToPath    ${TestPath_2-3-137-2}
    SetSubNodeValue    @{OptionValue_2-3-137-2}    @{OptionIndex_2-3-137-2}
    PushOK
    DoTearDown
    DoSetUp
    GoToPath    ${TestPath_2-3-137-3}
    SetSubNodeValue    @{OptionValue_2-3-137-3}    @{OptionIndex_2-3-137-3}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-139
    DoSetUp
    GoToPath    ${TestPath_2-3-139-1}
    SetSubNodeValue    @{OptionValue_2-3-139-1}    @{OptionIndex_2-3-139-1}
    PushOK
    DoTearDown
    DoSetUp
    GoToPath    ${TestPath_2-3-139-2}
    SetSubNodeValue    @{OptionValue_2-3-139-2}    @{OptionIndex_2-3-139-2}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-140
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-140}
    SetSubNodeValue    @{OptionValue_2-3-140}    @{OptionIndex_2-3-140}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-143
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-143}
    SetSubNodeValue    @{OptionValue_2-3-143}    @{OptionIndex_2-3-143}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-144
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-144}
    SetSubNodeValue    @{OptionValue_2-3-144}    @{OptionIndex_2-3-144}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-145
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-145}
    SetSubNodeValue    @{OptionValue_2-3-145}    @{OptionIndex_2-3-145}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-146
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-146}
    SetSubNodeValue    @{OptionValue_2-3-146}    @{OptionIndex_2-3-146}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-147
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-147}
    SetSubNodeValue    @{OptionValue_2-3-147}    @{OptionIndex_2-3-147}
    PushOK
    [Teardown]    DoTearDown
