using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using DotNetNuke.Entities.Users;
using FortyFingers.Library;

namespace FortyFingers.UserSelector.Components
{
    public class InitialViewCookie
    {
        private const string CookieVersion = "1";
        private int ModuleId { get; set; }
        public InitialViewCookie(int moduleId)
        {
            ModuleId = moduleId;

            var req = HttpContext.Current.Request;

            if (CookieUtil.GetEncryptedCookie(CookieKey) == null)
            {
                CreateXml();
                SetCookie();
            }
            else
            {
                GetCookie();
            }
        }

        private string CookieKey
        {
            get { return string.Format("{0}-{1}", Constants.InitialViewCookieName, ModuleId); }
        }

        private void GetCookie()
        {
            var value = CookieUtil.GetEncryptedCookieValue(CookieKey);

            CookieXmlDoc = new XmlDocument();
            CookieXmlDoc.LoadXml(value);

            // see if the version of the cookie matches the current version
            if (Version != CookieVersion)
            {
                // no matching version:
                // delete the cookie and re-init
                Delete();
                CreateXml();
            }
        }

        private void SetCookie()
        {
            var value = CookieXmlDoc.OuterXml;

            CookieUtil.SetEncryptedCookie(CookieKey, value);
        }

        private XmlDocument CookieXmlDoc { get; set; }

        private void CreateXml()
        {
            CookieXmlDoc = new XmlDocument();
            CookieXmlDoc.AppendChild(CookieXmlDoc.CreateElement("userselector"));

            Version = CookieVersion;
            UserId = CurrentUserInfo.UserID;
            Username = CurrentUserInfo.Username;
            DisplayName = CurrentUserInfo.DisplayName;
            UserRoles = CurrentUserInfo.Roles.ToList();
            IsSuperUser = CurrentUserInfo.IsSuperUser;
        }

        private UserInfo _currentUserInfo=null;
        private UserInfo CurrentUserInfo
        {
            get
            {
                if(_currentUserInfo == null)
                {
                    _currentUserInfo = UserController.Instance.GetCurrentUserInfo();
                }
                return _currentUserInfo;
            }
        }

        public string Version
        {
            get
            {
                // check for possible nullreferences
                if (CookieXmlDoc.SelectSingleNode("userselector/version") == null)
                {
                    // appearently there is no version element:
                    // delete the cookie and re-init
                    Delete();
                    CreateXml();

                    // initialize if needed
                    Version = CookieVersion;
                }

                return CookieXmlDoc.SelectSingleNode("userselector/version").InnerText;
            }
            private set
            {
                // see if the element exists
                var versionNode = CookieXmlDoc.SelectSingleNode("userselector/version");
                if (versionNode == null)
                {
                    // create the element if needed
                    versionNode = CookieXmlDoc.CreateElement("version");
                    CookieXmlDoc.DocumentElement.AppendChild(versionNode);
                }
                versionNode.InnerText = value;
            }
        }

        public string Username
        {
            get
            {
                // check for possible nullreferences
                if (CookieXmlDoc.SelectSingleNode("userselector/username") == null)
                {
                    // initialize if needed
                    Username = CurrentUserInfo.Username;
                }

                return CookieXmlDoc.SelectSingleNode("userselector/username").InnerText;
            }
            private set
            {
                // see if the element exists
                var usernameNode = CookieXmlDoc.SelectSingleNode("userselector/username");
                if (usernameNode==null)
                {
                    // create the element if needed
                    usernameNode = CookieXmlDoc.CreateElement("username");
                    CookieXmlDoc.DocumentElement.AppendChild(usernameNode);
                }
                usernameNode.InnerText = value;
            }
        }

        public string DisplayName
        {
            get
            {
                // check for possible nullreferences
                if (CookieXmlDoc.SelectSingleNode("userselector/displayname") == null)
                {
                    // initialize if needed
                    DisplayName = CurrentUserInfo.DisplayName;
                }

                return CookieXmlDoc.SelectSingleNode("userselector/displayname").InnerText;
            }
            private set
            {
                // see if the element exists
                var displaynameNode = CookieXmlDoc.SelectSingleNode("userselector/displayname");
                if (displaynameNode == null)
                {
                    // create the element if needed
                    displaynameNode = CookieXmlDoc.CreateElement("displayname");
                    CookieXmlDoc.DocumentElement.AppendChild(displaynameNode);
                }
                displaynameNode.InnerText = value;
            }
        }

        public int UserId
        {
            get
            {
                // check for possible nullreferences
                if (CookieXmlDoc.SelectSingleNode("userselector/userid") == null)
                {
                    // initialize if needed
                    UserId = CurrentUserInfo.UserID;
                }

                return int.Parse(CookieXmlDoc.SelectSingleNode("userselector/userid").InnerText);
            }
            private set
            {
                // see if the element exists
                var useridNode = CookieXmlDoc.SelectSingleNode("userselector/userid");
                if (useridNode==null)
                {
                    // create the element if needed
                    useridNode = CookieXmlDoc.CreateElement("userid");
                    CookieXmlDoc.DocumentElement.AppendChild(useridNode);
                }
                useridNode.InnerText = value.ToString();
            }
        }

        public bool IsSuperUser
        {
            get
            {
                // check for possible nullreferences
                if (CookieXmlDoc.SelectSingleNode("userselector/isSuperUser") == null)
                {
                    // initialize if needed
                    IsSuperUser = CurrentUserInfo.IsSuperUser;
                }

// ReSharper disable PossibleNullReferenceException
                return bool.Parse(CookieXmlDoc.SelectSingleNode("userselector/isSuperUser").InnerText);
// ReSharper restore PossibleNullReferenceException
            }
            private set
            {
                // see if the element exists
                var isSuperUserNode = CookieXmlDoc.SelectSingleNode("userselector/isSuperUser");
                if (isSuperUserNode == null)
                {
                    // create the element if needed
                    isSuperUserNode = CookieXmlDoc.CreateElement("isSuperUser");
                    CookieXmlDoc.DocumentElement.AppendChild(isSuperUserNode);
                }
                isSuperUserNode.InnerText = value.ToString();
            }
        }

        public List<string> UserRoles
        {
            get
            {
                // check for possible nullreferences
                if(CookieXmlDoc.SelectSingleNode("userselector/roles") == null 
                    || CookieXmlDoc.SelectNodes("userselector/roles/role") == null)
                {
                    // initialize if needed
                    UserRoles = CurrentUserInfo.Roles.ToList();
                }

                var roles = new List<String>();
// ReSharper disable PossibleNullReferenceException (Has been avoided)
                foreach (XmlNode roleElement in CookieXmlDoc.SelectNodes("userselector/roles/role"))
// ReSharper restore PossibleNullReferenceException
                {
                    roles.Add(roleElement.InnerText);
                }
                return roles;
            }
            private set
            {
                // see if the element exists
                var rolesNode = CookieXmlDoc.SelectSingleNode("userselector/roles");
                if(rolesNode == null)
                {
                    // create the element if needed
                    rolesNode = CookieXmlDoc.CreateElement("roles");
                    CookieXmlDoc.DocumentElement.AppendChild(rolesNode);
                }

                // clear all "role" elements
                rolesNode.InnerXml = "";

                foreach (string role in value)
                {
                    // recreate the needed ones
                    var roleNode = CookieXmlDoc.CreateElement("role");
                    roleNode.InnerText = role;
                    rolesNode.AppendChild(roleNode);
                }
            }
        }

        /// <summary>
        /// Removes the initialviewcookie
        /// </summary>
        public void Delete()
        {
            CookieUtil.DeleteEncryptedCookie(CookieKey);
        }
    }
}