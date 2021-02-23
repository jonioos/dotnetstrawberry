using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
namespace dotnetstrawberry
{
    /// <summary>
    /// classe EasyReorder
    /// </summary>
    public class EasyReorder
    {

        private static List<FileStructure> fileDatabase = new List<FileStructure>();
        #region Database Estensioni
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
        #endregion
        /// <summary>
        /// Stringa di output del trasferimento
        /// </summary>
        public static string report;

        /// <summary>
        /// Metodo utile per ricercare i file all'interno di una directory e inserirli in un database FileStructure
        /// </summary>
        /// <param name="path">
        /// Percorso
        /// </param>
        /// <returns>
        /// Lista di tutti i file all'interno di una directory nel formato FileStructure
        /// </returns>
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
                    Name = nameFile,
                    Directory = file,
                    Size = size,
                    Extension = GetExstension(file),
                    LastModifiedTime = lastModified
                };
                local.Add(obj);
            }
            return local;
        }
        /// <summary>
        /// Metodo utile per ricercare ricorsivamente i file all'interno di una directory e inserirli in un database FileStructure
        /// </summary>
        /// <param name="path">
        /// Percorso
        /// </param>
        /// <param name="recursive">
        /// Ricorsivo
        /// </param>
        /// <returns>
        /// Lista di tutti i file all'interno di una directory, e delle sue sottodirectory, nel formato FileStructure
        /// </returns>
        public static List<FileStructure> FilesInsideDir(string path, bool recursive)
        {
            if (recursive)
            {
                List<FileStructure> local = new List<FileStructure>();
                    foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
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
                            Name = nameFile,
                            Directory = file,
                            Size = size,
                            Extension = GetExstension(file),
                            LastModifiedTime = lastModified,
                            HashValue = SHA256calc(file)
                            
                        };
                        local.Add(obj);
                    }
                    foreach(var item in FilesInsideDir(path))
                    {
                        local.Add(item);
                    }
                return local;
            }
            else
            {   
                return FilesInsideDir(path); 
            }
        }
        private static byte[] SHA256calc(string p)
        {
            FileInfo files = new FileInfo(p);
            using (SHA256 mySHA256 = SHA256.Create())
                try
                {
                    FileStream fileStream = files.Open(FileMode.Open);
                    fileStream.Position = 0;
                    byte[] hashValue = mySHA256.ComputeHash(fileStream);
                    fileStream.Close();
                    return hashValue;
                }
                catch (IOException e)
                {
                    throw new IOException($"Errore: {e.Message}");
                }
                catch (UnauthorizedAccessException e)
                {
                    throw new UnauthorizedAccessException($"Errore: {e.Message}");
                }     
            throw new Exception("Errore durante il calcolo");
        }

        private static string FromByteToString(byte[] hashcalc)
        {
            string result = "";
            foreach (var car in hashcalc)
            {
                result += car.ToString();
            }
            return result;
        }

        /// <summary>
        /// Funzione utile a prendere l'estensione da un file, in minuscolo
        /// </summary>
        /// <param name="p">
        /// Percorso del file
        /// </param>
        /// <returns>
        /// Estensione del file, tutto in minuscolo.
        /// </returns>
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
                    Name = nameFile,
                    Directory = file,
                    Size = size,
                    Extension = GetExstension(file),
                    LastModifiedTime = lastModified
                };
                local.Add(obj);
            }
            fileDatabase = local;
        }
        /// <summary>
        /// Funzione utile a riordinare una cartella
        /// </summary>
        /// <param name="originalPath">
        /// Percorso originale
        /// </param>
        public static void Reorder(string originalPath)
        {
            if (Directory.Exists(originalPath))
            {
                try
                {
                    ScanFiles(originalPath);
                    foreach (var item in fileDatabase)
                    {
                        if (File.Exists(item.Directory))
                        {

                            //Image
                            if (Array.Exists(imageFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Immagini\";

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
                            //Video
                            if (Array.Exists(videoFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Video\";

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
                            //Music
                            if (Array.Exists(audioFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Audio\";

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
                            //Photoshop
                            if (Array.Exists(photoshopFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Photoshop\";

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
                            //Office
                            if (Array.Exists(officeFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Documenti\";

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
                            //Dev
                            if (Array.Exists(developersdevelopersdevelopers, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Codice Sorgente\";

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
                            //MIDI
                            if (Array.Exists(midiFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File MIDI\";

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
                            //
                            if (Array.Exists(mswinlinkFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Collegamenti\";

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
                            //exeFormats
                            if (Array.Exists(exeFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Eseguibili\";

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

                            //Audition
                            if (Array.Exists(auditionFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Audition\";

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

                            //Compressor
                            if (Array.Exists(zipFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Compressi\";

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

                            //DLL
                            if (Array.Exists(libandiniFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File DLL\";

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

                            //ISO
                            if (Array.Exists(diskimageFormats, ext => ext == item.Extension))
                            {
                                string newDirectory = originalPath + @"\File Immagine disco\";

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
        /// <param name="nameFile">
        /// Nome del file trasferito
        /// </param>
        /// <param name="extension">
        /// Estensione del file
        /// </param>
        /// <param name="size">
        /// Dimensioni del file
        /// </param>
        /// <returns></returns>
        public static string PrintReport(string nameFile, string extension, decimal size)
        {
            string toreturnreport = $"Trasferring {nameFile}{extension} size: {size}{Environment.NewLine}";
            return toreturnreport;
        }
        /// <summary>
        /// Struttura dei file duplicati
        /// </summary>
        public struct DuplicateFile
        {
            private string name;
            private string directory;
            private string nameClone;
            private string directoryClone;
            private byte[] hashCalc;
            private byte[] hashCalcClone;

            #region Properties
            public string Name
            {
                get { return name; }
                set
                {
                    if (value is string)
                    {
                        if (value != "")
                        {
                            name = value;
                        }
                        else
                        {
                            throw new NullReferenceException("Il valore non può essere nullo");
                        }
                    }
                    else
                    {
                        throw new FormatException("Il valore deve essere una stringa");
                    }
                }
            }

            public string Directory
            {
                get { return directory; }
                set
                {
                    if (value is string)
                    {
                        if (value != "")
                        {
                            directory = value;
                        }
                        else
                        {
                            throw new NullReferenceException("Il valore non può essere nullo");
                        }
                    }
                    else
                    {
                        throw new FormatException("Il valore deve essere una stringa");
                    }
                }
            }

            public string NameClone
            {
                get { return nameClone; }
                set
                {
                    if (value is string)
                    {
                        if (value != "")
                        {
                            nameClone = value;
                        }
                        else
                        {
                            throw new NullReferenceException("Il valore non può essere nullo");
                        }
                    }
                    else
                    {
                        throw new FormatException("Il valore deve essere una stringa");
                    }
                }
            }

            public string DirectoryClone
            {
                get { return directoryClone; }
                set
                {
                    if (value is string)
                    {
                        if (value != "")
                        {
                            directoryClone = value;
                        }
                        else
                        {
                            throw new NullReferenceException("Il valore non può essere nullo");
                        }
                    }
                    else
                    {
                        throw new FormatException("Il valore deve essere una stringa");
                    }
                }
            }

            public byte[] HashCalc { get => hashCalc; set => hashCalc = value; }

            public byte[] HashCalcClone { get => hashCalcClone; set => hashCalcClone = value; }
            #endregion

        }
        /// <summary>
        /// Metodo utile a stampare le strutture che compongono i file e le strutture dei file duplicati
        /// </summary>
        /// <param name="database">
        /// Database in formato FileStructure o DuplicateFile
        /// </param>
        /// <param name="path">
        /// Percorso dove verrà salvato il report
        /// </param>
        public static void PrintList(object database, string path)
        {
            if (database is List<FileStructure>)
            {
                var d = (List<FileStructure>)database;
                string r = "";
                foreach (var item in d)
                {
                    r += $"{item.Directory}|{item.Name}|{item.HashValue.ToString()}|{Environment.NewLine}";
                }
                File.WriteAllText(path, r);
            }
            else if (database is List<DuplicateFile>)
            {
                var d = (List<DuplicateFile>)database;
                string r = "";
                foreach (var item in d)
                {
                    r += $"{item.Directory}|{item.Name}{Environment.NewLine}";
                }
                File.WriteAllText(path, r);
            }
        }
    }
}
