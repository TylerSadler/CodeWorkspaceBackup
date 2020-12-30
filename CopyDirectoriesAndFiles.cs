using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeWorkspaceBackup
{
    class CopyDirectoriesAndFiles
    {
        private static Logger log;

        public void CopyAll(string copyFrom, string copyTo)
        {
            string logPath = Directory.GetCurrentDirectory();

            log = new Logger(logPath);

            log.WriteLine("Main - Copy From: " + copyFrom + " Copy To: " + copyTo);
            log.WriteLine("");

            log.WriteLine("Starting.....");
            log.WriteLine("");

            CopyFiles(copyFrom, copyTo);
            CopyDirectories(copyFrom, copyTo);

            log.WriteLine("Finished");
        }

        private static void CopyDirectories(string fromDirectory, string destination)
        {
            string[] dirPaths = Directory.GetDirectories(fromDirectory, "*", SearchOption.AllDirectories);

            foreach (var dirPath in dirPaths)
            {
                string copyDir = dirPath.Replace(fromDirectory, destination);

                if (!Directory.Exists(copyDir))
                {
                    try
                    {
                        Directory.CreateDirectory(copyDir);
                    }
                    catch (Exception ex)
                    {
                        log.WriteLine("+++++++++++ ERROR - " + copyDir + " +++++++++++");
                        log.WriteLine(ex.Message);
                        log.WriteLine("++++++++++++++++++++++");
                        log.WriteLine("");
                    }
                }

                CopyFiles(dirPath, copyDir);

            }
        }

        private static void CopyFiles(string fromDirectory, string destination)
        {
            string[] filePaths = Directory.GetFiles(fromDirectory);

            foreach (var filename in filePaths)
            {
                string originalFile = filename.ToString();
                string copyOfFile = originalFile.ToString().Replace(fromDirectory, destination);


                try
                {
                    if (!File.Exists(copyOfFile))
                    {
                        File.Copy(originalFile, copyOfFile);

                        log.WriteLine(copyOfFile);
                        log.WriteLine("Copied successfully.");
                        log.WriteLine("");
                    }
                    else if (File.Exists(copyOfFile) && FileIsNewer(originalFile, copyOfFile))
                    {
                        File.Copy(originalFile, copyOfFile, true);

                        log.WriteLine(copyOfFile);
                        log.WriteLine("Updated successfully.");
                        log.WriteLine("");
                    }
                    else
                    {
                        log.WriteLine(copyOfFile);
                        log.WriteLine("Was already up to date.");
                        log.WriteLine("");
                    }
                }
                catch (Exception ex)
                {
                    log.WriteLine("ERROR: " + ex.Message);
                    log.WriteLine("");
                }
            }
        }

        private static Boolean FileIsNewer(string originalFile, string copyOfFile)
        {
            Boolean result = false;

            DateTime ofTime = File.GetLastWriteTimeUtc(originalFile);
            DateTime cfTime = File.GetLastWriteTimeUtc(copyOfFile);

            if (ofTime > cfTime)
            {
                result = true;
            }

            return result;
        }
    }
}
