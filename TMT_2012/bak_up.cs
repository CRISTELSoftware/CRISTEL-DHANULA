using System;
using System.Diagnostics;
using System.IO;



namespace TMT_2012
{
    class bak_up
    {
        public static bool Backup(string user, string password, string host, string database, string path)
        {

            try
            {
                DateTime backupTime = DateTime.Now;
                int year = backupTime.Year;
                int month = backupTime.Month;
                int day = backupTime.Day;
                int hour = backupTime.Hour;
                int minute = backupTime.Minute;
                int second = backupTime.Second;
                int ms = backupTime.Millisecond;

                String tmestr;
                tmestr = path + "-" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + ".sql";

                StreamWriter file = new StreamWriter(tmestr);

                ProcessStartInfo proc = new ProcessStartInfo();

                proc.FileName = @"C:\Program Files\MySQL\MySQL Server 5.1\bin\mysqldump";

                string cmd = string.Format(@"-u{0} -p{1} -h{2} {3} ", user, password, host, database, tmestr /*"backup.sql"*/);

                proc.Arguments = cmd;
                proc.RedirectStandardInput = false;
                proc.RedirectStandardOutput = true;
                proc.UseShellExecute = false;
                proc.CreateNoWindow = true;

                Process p = Process.Start(proc);
                string res;
                res = p.StandardOutput.ReadToEnd();
                file.WriteLine(res);
                p.WaitForExit();
                file.Close();
                return true;
            }
            catch (IOException ex)
            {

                return false;
            }
        }

        public static void restor(string user, string password, string host, string database, string path)
        {
             
            StreamReader file = new StreamReader(path);
            ProcessStartInfo proc = new ProcessStartInfo();
            string cmdArgs = string.Format(@"-u{0} -p{1} -h{2} {3}", user, password, host, database);
            proc.FileName = "C:\\Program Files\\MySQL\\MySQL Server 5.1\\bin\\mysql.exe";
            proc.RedirectStandardInput = true;
            proc.RedirectStandardOutput = false;
            proc.Arguments = cmdArgs;
            proc.UseShellExecute = false;
            proc.CreateNoWindow = true;
            Process p = Process.Start(proc);
            string res = file.ReadToEnd();
            file.Close();
            p.StandardInput.WriteLine(res);
            p.Close();
        }



        }
    }


