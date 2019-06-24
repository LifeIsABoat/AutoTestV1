*** Settings ***
Library           FunctionHelper.py

*** Keywords ***
初始化机器
    [Documentation]    初始化机器所有设定，Panel侧点击实现。
    ${ret}    all settings reset
    should be true    ${ret}

停止当前操作
    [Documentation]    停止打印机当前操作，停止键点击实现。
    ${ret}    stop operation
    should be true    ${ret}

执行选项型条件
    [Arguments]    @{path}
    [Documentation]    执行选项设定类型的内部条件,参数${path}为包括option项的路径
    ${ret}    run option condition    ${path}
    should be true    ${ret}

执行输入型条件
    [Arguments]    @{path}
    [Documentation]    执行选项设定类型的内部条件,参数${path}为包括option项的路径
    ${ret}    input option condition    ${path}
    should be true    ${ret}

获取有线网络IP
    [Documentation]    获取有线网络IP，Panel侧识别实现。
    ${ip}    get wiredlan ip
    should not be equal    ${ip}    ${None}
    [Return]    ${ip}

获取无线网络IP
    [Documentation]    获取无线网络IP，Panel侧识别实现。
    ${ip}    get wlan ip
    should not be equal    ${ip}    ${None}
    [Return]    ${ip}
