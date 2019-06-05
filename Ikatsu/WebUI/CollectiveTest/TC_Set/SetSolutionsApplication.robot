*** Settings ***  
Resource        ../Variables/SolutionsApplication.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetNo.2-3-50
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-51
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-52
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID1}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName1}    ${PathValue1}    ${PathIndex1}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-53
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-54
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-55
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID2}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName2}    ${PathValue2}    ${PathIndex2}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-56
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-57
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-58
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-59
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID3}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName3}    ${PathValue3}    ${PathIndex3}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-60
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-61
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-62
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-63
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID4}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName4}    ${PathValue4}    ${PathIndex4}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-64
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-65
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-66
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-67
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID5}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName5}    ${PathValue5}    ${PathIndex5}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-68
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-69
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-70
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-71
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID6}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName6}    ${PathValue6}    ${PathIndex6}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-72
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-73
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-74
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-75
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID7}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName7}    ${PathValue7}    ${PathIndex7}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-76
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-77
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-78
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-79
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID8}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName8}    ${PathValue8}    ${PathIndex8}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-80
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-81
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-82
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-83
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID9}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName9}    ${PathValue9}    ${PathIndex9}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-84
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-85
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-86
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-87
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID10}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName10}    ${PathValue10}    ${PathIndex10}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-88
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-89
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-90
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-91
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID11}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName11}    ${PathValue11}    ${PathIndex11}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-92
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-93
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-94
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-95
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID12}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName12}    ${PathValue12}    ${PathIndex12}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-96
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-97
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-98
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID13}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName13}    ${PathValue13}    ${PathIndex13}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-99
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-100
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-101
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID14}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName14}    ${PathValue14}    ${PathIndex14}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-102
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-103
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-104
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID15}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName15}    ${PathValue15}    ${PathIndex15}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-105
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-106
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-107
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID16}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName16}    ${PathValue16}    ${PathIndex16}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown

SetNo.2-3-108
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-109
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-110
    [Setup]    DoSetup
    GoToPath    ${PagePath}
    Goto    ${Empty}    Table    0
    SetByIdCol    ${TableID17}    1    Click   
    :FOR    ${x}    ${y}    ${z}    IN ZIP    ${PathName17}    ${PathValue17}    ${PathIndex17}
    \    GoToPageRoot
    \    GoToName   ${x}
    \    SetSubNodeValue    ${y}    ${z}
    PushOK
    [Teardown]    DoTearDown
