using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
namespace dotnetstrawberry
{
    /// <summary>
    /// classe EasyReorder
    /// </summary>
    public class EasyReorder
    {
        /// <summary>
        /// Struttura file
        /// </summary>
        public struct FileStructure
        {
            public string name;
            public string directory;
            public string extension;
            public decimal size;
            public DateTime lastModifiedTime;
        }

        private static List<FileStructure> fileDatabase = new List<FileStructure>();

        //Database delle estensioni
        private static readonly string[] imageFormats = new string[] { ".png", ".jpg", ".jpeg", ".cr2", ".tiff", ".dng", ".xmp", ".pp3", ".gif" };
        private static readonly string[] videoFormats = new string[] { ".mp4", ".mov", ".m4v", ".3gp" };
        private static readonly string[] audioFormats = new string[] { ".mp3", ".m4a", ".wav", ".flac", ".wma", ".aac" };
        private static readonly string[] photoshopFormats = new string[] { ".psd", ".psb" };
        private static readonly string[] officeFormats = new string[] { ".docx", ".xlsx", ".pptx", ".pdf", ".txt", ".doc", ".xls", ".ppt", ".odf", ".odt", ".ods", ".dotx", ".xlsm", ".potx", ".one", ".pub", ".xps", ".accdb" };
        private static readonly string[] developersdevelopersdevelopers = new string[] { ".vb", ".cs", ".sh", ".py", ".cpp", ".h", ".html", ".css", ".js", ".m", ".xml", ".sln" };
        private static readonly string[] midiFormats = new string[] { ".mid", ".midi" };
        private static readonly string[] auditionFormats = new string[] { ".sesx", ".pkf" };
        private static readonly string[] mswinlinkFormats = new string[] { ".lnk" };
        private static readonly string[] exeFormats = new string[] { ".exe", ".bat", ".app" };
        private static readonly string[] libandiniFormats = new string[] { ".dll", ".lib", ".ini" };
        private static readonly string[] zipFormats = new string[] { ".zip", ".rar", ".7zip", ".7z", ".tar", ".tar.gz" };
        private static readonly string[] diskimageFormats = new string[] { ".iso", ".img", ".ima", ".dmg", ".udf" };
        /// <summary>
        /// Stringa di output del trasferimento
        /// </summary>
        public static string report;

        public static List<FileStructure> FilesInsideDir(string path)
        {
            List<FileStructure> local = new List<FileStructure>();
            string[] filelist = Directory.GetFiles(path);
            foreach (string file in filelist)
            {
                string nameFile;
                decimal size;
                DateTime lastModified;
                nameFile = Path.GetFileNameWithoutExtension(file);
                FileInfo informationFile = new FileInfo(file);
                size = informationFile.Length / 2 ^ 20;
                lastModified = informationFile.LastWriteTime;
                FileStructure obj = new FileStructure()
                {
                    name = nameFile,
                    directory = file,
                    size = size,
                    extension = GetExstension(file),
                    lastModifiedTime = lastModified
                };
                local.Add(obj);
            }
            return local;
        }

        /// <summary>
        /// Funzione utile a prendere l'estensione da un file, in minuscolo
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static string GetExstension(string p)
        {
            try
            {
                return Path.GetExtension(p).ToLower();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Funzione utile a scansionare i file in una directory ed aggiungerli ad una struttura dati
        /// </summary>
        private static void ScanFiles(string path)
        {
            List<FileStructure> local = new List<FileStructure>();
            string[] filelist = Directory.GetFiles(path);
            foreach (string file in filelist)
            {
                string nameFile;
                decimal size;
                DateTime lastModified;
                nameFile = Path.GetFileNameWithoutExtension(file);
                FileInfo informationFile = new FileInfo(file);
                size = informationFile.Length / 2 ^ 20;
                lastModified = informationFile.LastWriteTime;
                FileStructure obj = new FileStructure()
                {
                    name = nameFile,
                    directory = file,
                    size = size,
                    extension = GetExstension(file),
                    lastModifiedTime = lastModified
                };
                local.Add(obj);
            }
            fileDatabase = local;
        }


        /// <summary>
        /// Funzione utile a riordinare una cartella
        /// </summary>
        /// <param name="originalPath">Percorso originale</param>
        /// <param name="extension">Estensione</param>
        /// <param name="newDirectory">Nuova directory</param>
        public static void Reorder(string originalPath)
        {
            if (Directory.Exists(originalPath))
            {
                try
                {
                    ScanFiles(originalPath);
                    foreach (var item in fileDatabase)
                    {
                        if (File.Exists(item.directory))
                        {

                            //Image
                            if (Array.Exists(imageFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Immagini\";

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
                            //Video
                            if (Array.Exists(videoFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Video\";

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
                            //Music
                            if (Array.Exists(audioFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Audio\";

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
                            //Photoshop
                            if (Array.Exists(photoshopFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Photoshop\";

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
                            //Office
                            if (Array.Exists(officeFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Documenti\";

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
                            //Dev
                            if (Array.Exists(developersdevelopersdevelopers, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Codice Sorgente\";

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
                            //MIDI
                            if (Array.Exists(midiFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File MIDI\";

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
                            //
                            if (Array.Exists(mswinlinkFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Collegamenti\";

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
                            //exeFormats
                            if (Array.Exists(exeFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Eseguibili\";

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

                            //Audition
                            if (Array.Exists(auditionFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Audition\";

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

                            //Compressor
                            if (Array.Exists(zipFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Compressi\";

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

                            //DLL
                            if (Array.Exists(libandiniFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File DLL\";

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

                            //ISO
                            if (Array.Exists(diskimageFormats, ext => ext == item.extension))
                            {
                                string newDirectory = originalPath + @"\File Immagine disco\";

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
                        ScanFiles(originalPath);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        /// <summary>
        /// Funzione utile a stampare un report del file appena trasferito
        /// </summary>
        /// <param name="nameFile"></param>
        /// <param name="extension"></param>
        /// <param name="originalPath"></param>
        /// <param name="newPath"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static string PrintReport(string nameFile, string extension, decimal size)
        {
            string toreturnreport = $"Trasferring {nameFile}{extension} size: {size}{Environment.NewLine}";
            return toreturnreport;
        }
    }
}
