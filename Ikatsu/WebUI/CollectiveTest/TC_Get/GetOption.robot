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
GetNo.1-2-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-2}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-2}
    [Teardown]    DoTearDown
GetNo.1-2-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-3}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-3}
    [Teardown]    DoTearDown
GetNo.1-2-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-4}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-4}
    [Teardown]    DoTearDown
GetNo.1-2-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-5}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-5}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-5}
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
GetNo.1-2-8
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-8}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-8}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-8}
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
GetNo.1-2-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-11}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-11}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-11}
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
GetNo.1-2-21
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-21}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-21}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-21}
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
GetNo.1-2-32
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-32}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-32}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-32}
    [Teardown]    DoTearDown
GetNo.1-2-33
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-33}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-33}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-33}
    [Teardown]    DoTearDown
GetNo.1-2-34
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-34}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-34}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-34}
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
GetNo.1-2-37.1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-37.1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-37.1}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-37.1}
    [Teardown]    DoTearDown
GetNo.1-2-37.2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-37.2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-37.2}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-37.2}
    [Teardown]    DoTearDown
GetNo.1-2-39
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-39}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-39}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-39}
    [Teardown]    DoTearDown
GetNo.1-2-42
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-42}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-42}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-42}
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
GetNo.1-2-56
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-56}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-56}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-56}
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
GetNo.1-2-71
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-71}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-71}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-71}
    [Teardown]    DoTearDown
GetNo.1-2-72
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-72}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-72}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-72}
    [Teardown]    DoTearDown
GetNo.1-2-73
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-73}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-73}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-73}
    [Teardown]    DoTearDown
GetNo.1-2-74
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-74}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-74}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-74}
    [Teardown]    DoTearDown
GetNo.1-2-75
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-75}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-75}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-75}
    [Teardown]    DoTearDown
GetNo.1-2-76
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-76}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-76}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-76}
    [Teardown]    DoTearDown
GetNo.1-2-77
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-77}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-77}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-77}
    [Teardown]    DoTearDown
GetNo.1-2-78
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-78}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-78}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-78}
    [Teardown]    DoTearDown
GetNo.1-2-79
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-2-79}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-2-79}
    Should Be Equal    ${current_text}    @{OptionValue_1-2-79}
    [Teardown]    DoTearDown
GetNo.1-3-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-3-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-3-1}
    Should Be Equal    ${current_text}    @{OptionValue_1-3-1}
    [Teardown]    DoTearDown
GetNo.1-3-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-3-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-3-3}
    Should Be Equal    ${current_text}    @{OptionValue_1-3-3}
    [Teardown]    DoTearDown
GetNo.1-3-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-3-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-3-4}
    Should Be Equal    ${current_text}    @{OptionValue_1-3-4}
    [Teardown]    DoTearDown
GetNo.1-3-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-3-9}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-3-9}
    Should Be Equal    ${current_text}    @{OptionValue_1-3-9}
    [Teardown]    DoTearDown
GetNo.1-3-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-3-10}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-3-10}
    Should Be Equal    ${current_text}    @{OptionValue_1-3-10}
    [Teardown]    DoTearDown
GetNo.1-3-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-3-11}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-3-11}
    Should Be Equal    ${current_text}    @{OptionValue_1-3-11}
    [Teardown]    DoTearDown
GetNo.1-4-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-1}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-1}
    [Teardown]    DoTearDown
GetNo.1-4-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-2}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-2}
    [Teardown]    DoTearDown
GetNo.1-4-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-4}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-4}
    [Teardown]    DoTearDown
GetNo.1-4-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-6}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-6}
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
GetNo.1-4-27
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-27}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-27}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-27}
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
GetNo.1-4-31
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-31}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-31}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-31}
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
GetNo.1-4-43
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-43}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-43}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-43}
    [Teardown]    DoTearDown
GetNo.1-4-44
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-44}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-44}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-44}
    [Teardown]    DoTearDown
GetNo.1-4-45
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-45}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-45}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-45}
    [Teardown]    DoTearDown
GetNo.1-4-46
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-46}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-46}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-46}
    [Teardown]    DoTearDown
GetNo.1-4-47
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-47}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-47}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-47}
    [Teardown]    DoTearDown
