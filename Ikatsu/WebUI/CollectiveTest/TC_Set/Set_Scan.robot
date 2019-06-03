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
SetNo.4-1-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-1}
    SetSubNodeValue    @{OptionValue_4-1-1}    @{OptionIndex_4-1-1}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-1-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-2}
    SetSubNodeValue    @{OptionValue_4-1-2}    @{OptionIndex_4-1-2}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-1-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-3}
    SetSubNodeValue    @{OptionValue_4-1-3}    @{OptionIndex_4-1-3}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-1-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-4}
    SetSubNodeValue    @{OptionValue_4-1-4}    @{OptionIndex_4-1-4}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-1-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-5}
    SetSubNodeValue    @{OptionValue_4-1-5}    @{OptionIndex_4-1-5}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-1-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-6}
    SetSubNodeValue    @{OptionValue_4-1-6}    @{OptionIndex_4-1-6}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-1-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-1-7}
    SetSubNodeValue    @{OptionValue_4-1-7}    @{OptionIndex_4-1-7}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-2-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-2-1}
    SetSubNodeValue    @{OptionValue_4-2-1}    @{OptionIndex_4-2-1}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-2-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-2-3}
    SetSubNodeValue    @{OptionValue_4-2-3}    @{OptionIndex_4-2-3}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-2-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-2-4}
    SetSubNodeValue    @{OptionValue_4-2-4}    @{OptionIndex_4-2-4}
    PushOK
    [Teardown]    DoTearDown


*** Test Cases ***
OpenTable1
    [Setup]    DoSetUp
    GoToPath    ${ScanToUSBPath}
    GoTo    ${Empty}    Table   0
SetNo.4-2-8
    SetByIdCol  2   2   @{OptionValue_4-2-8}
SetNo.4-2-9
    SetByIdCol  3   2   @{OptionValue_4-2-9}
SetNo.4-2-10
    SetByIdCol  4   2   @{OptionValue_4-2-10}
SetNo.4-2-11
    SetByIdCol  5   2   @{OptionValue_4-2-11}
TearDownTable1
    PushOK
    [Teardown]    DoTearDown


OpenTable3
    [Setup]    DoSetUp
    GoToPath    ${ScanToFTP/SFTPPath}
    GoTo    ${Empty}    Table   0
SetNo.4-2-21
    SetByIdCol  2   2   @{OptionValue_4-2-21}
SetNo.4-2-22
    SetByIdCol  3   2   @{OptionValue_4-2-22}
SetNo.4-2-23
    SetByIdCol  4   2   @{OptionValue_4-2-23}
SetNo.4-2-24
    SetByIdCol  5   2   @{OptionValue_4-2-24}
SetNo.4-2-25
    SetByIdCol  6   2   @{OptionValue_4-2-25}
SetNo.4-2-26
    SetByIdCol  7   2   @{OptionValue_4-2-26}
SetNo.4-2-27
    SetByIdCol  8   2   @{OptionValue_4-2-27}
SetNo.4-2-28
    SetByIdCol  9   2   @{OptionValue_4-2-28}
SetNo.4-2-29
    SetByIdCol  10  2   @{OptionValue_4-2-29}
SetNo.4-2-30
    SetByIdCol  11  2   @{OptionValue_4-2-30}
SetNo.4-2-31
    SetByIdCol  12  2   @{OptionValue_4-2-31} 
SetNo.4-2-32
    SetByIdCol  13  2   @{OptionValue_4-2-32}
SetNo.4-2-33
    SetByIdCol  14  2   @{OptionValue_4-2-33}
SetNo.4-2-34
    SetByIdCol  15  2   @{OptionValue_4-2-34}
TearDownTable3
    PushOK
    [Teardown]    DoTearDown


OpenTable4
    [Setup]    DoSetUp
    GoToPath    ${ScanToNetwork/SharePointPath}
    GoTo    ${Empty}    Table   0
SetNo.4-2-35
    SetByIdCol  2   2   @{OptionValue_4-2-35}
SetNo.4-2-36
    SetByIdCol  3   2   @{OptionValue_4-2-36}
SetNo.4-2-37
    SetByIdCol  4   2   @{OptionValue_4-2-37}
SetNo.4-2-38
    SetByIdCol  5   2   @{OptionValue_4-2-38}
