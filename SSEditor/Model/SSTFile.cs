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
    public static class SSTFile
    {
        public const string DotSST = ".sst";
        public const string FilterString = "SSEditor documents(.sst)|*.sst";

        static public FileDialog DlgFilterSST(FileDialog dlg, string fileName = null)
        {
            dlg.DefaultExt = DotSST;
            dlg.Filter = FilterString;
            if (!String.IsNullOrEmpty(fileName))
                dlg.FileName = fileName;
            return dlg;
        }
        #region 

        static public Project Open(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                var se = new BinaryFormatter();

                return (Project)se.Deserialize(fs);
            }
        }

        static public void Save(Project project, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var se = new BinaryFormatter();
                se.Serialize(fs, project);
            }
        }
        #endregion

    }
}
