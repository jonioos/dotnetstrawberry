using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetstrawberry
{
    /// <summary>
    /// La classe FileStructure serve a contenere le informazioni di un file
    /// </summary>
    public class FileStructure
    {
        private string name;
        private string directory;
        private string extension;
        private decimal size;
        private DateTime lastModifiedTime;
        private byte[] hashvalue;

        #region Properties
        /// <summary>
        /// Nome del file
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if(value is string)
                {
                    if (value != "")
                    {
                        name = value;
                    }
                    else
                    {
                        throw new ArgumentNullException("Il campo nome non può essere vuoto");
                    }
                }
                else
                {
                    throw new FormatException("Il campo deve essere una stringa");
                }                
            }
        }
        /// <summary>
        /// Percorso originale del file
        /// </summary>
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
                        throw new ArgumentNullException("Il campo directory non può essere vuoto");
                    }
                }
                else
                {
                    throw new FormatException("Il campo deve essere una stringa");
                }
            }
        }
        /// <summary>
        /// Estensione del file
        /// </summary>
        public string Extension
        {
            get { return extension; }
            set
            {
                if (value is string)
                {
                    if (value != "")
                    {
                        if(value[0] == '.')
                        {
                            extension = value;
                        }
                        else
                        {
                            throw new FormatException("Il campo non è un estensione.");
                        }
                    }
                    else
                    {
                        throw new ArgumentNullException("Il campo directory non può essere vuoto");
                    }
                }
                else
                {
                    throw new FormatException("Il campo deve essere una stringa");
                }
            }
        }
        /// <summary>
        /// Dimensioni del file
        /// </summary>
        public decimal Size
        {
            get { return size; }
            set
            {
                if(value is decimal)
                {
                    size = value;
                }
                else
                {
                    throw new FormatException("Il campo deve essere decimal");
                }
            }
        }
        /// <summary>
        /// Data dell'ultima modifica effettuata al file
        /// </summary>
        public DateTime LastModifiedTime
        {
            get { return lastModifiedTime; }
            set
            {
                if(value is DateTime)
                {
                    lastModifiedTime = value;
                }
                else
                {
                    throw new FormatException("Il formato deve essere DateTime");
                }
            }
        }
        /// <summary>
        /// Calcolo hash del file
        /// </summary>
        public byte[] HashValue
        {
            get { return hashvalue; }
            set
            {
                if(value is byte[])
                {
                    hashvalue = value;
                }
                else
                {
                    throw new FormatException("Il formato deve essere byte[]");
                }
            }
        }

        #endregion

        #region Constructor
        public FileStructure(string n, string d, string ex, decimal s, DateTime lmt, byte[] h)
        {
            Name = n;
            Directory = d;
            Extension = ex;
            Size = s;
            LastModifiedTime = lmt;
            HashValue = h;
        }
        public FileStructure(string n, string d, string ex, decimal s, DateTime lmt)
        {
            Name = n;
            Directory = d;
            Extension = ex;
            Size = s;
            LastModifiedTime = lmt;
        }
        public FileStructure() { }
        #endregion
    }
}
