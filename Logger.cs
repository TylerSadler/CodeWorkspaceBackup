using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeWorkspaceBackup
{
    class Logger
    {
        private string path;
        private string file;

        public Logger(string path)
        {
            string date = DateTime.UtcNow.ToString().Replace("/", "-").Replace(":", "").Replace(" ", "");

            this.path = path + "\\Logs";
            this.file = this.path + "\\Log-WorkspaceBackup-" + date + ".txt";

            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(this.path);
            }

            CreateNewLog();
        }

        public void WriteLine(string text)
        {
            using (StreamWriter file = new StreamWriter(this.file, true))
            {
                file.WriteLine(text);
            }
        }

        private void CreateNewLog()
        {
            var newFile = File.Create(this.file);
            newFile.Flush();
            newFile.Close();
        }
    }
}
