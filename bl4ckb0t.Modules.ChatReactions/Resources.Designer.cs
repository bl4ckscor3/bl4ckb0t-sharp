﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bl4ckb0t.Modules.ChatReactions {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ChatReactions.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to afkeating[öÖäÄüÜßA-Za-z0-9]*.
        /// </summary>
        internal static string regex {
            get {
                return ResourceManager.GetString("regex", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ba.
        /// </summary>
        internal static string reply {
            get {
                return ResourceManager.GetString("reply", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to afkeating.
        /// </summary>
        internal static string trigger {
            get {
                return ResourceManager.GetString("trigger", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Wishes bon appetit if &quot;{0}&quot; got sent into the channel..
        /// </summary>
        internal static string usage1 {
            get {
                return ResourceManager.GetString("usage1", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shrugs for you, if you write &quot;/me shrugs&quot;.
        /// </summary>
        internal static string usage2 {
            get {
                return ResourceManager.GetString("usage2", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcomes you back, if you write &quot;re insert_anything_here&quot;.
        /// </summary>
        internal static string usage3 {
            get {
                return ResourceManager.GetString("usage3", resourceCulture);
            }
        }
    }
}
