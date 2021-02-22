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
        /// <param name="oldDirectory">
        /// Percorso originale
        /// </param>
        /// <param name="extension">
        /// Estensione
        /// </param>
        /// <param name="initialDate">
        /// Data iniziale del range
        /// </param>
        /// <param name="finalDate">
        /// Data finale del range
        /// </param>
        /// <param name="includeDay">
        /// Includere o no il giorno esatto nel nome della cartella di destinazione
        /// </param>
        public static void Reorder(string oldDirectory, string extension, DateTime initialDate, DateTime finalDate, bool includeDay)
        {
            if (DateParam(initialDate) > DateParam(initialDate))
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

                    if (DateParam(item.LastModifiedTime) >= DateParam(initialDate) && DateParam(item.LastModifiedTime) <= DateParam(finalDate))
                    {
                        if(extension == ".*")
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
                        else
                        {
                            if(item.Extension == extension.ToLower())
                            {
                                newDirectory += $" ({item.Extension})";
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
        /// <returns>
        /// Nome del mese in italiano
        /// </returns>
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
        /// <summary>
        /// Funzione utile a ritornare una data senza l'orario nel formato gg/mm/YYYY
        /// </summary>
        /// <param name="d">
        /// Data qualsiasi in formato DateTime
        /// </param>
        /// <returns>
        /// Ritorna la data nel formato gg/mm/YYYY
        /// </returns>
        private static DateTime DateParam(DateTime d)
        {
            int day = d.Day;
            int month = d.Month;
            int year = d.Year;
            DateTime daritornare = DateTime.Parse($"{d.Day}/{d.Month}/{d.Year}");
            return daritornare;
        }
    }
}
