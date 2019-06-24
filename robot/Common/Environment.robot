*** Settings ***
Library           Environment.py

*** Keywords ***
设定机器信息
    [Arguments]    ${machine_info}
    [Documentation]    更新机器信息
    set machine    ${machine_info}

设定机能信息
    [Arguments]    ${function_info}
    [Documentation]    更新机能信息
    set function    ${function_info}

设定所有信息
    [Arguments]    ${function_info}    ${machine_info}
    [Documentation]    更新机能信息
    set all    ${function_info}    ${machine_info}
