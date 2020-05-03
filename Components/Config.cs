using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Modules;
using FortyFingers.Library;

namespace FortyFingers.UserSelector.Components
{
    public class Config
    {
        private Hashtable Settings { get; set; }
        private int ModuleId { get; set; }
        private int TabModuleId { get; set; }
        public Config(int moduleId, int tabModuleId, Hashtable settings)
        {
            ModuleId = moduleId;
            TabModuleId = tabModuleId;
            Settings = settings;
        }

        private ModuleController _moduleController = null;
        private ModuleController ModuleController
        {
            get
            {
                if(_moduleController==null)
                    _moduleController = new ModuleController();

                return _moduleController;
            }
        }

        public void Update()
        {
            var ctl = new ModuleController();

            ctl.UpdateModuleSetting(ModuleId, "AllowedRoles", AllowedRoles.ToString(";"));
            ctl.UpdateModuleSetting(ModuleId, "AvailableUsers", AvailableUsers.ToString(";"));
            ctl.UpdateModuleSetting(ModuleId, "AllowAdminToAnyUser", AllowAdminToAnyUser.ToString());
        }

        private List<string> _allowedRoles;
        public List<string> AllowedRoles
        {
            get
            {
                // if not used yet, get it from settings
                if(_allowedRoles == null)
                {
                    // instanciate the new list
                    _allowedRoles = new List<string>();
                    // check if it has a value
                    if (Settings.ContainsKey("AllowedRoles") && !String.IsNullOrEmpty(Settings["AllowedRoles"].ToString()))
                    {
                        // add each of the seperated values to the List
                        foreach (string allowedRole in Settings["AllowedRoles"].ToString().Split(';'))
                        {
                            _allowedRoles.Add(allowedRole);
                        }
                    }
                }

                // return the List
                return _allowedRoles;
            }
            set
            {
                _allowedRoles = value;
            }
        }

        private List<string> _availableUsers;

        public List<string> AvailableUsers
        {
            get
            {
                // if not used yet, get it from settings
                if (_availableUsers == null)
                {
                    // instanciate the new list
                    _availableUsers = new List<string>();
                    // check if it has a value
                    if (Settings.ContainsKey("AvailableUsers") && !String.IsNullOrEmpty(Settings["AvailableUsers"].ToString()))
                    {
                        // add each of the seperated values to the List
                        foreach (string availableUser in Settings["AvailableUsers"].ToString().Split(';'))
                        {
                            _availableUsers.Add(availableUser);
                        }
                    }
                }

                // return the List
                return _availableUsers;
            }
            set
            {
                _availableUsers = value;
            }
        }

        private bool? _allowAdminToAnyUser = null;
        public bool AllowAdminToAnyUser 
        {
            get
            {
                // if not used yet, get it from settings
                if (!_allowAdminToAnyUser.HasValue)
                {
                    // check if it has a value
                    if (Settings.ContainsKey("AllowAdminToAnyUser") && !String.IsNullOrEmpty(Settings["AllowAdminToAnyUser"].ToString()))
                    {
                        _allowAdminToAnyUser = bool.Parse(Settings["AllowAdminToAnyUser"].ToString());
                    }
                }

                return _allowAdminToAnyUser.GetValueOrDefault(false);
            }
            set { _allowAdminToAnyUser = value; }
        }
    }
}