GetNo.1-4-48
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-48}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-48}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-48}
    [Teardown]    DoTearDown
GetNo.1-4-49
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-49}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-49}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-49}
    [Teardown]    DoTearDown
GetNo.1-4-50
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-50}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-50}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-50}
    [Teardown]    DoTearDown
GetNo.1-4-51
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-51}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-51}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-51}
    [Teardown]    DoTearDown
GetNo.1-4-52
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-52}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-52}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-52}
    [Teardown]    DoTearDown
GetNo.1-4-53
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-53}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-53}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-53}
    [Teardown]    DoTearDown
GetNo.1-4-54
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-54}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-54}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-54}
    [Teardown]    DoTearDown
GetNo.1-4-55
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-55}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-55}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-55}
    [Teardown]    DoTearDown
GetNo.1-4-56
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-56}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-56}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-56}
    [Teardown]    DoTearDown
GetNo.1-4-57
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-57}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-57}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-57}
    [Teardown]    DoTearDown
GetNo.1-4-58
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-58}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-58}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-58}
    [Teardown]    DoTearDown
GetNo.1-4-59
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-59}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-59}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-59}
    [Teardown]    DoTearDown
GetNo.1-4-60
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-60}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-60}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-60}
    [Teardown]    DoTearDown
GetNo.1-4-61
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-61}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-61}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-61}
    [Teardown]    DoTearDown
GetNo.1-4-62
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-62}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-62}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-62}
    [Teardown]    DoTearDown
GetNo.1-4-63
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-63}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-63}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-63}
    [Teardown]    DoTearDown
GetNo.1-4-69
    [Setup]    DoSetUp
    GoToPath    ${TestPath_1-4-69}
    ${current_text}    GetSubNodeValue    @{OptionIndex_1-4-69}
    Should Be Equal    ${current_text}    @{OptionValue_1-4-69}
    [Teardown]    DoTearDown
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
SetNo.Condition3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_Condition3}
    SetSubNodeValue    @{OptionValue_Condition3}    @{OptionIndex_Condition3}
    PushOK
    [Teardown]    DoTearDown
GetNo.2-3-22
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-22}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-22}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-22}
    [Teardown]    DoTearDown
GetNo.2-3-24
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-24}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-24}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-24}
    [Teardown]    DoTearDown
GetNo.2-3-25
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-25}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-25}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-25}
    [Teardown]    DoTearDown
GetNo.2-3-26
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-26}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-26}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-26}
    [Teardown]    DoTearDown
GetNo.2-3-27
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-27}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-27}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-27}
    [Teardown]    DoTearDown
GetNo.2-3-28
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-28}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-28}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-28}
    [Teardown]    DoTearDown
GetNo.2-3-29
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-29}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-29}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-29}
    [Teardown]    DoTearDown
GetNo.2-3-30
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-30}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-30}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-30}
    [Teardown]    DoTearDown
GetNo.2-3-31
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-31}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-31}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-31}
    [Teardown]    DoTearDown
GetNo.2-3-32
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-32}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-32}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-32}
    [Teardown]    DoTearDown
GetNo.2-3-33
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-33}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-33}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-33}
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
GetNo.2-3-111
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-111}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-111}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-111}
    [Teardown]    DoTearDown
GetNo.2-3-112
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-112}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-112}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-112}
    [Teardown]    DoTearDown
GetNo.2-3-113
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-113}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-113}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-113}
    [Teardown]    DoTearDown
GetNo.2-3-114
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-114}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-114}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-114}
    [Teardown]    DoTearDown
GetNo.2-3-115
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-115}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-115}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-115}
    [Teardown]    DoTearDown
GetNo.2-3-116
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-116}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-116}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-116}
    [Teardown]    DoTearDown
GetNo.2-3-117
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-117}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-117}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-117}
    [Teardown]    DoTearDown
GetNo.2-3-118
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-118}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-118}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-118}
    [Teardown]    DoTearDown
GetNo.2-3-119
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-119}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-119}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-119}
    [Teardown]    DoTearDown
GetNo.2-3-120
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-120}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-120}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-120}
    [Teardown]    DoTearDown
GetNo.2-3-121
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-121}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-121}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-121}
    [Teardown]    DoTearDown
