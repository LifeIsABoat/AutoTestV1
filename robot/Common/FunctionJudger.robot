*** Settings ***
Library           FunctionJudger.py

*** Keywords ***
是否存在选项
    [Arguments]    ${item}
    [Documentation]    判断是否存在目标选项，Panel侧解析实现。
    ${ret}    is exist    ${item}
    [Return]    ${ret}

是否选中窗口项
    [Arguments]    ${window}
    [Documentation]    判断当前画面中，窗口项目是否被发选中。
    ${ret}    is window selected    ${window}
    [Return]    ${ret}

是否选中选项
    [Arguments]    ${option}
    [Documentation]    判断当前菜单中，目标项目是否被选中。
    ${ret}    is option selected    ${option}
    [Return]    ${ret}
