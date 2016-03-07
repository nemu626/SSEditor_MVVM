using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor
{
    public static class FileManager
    {
        public const string ExtSST = ".sst";
        public const string ExtTXT = ".txt";
        public const string FilterSST = "SSEditor documents(.sst)|*.sst";
        public const string FilterTXT = "Text files(.txt)|*.txt";

        #region　汎用
        static public FileDialog DlgFilter(FileDialog dlg,string ext,string filter, string fileName = null)
        {
            dlg.DefaultExt = ext;
            dlg.Filter = filter;
            if (!String.IsNullOrEmpty(fileName))
                dlg.FileName = fileName;
            return dlg;
        }

        static public object Deserialize(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                var se = new BinaryFormatter();

                return se.Deserialize(fs);
            }
        }
        static public void Serialize(object obj, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var se = new BinaryFormatter();
                se.Serialize(fs, obj);
            }
        }
        static public void WriteFile(string filePath,string text)
        {
            using (StreamWriter sw = new StreamWriter(filePath, false))
            {
                sw.Write(text);
                sw.Close();
            }
        }
        static public string ReadFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath, false))
            {
                return sr.ReadToEnd();
            }
        }
        #endregion

        static public FileDialog DlgFilterSST(FileDialog dlg, string fileName = null)
        {
            return DlgFilter(dlg, ExtSST, FilterSST, fileName);
        }
        static public FileDialog DlgFilterTXT(FileDialog dlg, string fileName = null)
        {
            return DlgFilter(dlg, ExtTXT, FilterTXT, fileName);
        }
        static public Project DeserializeProject(string filePath)
        {
            return (Project)Deserialize(filePath);
        }
        static public void SerializeProject(Project project, string filePath)
        {
            Serialize(project, filePath);
        }

    }
}
