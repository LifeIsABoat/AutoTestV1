*** Settings ***  
Resource        ../Variables/ScanFileNameTable.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
OpenTable1
    [Setup]    DoSetUp
    GoToPath    ${ScanToUSBPath}
    GoTo	${Empty}	Table	0
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

OpenTable2
    [Setup]    DoSetUp
    GoToPath    ${ScanToE-mailServerPath}
    GoTo    ${Empty}    Table    0
GetNo.4-2-12
    ${CurrentValue}    GetByIdCol   2   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-12}
GetNo.4-2-13
    ${CurrentValue}    GetByIdCol   3   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-13}
GetNo.4-2-14
    ${CurrentValue}    GetByIdCol   4   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-14}
GetNo.4-2-15
    ${CurrentValue}    GetByIdCol   5   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-15}
GetNo.4-2-16
    ${CurrentValue}    GetByIdCol   6   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-16}
GetNo.4-2-17
    ${CurrentValue}    GetByIdCol   7   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-17}
GetNo.4-2-18
    ${CurrentValue}    GetByIdCol   8   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-18}
GetNo.4-2-19
    ${CurrentValue}    GetByIdCol   9   2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-19}
GetNo.4-2-20
    ${CurrentValue}    GetByIdCol   10  2
    Should Be Equal    ${CurrentValue}    @{OptionValue_4-2-20}
TearDownTable2
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
