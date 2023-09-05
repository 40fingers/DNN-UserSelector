using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using DotNetNuke.Services.Localization;
using FortyFingers.UserSelector.Components;
using Config = FortyFingers.UserSelector.Components.Config;

namespace FortyFingers.UserSelector
{
    public partial class View : PortalModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorLabel.Visible = false;

            // the module is visible if the "initialViewCookie" 
            // contains a role that's allowed to the module
            // or if the user is (or has been) SuperUser
            SelectUserDiv.Visible = InitialViewCookie.IsSuperUser || InitialViewCookie.UserRoles.Any(r => Config.AllowedRoles.Contains(r));
            NotAllowedLabel.Visible = !SelectUserDiv.Visible;

            // if the module is not visible, we're removing the cookie
            // that gives the user a chance to login as a different user
            // and try to view the module again
            if (!SelectUserDiv.Visible) InitialViewCookie.Delete();

            if (!IsPostBack)
            {
                FillBackToInitialUserButton();
                FillUsersList();
                FillSuperUsersList();
                UserNamePanel.Visible = SuperUsersPanel.Visible;
            }
        }

        private void FillBackToInitialUserButton()
        {
            BackToInitialUserButton.Visible = UserInfo.UserID != InitialViewCookie.UserId;

            BackToInitialUserButton.Text = Localization.GetString("BackToInitialUserButton.Text", LocalResourceFile);
            if (BackToInitialUserButton.Text.Contains("{0}"))
            {
                BackToInitialUserButton.Text = String.Format(BackToInitialUserButton.Text,
                                                             String.Format("{0} - {1} ({2})",
                                                                           InitialViewCookie.Username,
                                                                           InitialViewCookie.UserId,
                                                                           InitialViewCookie.DisplayName));
            }
        }


        private Config _config;
        private Config Config
        {
            get
            {
                if (_config == null)
                    _config = new Config(ModuleId, TabModuleId, Settings);

                return _config;
            }
        }

        private InitialViewCookie _initialViewCookie;
        private InitialViewCookie InitialViewCookie
        {
            get
            {
                if (_initialViewCookie == null)
                    _initialViewCookie = new InitialViewCookie(ModuleId);

                return _initialViewCookie;
            }
        }

        private static int CompareUsersByUsername(UserInfo first, UserInfo second)
        {
            return first.Username.CompareTo(second.Username);
        }

        private void FillUsersList()
        {
            List<UserInfo> users = null;

            // if no users specified: take all
            if (Config.AvailableUsers.Count == 0)
                users = UserSwitcherController.GetPortalUsers(PortalId);
            else
            {
                // take configured users
                users = new List<UserInfo>();
                foreach (string availableUser in Config.AvailableUsers)
                {
                    // try to get the user and add it when succeeded
                    try
                    {

                        UserInfo user;

                        // could be anonymous
                        if (availableUser != Null.NullInteger.ToString())
                        {
                            user = UserController.GetUserById(PortalId, int.Parse(availableUser));
                            if (user != null)
                                users.Add(user);
                        }
                    }
                    catch
                    {
                        // do nothing
                    }
                }
            }

            users = users.OrderBy(u => u.Username).ToList();

            // we might need to add the anonymous user
            if (Config.AvailableUsers.Count == 0 || Config.AvailableUsers.Contains(Null.NullInteger.ToString()))
                UserDropDown.Items.Add(new ListItem(Localization.GetString("AnonymousUser.Text", LocalResourceFile), Null.NullInteger.ToString()));

            users.ForEach(u =>
                          UserDropDown.Items.Add(
                              new ListItem(String.Format("{0} - {1} ({2})", u.Username, u.UserID, u.DisplayName),
                                           u.UserID.ToString())));

            var item = UserDropDown.Items.FindByValue(UserId.ToString());
            if (item != null)
                item.Selected = true;
            else
                UserDropDown.Items.Insert(0, new ListItem("", "".ToString()));
        }
        private void FillSuperUsersList()
        {
            SuperUsersPanel.Visible = InitialViewCookie.IsSuperUser || 
                (Config.AllowAdminToAnyUser && InitialViewCookie.UserRoles.Contains(PortalSettings.AdministratorRoleName));
            if (!SuperUsersPanel.Visible) return;
            
            SuperUserLabel.Visible = InitialViewCookie.IsSuperUser;
            AdminLabel.Visible = !SuperUserLabel.Visible;

            SuperUserButton.Text = String.Format(Localization.GetString("SuperUserButton", LocalResourceFile), InitialViewCookie.Username);
        }

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection();

                //actions.Add(GetNextActionID(),
                //            Localization.GetString("EditModule.Action", LocalResourceFile),
                //            ModuleActionType.EditContent,
                //            "",
                //            "",
                //            EditUrl(),
                //            false, DotNetNuke.Security.SecurityAccessLevel.Edit,
                //            true,
                //            false);

                return actions;
            }
        }

        protected void UserDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dropdown = (DropDownList)sender;
            if ((dropdown.SelectedValue != this.UserId.ToString()))
            {
                if ((dropdown.SelectedValue == Null.NullInteger.ToString() && UserId != Null.NullInteger))
                {
                    Response.Redirect(Globals.NavigateURL("LogOff"));
                }
                else
                {
                    SelectUser(int.Parse(dropdown.SelectedValue));
                }

            }

        }

        protected void BackToInitialUserButton_Click(object sender, EventArgs e)
        {
            SelectUser(InitialViewCookie.UserId);
        }

        private void SelectUser(int userId)
        {
            UserInfo switchToUser = UserController.GetUserById(PortalId, userId);
            if ((switchToUser != null))
            {
                // is the current user allowed to switch to this user?
                if (switchToUser.IsSuperUser && UserInfo.IsSuperUser)
                {
                    // no switching between superusers
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = Localization.GetString("NotAllowedToSwitchToUser.Error", LocalResourceFile);
                    return;
                }

                if (!InitialViewCookie.IsSuperUser && switchToUser.IsInRole(PortalSettings.AdministratorRoleName))
                {
                    // must have been administrator to be able to switch to an administrator
                    if (!InitialViewCookie.UserRoles.Contains(PortalSettings.AdministratorRoleName))
                    {
                        return;
                    }
                }

                //Remove user from cache
                if (Page.User != null)
                {
                    DataCache.ClearUserCache(this.PortalSettings.PortalId, Context.User.Identity.Name);
                }

                // sign current user out
                PortalSecurity objPortalSecurity = new PortalSecurity();
                objPortalSecurity.SignOut();

                // sign new user in
                UserController.UserLogin(PortalId, switchToUser, PortalSettings.PortalName, Request.UserHostAddress, false);

                // redirect to current url
                Response.Redirect(Request.RawUrl, true);
            }

        }

        protected void SuperUserButton_Click(object sender, EventArgs e)
        {
            SelectUser(InitialViewCookie.UserId);
        }

        protected void SwitchToUserNameButton_Click(object sender, EventArgs e)
        {
            var user = UserController.GetUserByName(PortalId, UserNameTextBox.Text);
            if (user == null)
            {
                int userId = 0;
                if (int.TryParse(UserNameTextBox.Text, out userId))
                {
                    user = UserController.GetUserById(PortalId, userId);
                }
            }
            if (user != null)
            {
                if (user.IsDeleted)
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = Localization.GetString("UserDeleted.Error", LocalResourceFile);
                    return;
                }
                SelectUser(user.UserID);
            }
        }
    }
}