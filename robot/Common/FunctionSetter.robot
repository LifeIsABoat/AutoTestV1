*** Settings ***
Library           FunctionSetter.py

*** Keywords ***
设定选项
    [Arguments]    ${option}
    [Documentation]    在当前菜单中设定当前项目，Panel侧解析实现。
    ${ret}    set option    ${option}
    should be true    ${ret}

输入选项
    [Arguments]    ${content}
    [Documentation]    在当前画面中设定输入项，Panel侧解析实现。
    ${ret}    input option    ${content}
    should be true    ${ret}

键盘输入
    [Arguments]    ${content}
    [Documentation]    使用键盘输入指定内容。
    ${ret}    input key value    ${content}
    should be true    ${ret}

设定窗口项
    [Arguments]    ${window}
    [Documentation]    在当前画面中设定窗口项，Panel侧解析实现。
    ${ret}    set window    ${window}
    should be true    ${ret}

输入用户密码
    [Arguments]
    [Documentation]    使用键盘输入指定内容。
    ${ret}    input admin password
    should be true    ${ret}
