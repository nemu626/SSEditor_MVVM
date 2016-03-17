using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSEditor.Model
{
    public class Theme
    {
        public const int PropNum = 15;

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
            BlocksEditor_BackGroundHex = "#1e1e1e";
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
        public Theme(string themeName, string[] colors)
        {
            if (colors.Count() == PropNum)
            {
                themeName = ThemeName;
                PlainEditor_ForeGroundHex = colors[0];
                PlainEditor_BackGroundHex = colors[1];
                BlocksEditor_ForeGroundHex = colors[2];
                BlocksEditor_BackGroundHex = colors[3];
                UI_ForeGroundHex = colors[4];
                UI_BackGroundHex = colors[5];
                UI_BorderHex = colors[6];

                TabControl_BackGroundHex = colors[7];
                TabItem_ForeGroundHex = colors[8];
                TabItem_BackGroundHex = colors[9];
                TabItem_SelectedBackGroundHex = colors[10]; //Blue


                Button_ForeGroundHex = colors[11];
                Button_BackGroundHex = colors[12];   //Purple

                Contols_ForeGroundHex = colors[13];
                Contols_BackGroundHex = colors[14];
            }
        }

        #region Default Themes
        public static readonly Theme DarkTheme =
            new Theme("Dark",
                new string[15]{
                    "#FFFFFF",
                    "#1e1e1e",
                    "#FFFFFF",
                    "#1e1e1e",
                    "#FFFFFF",
                    "#2d2d30",
                    "#007acc",
                    "#2d2d30",
                    "#FFFFFF",
                    "#2d2d30",
                    "#007acc",
                    "#FFFFFF",
                    "#551a8b",
                    "#FFFFFF",
                    "#161E20"
                    });
        
        #endregion



    }
}
