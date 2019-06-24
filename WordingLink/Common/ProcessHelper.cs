namespace WordingLink
{
    public class ProcessHelper
    {
        public static string Excute(string fileName,string arg,string inputArg)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Arguments = arg;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.StandardInput.WriteLine(inputArg);

                //process.StandardInput.AutoFlush = true;
                process.StandardInput.Close();
                process.WaitForExit();
                string outputStr = process.StandardOutput.ReadToEnd();
                
                process.Close();
                return outputStr;
            }
            catch { return null; }
        }

        public static string cmdExcute(string inputStr)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();
                process.StandardInput.WriteLine(inputStr);

                process.StandardInput.AutoFlush = true;
                process.StandardInput.WriteLine("exit");

                process.WaitForExit();
                string outputStr = process.StandardOutput.ReadToEnd();

                process.Close();
                return outputStr;
            }
            catch { return null; }
        }
    }
}
