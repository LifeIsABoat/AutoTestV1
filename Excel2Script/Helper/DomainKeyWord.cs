using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTBExcel2Script
{
    public class DomainKeyWord
    {
        /* 共通 */
        public class KW_Common
        {
            private const string Domain_Name = "共通_";
            public const string Back_Home = Domain_Name + "バックホーム";
        }

        /* 实体键 */
        public class KW_HardKey
        {
            private const string Domain_Name = "实体键_";
            public const string Click = Domain_Name + "点击";
            public const string Input = Domain_Name + "输入";
        }

        /* 菜单 */
        public class KW_Menu
        {
            private const string Domain_Name = "メニュー_";
            public const string Enable = Domain_Name + "有効";
            public const string Disable = Domain_Name + "無効";
            public const string List = Domain_Name + "リストチェック";
            public const string SubValue = Domain_Name + "右下値チェック";
            public const string Exist = Domain_Name + "存在";
            public const string Select = Domain_Name + "選択";
        }

        /* 选项 */
        public class KW_Option
        {
            private const string Domain_Name = "オプション_";
            public const string Exist = Domain_Name + "存在";
            public const string List = Domain_Name + "リストチェック";
            public const string Select = Domain_Name + "選択";
            public const string SelectValue = Domain_Name + "選択値チェック";
            public const string Enable = Domain_Name + "有効";
            public const string Disable = Domain_Name + "無効";
        }

        /* 功能 */
        public class KW_Funtion
        {
            private const string Domain_Name = "機能_";
            public const string Enable = Domain_Name + "有効";
            public const string Disable = Domain_Name + "無効";
            public const string Exist = Domain_Name + "存在";
            public const string Select = Domain_Name + "選択";
        }

        /* 标题 */
        public class KW_Title
        {
            private const string Domain_Name = "タイトル_";
            public const string Title = Domain_Name + "チェック";
        }

        /* 输入 */
        public class KW_SoftInput
        {
            private const string Domain_Name = "入力_";
            public const string Type = Domain_Name + "タイプチェック";
            public const string Clear = Domain_Name + "クリーン";
            public const string OK = Domain_Name + "コミット";
            public const string Input = Domain_Name + "入力";
            public const string InputValue = Domain_Name + "值チェック";
        }

        /* 只读 */
        public class KW_MachineInfo
        {
            private const string Domain_Name = "表示専用_";
            public const string Value = Domain_Name + "值チェック";
            public const string Type = Domain_Name + "タイプチェック";
        }

        /* 弹出 */
        public class KW_PopUp
        {
            private const string Domain_Name = "ポップアップ_";
            public const string Click = Domain_Name + "クリック";
        }
    }
}
