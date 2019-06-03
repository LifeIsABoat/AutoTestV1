*** Settings ***
Resource        ../Variables/Option.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GetNo.1-1-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-2}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-2}
    [Teardown]    DoTearDown
GetNo.1-1-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-3}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-3}
    [Teardown]    DoTearDown
GetNo.1-1-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-4}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-4}
    [Teardown]    DoTearDown
GetNo.1-1-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-5}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-5}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-5}
    [Teardown]    DoTearDown
GetNo.1-1-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-6}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-6}
    [Teardown]    DoTearDown
GetNo.1-1-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-7}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-7}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-7}
    [Teardown]    DoTearDown
GetNo.1-1-8
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-8}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-8}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-8}
    [Teardown]    DoTearDown
GetNo.1-1-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-9}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-9}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-9}
    [Teardown]    DoTearDown
GetNo.1-1-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-10}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-10}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-10}
    [Teardown]    DoTearDown
GetNo.1-1-15
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-15}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-15}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-15}
    [Teardown]    DoTearDown
GetNo.1-1-16
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-16}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-16}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-16}
    [Teardown]    DoTearDown
GetNo.1-1-17
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-1-17}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-1-17}
    Should Be Equal    ${current_text}    @{OptionValue_1-1-17}
    [Teardown]    DoTearDown
GetNo.1-2-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-1}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-1}
    [Teardown]    DoTearDown
GetNo.1-2-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-3}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-3}
    [Teardown]    DoTearDown
GetNo.1-2-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-6}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-6}
    [Teardown]    DoTearDown
GetNo.1-2-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-7}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-7}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-7}
    [Teardown]    DoTearDown
GetNo.1-2-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-9}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-9}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-9}
    [Teardown]    DoTearDown
GetNo.1-2-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-10}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-10}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-10}
    [Teardown]    DoTearDown
GetNo.1-2-12
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-12}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-12}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-12}
    [Teardown]    DoTearDown
GetNo.1-2-13
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-13}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-13}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-13}
    [Teardown]    DoTearDown
GetNo.1-2-14
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-14}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-14}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-14}
    [Teardown]    DoTearDown
GetNo.1-2-16
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-16}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-16}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-16}
    [Teardown]    DoTearDown
GetNo.1-2-17
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-17}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-17}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-17}
    [Teardown]    DoTearDown
GetNo.1-2-18
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-18}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-18}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-18}
    [Teardown]    DoTearDown
GetNo.1-2-19
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-19}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-19}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-19}
    [Teardown]    DoTearDown
GetNo.1-2-20
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-20}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-20}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-20}
    [Teardown]    DoTearDown
GetNo.1-2-22
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-22}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-22}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-22}
    [Teardown]    DoTearDown
GetNo.1-2-24
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-24}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-24}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-24}
    [Teardown]    DoTearDown
GetNo.1-2-25
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-25}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-25}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-25}
    [Teardown]    DoTearDown
GetNo.1-2-81
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-81}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-81}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-81}
    [Teardown]    DoTearDown
GetNo.1-2-26
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-26}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-26}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-26}
    [Teardown]    DoTearDown
GetNo.1-2-27
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-27}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-27}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-27}
    [Teardown]    DoTearDown
GetNo.1-2-28
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-28}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-28}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-28}
    [Teardown]    DoTearDown
GetNo.1-2-33
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-33}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-33}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-33}
    [Teardown]    DoTearDown
GetNo.1-2-35
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-35}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-35}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-35}
    [Teardown]    DoTearDown
GetNo.1-2-36
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-36}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-36}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-36}
    [Teardown]    DoTearDown
GetNo.1-2-37
    DoSetUp
    GoToPath    ${TestPath_1-2-37-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-37-1}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-37-1}
    DoTearDown
    DoSetUp
    GoToPath    ${TestPath_1-2-37-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-37-2}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-37-2}
    [Teardown]    DoTearDown
