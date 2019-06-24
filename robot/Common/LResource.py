#!/usr/bin/env python
# -*- coding: utf-8 -*-
# @Date    : 2018-04-11 16:51:28
# @Link    :
# @Version : V0.1

machine_info_dict = {
    "machine_url": "http://APBSH0675:65001",
    "up_label": "up",
    "down_label": "down",
    "left_label": "left",
    "right_label": "right",
    "parser_info_dict": {
        "component_parser_url_list": [
            "http://APBSH0675:65002/LogScreenParser",
            "http://APBSH0675:65004/TesseractScreenParser",
            "http://APBSH0675:65002/LogScreenParser",
            "http://APBSH0675:65003/FeatureScreenParser",
            "",
            ""
        ],
        "match_parser_url": "http://APBSH0675:65005",
        "similarity_dic": {
            'OK': ['0K'],
            '2-sided Scan: Long Edge': ['2-sided Scan:Long Edge'],
            '2-sided Scan: Short Edge': ['2-sided Scan:Short Edge'],
            'File Type': ['F ile Type'],
            'PDF/A Single-Page': ['PDF/A Single-Page'],
            '2sided(1⇒2)': ['2sided(1=2)','2sided(1=>2)'],
            '2sided(2⇒2)': ['2sided(2=2)','2sided(2=>2)'],
            '141% A5⇒A4': ['141% A5=A4','141% A5=>A4'],
            '104% EXE⇒LTR': ['104% EXE=LTR','104% EXE=>LTR'],
            '97% LTR⇒A4': ['97% LTR=A4','97% LTR=>A4'],
            '94% A4=LTR': ['94% A4=LTR','94% A4=>LTR'],
            '85% LTR⇒EXE': ['85% LTR=EXE','85% LTR=>EXE'],
            '83% LGL⇒A4': ['83% LGL=A4','83% LGL=>A4'],
            '78% LGL⇒LTR': ['78% LGL=LTR','78% LGL=>LTR'],
            '70% A4⇒A5': ['70% A4=A5','70% A4=>A5'],
            '2-sided⇒2-sided': ['2-sided=2-sided','2-sided=>2-sided'],
            '1-sided⇒2-sided': ['1-sided=2-sided','1-sided=>2-sided'],
            '2-sided⇒1-sided': ['2-sided=1-sided','2-sided=>1-sided'],
            'LongEdge⇒ ShortEdge': ['LongEdge= ShortEdge','LongEdge=> ShortEdge'],
            '78% LGL⇒LTR': ['78% LGL=LTR','78% LGL=>LTR'],
            '70% A4⇒A5': ['70% A4=A5','70% A4=>A5'],
            'S1': ['SI','Sl']
        }
    }
}

function_info_dict = {
    "clear_key": "CLEAR_KEY",
    "home_key": "HOME_KEY",
    "stop_key": "STOP_KEY",

    "reset_path": [
        "All Settings",
        "Initial Setup",
        "Reset",
        "All Settings"
    ],
    "reset_ok": "OK",
    "reset_ok_time": 4,
    "reset_wait_time": 60,
    "reset_after_path": [
        "OK",
        "No"
    ],

    "language_path": [
        "All Settings",
        "Initial Setup",
        "Local Language"
    ],

    "ethernet_ip_path": [
        "All Settings",
        "Network",
        "Wired LAN",
        "TCP/IP",
        "IP Address"
    ],

    "wifi_ip_path": [
        "All Settings",
        "Network",
        "WLAN",
        "TCP/IP",
        "IP Address"
    ],

    "input_caps_lock_region": [6, 414, 125, 60],
    "input_clear_region": [604, 75, 63, 56],
    "input_tab_region": [[385, 416, 94, 61],
                         [478, 416, 90, 59],
                         [569, 417, 91, 59]],
    "input_ok_button": "OK",
    "input_box_region": [13, 75, 547, 55],

    "window_5_template": [
        ['-2', '-1', '0', '+1', '+2'],
        ['-20', '-10', '0', '+10', '+20'],
        ['-90°', '-45°', '0', '+45°', '+90°'],
        ['-50', '-25', '0', '+25', '+50']
    ],
    "window_11_template": [
        ['-5', '-4', '-3', '-2', '-1',
         '0',
         '+1', '+2', '+3', '+4', '+5'],
        ['-50', '-40', '-30', '-20', '-10',
         '0',
         '+10', '+20', '+30', '+40', '+50']
    ],
    "window_ok_button": "OK",
    "window_value_region": [382, 344, 34, 36],

    "home_similarity": 0.6,
    "home_screen": "home_screen",

    "times_wait_for_home": 60000,

    "key_board_dict": {
        "0": "TEN0_KEY", "1": "TEN1_KEY", "2": "TEN2_KEY",
        "3": "TEN3_KEY", "4": "TEN4_KEY", "5": "TEN5_KEY",
        "6": "TEN6_KEY", "7": "TEN7_KEY", "8": "TEN8_KEY",
        "9": "TEN9_KEY", "#": "IGETA_KEY", "*": "KOME_KEY"},

    "machine_name": "BRN0080774C2B70",
    "admin_password": "initpass",
    "admin_password_regions": [[477, 220, 47, 45], [407, 349, 53, 50], [477, 220, 47, 45], [276, 217, 50, 50],
                               [608, 218, 50, 50], [42, 283, 50, 50], [110, 283, 50, 50], [110, 283, 50, 50]]
}
