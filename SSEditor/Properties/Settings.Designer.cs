﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace SSEditor.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("16")]
        public int PlainEditor_Font_Size {
            get {
                return ((int)(this["PlainEditor_Font_Size"]));
            }
            set {
                this["PlainEditor_Font_Size"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Meiryo")]
        public string PlainEditor_FontFamily {
            get {
                return ((string)(this["PlainEditor_FontFamily"]));
            }
            set {
                this["PlainEditor_FontFamily"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#FFFFFF")]
        public string PlainEditor_Font_ForeGroundHex {
            get {
                return ((string)(this["PlainEditor_Font_ForeGroundHex"]));
            }
            set {
                this["PlainEditor_Font_ForeGroundHex"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#161E20")]
        public string PlainEditor_Font_BackGroundHex {
            get {
                return ((string)(this["PlainEditor_Font_BackGroundHex"]));
            }
            set {
                this["PlainEditor_Font_BackGroundHex"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("#000000")]
        public string BlocksEditor_BackGroundHex {
            get {
                return ((string)(this["BlocksEditor_BackGroundHex"]));
            }
            set {
                this["BlocksEditor_BackGroundHex"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("800")]
        public int Window_Width {
            get {
                return ((int)(this["Window_Width"]));
            }
            set {
                this["Window_Width"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("640")]
        public string Window_Heigth {
            get {
                return ((string)(this["Window_Heigth"]));
            }
            set {
                this["Window_Heigth"] = value;
            }
        }
    }
}
