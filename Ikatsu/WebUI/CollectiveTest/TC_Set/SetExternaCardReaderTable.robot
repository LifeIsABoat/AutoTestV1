*** Settings ***
Resource        ../Variables/OptionExternaCardReader.txt
Resource        ../../EWSCommon/KeyWords.robot
Library         ../../EWSCommon/control.py    http://127.0.0.1:8003/Service

*** Test Cases ***
SetNo.2-3-111
    [Setup]    DoSetUp
    GoToPath    ${TestPath_2-3-111}
    SetSubNodeValue    @{OptionValue_2-3-111}    @{OptionIndex_2-3-111}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-112
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path1}
    SetSubNodeValue    ${Value1}    ${Index1}
SetNo.2-3-113
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path2}
    SetSubNodeValue    ${Value2}    ${Index2}
SetNo.2-3-114
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path3}
    SetSubNodeValue    ${Value3}    ${Index3}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-115
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path4}
    SetSubNodeValue    ${Value4}    ${Index4}
SetNo.2-3-116
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path5}
    SetSubNodeValue    ${Value5}    ${Index5}
SetNo.2-3-117
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path6}
    SetSubNodeValue    ${Value6}    ${Index6}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-118
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path7}
    SetSubNodeValue    ${Value7}    ${Index7}
SetNo.2-3-119
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path8}
    SetSubNodeValue    ${Value8}    ${Index8}
SetNo.2-3-120
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path9}
    SetSubNodeValue    ${Value9}    ${Index9}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-121
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path10}
    SetSubNodeValue    ${Value10}    ${Index10}
SetNo.2-3-122
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path11}
    SetSubNodeValue    ${Value11}    ${Index11}
SetNo.2-3-123
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path12}
    SetSubNodeValue    ${Value12}    ${Index12}
    PushOK
    [Teardown]    DoTearDown
SetNo.2-3-124
    [Setup]    DoSetUp
    GoToPath    ${PathMain}
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path13}
    SetSubNodeValue    ${Value13}    ${Index13}
SetNo.2-3-125
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path14}
    SetSubNodeValue    ${Value14}    ${Index14}
SetNo.2-3-126
    GoToPageRoot
    GoToName    ${CardReaderPath}
    GoToPath    ${Path15}
    SetSubNodeValue    ${Value15}    ${Index15}
    PushOK
    [Teardown]    DoTearDown
