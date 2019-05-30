using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Machine
{
    //public delegate void ButtonCommandSendProgram(string sendBuf);
    abstract class AbstractComponentKeyBoardMFCTP
    {
        protected Dictionary<string, int> keyCmdDt;
        public abstract void sendKey(string code);

        public AbstractComponentKeyBoardMFCTP()
        {
            keyCmdDt = new Dictionary<string, int>();
        }

        public void addHardKey(string key, int value)
        {
            keyCmdDt.Add(key,value);
        }
        public void removeHardKey(string key)
        {
            keyCmdDt.Remove(key);
        }
    }//abstract class AbstractKeyBoardMFCTP
    public class MFCTPKeyCode
    {
        public const string COPYNUM_KEY = "COPYNUM_KEY";
        public const string COPYNUM_DEC_KEY = "COPYNUM_DEC_KEY";
        public const string IGETA_KEY = "IGETA_KEY";
        public const string REPORT_KEY = "REPORT_KEY";
        public const string SPEED_KEY = "SPEED_KEY";
        public const string OPTIONS_KEY = "OPTIONS_KEY";
        public const string POP_MANUAL_RCV_KEY = "POP_MANUAL_RCV_KEY";
        public const string AUTOANS_KEY = "AUTOANS_KEY";
        public const string FAXPREVIEW_KEY = "FAXPREVIEW_KEY";
        public const string KOME_KEY = "KOME_KEY";
        public const string CLEAR_KEY = "CLEAR_KEY";
        public const string SHIFT_KEY = "SHIFT_KEY";
        public const string DCPOPTIONS_KEY = "DCPOPTIONS_KEY";
        public const string FAXSELECT_KEY = "FAXSELECT_KEY";
        public const string CAPS_KEY = "CAPS_KEY";
        public const string TEN0_KEY = "TEN0_KEY";
        public const string TEN1_KEY = "TEN1_KEY";
        public const string TEN2_KEY = "TEN2_KEY";
        public const string TEN3_KEY = "TEN3_KEY";
        public const string TEN4_KEY = "TEN4_KEY";
        public const string TEN5_KEY = "TEN5_KEY";
        public const string TEN6_KEY = "TEN6_KEY";
        public const string TEN7_KEY = "TEN7_KEY";
        public const string TEN8_KEY = "TEN8_KEY";
        public const string TEN9_KEY = "TEN9_KEY";
        public const string VER_DISP_KEY = "VER_DISP_KEY";
        public const string DEMO_PRINT_KEY = "DEMO_PRINT_KEY";
        public const string COUNTRY_DSP_KEY = "COUNTRY_DSP_KEY";
        public const string R_KEY = "R_KEY";
        public const string PANNOR_SKIP_KEY = "PANNOR_SKIP_KEY";
        public const string TESTPRINT_KEY = "TESTPRINT_KEY";
        public const string MAIL_ADDRIN_KEY = "MAIL_ADDRIN_KEY";
        public const string START_KEY = "START_KEY";
        public const string STOP_KEY = "STOP_KEY";
        public const string FUNCTION_KEY = "FUNCTION_KEY";
        public const string SET_KEY = "SET_KEY";
        public const string PCCSELECT_KEY = "PCCSELECT_KEY";
        public const string UP_KEY = "UP_KEY";
        public const string DOWN_KEY = "DOWN_KEY";
        public const string BWD_KEY = "BWD_KEY";
        public const string FWD_KEY = "FWD_KEY";
        public const string CLEANING_KEY = "CLEANING_KEY";
        public const string SCANSELECT_KEY = "SCANSELECT_KEY";
        public const string REDIAL_KEY = "REDIAL_KEY";
        public const string HOOK_KEY = "HOOK_KEY";
        public const string RESOLUTION_KEY = "RESOLUTION_KEY";
        public const string JOBCANCEL_KEY = "JOBCANCEL_KEY";
        public const string SCAN_TO_KEY = "SCAN_TO_KEY";
        public const string ENLARGE_KEY = "ENLARGE_KEY";
        public const string INKREMAIN_KEY = "INKREMAIN_KEY";
        public const string QUALITY_KEY = "QUALITY_KEY";
        public const string TRAYSEL_KEY = "TRAYSEL_KEY";
        public const string POWER_KEY = "POWER_KEY";
        public const string MC_KEY = "MC_KEY";
        public const string DCPQUALITY_KEY = "DCPQUALITY_KEY";
        public const string COPY_KEY = "COPY_KEY";
        public const string COLORCOPY_KEY = "COLORCOPY_KEY";
        public const string COLORFAX_KEY = "COLORFAX_KEY";
        public const string HOLD_KEY = "HOLD_KEY";
        public const string SLEEPING_MODE_KEY = "SLEEPING_MODE_KEY";
        public const string FUNCTIONLOCK_KEY = "FUNCTIONLOCK_KEY";
        public const string DUPLEX_COPY_KEY = "DUPLEX_COPY_KEY";
        public const string _null_5f = "_null_5f";
        public const string _null_60 = "_null_60";
        public const string TOUCH01_KEY = "TOUCH01_KEY";
        public const string TOUCH02_KEY = "TOUCH02_KEY";
        public const string TOUCH03_KEY = "TOUCH03_KEY";
        public const string TOUCH04_KEY = "TOUCH04_KEY";
        public const string TOUCH05_KEY = "TOUCH05_KEY";
        public const string TOUCH06_KEY = "TOUCH06_KEY";
        public const string TOUCH07_KEY = "TOUCH07_KEY";
        public const string TOUCH08_KEY = "TOUCH08_KEY";
        public const string TOUCH09_KEY = "TOUCH09_KEY";
        public const string TOUCH10_KEY = "TOUCH10_KEY";
        public const string TOUCH11_KEY = "TOUCH11_KEY";
        public const string TOUCH12_KEY = "TOUCH12_KEY";
        public const string TOUCH13_KEY = "TOUCH13_KEY";
        public const string TOUCH14_KEY = "TOUCH14_KEY";
        public const string TOUCH15_KEY = "TOUCH15_KEY";
        public const string TOUCH16_KEY = "TOUCH16_KEY";
        public const string TOUCH17_KEY = "TOUCH17_KEY";
        public const string TOUCH18_KEY = "TOUCH18_KEY";
        public const string PHOTOCAPTURE_KEY = "PHOTOCAPTURE_KEY";
        public const string KOKI_KEY = "KOKI_KEY";
        public const string ERASE_KEY = "ERASE_KEY";
        public const string PLAY_KEY = "PLAY_KEY";
        public const string INKMNG_KEY = "INKMNG_KEY";
        public const string PAPERTYPE_KEY = "PAPERTYPE_KEY";
        public const string PRINTINDEX_KEY = "PRINTINDEX_KEY";
        public const string SPHONE_KEY = "SPHONE_KEY";
        public const string HOME_KEY = "HOME_KEY";
        public const string DUMMY_KEY = "DUMMY_KEY";
        public const string MAINTEN_KEY = "MAINTEN_KEY";
        public const string COPYSELECT_KEY = "COPYSELECT_KEY";
        public const string HISTORY_KEY = "HISTORY_KEY";
        public const string TOUCH19_KEY = "TOUCH19_KEY";
        public const string TOUCH20_KEY = "TOUCH20_KEY";
        public const string TOUCH21_KEY = "TOUCH21_KEY";
        public const string TOUCH22_KEY = "TOUCH22_KEY";
        public const string TOUCH23_KEY = "TOUCH23_KEY";
        public const string TOUCH24_KEY = "TOUCH24_KEY";
        public const string TOUCH25_KEY = "TOUCH25_KEY";
        public const string TOUCH26_KEY = "TOUCH26_KEY";
        public const string TOUCH27_KEY = "TOUCH27_KEY";
        public const string TOUCH28_KEY = "TOUCH28_KEY";
        public const string TOUCH29_KEY = "TOUCH29_KEY";
        public const string TOUCH30_KEY = "TOUCH30_KEY";
        public const string TOUCH31_KEY = "TOUCH31_KEY";
        public const string TOUCH32_KEY = "TOUCH32_KEY";
        public const string TOUCH33_KEY = "TOUCH33_KEY";
        public const string TOUCH34_KEY = "TOUCH34_KEY";
        public const string TOUCH35_KEY = "TOUCH35_KEY";
        public const string TOUCH36_KEY = "TOUCH36_KEY";
        public const string TOUCH37_KEY = "TOUCH37_KEY";
        public const string TOUCH38_KEY = "TOUCH38_KEY";
        public const string TOUCH39_KEY = "TOUCH39_KEY";
        public const string TOUCH40_KEY = "TOUCH40_KEY";
        public const string TOUCH_MAX_KEY = "TOUCH_MAX_KEY";
        public const string L_OPTIONS_KEY = "L_OPTIONS_KEY";
        public const string L_COPY_KEY = "L_COPY_KEY";
        public const string MODECLEAR_KEY = "MODECLEAR_KEY";
        public const string COLORSTART_KEY = "COLORSTART_KEY";
        public const string BWSCAN_KEY = "BWSCAN_KEY";
        public const string COLORSCAN_KEY = "COLORSCAN_KEY";
        public const string EMAILSCAN_KEY = "EMAILSCAN_KEY";
        public const string RESET_KEY = "RESET_KEY";
        public const string VOLUP_KEY = "VOLUP_KEY";
        public const string VOLDW_KEY = "VOLDW_KEY";
        public const string SEARCH_KEY = "SEARCH_KEY";
        public const string PAUSE_KEY = "PAUSE_KEY";
        public const string INDEX_KEY = "INDEX_KEY";
        public const string BROAD_KEY = "BROAD_KEY";
        public const string RECORD_KEY = "RECORD_KEY";
        public const string ONLINE_KEY = "ONLINE_KEY";
        public const string FFCONT_KEY = "FFCONT_KEY";
        public const string PRIORITY_KEY = "PRIORITY_KEY";
        public const string TESTRESET_KEY = "TESTRESET_KEY";
        public const string REDUCE_KEY = "REDUCE_KEY";
        public const string SORT_KEY = "SORT_KEY";
        public const string PHOTO_KEY = "PHOTO_KEY";
        public const string HELP_KEY = "HELP_KEY";
        public const string CONTRAST_KEY = "CONTRAST_KEY";
        public const string PAPERSIZE_KEY = "PAPERSIZE_KEY";
        public const string PRINTMENU_KEY = "PRINTMENU_KEY";
        public const string TRANSFER_KEY = "TRANSFER_KEY";
        public const string DENSITY_KEY = "DENSITY_KEY";
        public const string DTYPE_KEY = "DTYPE_KEY";
        public const string SCAN_KEY = "SCAN_KEY";
        public const string EMAIL_KEY = "EMAIL_KEY";
        public const string CANCEL_KEY = "CANCEL_KEY";
        public const string DELAYED_TX_KEY = "DELAYED_TX_KEY";
        public const string VERIFY_KEY = "VERIFY_KEY";
        public const string SET_SECURITY_KEY = "SET_SECURITY_KEY";
        public const string COPYMODE_KEY = "COPYMODE_KEY";
        public const string TONERSAVE_KEY = "TONERSAVE_KEY";
        public const string POSTER_KEY = "POSTER_KEY";
        public const string NIN1_KEY = "NIN1_KEY";
        public const string _null_be = "_null_be";
        public const string SECUREPRINT_KEY = "SECUREPRINT_KEY";
        public const string DIRECTPRINT_KEY = "DIRECTPRINT_KEY";
        public const string GO_KEY = "GO_KEY";
        public const string BILLING_IDCOPY_KEY = "BILLING_IDCOPY_KEY";
        public const string CODEBOTTOM_KEY = "CODEBOTTOM_KEY";
        public const string TEL_TYPE1_KEY = "TEL_TYPE1_KEY";
        public const string TEL_TYPE2_KEY = "TEL_TYPE2_KEY";
        public const string TEL_TYPE3_KEY = "TEL_TYPE3_KEY";
        public const string TEL_TYPE4_KEY = "TEL_TYPE4_KEY";
        public const string PRINTIMAGE_KEY = "PRINTIMAGE_KEY";
        public const string DUPLEX_KEY = "DUPLEX_KEY";
    }//class MFCKeyCode
}
