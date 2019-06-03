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
GetNo.2-1-12
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
GetNo.2-1-13
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
GetNo.2-1-25
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
GetNo.2-1-26
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
GetNo.2-1-38
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
GetNo.2-1-39
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
GetNo.2-1-50
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
GetNo.2-1-51
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


*** Test Cases ***
GetNo.2-2-1
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row1[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-2
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row1[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-3
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row1[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-4
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row1[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-5
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath4}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row1}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row1[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.2-2-6
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row3[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row3[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-7
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row3[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row3[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-8
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row3[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row3[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-9
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row3[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row3[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-10
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath6}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row3}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row3[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row3[${idx}]}
    [Teardown]    DoTearDown

GetNo.2-2-11
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row2[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row2[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-12
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row2[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row2[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-13
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row2[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row2[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-14
    [Setup]    DoSetUp
    GoToPath    ${FunctionsPath5}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Row2}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${index} =    Evaluate    ${idx} + 1
    \    ${Result}    GetByCoord    ${Row2[0]}    ${index}
    \    Should Be Equal    ${Result}    ${Row2[${idx}]}
    [Teardown]    DoTearDown
GetNo.2-2-15
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
GetNo.2-2-19
    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${Empty}    Text    0
    ${Result}    GetText
    Should Be Equal    ${Result}    @{TimeValue1}
    DoTearDown
    DoSetUp
    GoToPath    ${TimePath}
    GoTo    ${empty}    Text    1
    ${Result}    GetText
    Should Be Equal    ${Result}    @{TimeValue2}
    [Teardown]    DoTearDown


SetActiveDirectoryAuthenticationOn
    [Setup]    DoSetUp
    GoToPath    ${TestPath_Condition1}
    SetSubNodeValue    @{OptionValue_Condition1}    @{OptionIndex_Condition1}
    PushOK
    [Teardown]    DoTearDown
GetNo.2-3-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-3}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-3}
    [Teardown]    DoTearDown
GetNo.2-3-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-4}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-4}
    [Teardown]    DoTearDown
GetNo.2-3-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-5}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-5}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-5}
    [Teardown]    DoTearDown
GetNo.2-3-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-6}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-6}
    [Teardown]    DoTearDown
GetNo.2-3-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-7}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-7}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-7}
    [Teardown]    DoTearDown
GetNo.2-3-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-9}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-9}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-9}
    [Teardown]    DoTearDown
GetNo.2-3-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-10}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-10}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-10}
    [Teardown]    DoTearDown
GetNo.2-3-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-11}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-11}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-11}
    [Teardown]    DoTearDown
GetNo.2-3-12
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-12}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-12}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-12}
    [Teardown]    DoTearDown

SetLDAPAuthenticationOn
    [Setup]    DoSetUp
    GoToPath    ${TestPath_Condition2}
    SetSubNodeValue    @{OptionValue_Condition2}    @{OptionIndex_Condition2}
    PushOK
    [Teardown]    DoTearDown
GetNo.2-3-13
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-13}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-13}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-13}
    [Teardown]    DoTearDown
GetNo.2-3-14
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-14}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-14}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-14}
    [Teardown]    DoTearDown
GetNo.2-3-15
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-15}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-15}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-15}
    [Teardown]    DoTearDown
GetNo.2-3-16
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-16}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-16}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-16}
    [Teardown]    DoTearDown
GetNo.2-3-17
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-17}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-17}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-17}
    [Teardown]    DoTearDown
GetNo.2-3-18
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-18}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-18}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-18}
    [Teardown]    DoTearDown
GetNo.2-3-19
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-19}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-19}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-19}
    [Teardown]    DoTearDown
GetNo.2-3-20
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-20}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-20}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-20}
    [Teardown]    DoTearDown
GetNo.2-3-21
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-21}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-21}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-21}
    [Teardown]    DoTearDown

GetNo.2-3-34
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-34}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-34}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-34}
    [Teardown]    DoTearDown
GetNo.2-3-35
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-35}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-35}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-35}
    [Teardown]    DoTearDown
GetNo.2-3-36
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-36}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-36}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-36}
    [Teardown]    DoTearDown
GetNo.2-3-37
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-37}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-37}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-37}
    [Teardown]    DoTearDown
GetNo.2-3-38
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-38}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-38}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-38}
    [Teardown]    DoTearDown
GetNo.2-3-39
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-39}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-39}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-39}
    [Teardown]    DoTearDown
GetNo.2-3-40
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-40}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-40}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-40}
    [Teardown]    DoTearDown
GetNo.2-3-41
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-41}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-41}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-41}
    [Teardown]    DoTearDown
GetNo.2-3-42
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-42}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-42}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-42}
    [Teardown]    DoTearDown
GetNo.2-3-43
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-43}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-43}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-43}
    [Teardown]    DoTearDown
GetNo.2-3-44
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-44}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-44}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-44}
    [Teardown]    DoTearDown
GetNo.2-3-45
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-45}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-45}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-45}
    [Teardown]    DoTearDown
GetNo.2-3-46
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-46}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-46}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-46}
    [Teardown]    DoTearDown
GetNo.2-3-47
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-47}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-47}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-47}
    [Teardown]    DoTearDown
