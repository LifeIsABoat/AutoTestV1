using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Machine
{
    public delegate void TouchCommandSendProgram(string sendBuf);
    abstract class AbstractComponentTouchPanelMFCTP
    {
        protected Dictionary<string, string> tpCmdTable;
        public TouchCommandSendProgram sendProgram;

        //Click on the touch screen
        public abstract void TPClick(Position position);

        //Press the touch screen
        public abstract void TPPush(Position position);

        //LongTPPushs
        public abstract void LongTPPush(Position position);

        //Release the touch screen
        public abstract void TPRelease(Position position);

        public AbstractComponentTouchPanelMFCTP()
        {
            tpCmdTable = new Dictionary<string, string>();
        }

        public void addTouchCmd(string key, string value)
        {
            tpCmdTable.Add(key,value);
        }
        public void removeTouchCmd(string key, string value)
        {
            tpCmdTable.Remove(key);
        }
    }//abstract class AbstractTouchPanelMFCTP

    public class MFCTPTpCode
    {
        // command head
        public const string TCHPNL_HEAD_POINT = "TCHPNL_HEAD_POINT";

        //command type--TP event type
        public const string TCHPNL_TYPE_PUSHED = "TCHPNL_TYPE_PUSHED";
        public const string TCHPNL_TYPE_RELEASED = "TCHPNL_TYPE_RELEASED";
        public const string TCHPNL_TYPE_MOVED = "TCHPNL_TYPE_MOVED";
        public const string TCHPNL_TYPE_REPEAT = "TCHPNL_TYPE_REPEAT";
        // command type -- get screen info
        public const string TCHPNL_TYPE_SCRN = "TCHPNL_TYPE_SCRN";
        //画面キャプチャ
        public const string TCHPNL_TYPE_CAPTURE = "TCHPNL_TYPE_CAPTURE";

        //command type -- not be used
        public const string TCHPNL_TYPE_KIND = "TCHPNL_TYPE_KIND";
        public const string TCHPNL_TYPE_MSTATE = "TCHPNL_TYPE_MSTATE";
        public const string TCHPNL_TYPE_MSTATE_ON = "TCHPNL_TYPE_MSTATE_ON";
        public const string TCHPNL_TYPE_MSTATE_OFF = "TCHPNL_TYPE_MSTATE_OFF";
    }
}
