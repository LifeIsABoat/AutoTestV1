*** Settings ***
Resource        ../Variables/Option.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetNo.Condition1
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
