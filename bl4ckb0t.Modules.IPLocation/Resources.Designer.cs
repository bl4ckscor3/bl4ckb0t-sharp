﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace bl4ckb0t.Modules.IPLocation {
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("IPLocation.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to City:&amp;g {0}.
        /// </summary>
        internal static string city {
            get {
                return ResourceManager.GetString("city", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Country:&amp;g {0}.
        /// </summary>
        internal static string country {
            get {
                return ResourceManager.GetString("country", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Powered by:&amp;g {0}.
        /// </summary>
        internal static string credit {
            get {
                return ResourceManager.GetString("credit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The input is invalid..
        /// </summary>
        internal static string http404 {
            get {
                return ResourceManager.GetString("http404", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can use hostnames, host addresses, IPv4 and IPv6 addresses. Please note that the result is not 100% correct.
        /// </summary>
        internal static string notes {
            get {
                return ResourceManager.GetString("notes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Region:&amp;g {0}.
        /// </summary>
        internal static string region {
            get {
                return ResourceManager.GetString("region", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to %cmd%iplocation &lt;ip&gt;.
        /// </summary>
        internal static string syntax {
            get {
                return ResourceManager.GetString("syntax", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Shows you the location of the given IP-Address or host name.
        /// </summary>
        internal static string usage {
            get {
                return ResourceManager.GetString("usage", resourceCulture);
            }
        }
    }
}
