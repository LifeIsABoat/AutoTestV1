*** Settings ***
Resource        ../Variables/Option.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetNo.Condition2
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
