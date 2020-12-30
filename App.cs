using System;
using System.IO;

namespace CodeWorkspaceBackup
{
    class App
    {
        static void Main(string[] args)
        {
            CopyDirectoriesAndFiles cpyDirFil = new CopyDirectoriesAndFiles();

            string copyFrom = "C:\\WorkSpace";
            string copyTo = "T:\\WorkSpace";

            cpyDirFil.CopyAll(copyFrom, copyTo);
        }
    }
}