GetNo.1-2-39
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-39}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-39}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-39}
    [Teardown]    DoTearDown
GetNo.1-2-43
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-43}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-43}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-43}
    [Teardown]    DoTearDown
GetNo.1-2-46
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-46}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-46}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-46}
    [Teardown]    DoTearDown
GetNo.1-2-47
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-47}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-47}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-47}
    [Teardown]    DoTearDown
GetNo.1-2-49
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-49}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-49}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-49}
    [Teardown]    DoTearDown
GetNo.1-2-50
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-50}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-50}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-50}
    [Teardown]    DoTearDown
GetNo.1-2-51
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-51}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-51}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-51}
    [Teardown]    DoTearDown
GetNo.1-2-52
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-52}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-52}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-52}
    [Teardown]    DoTearDown
GetNo.1-2-55
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-55}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-55}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-55}
    [Teardown]    DoTearDown
GetNo.1-2-57
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-57}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-57}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-57}
    [Teardown]    DoTearDown
GetNo.1-2-58
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-58}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-58}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-58}
    [Teardown]    DoTearDown
GetNo.1-2-59
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-59}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-59}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-59}
    [Teardown]    DoTearDown
GetNo.1-2-60
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-60}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-60}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-60}
    [Teardown]    DoTearDown
GetNo.1-2-61
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-61}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-61}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-61}
    [Teardown]    DoTearDown
GetNo.1-2-62
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-62}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-62}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-62}
    [Teardown]    DoTearDown
GetNo.1-2-63
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-63}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-63}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-63}
    [Teardown]    DoTearDown
GetNo.1-2-64
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-64}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-64}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-64}
    [Teardown]    DoTearDown
GetNo.1-2-65
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-65}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-65}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-65}
    [Teardown]    DoTearDown
GetNo.1-2-66
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-66}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-66}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-66}
    [Teardown]    DoTearDown
GetNo.1-2-67
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-67}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-67}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-67}
    [Teardown]    DoTearDown
GetNo.1-2-68
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-68}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-68}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-68}
    [Teardown]    DoTearDown
GetNo.1-2-69
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-69}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-69}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-69}
    [Teardown]    DoTearDown
GetNo.1-2-70
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-70}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-70}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-70}
    [Teardown]    DoTearDown
GetNo.1-2-76
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-76}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-76}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-76}
    [Teardown]    DoTearDown
GetNo.1-4-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-2}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-2}
    [Teardown]    DoTearDown
GetNo.1-4-14
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-14}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-14}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-14}
    [Teardown]    DoTearDown
GetNo.1-4-15
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-15}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-15}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-15}
    [Teardown]    DoTearDown
GetNo.1-4-20
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-20}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-20}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-20}
    [Teardown]    DoTearDown
GetNo.1-4-21
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-21}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-21}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-21}
    [Teardown]    DoTearDown
GetNo.1-4-22
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-22}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-22}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-22}
    [Teardown]    DoTearDown
GetNo.1-4-26
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-26}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-26}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-26}
    [Teardown]    DoTearDown
GetNo.1-4-28
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-28}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-28}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-28}
    [Teardown]    DoTearDown
GetNo.1-4-29
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-29}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-29}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-29}
    [Teardown]    DoTearDown
GetNo.1-4-30
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-30}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-30}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-30}
    [Teardown]    DoTearDown
GetNo.1-4-132
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-132}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-132}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-132}
    [Teardown]    DoTearDown
GetNo.1-4-133
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-133}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-133}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-133}
    [Teardown]    DoTearDown
GetNo.1-4-135
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-135}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-135}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-135}
    [Teardown]    DoTearDown
GetNo.1-4-137
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-137}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-137}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-137}
    [Teardown]    DoTearDown
GetNo.1-4-139
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-139}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-139}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-139}
    [Teardown]    DoTearDown
GetNo.1-4-42
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-42}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-42}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-42}
    [Teardown]    DoTearDown
