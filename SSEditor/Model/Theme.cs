using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.Model
{
    public class Theme
    {
        public string ThemeName;

        public string PlainEditor_ForeGroundHex { get; set; }
        public string PlainEditor_BackGroundHex { get; set; }

        public string BlocksEditor_ForeGroundHex { get; set; }
        public string BlocksEditor_BackGroundHex { get; set; }

        public string UI_ForeGroundHex { get; set; }
        public string UI_BackGroundHex { get; set; }
        public string UI_BorderHex { get; set; }

        public string TabControl_BackGroundHex { get; set; }
        public string TabItem_ForeGroundHex { get; set; }
        public string TabItem_BackGroundHex { get; set; }
        public string TabItem_SelectedBackGroundHex { get; set; }

        public string Button_ForeGroundHex { get; set; }
        public string Button_BackGroundHex { get; set; }

        public string Contols_ForeGroundHex { get; set; }
        public string Contols_BackGroundHex { get; set; }

        public Theme()
        {
            ThemeName = "Dark";

            PlainEditor_ForeGroundHex = "#FFFFFF";
            PlainEditor_BackGroundHex = "#1e1e1e";
            BlocksEditor_ForeGroundHex = "#FFFFFF";
            BlocksEditor_BackGroundHex = "#000000";
            UI_ForeGroundHex = "#FFFFFF";
            UI_BackGroundHex = "#2d2d30";
            UI_BorderHex = "#007acc";

            TabControl_BackGroundHex = "#2d2d30";
            TabItem_ForeGroundHex = "#FFFFFF";
            TabItem_BackGroundHex = "#2d2d30";
            TabItem_SelectedBackGroundHex = "#007acc"; //Blue


            Button_ForeGroundHex = "#FFFFFF";   
            Button_BackGroundHex = "#551a8b";   //Purple

            Contols_ForeGroundHex = "#FFFFFF";
            Contols_BackGroundHex = "#161E20";
            

        }
    }
}