SetNo.4-2-39
    SetByIdCol  6   2   @{OptionValue_4-2-39}
SetNo.4-2-40
    SetByIdCol  7   2   @{OptionValue_4-2-40}
SetNo.4-2-41
    SetByIdCol  8   2   @{OptionValue_4-2-41}
SetNo.4-2-42
    SetByIdCol  9   2   @{OptionValue_4-2-42}
SetNo.4-2-43
    SetByIdCol  10  2   @{OptionValue_4-2-43}
SetNo.4-2-44
    SetByIdCol  11  2   @{OptionValue_4-2-44}
SetNo.4-2-45
    SetByIdCol  12  2   @{OptionValue_4-2-45}
SetNo.4-2-46
    SetByIdCol  13  2   @{OptionValue_4-2-46}
SetNo.4-2-47
    SetByIdCol  14  2   @{OptionValue_4-2-47}
SetNo.4-2-48
    SetByIdCol  15  2   @{OptionValue_4-2-48}
TearDownTable4
    PushOK
    [Teardown]    DoTearDown


*** Test Cases ***
SetNo.4-3-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-1}
    SetSubNodeValue    @{OptionValue_4-3-1}    @{OptionIndex_4-3-1}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-2}
    SetSubNodeValue    @{OptionValue_4-3-2}    @{OptionIndex_4-3-2}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-3}
    SetSubNodeValue    @{OptionValue_4-3-3}    @{OptionIndex_4-3-3}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-4}
    SetSubNodeValue    @{OptionValue_4-3-4}    @{OptionIndex_4-3-4}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-6
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-6}
    SetSubNodeValue    @{OptionValue_4-3-6}    @{OptionIndex_4-3-6}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-7
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-7}
    SetSubNodeValue    @{OptionValue_4-3-7}    @{OptionIndex_4-3-7}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-8
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-8}
    SetSubNodeValue    @{OptionValue_4-3-8}    @{OptionIndex_4-3-8}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-9}
    SetSubNodeValue    @{OptionValue_4-3-9}    @{OptionIndex_4-3-9}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-10}
    SetSubNodeValue    @{OptionValue_4-3-10}    @{OptionIndex_4-3-10}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-11}
    SetSubNodeValue    @{OptionValue_4-3-11}    @{OptionIndex_4-3-11}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-3-12
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-3-12}
    SetSubNodeValue    @{OptionValue_4-3-12}    @{OptionIndex_4-3-12}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-4-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-2}
    SetSubNodeValue    @{OptionValue_4-4-2}    @{OptionIndex_4-4-2}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-4-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-3}
    SetSubNodeValue    @{OptionValue_4-4-3}    @{OptionIndex_4-4-3}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-4-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-4}
    SetSubNodeValue    @{OptionValue_4-4-4}    @{OptionIndex_4-4-4}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-4-9
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-9}
    SetSubNodeValue    @{OptionValue_4-4-9}    @{OptionIndex_4-4-9}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-4-10
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-10}
    SetSubNodeValue    @{OptionValue_4-4-10}    @{OptionIndex_4-4-10}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-4-11
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-11}
    SetSubNodeValue    @{OptionValue_4-4-11}    @{OptionIndex_4-4-11}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-4-13
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-4-13}
    SetSubNodeValue    @{OptionValue_4-4-13}    @{OptionIndex_4-4-13}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-5-1
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-1}
    SetSubNodeValue    @{OptionValue_4-5-1}    @{OptionIndex_4-5-1}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-5-2
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-2}
    SetSubNodeValue    @{OptionValue_4-5-2}    @{OptionIndex_4-5-2}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-5-3
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-3}
    SetSubNodeValue    @{OptionValue_4-5-3}    @{OptionIndex_4-5-3}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-5-4
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-4}
    SetSubNodeValue    @{OptionValue_4-5-4}    @{OptionIndex_4-5-4}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-5-5
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-5}
    SetSubNodeValue    @{OptionValue_4-5-5}    @{OptionIndex_4-5-5}
    PushOK
    [Teardown]    DoTearDown
SetNo.4-5-26
    [Setup]    DoSetUp
    GoToPath    ${TestPath_4-5-26}
    SetSubNodeValue    @{OptionValue_4-5-26}    @{OptionIndex_4-5-26}
    PushOK
    [Teardown]    DoTearDown



