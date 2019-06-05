*** Settings ***  
Resource        ../Variables/ScanFileNameTable.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
OpenTable1
    [Setup]    DoSetUp
    GoToPath    ${ScanToUSBPath}
    GoTo	${Empty}	Table	0
SetNo.4-2-8
    SetByIdCol	2	2	@{OptionValue_4-2-8}
SetNo.4-2-9
    SetByIdCol	3	2	@{OptionValue_4-2-9}
SetNo.4-2-10
    SetByIdCol	4	2	@{OptionValue_4-2-10}
SetNo.4-2-11
    SetByIdCol	5	2	@{OptionValue_4-2-11}
TearDownTable1
    PushOK
    [Teardown]    DoTearDown

OpenTable2
    [Setup]    DoSetUp
    GoToPath    ${ScanToE-mailServerPath}
    GoTo	${Empty}	Table	0
SetNo.4-2-12
    SetByIdCol	2	2	@{OptionValue_4-2-12}
SetNo.4-2-13
    SetByIdCol	3	2	@{OptionValue_4-2-13}
SetNo.4-2-14
    SetByIdCol	4	2	@{OptionValue_4-2-14}
SetNo.4-2-15
    SetByIdCol	5	2	@{OptionValue_4-2-15}
SetNo.4-2-16
    SetByIdCol	6	2	@{OptionValue_4-2-16}
SetNo.4-2-17
    SetByIdCol	7	2	@{OptionValue_4-2-17}
SetNo.4-2-18
    SetByIdCol	8	2	@{OptionValue_4-2-18}
SetNo.4-2-19
    SetByIdCol	9	2	@{OptionValue_4-2-19}
SetNo.4-2-20
    SetByIdCol	10	2	@{OptionValue_4-2-20}
TearDownTable2
    PushOK
    [Teardown]    DoTearDown

OpenTable3
    [Setup]    DoSetUp
    GoToPath    ${ScanToFTP/SFTPPath}
    GoTo	${Empty}	Table	0
SetNo.4-2-21
    SetByIdCol	2	2	@{OptionValue_4-2-21}
SetNo.4-2-22
    SetByIdCol	3	2	@{OptionValue_4-2-22}
SetNo.4-2-23
    SetByIdCol	4	2	@{OptionValue_4-2-23}
SetNo.4-2-24
    SetByIdCol	5	2	@{OptionValue_4-2-24}
SetNo.4-2-25
    SetByIdCol	6	2	@{OptionValue_4-2-25}
SetNo.4-2-26
    SetByIdCol	7	2	@{OptionValue_4-2-26}
SetNo.4-2-27
    SetByIdCol	8	2	@{OptionValue_4-2-27}
SetNo.4-2-28
    SetByIdCol	9	2	@{OptionValue_4-2-28}
SetNo.4-2-29
    SetByIdCol	10	2	@{OptionValue_4-2-29}
SetNo.4-2-30
    SetByIdCol	11	2	@{OptionValue_4-2-30}
SetNo.4-2-31
    SetByIdCol	12	2	@{OptionValue_4-2-31} 
SetNo.4-2-32
    SetByIdCol	13	2	@{OptionValue_4-2-32}
SetNo.4-2-33
    SetByIdCol	14	2	@{OptionValue_4-2-33}
SetNo.4-2-34
    SetByIdCol	15	2	@{OptionValue_4-2-34}
TearDownTable3
    PushOK
    [Teardown]    DoTearDown


OpenTable4
    [Setup]    DoSetUp
    GoToPath    ${ScanToNetwork/SharePointPath}
    GoTo	${Empty}	Table	0
SetNo.4-2-35
    SetByIdCol	2	2	@{OptionValue_4-2-35}
SetNo.4-2-36
    SetByIdCol	3	2	@{OptionValue_4-2-36}
SetNo.4-2-37
    SetByIdCol	4	2	@{OptionValue_4-2-37}
SetNo.4-2-38
    SetByIdCol	5	2	@{OptionValue_4-2-38}
SetNo.4-2-39
    SetByIdCol	6	2	@{OptionValue_4-2-39}
SetNo.4-2-40
    SetByIdCol	7	2	@{OptionValue_4-2-40}
SetNo.4-2-41
    SetByIdCol	8	2	@{OptionValue_4-2-41}
SetNo.4-2-42
    SetByIdCol	9	2	@{OptionValue_4-2-42}
SetNo.4-2-43
    SetByIdCol	10	2	@{OptionValue_4-2-43}
SetNo.4-2-44
    SetByIdCol	11	2	@{OptionValue_4-2-44}
SetNo.4-2-45
    SetByIdCol	12	2	@{OptionValue_4-2-45}
SetNo.4-2-46
    SetByIdCol	13	2	@{OptionValue_4-2-46}
SetNo.4-2-47
    SetByIdCol	14	2	@{OptionValue_4-2-47}
SetNo.4-2-48
    SetByIdCol	15	2	@{OptionValue_4-2-48}
TearDownTable4
    PushOK
    [Teardown]    DoTearDown
