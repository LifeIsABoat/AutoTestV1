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
