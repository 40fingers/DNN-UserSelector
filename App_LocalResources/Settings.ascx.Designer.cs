﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FortyFingers.UserSelector.App_LocalResources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Settings_ascx {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Settings_ascx() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FortyFingers.UserSelector.App_LocalResources.Settings.ascx", typeof(Settings_ascx).Assembly);
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
        ///   Looks up a localized string similar to Checking this box qwill allow admins to freely switch to and from any user using the userId/username textbox..
        /// </summary>
        internal static string AllowAdminAnySwitchLabel_Help {
            get {
                return ResourceManager.GetString("AllowAdminAnySwitchLabel.Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allow Admin to select any user.
        /// </summary>
        internal static string AllowAdminAnySwitchLabel_Text {
            get {
                return ResourceManager.GetString("AllowAdminAnySwitchLabel.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allowed roles.
        /// </summary>
        internal static string AllowedRolesLabel_Text {
            get {
                return ResourceManager.GetString("AllowedRolesLabel.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Anonymous.
        /// </summary>
        internal static string AnonymousUser_Text {
            get {
                return ResourceManager.GetString("AnonymousUser.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Available roles.
        /// </summary>
        internal static string AvailableRolesLabel_Text {
            get {
                return ResourceManager.GetString("AvailableRolesLabel.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;p&gt;Choose the roles allowed to use the module. This means the user has to be in one of these roles at some point during his login session.&lt;/p&gt;&lt;p&gt;This allows you to make the module available to anonymous users and still ensure that only selected roles will be able to switch their identity.&lt;/p&gt;&lt;p&gt;Selecting no roles will show the module to all users with view permissions in DotNetNuke&lt;/p&gt;.
        /// </summary>
        internal static string plAllowedRoles_Help {
            get {
                return ResourceManager.GetString("plAllowedRoles.Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Allowed Roles.
        /// </summary>
        internal static string plAllowedRoles_Text {
            get {
                return ResourceManager.GetString("plAllowedRoles.Text", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select which Users will be available in the Users Dropdown List. Selecting no users will result in All users to be in the list..
        /// </summary>
        internal static string plAvailableUsers_Help {
            get {
                return ResourceManager.GetString("plAvailableUsers.Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Available Users.
        /// </summary>
        internal static string plAvailableUsers_Text {
            get {
                return ResourceManager.GetString("plAvailableUsers.Text", resourceCulture);
            }
        }
    }
}
