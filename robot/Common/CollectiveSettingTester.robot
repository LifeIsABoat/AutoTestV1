*** Settings ***
Resource          ../Common/FunctionHelper.robot
Resource          ../Common/FunctionTransfer.robot
Resource          ../Common/FunctionSetter.robot
Resource          ../Common/FunctionDetector.robot
Resource          ../Common/FunctionJudger.robot

*** Keywords ***
测试Shortcuts创建
    [Arguments]    ${path}    ${key_value}    ${name}    ${extra}
    [Documentation]    测试流程关键字，创建Shortcuts
    从Home迁移到    ${path}
    键盘输入    ${key_value}
    迁移到    Save as\r\nShortcut    OK
    输入选项    ${name}
    Run Keyword If    ${extra}!=${None}    迁移到    ${extra}
    迁移到    OK

测试Shortcuts创建
    [Arguments]    ${path}    ${name}    ${extra}
    [Documentation]    测试流程关键字，创建Shortcuts
    从Home迁移到    ${path}
    输入选项    ${name}
    Run Keyword If    ${extra}!=${None}    迁移到    ${extra}

测试Shortcuts创建
    [Arguments]    ${path}    ${name}    ${extra}
    [Documentation]    测试流程关键字，创建Shortcuts
    从Home迁移到    ${path}
    输入选项    ${name}
    Run Keyword If    ${extra}!=${None}    迁移到    ${extra}
    迁移到    OK

测试设定选项值
    [Arguments]    ${type}    ${path}    ${option}    ${extra}
    [Documentation]    测试流程关键字，设置选项值
    迁移到    ${path}
    Run Keyword If    '${type}' == 'Option'    设定选项    ${option}
    ...    ELSE IF    '${type}' == 'Input'    输入选项    ${option}
    ...    ELSE IF    '${type}' == 'Window'    设定窗口项    ${option}
    Run Keyword If    ${extra}[0]!=${None}    迁移到    ${extra}

测试获取选项值
    [Arguments]    ${type}    ${path}    ${option}
    [Documentation]    测试流程关键字，获取选项值
    迁移到    ${path}
    Run Keyword And Return If    '${type}' == 'Input'    获取输入框的值
    Run Keyword And Return If    '${type}' == 'Window'    获取窗口项的值
    Return From Keyword If    '${type}' != 'Option'    ${None}
    ${ret}    是否存在选项    ${option}
    ${statue}    Run Keyword If    '${ret}' == '${True}'    获取选项的状态    ${option}
    Return From Keyword If    '${statue}' == 'Selected'    ${option}
