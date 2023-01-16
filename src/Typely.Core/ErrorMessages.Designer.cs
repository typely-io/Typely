﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Typely.Core {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Typely.Core.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must contain exactly {ExactLength} characters but currently has {ActualLength}..
        /// </summary>
        public static string ExactLength {
            get {
                return ResourceManager.GetString("ExactLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be between {From} and {To}, excluding both limits..
        /// </summary>
        public static string ExclusiveBetween {
            get {
                return ResourceManager.GetString("ExclusiveBetween", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be greater than &apos;{ComparisonValue}&apos;..
        /// </summary>
        public static string GreaterThan {
            get {
                return ResourceManager.GetString("GreaterThan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be greater than or equal to &apos;{ComparisonValue}&apos;..
        /// </summary>
        public static string GreaterThanOrEqualTo {
            get {
                return ResourceManager.GetString("GreaterThanOrEqualTo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be between {From} and {To}, including both limits..
        /// </summary>
        public static string InclusiveBetween {
            get {
                return ResourceManager.GetString("InclusiveBetween", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be between {MinLength} and {MaxLength} characters but currently has {ActualLength}..
        /// </summary>
        public static string Length {
            get {
                return ResourceManager.GetString("Length", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be less than &apos;{ComparisonValue}&apos;..
        /// </summary>
        public static string LessThan {
            get {
                return ResourceManager.GetString("LessThan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be less than or equal to &apos;{ComparisonValue}&apos;..
        /// </summary>
        public static string LessThanOrEqualTo {
            get {
                return ResourceManager.GetString("LessThanOrEqualTo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; is not in the correct format. Expected format &apos;{RegularExpression}&apos;..
        /// </summary>
        public static string Matches {
            get {
                return ResourceManager.GetString("Matches", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be {MaxLength} characters or less but currently has {ActualLength}..
        /// </summary>
        public static string MaxLength {
            get {
                return ResourceManager.GetString("MaxLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must be at least {MinLength} characters but currently has {ActualLength}..
        /// </summary>
        public static string MinLength {
            get {
                return ResourceManager.GetString("MinLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The specified condition was not met for &apos;{Name}&apos;..
        /// </summary>
        public static string Must {
            get {
                return ResourceManager.GetString("Must", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; must not be empty..
        /// </summary>
        public static string NotEmpty {
            get {
                return ResourceManager.GetString("NotEmpty", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{Name}&apos; should not be equal to &apos;{ComparisonValue}&apos;..
        /// </summary>
        public static string NotEqual {
            get {
                return ResourceManager.GetString("NotEqual", resourceCulture);
            }
        }
    }
}
