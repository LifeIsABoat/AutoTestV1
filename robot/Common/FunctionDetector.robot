*** Settings ***
Library           FunctionDetector.py

*** Keywords ***
获取窗口项的值
    [Documentation]    获取窗口项目画面，选中的窗口值
    ${window_value}    get_window_value
    should not be equal    ${window_value}    ${None}
    [Return]    ${window_value}

获取输入框的值
    [Documentation]    获取输入画面，输入的内容，光标遮挡未处理。
    ${inputed_value}    get_input_value
    should not be equal    ${inputed_value}    ${None}
    [Return]    ${inputed_value}

获取选中的选项
    [Documentation]    获取当前菜单中选中的项目，Panel侧解析实现。
    ${selected_item}    get_option_selected
    should not be equal    ${selected_item}    ${None}
    [Return]    ${selected_item}

获取选项的值
    [Arguments]    ${item}
    [Documentation]    获取项目右下角的值。
    ${item_value}    get_option_value    ${item}
    should not be equal    ${item_value}    ${None}
    [Return]    ${item_value}

获取选项的状态
    [Arguments]    ${item}
    [Documentation]    获取项目的状态。无效：Invalid,有效：Valid,选中：Selected。
    ${item_status}    get option status    ${item}
    should not be equal    ${item_status}    ${None}
    [Return]    ${item_status}
