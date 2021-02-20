using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace dotnetstrawberry
{
    class AdvancedReorder : EasyReorder
    {
        private static List<FileStructure> fileDatabase = new List<FileStructure>();
        /// <summary>
        /// Funzione utile a riordinare una cartella
        /// </summary>
        /// <param name="oldDirectory"></param>
        /// <param name="extension"></param>
        /// <param name="newDirectory"></param>
        public static void Reorder(string oldDirectory, string extension, string newDirectory)
        {
            if (Directory.Exists(oldDirectory))
            {
                fileDatabase = FilesInsideDir(oldDirectory);
                foreach (var item in fileDatabase)
                {
                    if (item.extension == extension)
                    {
                        if (!Directory.Exists(newDirectory))
                            Directory.CreateDirectory(newDirectory);

                        if (!File.Exists(newDirectory + item.name + item.extension))
                        {
                            File.Move(item.directory, newDirectory + @"\" + item.name + item.extension);
                            report += PrintReport(item.name, item.extension, item.size);
                        }
                        else
                        {
                            //Duplicate
                            File.Move(item.directory, newDirectory + @"\" + item.name + "[dx]" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + item.extension);
                            report += PrintReport(item.name, item.extension, item.size);
                        }
                    }
                    fileDatabase = FilesInsideDir(oldDirectory);
                }
            }
            else
            {
                throw new TransferringErrorException("Directory non esistente");
            }
        }
    }
}
