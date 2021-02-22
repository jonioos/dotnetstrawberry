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
        /// <param name="oldDirectory">
        /// Directory iniziale
        /// </param>
        /// <param name="extension">
        /// Estensione di riferimento
        /// </param>
        /// <param name="newDirectory">
        /// Directory finale
        /// </param>
        public static void Reorder(string oldDirectory, string extension, string newDirectory)
        {
            if (Directory.Exists(oldDirectory))
            {
                fileDatabase = FilesInsideDir(oldDirectory);
                foreach (var item in fileDatabase)
                {
                    if (item.Extension == extension)
                    {
                        if (!Directory.Exists(newDirectory))
                            Directory.CreateDirectory(newDirectory);

                        if (!File.Exists(newDirectory + item.Name + item.Extension))
                        {
                            File.Move(item.Directory, newDirectory + @"\" + item.Name + item.Extension);
                            report += PrintReport(item.Name, item.Extension, item.Size);
                        }
                        else
                        {
                            //Duplicate
                            File.Move(item.Directory, newDirectory + @"\" + item.Name + "[dx]" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + item.Extension);
                            report += PrintReport(item.Name, item.Extension, item.Size);
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
