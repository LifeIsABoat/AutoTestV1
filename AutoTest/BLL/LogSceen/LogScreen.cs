using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.BLL
{
    class LogScreen
    {
        public string scrid;                // screen id
        //public int ctl_num;               // number of controls
        public int btn_num_inlist;          // numberof button in button list
        public List<LogControl> ctls;       // list of controls

        public List<LogControl> getItemList(string ctrlType,
                                            ushort? dataHolderID = null)
        {
            if (0 == ctls.Count)
                return null;

            List<LogControl> targetControlList = new List<LogControl>();
            foreach (LogControl ctl in ctls)
            {
                if (ctrlType == ctl.ctl_type)
                {
                    if (null == dataHolderID)
                    {
                        targetControlList.Add(ctl);
                    }
                    else if (dataHolderID == ctl.dataHolderID)
                    {
                        targetControlList.Add(ctl);
                    }
                }
            }

            if (0 == targetControlList.Count())
                return null;
            else
                return targetControlList;
        }

        public LogControl getItem(ushort? layerID)
        {
            if (0 == ctls.Count)
                return null;
            if (null == layerID)
                return null;

            foreach (LogControl ctl in ctls)
            {
                if (layerID == ctl.layerID)
                    return ctl;
            }

            return null;
        }
    }
}