GetNo.2-3-48
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-48}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-48}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-48}
    [Teardown]    DoTearDown
GetNo.2-3-49
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-49}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-49}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-49}
    [Teardown]    DoTearDown


*** Test Cases ***
GetNo.2-3-50
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-51
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-52
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-53
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-54
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-55
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-56
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-57
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-58
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-59
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-60
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-61
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-62
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-63
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-64
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-65
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-66
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-67
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-68
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-69
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-70
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-71
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-72
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-73
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-74
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-75
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-76
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-77
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-78
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-79
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-80
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-81
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-82
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-83
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-84
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-85
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-86
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-87
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-88
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-89
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-90
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-91
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-92
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-93
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-94
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-95
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-96
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-97
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-98
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-99
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-100
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-101
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-102
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-103
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-104
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-105
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-106
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-107
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown

GetNo.2-3-108
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-109
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown
GetNo.2-3-110
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    ${Result}    GetSubNodeValue    ${z}
    \    Should Be Equal    ${Result}    ${y}
    [Teardown]    DoTearDown


*** Test Cases ***
GetNo.2-3-111
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-111}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-111}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-111}
    [Teardown]    DoTearDown
GetNo.2-3-112
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path1}
    ${current_text}    GetSubNodeValue    ${Index1}
    Should Be Equal    ${current_text}    ${Value1}
GetNo.2-3-113
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path2}
    ${current_text}    GetSubNodeValue    ${Index2}
    Should Be Equal    ${current_text}    ${Value2}
GetNo.2-3-114
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path3}
    ${current_text}    GetSubNodeValue    ${Index3}
    Should Be Equal    ${current_text}    ${Value3}
    [Teardown]    DoTearDown
GetNo.2-3-115
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path4}
    ${current_text}    GetSubNodeValue    ${Index4}
    Should Be Equal    ${current_text}    ${Value4}
GetNo.2-3-116
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path5}
    ${current_text}    GetSubNodeValue    ${Index5}
    Should Be Equal    ${current_text}    ${Value5}
GetNo.2-3-117
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path6}
    ${current_text}    GetSubNodeValue    ${Index6}
    Should Be Equal    ${current_text}    ${Value6}
    [Teardown]    DoTearDown
GetNo.2-3-118
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path7}
    ${current_text}    GetSubNodeValue    ${Index7}
    Should Be Equal    ${current_text}    ${Value7}
GetNo.2-3-119
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path8}
    ${current_text}    GetSubNodeValue    ${Index8}
    Should Be Equal    ${current_text}    ${Value8}
GetNo.2-3-120
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path9}
    ${current_text}    GetSubNodeValue    ${Index9}
    Should Be Equal    ${current_text}    ${Value9}
    [Teardown]    DoTearDown
GetNo.2-3-121
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path10}
    ${current_text}    GetSubNodeValue    ${Index10}
    Should Be Equal    ${current_text}    ${Value10}
GetNo.2-3-122
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path11}
    ${current_text}    GetSubNodeValue    ${Index11}
    Should Be Equal    ${current_text}    ${Value11}
GetNo.2-3-123
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path12}
    ${current_text}    GetSubNodeValue    ${Index12}
    Should Be Equal    ${current_text}    ${Value12}
    [Teardown]    DoTearDown
GetNo.2-3-124
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path13}
    ${current_text}    GetSubNodeValue    ${Index13}
    Should Be Equal    ${current_text}    ${Value13}
GetNo.2-3-125
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path14}
    ${current_text}    GetSubNodeValue    ${Index14}
    Should Be Equal    ${current_text}    ${Value14}
GetNo.2-3-126
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path15}
    ${current_text}    GetSubNodeValue    ${Index15}
    Should Be Equal    ${current_text}    ${Value15}
    [Teardown]    DoTearDown

GetNo.2-3-142
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-142}
    ${current_text}    GetValueSingle
    Should Be Equal    ${current_text}    @{OptionValue_2-3-142}
    [Teardown]    DoTearDown
GetNo.2-3-137
    DoSetUp
    GoToPath    ${TestPath_2-3-137-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-137-1}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-137-1}
    DoTearDown
    DoSetUp
    GoToPath    ${TestPath_2-3-137-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-137-2}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-137-2}
    DoTearDown
    DoSetUp
    GoToPath    ${TestPath_2-3-137-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-137-3}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-137-3}
    [Teardown]    DoTearDown
GetNo.2-3-139
    DoSetUp
    GoToPath    ${TestPath_2-3-139-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-139-1}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-139-1}
    DoTearDown
    DoSetUp
    GoToPath    ${TestPath_2-3-139-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-139-2}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-139-2}
    [Teardown]    DoTearDown
GetNo.2-3-140
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-140}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-140}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-140}
    [Teardown]    DoTearDown
GetNo.2-3-143
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-143}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-143}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-143}
    [Teardown]    DoTearDown
GetNo.2-3-144
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-144}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-144}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-144}
    [Teardown]    DoTearDown
GetNo.2-3-145
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-145}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-145}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-145}
    [Teardown]    DoTearDown
GetNo.2-3-146
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-146}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-146}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-146}
    [Teardown]    DoTearDown
GetNo.2-3-147
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-147}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-147}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-147}
    [Teardown]    DoTearDown
