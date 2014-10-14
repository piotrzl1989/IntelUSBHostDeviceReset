using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Log
{
    public class Log
    {
        static Log _instance;

        string sSource;
        string sLog;
        string sEvent;

        private StreamWriter _streamWriter;

        static public Log GetLogger()
        {
            if (_instance == null)
            {
                _instance = new Log();
            }

            return _instance;
        }

        private Log()
        {
            InitFileLog();
        }

         ~Log()
        {
          //  CloseLogFile();
        }
        private void InitFileLog()
        {



            sSource = "Intel USB Host Device Reset";
            sLog = "Intel USB Host Device Reset";

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);


            var path = ConfigurationManager.AppSettings["logfilelocation"];
            if (String.IsNullOrEmpty(path))
            {
                path = Directory.GetCurrentDirectory();
            }

            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            FileStream fs = new FileStream(path +
                        "\\" + ConfigurationManager.AppSettings["logfilename"],
                        FileMode.OpenOrCreate, FileAccess.Write);

            _streamWriter = new StreamWriter(fs);
            _streamWriter.BaseStream.Seek(0, SeekOrigin.End);

        }

        public void CloseLogFile()
        {
            _streamWriter.Close();
        }

        public void Error(String line)
        {
            line = line.Replace(Convert.ToChar(0), ' ');
            EventLog.WriteEntry(sSource, line,
                EventLogEntryType.Error);


            _streamWriter.WriteLine(DateTime.Now.ToShortDateString() + " " +
              DateTime.Now.ToLongTimeString()+ " : " + "Error - " + line + "\n");
            _streamWriter.Flush();
        }

        public void Info(String line)
        {
            line = line.Replace(Convert.ToChar(0), ' ');
            EventLog.WriteEntry(sSource, line,
                EventLogEntryType.Information);

            _streamWriter.WriteLine(DateTime.Now.ToShortDateString() + " " +
              DateTime.Now.ToLongTimeString() + " : " + "Info - " + line + "\n");
            _streamWriter.Flush();
        }




    }
}
