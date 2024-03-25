namespace Nakheel_Web.Authentication
{
    public static class Log
    {
        public static void LogError(Exception ex, string Ctrl, string Action)
        {

            try
            {
                string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
                message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                message += string.Format("Controller: {0} || Action:{1}", Ctrl, Action);
                message += Environment.NewLine;
                message += Environment.NewLine;
                message += string.Format("Message: {0}", ex.Message);
                message += Environment.NewLine;
                message += string.Format("StackTrace: {0}", ex.StackTrace);
                message += Environment.NewLine;
                message += string.Format("Source: {0}", ex.Source);
                message += Environment.NewLine;
                //message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
                //message += Environment.NewLine;
                message += "-----------------------------------------------------------";
                message += Environment.NewLine;
                string path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "ErrorFile/ErrorLog.txt"));

                //string path = "C:\\Users\\Ardhas\\source\\repos\\DLF_WEB\\DLF_WEB\\ErrorFile\\ErrorLog.txt";

                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(message);
                    writer.Close();
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