GetNo.2-3-122
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-122}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-122}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-122}
    [Teardown]    DoTearDown
GetNo.2-3-123
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-123}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-123}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-123}
    [Teardown]    DoTearDown
GetNo.2-3-124
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-124}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-124}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-124}
    [Teardown]    DoTearDown
GetNo.2-3-125
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-125}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-125}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-125}
    [Teardown]    DoTearDown
GetNo.2-3-126
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-126}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-126}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-126}
    [Teardown]    DoTearDown
GetNo.2-3-127
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-127}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-127}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-127}
    [Teardown]    DoTearDown
GetNo.2-3-128
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-128}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-128}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-128}
    [Teardown]    DoTearDown
GetNo.2-3-129
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-129}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-129}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-129}
    [Teardown]    DoTearDown
GetNo.2-3-130
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-130}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-130}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-130}
    [Teardown]    DoTearDown
GetNo.2-3-131
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-131}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-131}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-131}
    [Teardown]    DoTearDown
GetNo.2-3-132
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-132}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-132}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-132}
    [Teardown]    DoTearDown
GetNo.2-3-134
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-134}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-134}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-134}
    [Teardown]    DoTearDown
GetNo.2-3-135
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-135}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-135}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-135}
    [Teardown]    DoTearDown
GetNo.2-3-142
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-142}
    ${current_text}    GetValueSingle
    Should Be Equal    ${current_text}    @{OptionValue_2-3-142}
    [Teardown]    DoTearDown
GetNo.2-3-137-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-137-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-137-1}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-137-1}
    [Teardown]    DoTearDown
GetNo.2-3-137-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-137-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-137-2}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-137-2}
    [Teardown]    DoTearDown
GetNo.2-3-137-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-137-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-137-3}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-137-3}
    [Teardown]    DoTearDown
GetNo.2-3-139-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-139-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_2-3-139-1}
    Should Be Equal    ${current_text}    @{OptionValue_2-3-139-1}
    [Teardown]    DoTearDown
GetNo.2-3-139-2
    [Setup]    DoSetUp
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
GetNo.4-1-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-1-1}
    Should Be Equal    ${current_text}    @{OptionValue_4-1-1}
    [Teardown]    DoTearDown
GetNo.4-1-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-1-2}
    Should Be Equal    ${current_text}    @{OptionValue_4-1-2}
    [Teardown]    DoTearDown
GetNo.4-1-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-1-3}
    Should Be Equal    ${current_text}    @{OptionValue_4-1-3}
    [Teardown]    DoTearDown
GetNo.4-1-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-1-4}
    Should Be Equal    ${current_text}    @{OptionValue_4-1-4}
    [Teardown]    DoTearDown
GetNo.4-1-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-5}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-1-5}
    Should Be Equal    ${current_text}    @{OptionValue_4-1-5}
    [Teardown]    DoTearDown
GetNo.4-1-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-1-6}
    Should Be Equal    ${current_text}    @{OptionValue_4-1-6}
    [Teardown]    DoTearDown
GetNo.4-1-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-7}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-1-7}
    Should Be Equal    ${current_text}    @{OptionValue_4-1-7}
    [Teardown]    DoTearDown
GetNo.4-2-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-2-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-2-1}
    Should Be Equal    ${current_text}    @{OptionValue_4-2-1}
    [Teardown]    DoTearDown
GetNo.4-2-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-2-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-2-3}
    Should Be Equal    ${current_text}    @{OptionValue_4-2-3}
    [Teardown]    DoTearDown
GetNo.4-2-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-2-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-2-4}
    Should Be Equal    ${current_text}    @{OptionValue_4-2-4}
    [Teardown]    DoTearDown
GetNo.4-3-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-1}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-1}
    [Teardown]    DoTearDown
GetNo.4-3-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-2}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-2}
    [Teardown]    DoTearDown
GetNo.4-3-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-3}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-3}
    [Teardown]    DoTearDown
GetNo.4-3-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-4}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-4}
    [Teardown]    DoTearDown
GetNo.4-3-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-5}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-5}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-5}
    [Teardown]    DoTearDown
GetNo.4-3-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-6}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-6}
    [Teardown]    DoTearDown
