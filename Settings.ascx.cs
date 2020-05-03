using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.Security.Roles;
using DotNetNuke.Services.Localization;
using FortyFingers.UserSelector.Components;
using Config = FortyFingers.UserSelector.Components.Config;

namespace FortyFingers.UserSelector
{
    public partial class Settings : ModuleSettingsBase
    {
        private Config _config;
        private Config Config
        {
            get
            {
                if(_config == null)
                    _config = new Config(ModuleId, TabModuleId, Settings);

                return _config;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //AJAX.WrapUpdatePanelControl(AvailableRolesTable, true);
                //AJAX.WrapUpdatePanelControl(AvailableUsersTable, true);
            }
        }
        public override void LoadSettings()
        {
            
            var allRoles = new RoleController().GetRoles(PortalId);
            foreach (RoleInfo roleInfo in allRoles)
            {
                // see if we need to add the role to Available Roles or Allowed Roles
                if (Config.AllowedRoles.Contains(roleInfo.RoleName.ToString()))
                    lstAllowedRoles.Items.Add(new ListItem(roleInfo.RoleName, roleInfo.RoleName.ToString()));
                else
                    lstAvailableRoles.Items.Add(new ListItem(roleInfo.RoleName, roleInfo.RoleName.ToString()));
            }

            // add the Anonymous user
            if(Config.AvailableUsers.Contains(Null.NullInteger.ToString()))
                lstSelectedUsers.Items.Add(new ListItem(Localization.GetString("AnonymousUser.Text", LocalResourceFile), Null.NullInteger.ToString()));
            else
                lstAllUsers.Items.Add(new ListItem(Localization.GetString("AnonymousUser.Text", LocalResourceFile), Null.NullInteger.ToString()));

            var allUsers = UserSwitcherController.GetPortalUsers(PortalId);
            foreach (UserInfo userInfo in allUsers)
            {
                // see if we need to add the user to All Users or Selected Users
                if (Config.AvailableUsers.Contains(userInfo.UserID.ToString()))
                    lstSelectedUsers.Items.Add(new ListItem(userInfo.Username, userInfo.UserID.ToString()));
                else
                    lstAllUsers.Items.Add(new ListItem(userInfo.Username, userInfo.UserID.ToString()));
            }

            chkAllowAdminAnySwitch.Checked = Config.AllowAdminToAnyUser;
        }
        public override void UpdateSettings()
        {
            var allowedRoles = new List<string>();
            foreach (ListItem item in lstAllowedRoles.Items)
            {
                allowedRoles.Add(item.Value);
            }
            Config.AllowedRoles = allowedRoles;

            var availableUsers = new List<string>();
            foreach (ListItem item in lstSelectedUsers.Items)
            {
                availableUsers.Add(item.Value);
            }
            Config.AvailableUsers = availableUsers;

            Config.AllowAdminToAnyUser = chkAllowAdminAnySwitch.Checked;

            // store the new settings
            Config.Update();
        }

        protected void btnAddRoles_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lstAvailableRoles, lstAllowedRoles);
        }

        protected void btnRemoveRoles_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lstAllowedRoles, lstAvailableRoles);
        }

        protected void btnAddUsers_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lstAllUsers, lstSelectedUsers);
        }

        protected void btnRemoveUsers_Click(object sender, EventArgs e)
        {
            MoveSelectedItems(lstSelectedUsers, lstAllUsers);
        }

        private void MoveSelectedItems(ListBox fromListBox, ListBox toListBox)
        {
            var itemsToRemove = new List<ListItem>();
            // loop through selected items
            foreach (int index in fromListBox.GetSelectedIndices())
            {
                // add to the other box
                toListBox.Items.Add(fromListBox.Items[index]);
                // list item to be removed from selected box
                itemsToRemove.Add(fromListBox.Items[index]);
            }
            itemsToRemove.ForEach(i => fromListBox.Items.Remove(i));
        }
    }
}