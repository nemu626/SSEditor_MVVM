using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.Model
{
    public class AppOption
    {
        public int PlainEditor_Font_Size { get; set; }

        public string PlainEditor_FontFamily { get; set; }

        public string PlainEditor_Font_ForeGroundHex { get; set; }
        public string PlainEditor_Font_BackGroundHex { get; set; }

        public string BlocksEditor_BackGroundHex { get; set; }

        public string UI_ForeGroundHex { get; set; }
        public string UI_BackGroundHex { get; set; }

        public int Window_Width { get; set; }
        public int Window_Heigth { get; set; }

        public string Default_Dirpath { get; set; }

        



        public AppOption()
        {
            PlainEditor_Font_Size = 16;
            PlainEditor_FontFamily = "Meiryo";

            PlainEditor_Font_ForeGroundHex = "#FFFFFF";
            PlainEditor_Font_BackGroundHex = "#161E20";

            UI_ForeGroundHex = "#FFFFFF";
            UI_BackGroundHex = "#161E20";

            BlocksEditor_BackGroundHex = "#000000";
            
            Window_Width = 800;
            Window_Heigth = 640;

            Default_Dirpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        }


    }
}
