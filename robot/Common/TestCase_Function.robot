*** Settings ***
Resource          ../Common/FunctionDetector.robot
Resource          ../Common/FunctionHelper.robot
Resource          ../Common/FunctionJudger.robot
Resource          ../Common/FunctionSetter.robot
Resource          ../Common/FunctionTransfer.robot
Variables         ../Common/BC4Resource_A.py
Variables         ../Common/BC4Resource_B.py
Resource          ../TestCollectiveSetting/BC4CollectiveSettingTester.robot
Resource          ../TestCollectiveSetting/BC4TestCase.robot
Resource          ../Common/Environment.robot
Resource          CollectiveToolDriver.robot

*** Test Cases ***
初始化机器A
    ##########################################################
    ### Func Func Func #######################################
    ##########################################################
    # Func 1
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-1-1_set
    回到Home
    一括设定测试设定选项值    ${5-1-1_type}    ${5-1-1_path}    ${5-1-1_option}    ${5-1-1_extra}

TC_5-1-2_set
    回到Home
    一括设定测试设定选项值    ${5-1-2_type}    ${5-1-2_path}    ${5-1-2_option}    ${5-1-2_extra}

TC_5-1-3_set
    回到Home
    一括设定测试设定选项值    ${5-1-3_type}    ${5-1-3_path}    ${5-1-3_option}    ${5-1-3_extra}

一括设定导出
    导出一括设定    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-1-1_get
    回到Home
    ${option_get}    测试获取选项值    ${5-1-1_type}    ${5-1-1_path}    ${5-1-1_option}
    should be equal    ${5-1-1_option}    ${option_get}

TC_5-1-2_get
    回到Home
    ${option_get}    测试获取选项值    ${5-1-2_type}    ${5-1-2_path}    ${5-1-2_option}
    should be equal    ${5-1-2_option}    ${option_get}

TC_5-1-3_get
    回到Home
    ${option_get}    测试获取选项值    ${5-1-3_type}    ${5-1-3_path}    ${5-1-3_option}
    should be equal    ${5-1-3_option}    ${option_get}
    ##########################################################
    ### Admin Admin Admin ####################################
    ##########################################################
    # Admin 1

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-25_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-25_type}    ${5-2-25_path}    ${5-2-25_option}    ${5-2-25_extra}

TC_5-2-27_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-27_type}    ${5-2-27_path}    ${5-2-27_option}    ${5-2-27_extra}

TC_5-2-28_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-28_type}    ${5-2-28_path}    ${5-2-28_option}    ${5-2-28_extra}

TC_5-2-29_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-29_type}    ${5-2-29_path}    ${5-2-29_option}    ${5-2-29_extra}

TC_5-2-31_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-31_type}    ${5-2-31_path}    ${5-2-31_option}    ${5-2-31_extra}

TC_5-2-38_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-38_type}    ${5-2-38_path}    ${5-2-38_option}    ${5-2-38_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-25_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-25_type}    ${5-2-25_path}    ${5-2-25_option}
    should be equal    ${5-2-25_option}    ${option_get}

TC_5-2-27_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-27_type}    ${5-2-27_path}    ${5-2-27_option}
    should be equal    ${5-2-27_option}    ${option_get}

TC_5-2-28_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-28_type}    ${5-2-28_path}    ${5-2-28_option}
    should be equal    ${5-2-28_option}    ${option_get}

TC_5-2-29_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-29_type}    ${5-2-29_path}    ${5-2-29_option}
    should be equal    ${5-2-29_option}    ${option_get}

TC_5-2-31_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-31_type}    ${5-2-31_path}    ${5-2-31_option}
    should be equal    ${5-2-31_option}    ${option_get}

TC_5-2-38_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-38_type}    ${5-2-38_path}    ${5-2-38_option}
    should be equal    ${5-2-38_option}    ${option_get}
    # Admin 2

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-26_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-26_type}    ${5-2-26_path}    ${5-2-26_option}    ${5-2-26_extra}

TC_5-2-32_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-32_type}    ${5-2-32_path}    ${5-2-32_option}    ${5-2-32_extra}

TC_5-2-39_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-39_type}    ${5-2-39_path}    ${5-2-39_option}    ${5-2-39_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-26_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-26_type}    ${5-2-26_path}    ${5-2-26_option}
    should be equal    ${5-2-26_option}    ${option_get}

TC_5-2-32_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-32_type}    ${5-2-32_path}    ${5-2-32_option}
    should be equal    ${5-2-32_option}    ${option_get}