GetNo.4-3-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-7}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-7}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-7}
    [Teardown]    DoTearDown
GetNo.4-3-8
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-8}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-8}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-8}
    [Teardown]    DoTearDown
GetNo.4-3-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-9}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-9}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-9}
    [Teardown]    DoTearDown
GetNo.4-3-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-10}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-10}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-10}
    [Teardown]    DoTearDown
GetNo.4-3-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-11}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-11}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-11}
    [Teardown]    DoTearDown
GetNo.4-3-12
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-12}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-3-12}
    Should Be Equal    ${current_text}    @{OptionValue_4-3-12}
    [Teardown]    DoTearDown
GetNo.4-4-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-1}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-1}
    [Teardown]    DoTearDown
GetNo.4-4-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-2}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-2}
    [Teardown]    DoTearDown
GetNo.4-4-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-3}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-3}
    [Teardown]    DoTearDown
GetNo.4-4-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-4}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-4}
    [Teardown]    DoTearDown
GetNo.4-4-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-5}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-5}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-5}
    [Teardown]    DoTearDown
GetNo.4-4-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-6}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-6}
    [Teardown]    DoTearDown
GetNo.4-4-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-7}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-7}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-7}
    [Teardown]    DoTearDown
GetNo.4-4-8
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-8}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-8}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-8}
    [Teardown]    DoTearDown
GetNo.4-4-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-9}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-9}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-9}
    [Teardown]    DoTearDown
GetNo.4-4-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-10}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-10}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-10}
    [Teardown]    DoTearDown
GetNo.4-4-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-11}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-11}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-11}
    [Teardown]    DoTearDown
GetNo.4-4-12
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-12}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-12}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-12}
    [Teardown]    DoTearDown
GetNo.4-5-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-1}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-1}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-1}
    [Teardown]    DoTearDown
GetNo.4-5-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-2}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-2}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-2}
    [Teardown]    DoTearDown
GetNo.4-5-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-3}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-3}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-3}
    [Teardown]    DoTearDown
GetNo.4-5-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-4}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-4}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-4}
    [Teardown]    DoTearDown
GetNo.4-5-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-5}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-5}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-5}
    [Teardown]    DoTearDown
GetNo.4-5-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-6}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-6}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-6}
    [Teardown]    DoTearDown
GetNo.4-5-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-7}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-7}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-7}
    [Teardown]    DoTearDown
GetNo.4-5-8
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-8}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-8}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-8}
    [Teardown]    DoTearDown
GetNo.4-5-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-9}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-9}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-9}
    [Teardown]    DoTearDown
GetNo.4-5-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-10}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-10}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-10}
    [Teardown]    DoTearDown
GetNo.4-5-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-11}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-11}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-11}
    [Teardown]    DoTearDown
GetNo.4-5-12
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-12}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-12}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-12}
    [Teardown]    DoTearDown
GetNo.4-5-13
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-13}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-13}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-13}
    [Teardown]    DoTearDown
GetNo.4-5-14
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-14}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-14}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-14}
    [Teardown]    DoTearDown
GetNo.4-5-15
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-15}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-15}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-15}
    [Teardown]    DoTearDown
GetNo.4-5-16
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-16}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-16}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-16}
    [Teardown]    DoTearDown
GetNo.4-5-17
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-17}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-17}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-17}
    [Teardown]    DoTearDown
GetNo.4-5-18
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-18}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-18}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-18}
    [Teardown]    DoTearDown
GetNo.4-5-19
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-19}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-19}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-19}
    [Teardown]    DoTearDown
GetNo.4-5-20
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-20}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-20}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-20}
    [Teardown]    DoTearDown
GetNo.4-5-21
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-21}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-21}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-21}
    [Teardown]    DoTearDown
GetNo.4-5-22
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-22}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-22}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-22}
    [Teardown]    DoTearDown
GetNo.4-5-23
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-23}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-23}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-23}
    [Teardown]    DoTearDown
GetNo.4-5-24
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-24}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-24}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-24}
    [Teardown]    DoTearDown
GetNo.4-5-25
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-25}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-25}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-25}
    [Teardown]    DoTearDown
GetNo.4-5-26
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-26}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-26}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-26}
    [Teardown]    DoTearDown
