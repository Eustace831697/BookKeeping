using BookingKeeping.Common.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BookingKeeping.Common.Implement
{
    public class ErrorLog : IErrorLog
    {
        private readonly string _LogPathRoot;

        public ErrorLog(string LogPathRoot)
        {
            _LogPathRoot = LogPathRoot;
        }

        public void WriterLog(string Exception)
        {
            string LogContent = $"{DateTime.Now.ToString()}：\r\n{Exception}\r\n";

            string LogFolderPath = GetFolderPath(_LogPathRoot);
            string LogFilePath = GetFilePath(LogFolderPath);

            File.AppendAllText(LogFilePath, LogContent);
        }

        private string GetFolderPath(string LogPath)
        {
            string FolderName = DateTime.Now.ToString("yyyyMM");
            string FolderPath = Path.Combine(LogPath, FolderName);

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            return FolderPath;
        }

        private string GetFilePath(string FolderPath)
        {
            string LogFileName = $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
            string LogFilePath = Path.Combine(FolderPath, LogFileName);

            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }

            return LogFilePath;
        }
    }
}
