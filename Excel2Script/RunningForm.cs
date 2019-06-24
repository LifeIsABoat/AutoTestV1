using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTBExcel2Script
{
    public partial class RunningForm : Form
    {
        public RunningForm()
        {
            InitializeComponent();
        }

        // スレッド
        private Thread thRunningShow = null;
        // 処理継続フラグ
        private bool bContinueFlg = true;

        // 配列定義
        private const int POSITION_X = 0;
        private const int POSITION_Y = 1;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">表示位置　X, Y</param>
        /// <param name="strTitle">ダイアログタイトル</param>
        /// <param name="message">表示メッセージ</param>
        public RunningForm(int[] position, string strTitle, string strMessage)
        {
            // 値を初期化する
            InitializeComponent();
            this.Text = strTitle;
            labelMsgRunning.Text = strMessage;
            // 表示位置(中央)を設定する
            this.Location = new Point(position[POSITION_X], position[POSITION_Y]);
            // スレッドを定義する
            thRunningShow = new Thread(new ThreadStart(ShowRunningForm));
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="position">表示位置　X, Y</param>
        public RunningForm(int[] position)
        {
            // 値を初期化する
            InitializeComponent();
            // 表示位置(中央)を設定する
            Location = new Point(position[POSITION_X], position[POSITION_Y]);
            // スレッドを定義する
            thRunningShow = new Thread(new ThreadStart(ShowRunningForm));
        }

        /// <summary>
        /// 実行中画面を表示する。
        /// </summary>
        public void Start()
        {
            // スレッドを開始する
            thRunningShow.Start();
        }

        /// <summary>
        /// 実行中画面を終了する。
        /// </summary>
        public void Abort()
        {
            bContinueFlg = false;
            if ( null!= thRunningShow)
            {
                thRunningShow.Join();
                thRunningShow = null;
            }
        }

        /// <summary>
        /// 実行中のDialog表示を行う。
        /// </summary>
        private void ShowRunningForm()
        {
            // 実行中画面を起動する。
            this.Show();
            this.BringToFront();

            while (bContinueFlg)
            {
                this.Refresh();
                Thread.Sleep(50);
                Application.DoEvents();
            }
            this.Close();
        }
    }
}
