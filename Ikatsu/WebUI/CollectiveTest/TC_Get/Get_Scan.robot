*** Settings ***
Resource        ../Variables/Option.txt
Resource        ../Variables/ScanFileNameTable.txt
Resource        ../Variables/ScanProfileFTP.txt
Resource        ../Variables/ScanProfileSFTP.txt
Resource        ../Variables/ScanProfileNetwork.txt
Resource        ../Variables/ScanProfileHTTP(S).txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
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


*** Test Cases ***
OpenTable1
    [Setup]    DoSetUp
    GoToPath    ${ScanToUSBPath}
    GoTo    ${Empty}    Table   0
GetNo.4-2-8
    ${CurrentValue}    GetByIdCol   2   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-8}
GetNo.4-2-9
    ${CurrentValue}    GetByIdCol   3   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-9}
GetNo.4-2-10
    ${CurrentValue}    GetByIdCol   4   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-10}
GetNo.4-2-11
    ${CurrentValue}    GetByIdCol   5   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-11}
TearDownTable1
    Sleep    1
    [Teardown]    DoTearDown


OpenTable3
    [Setup]    DoSetUp
    GoToPath    ${ScanToFTP/SFTPPath}
    GoTo    ${Empty}    Table   0
GetNo.4-2-21
    ${CurrentValue}    GetByIdCol   2   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-21}
GetNo.4-2-22
    ${CurrentValue}    GetByIdCol   3   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-22}
GetNo.4-2-23
    ${CurrentValue}    GetByIdCol   4   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-23}
GetNo.4-2-24
    ${CurrentValue}    GetByIdCol   5   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-24}
GetNo.4-2-25
    ${CurrentValue}    GetByIdCol   6   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-25}
GetNo.4-2-26
    ${CurrentValue}    GetByIdCol   7   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-26}
GetNo.4-2-27
    ${CurrentValue}    GetByIdCol   8   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-27}
GetNo.4-2-28
    ${CurrentValue}    GetByIdCol   9   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-28}
GetNo.4-2-29
    ${CurrentValue}    GetByIdCol   10  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-29}
GetNo.4-2-30
    ${CurrentValue}    GetByIdCol   11  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-30}
GetNo.4-2-31
    ${CurrentValue}    GetByIdCol   12  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-31}
GetNo.4-2-32
    ${CurrentValue}    GetByIdCol   13  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-32}
GetNo.4-2-33
    ${CurrentValue}    GetByIdCol   14  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-33}
GetNo.4-2-34
    ${CurrentValue}    GetByIdCol   15  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-34}
TearDownTable3
    Sleep    1
    [Teardown]    DoTearDown

OpenTable4
    [Setup]    DoSetUp
    GoToPath    ${ScanToNetwork/SharePointPath}
    GoTo    ${Empty}    Table   0
GetNo.4-2-35
    ${CurrentValue}    GetByIdCol   2   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-35}
GetNo.4-2-36
    ${CurrentValue}    GetByIdCol   3   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-36}
GetNo.4-2-37
    ${CurrentValue}    GetByIdCol   4   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-37}
GetNo.4-2-38
    ${CurrentValue}    GetByIdCol   5   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-38}
GetNo.4-2-39
    ${CurrentValue}    GetByIdCol   6   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-39}
GetNo.4-2-40
    ${CurrentValue}    GetByIdCol   7   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-40}
GetNo.4-2-41
    ${CurrentValue}    GetByIdCol   8   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-41}
GetNo.4-2-42
    ${CurrentValue}    GetByIdCol   9   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-42}
GetNo.4-2-43
    ${CurrentValue}    GetByIdCol   10  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-43}
GetNo.4-2-44
    ${CurrentValue}    GetByIdCol   11  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-44}
GetNo.4-2-45
    ${CurrentValue}    GetByIdCol   12  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-45}
GetNo.4-2-46
    ${CurrentValue}    GetByIdCol   13  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-46}
GetNo.4-2-47
    ${CurrentValue}    GetByIdCol   14  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-47}
GetNo.4-2-48
    ${CurrentValue}    GetByIdCol   15  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-48}
TearDownTable4
    Sleep    1
    [Teardown]    DoTearDown

*** Test Cases ***
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
GetNo.4-4-13
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-13}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-4-13}
    Should Be Equal    ${current_text}    @{OptionValue_4-4-13}
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
GetNo.4-5-26
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-26}
    ${current_text}    GetSubNodeValue    @{OptionIndex_4-5-26}
    Should Be Equal    ${current_text}    @{OptionValue_4-5-26}
    [Teardown]    DoTearDown



