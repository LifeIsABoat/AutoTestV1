*** Settings ***
Library    control.py    http://127.0.0.1:8003/Service

*** Keywords ***
GoToPath
    [Arguments]    ${path}=${VARIABLE}
    :FOR    ${x}    IN ZIP    ${path}
    \    GoToName    ${x}
