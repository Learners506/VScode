﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZZZIFOX.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.10.0.0")]
    internal sealed partial class NewCopy : global::System.Configuration.ApplicationSettingsBase {
        
        private static NewCopy defaultInstance = ((NewCopy)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new NewCopy())));
        
        public static NewCopy Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("1*1000,2*2000")]
        public string TextString {
            get {
                return ((string)(this["TextString"]));
            }
            set {
                this["TextString"] = value;
            }
        }
    }
}
