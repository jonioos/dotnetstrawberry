using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace dotnetstrawberry
{
    class ByDateReorder : EasyReorder
    {
        private static List<FileStructure> fileDatabase = new List<FileStructure>();
        /// <summary>
        /// Funzione utile a riordinare una cartella
        /// </summary>
        /// <param name="oldDirectory"></param>
        /// <param name="extension"></param>
        /// <param name="newDirectory"></param>
        public static void Reorder(string oldDirectory, string extension, DateTime initialDate, DateTime finalDate, bool includeDay)
        {
            if(initialDate < finalDate)
                throw new Exception("Errore, la data finale non può superare la data iniziale");
           
            if (Directory.Exists(oldDirectory))
            {
                fileDatabase = FilesInsideDir(oldDirectory);
                foreach (var item in fileDatabase)
                {
                    string newDirectory;
                    if (includeDay)
                        newDirectory = oldDirectory + $@"\File dal {initialDate.Day} {MeseInItaliano(initialDate.Month)} {initialDate.Year} al {finalDate.Day} {MeseInItaliano(finalDate.Month)} {finalDate.Year}";
                    else
                        newDirectory = oldDirectory + $@"\File da {MeseInItaliano(initialDate.Month)} {initialDate.Year} a {MeseInItaliano(finalDate.Month)} {finalDate.Year}";

                    if (item.lastModifiedTime >= initialDate && item.lastModifiedTime <= finalDate)
                    {
                        if(extension == ".*")
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
                        else
                        {
                            if(item.extension == extension.ToLower())
                            {
                                newDirectory += $" ({item.extension})";
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
        /// <summary>
        /// Funzione utile ad ottenere il mese per intero in lingua italiana
        /// </summary>
        /// <param name="mese"></param>
        /// <returns></returns>
        private static string MeseInItaliano(int mese)
        {
            switch (mese)
            {
                case 1:
                    return "gennaio";
                case 2:
                    return "febbraio";
                case 3:
                    return "marzo";
                case 4:
                    return "aprile";
                case 5:
                    return "maggio";
                case 6:
                    return "giugno";
                case 7:
                    return "luglio";
                case 8:
                    return "agosto";
                case 9:
                    return "settembre";
                case 10:
                    return "ottobre";
                case 11:
                    return "novembre";
                case 12:
                    return "dicembre";
                default:
                    return "";
            }
        }
    }
}
