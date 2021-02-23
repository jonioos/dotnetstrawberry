using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
namespace dotnetstrawberry
{
    class FindDuplicate : EasyReorder
    {
        
        private static List<FileStructure> fileDatabaseClone = new List<FileStructure>();
        private static List<FileStructure> fileDatabase = new List<FileStructure>();
        private static new string report;

        public static dynamic Find(string path)
        {
            fileDatabase = FilesInsideDir(path, true);
            List<DuplicateFile> toReturnList = new List<DuplicateFile>();
            fileDatabaseClone = fileDatabase;
            fileDatabaseClone.Reverse();
            foreach (var item in fileDatabase)
            {
                foreach(var itemClone in fileDatabaseClone)
                {
                    Debug.WriteLine($"{item.Name}|{itemClone.Name}");
                    if (item.HashValue == itemClone.HashValue && item.Directory != itemClone.Directory)
                    {
                        report += $"{item.Name}|{item.Directory} è uguale a {itemClone.Name}|{itemClone.Directory}{Environment.NewLine}";
                        //report += $"{item.Name}|{item.Directory}|{itemClone.Name}|{itemClone.Directory}";
                        var duplicate = new DuplicateFile();
                        duplicate.Name = item.Name;
                        duplicate.NameClone = itemClone.Name;
                        duplicate.Directory = item.Directory;
                        duplicate.DirectoryClone = itemClone.Directory;
                        duplicate.HashCalc = item.HashValue;
                        duplicate.HashCalcClone = itemClone.HashValue;
                        toReturnList.Add(duplicate);
                    }
                }
            }
            
            return toReturnList;
        }

        public static dynamic Find(string path, bool Report)
        {
            if (Report)
            {
                Find(path);
                return report;
            }
            else
            {
                return Find(path);
            }
        }
    }
}
