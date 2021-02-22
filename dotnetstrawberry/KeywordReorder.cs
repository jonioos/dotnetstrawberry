using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace dotnetstrawberry
{
    class KeywordReorder : EasyReorder
    {
        private static List<FileStructure> fileDatabase = new List<FileStructure>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldDirectory">
        /// Directory originale del file
        /// </param>
        /// <param name="Keyword">
        /// Parola chiave che si vuole ricercare nella cartella
        /// </param>
        public static void Reorder(string oldDirectory, string Keyword)
        {
            if (Directory.Exists(oldDirectory))
            {
                fileDatabase = FilesInsideDir(oldDirectory);
                foreach (var item in fileDatabase)
                {
                    string newDirectory = oldDirectory + $@"\{Keyword}";
                    if (item.Name.ToLower().Contains(Keyword.ToLower()))
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
                throw new TransferringErrorException("Errore durante il trasferimento del file");
            }

        }
    }
}
