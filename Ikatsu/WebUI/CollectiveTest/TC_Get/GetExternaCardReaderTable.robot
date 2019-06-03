*** Settings ***
Resource        ../Variables/OptionExternaCardReader.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
GetNo.2-3-112
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path1}
    ${current_text}    GetSubNodeValue    ${Index1}
    Should Be Equal    ${current_text}    ${Value1}
GetNo.2-3-113
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path2}
    ${current_text}    GetSubNodeValue    ${Index2}
    Should Be Equal    ${current_text}    ${Value2}
GetNo.2-3-114
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path3}
    ${current_text}    GetSubNodeValue    ${Index3}
    Should Be Equal    ${current_text}    ${Value3}
    [Teardown]    DoTearDown
GetNo.2-3-115
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path4}
    ${current_text}    GetSubNodeValue    ${Index4}
    Should Be Equal    ${current_text}    ${Value4}
GetNo.2-3-116
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path5}
    ${current_text}    GetSubNodeValue    ${Index5}
    Should Be Equal    ${current_text}    ${Value5}
GetNo.2-3-117
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path6}
    ${current_text}    GetSubNodeValue    ${Index6}
    Should Be Equal    ${current_text}    ${Value6}
    [Teardown]    DoTearDown
GetNo.2-3-118
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path7}
    ${current_text}    GetSubNodeValue    ${Index7}
    Should Be Equal    ${current_text}    ${Value7}
GetNo.2-3-119
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path8}
    ${current_text}    GetSubNodeValue    ${Index8}
    Should Be Equal    ${current_text}    ${Value8}
GetNo.2-3-120
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path9}
    ${current_text}    GetSubNodeValue    ${Index9}
    Should Be Equal    ${current_text}    ${Value9}
    [Teardown]    DoTearDown
GetNo.2-3-121
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path10}
    ${current_text}    GetSubNodeValue    ${Index10}
    Should Be Equal    ${current_text}    ${Value10}
GetNo.2-3-122
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path11}
    ${current_text}    GetSubNodeValue    ${Index11}
    Should Be Equal    ${current_text}    ${Value11}
GetNo.2-3-123
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path12}
    ${current_text}    GetSubNodeValue    ${Index12}
    Should Be Equal    ${current_text}    ${Value12}
    [Teardown]    DoTearDown
GetNo.2-3-124
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path13}
    ${current_text}    GetSubNodeValue    ${Index13}
    Should Be Equal    ${current_text}    ${Value13}
GetNo.2-3-125
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path14}
    ${current_text}    GetSubNodeValue    ${Index14}
    Should Be Equal    ${current_text}    ${Value14}
GetNo.2-3-126
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path15}
    ${current_text}    GetSubNodeValue    ${Index15}
    Should Be Equal    ${current_text}    ${Value15}
    [Teardown]    DoTearDown
