using System.Windows;
using ColorDivisionTool.ViewModel;
using System;
using System.Collections.Generic;

namespace ColorDivisionTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            string[] pargs = Environment.GetCommandLineArgs();
            string[] new_args = Remove(pargs, 0);
            List<string> newList = new List<string>(new_args);
            MainViewModel mainViewModel = new MainViewModel(new BindItem(), newList);
            if (newList.Count > 2)
            {
                Console.WriteLine("対象機種：" + newList[0]);
                Console.WriteLine("機種類型：" + newList[1]);
                Console.WriteLine("削除項目一覧EXCEL：" + newList[2]);
                MainWindow mwindow = new MainWindow();
                mainViewModel.ViewBindModel.MWindowShowLog = "Ready to begin ...";
                mwindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                mwindow.DataContext = mainViewModel.ViewBindModel;
                mwindow.Show();
                mainViewModel.Btn_StartChgColorViewModel(null);
            }
            else
            {
                MainWindow mwindow = new MainWindow();
                mainViewModel.ViewBindModel.MWindowShowLog = "Ready to begin ...";
                mwindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //mainViewModel.ViewBindModel.exeTitle = "色分けツール " + "Ver " + "1." + compileTime.Month.ToString("00") + "." + compileTime.Day.ToString("00") + "." + compileTime.Hour.ToString("00") + compileTime.Minute.ToString("00");
                mwindow.DataContext = mainViewModel.ViewBindModel;
                mwindow.Show();
            }
        }

        private string[] Remove(string[] old, int index)
        {
            if (index < 0 || index >= old.Length - 1) return old;
            string[] new_String = new string[old.Length - 1];
            for (int i = 0; i < old.Length - 1; i++)
            {
                if (i < index)
                    new_String[i] = old[i];
                else
                    new_String[i] = old[i + 1];
            }
            return new_String;
        }
    }
}
