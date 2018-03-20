using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstructuraDeDatos
{
    public class LogFile
    {
        private Stopwatch time;

        public LogFile()
        {
            time = new Stopwatch();
            time.Start();
        }
        public void Logcreate(string action)
        {
            string date = DateTime.Today.Day.ToString() + "_" + DateTime.Today.DayOfWeek.ToString() + "_" + DateTime.Today.Month.ToString() + "_" + DateTime.Today.Year.ToString();
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filepath = filepath + @"\Log" + "_" + date + ".txt";

            StreamWriter writer = File.AppendText(filepath);
            time.Stop();
            writer.WriteLine(action + ":" + " " + time.ElapsedMilliseconds + " " + "Milisegundos" + " " + "[" + DateTime.Now + "]");
            writer.Close();

        }
    }
}