TC_5-2-39_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-39_type}    ${5-2-39_path}    ${5-2-39_option}
    should be equal    ${5-2-39_option}    ${option_get}
    # Admin 3

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-33_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-33_type}    ${5-2-33_path}    ${5-2-33_option}    ${5-2-33_extra}

TC_5-2-40_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-40_type}    ${5-2-40_path}    ${5-2-40_option}    ${5-2-40_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-33_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-33_type}    ${5-2-33_path}    ${5-2-33_option}
    should be equal    ${5-2-33_option}    ${option_get}

TC_5-2-40_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-40_type}    ${5-2-40_path}    ${5-2-40_option}
    should be equal    ${5-2-40_option}    ${option_get}
    # Admin 4

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-34_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-34_type}    ${5-2-34_path}    ${5-2-34_option}    ${5-2-34_extra}

TC_5-2-41_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-41_type}    ${5-2-41_path}    ${5-2-41_option}    ${5-2-41_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-34_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-34_type}    ${5-2-34_path}    ${5-2-34_option}
    should be equal    ${5-2-34_option}    ${option_get}

TC_5-2-41_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-41_type}    ${5-2-41_path}    ${5-2-41_option}
    should be equal    ${5-2-41_option}    ${option_get}
    # Admin 5

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-35_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-35_type}    ${5-2-35_path}    ${5-2-35_option}    ${5-2-35_extra}

TC_5-2-42_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-42_type}    ${5-2-42_path}    ${5-2-42_option}    ${5-2-42_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-35_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-35_type}    ${5-2-35_path}    ${5-2-35_option}
    should be equal    ${5-2-35_option}    ${option_get}

TC_5-2-42_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-42_type}    ${5-2-42_path}    ${5-2-42_option}
    should be equal    ${5-2-42_option}    ${option_get}
    # Admin 6

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-36_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-36_type}    ${5-2-36_path}    ${5-2-36_option}    ${5-2-36_extra}

TC_5-2-43_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-43_type}    ${5-2-43_path}    ${5-2-43_option}    ${5-2-43_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-36_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-36_type}    ${5-2-36_path}    ${5-2-36_option}
    should be equal    ${5-2-36_option}    ${option_get}

TC_5-2-43_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-43_type}    ${5-2-43_path}    ${5-2-43_option}
    should be equal    ${5-2-43_option}    ${option_get}
    # Admin 7

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-37_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-37_type}    ${5-2-37_path}    ${5-2-37_option}    ${5-2-37_extra}

TC_5-2-44_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-44_type}    ${5-2-44_path}    ${5-2-44_option}    ${5-2-44_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-37_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-37_type}    ${5-2-37_path}    ${5-2-37_option}
    should be equal    ${5-2-37_option}    ${option_get}

TC_5-2-44_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-44_type}    ${5-2-44_path}    ${5-2-44_option}
    should be equal    ${5-2-44_option}    ${option_get}
    # Admin 8

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-45_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-45_type}    ${5-2-45_path}    ${5-2-45_option}    ${5-2-45_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-45_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-45_type}    ${5-2-45_path}    ${5-2-45_option}
    should be equal    ${5-2-45_option}    ${option_get}
    # Admin 9

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-46_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-46_type}    ${5-2-46_path}    ${5-2-46_option}    ${5-2-46_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-46_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-46_type}    ${5-2-46_path}    ${5-2-46_option}
    should be equal    ${5-2-46_option}    ${option_get}
    # Admin 10

初始化机器A
    设定所有信息    ${function_info_dict_bc4_A}    ${machine_info_dict_bc4_A}
    初始化机器

TC_5-2-47_set
    从Home迁移到    Admin
    输入用户密码
    测试设定选项值    ${5-2-47_type}    ${5-2-47_path}    ${5-2-47_option}    ${5-2-47_extra}

导出
    导出    &{function_info_dict_bc4_A}[machine_name]    &{function_info_dict_bc4_A}[admin_password]

初始化机器B
    设定所有信息    ${function_info_dict_bc4_B}    ${machine_info_dict_bc4_B}
    初始化机器

导入
    导入    &{function_info_dict_bc4_B}[machine_name]    &{function_info_dict_bc4_B}[admin_password]

TC_5-2-47_get
    从Home迁移到    Admin
    输入用户密码
    ${option_get}    测试获取选项值    ${5-2-47_type}    ${5-2-47_path}    ${5-2-47_option}
    should be equal    ${5-2-47_option}    ${option_get}
