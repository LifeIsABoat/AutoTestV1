*** Settings ***
Library           FunctionTransfer.py

*** Keywords ***
迁移到
    [Arguments]    @{path}
    [Documentation]    基于当前画面，执行指定迁移操作。
    ${ret}    move to    ${path}
    should be true    ${ret}

从Home迁移到
    [Arguments]    @{path}
    [Documentation]    从Home画面开始，执行指定的迁移操作。
    ${ret}    move to from home    ${path}
    should be true    ${ret}

返回上一个Level
    [Documentation]    迁移到上一个Level,点击返回键实现。
    ${ret}    back previous level
    should be true    ${ret}

回到Home
    [Documentation]    回到Home画面
    ${ret}    back home
    should be true    ${ret}
