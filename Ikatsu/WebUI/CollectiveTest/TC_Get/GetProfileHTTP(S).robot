*** Settings ***  
Resource        ../Variables/ScanProfileHTTP(S).txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GetNo.4-10-18
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-19
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-20
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-21
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-22
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-23
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown


GetNo.4-10-24
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-25
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-26
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-27
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-28
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown

GetNo.4-10-29
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${httpHeaderPath}
    GoTo    ${Empty}    Table    0
    ${length}=    Get Length    ${Head}
    :FOR    ${idx}    IN RANGE    1    ${length}
    \    ${Result}    GetByName    ${Row1[0]}    ${Head[${idx}]}
    \    Should Be Equal    ${Result}    ${Row1[${idx}]}
    [Teardown]    DoTearDown


GetNo.4-10-30
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${BodyPath}
    ${Result}    GetSubNodeValue    ${BodyIndex}
    Should Be Equal    ${Result}    ${BodyValue}
    [Teardown]    DoTearDown
    
GetNo.4-10-31
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoTo    ${Empty}    Table    0
    SetByIdCol    ${ProfileName}   1    Click
    GoToName    ${AuthenticationSettingPath}
    GoToName    ${UsernamePath}
    ${Result}    GetSubNodeValue    ${UsernameIndex}
    Should Be Equal    ${Result}    ${UsernameValue}
    [Teardown]    DoTearDown
