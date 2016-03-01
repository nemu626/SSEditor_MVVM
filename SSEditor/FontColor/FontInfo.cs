using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ColorFont
{
    public class FontInfo
    {
        public FontFamily Family { get; set; }
        public double Size { get; set; }
        public FontStyle Style { get; set; }
        public FontStretch Stretch { get; set; }
        public FontWeight Weight { get; set; }
        public SolidColorBrush BrushColor { get; set; }

        #region Static Utils

        public static string TypefaceToString(FamilyTypeface ttf)
        {
            StringBuilder sb = new StringBuilder(ttf.Stretch.ToString());
            sb.Append("-");
            sb.Append(ttf.Weight.ToString());
            sb.Append("-");
            sb.Append(ttf.Style.ToString());
            return sb.ToString();
        }

        public static void ApplyFont(Control control, FontInfo font)
        {
            control.FontFamily = font.Family;
            control.FontSize = font.Size;
            control.FontStyle = font.Style;
            control.FontStretch = font.Stretch;
            control.FontWeight = font.Weight;
            control.Foreground = font.BrushColor;
        }

        public static FontInfo GetControlFont(Control control)
        {
            FontInfo font = new FontInfo();
            font.Family = control.FontFamily;
            font.Size = control.FontSize;
            font.Style = control.FontStyle;
            font.Stretch = control.FontStretch;
            font.Weight = control.FontWeight;
            font.BrushColor = (SolidColorBrush)control.Foreground;
            return font;
        }
        #endregion

        public FontInfo()
        {
            Family = new FontFamily("Times New Roman");
            Size = 16.0F;
            Style = FontStyles.Normal;
            Stretch = FontStretches.Normal;
            Weight = FontWeights.Normal;
            BrushColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255,255,255,255));

        }

        public FontInfo(FontFamily fam, double sz, FontStyle style, 
                        FontStretch strc, FontWeight weight, SolidColorBrush c)
        {
            this.Family = fam;
            this.Size = sz;
            this.Style = style;
            this.Stretch = strc;
            this.Weight = weight;
            this.BrushColor = c;
        }

        public FontColor Color
        {
            get
            {
                return AvailableColors.GetFontColor(this.BrushColor);
            }
        }

        public FamilyTypeface Typeface
        {
            get
            {
                FamilyTypeface ftf = new FamilyTypeface();
                ftf.Stretch = this.Stretch;
                ftf.Weight = this.Weight;
                ftf.Style = this.Style;
                return ftf;
            }
        }



    }

    /*
    * This Class is just use for Serialize 

        style, stretch, weightはまだ未実装。
    */
    [Serializable]
    public class SerializableFontInfo
    {
        public string family { get; set; }
        public double size { get; set; }
        public string style { get; set; }
        public string stretch { get; set; }
        public string weigth { get; set; }
        public string colorHex;

        public SerializableFontInfo()
        { }
        public SerializableFontInfo(FontInfo f)
        {
            FontStyleConverter fsc = new FontStyleConverter();
            family = f.Family.Source;
            size = f.Size;
            colorHex = f.Color.Brush.Color.ToString();

        }
        public FontInfo convert2FontInfo()
        {
            FontInfo f = new FontInfo();
            f.Family = new FontFamily(this.family);
            f.Size = this.size;
            Color c = (Color)ColorConverter.ConvertFromString(this.colorHex);
            f.BrushColor = new SolidColorBrush(c);

            return f;
        }
    }
}
