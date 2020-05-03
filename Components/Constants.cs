using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FortyFingers.UserSelector.Components
{
    public class Constants
    {
        public const string InitialViewCookieName = "40Fingers_UserSelector_InitialView";
        public const string InitialViewCookieXml = 
@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<userselector>
  <username></username>
  <roles></roles>
  <issuperuser></issuperuser>
</userselector>";

    }
}