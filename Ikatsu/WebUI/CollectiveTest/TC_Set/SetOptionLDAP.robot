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
