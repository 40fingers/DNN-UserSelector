using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;

namespace FortyFingers.UserSelector.Components
{
    public static class UserSwitcherController
    {
        #region GetPortalUsers

        private static string GetPortalUsersCacheKey()
        {
            return String.Format("UserSwitcherController_GetUsers");
        }

        public static List<UserInfo> GetPortalUsers(int portalId)
        {
            var cacheKey = GetPortalUsersCacheKey();

            var cacheItemArgs = new CacheItemArgs(cacheKey);
            cacheItemArgs.CacheTimeOut = 60;

            var retval = DataCache.GetCachedData<List<UserInfo>>(cacheItemArgs,
                GetPortalUsersCallBack);
            return retval.Where(u => u.PortalID == portalId).ToList();
        }

        private static void GetPortalUsersInvalidateCache()
        {
            var cacheKey = GetPortalUsersCacheKey();

            DataCache.RemoveCache(cacheKey);
        }

        private static List<UserInfo> GetPortalUsersCallBack(CacheItemArgs cacheItemArgs)
        {
            var usersArray = UserController.GetUsers(Null.NullInteger);
            var retval = new List<UserInfo>();

            foreach (UserInfo userInfo in usersArray)
                retval.Add(userInfo);

            retval.Sort(CompareUsersByUsername);

            return retval;
        }

        private static int CompareUsersByUsername(UserInfo first, UserInfo second)
        {
            return first.Username.CompareTo(second.Username);
        }

        #endregion

        #region GetSuperUsers

        private static string GetSuperUsersCacheKey()
        {
            return String.Format("UserSwitcherController_GetSuperUsers");
        }

        public static List<UserInfo> GetSuperUsers()
        {
            var cacheKey = GetSuperUsersCacheKey();

            var cacheItemArgs = new CacheItemArgs(cacheKey);
            cacheItemArgs.CacheTimeOut = 60;

            var retval = DataCache.GetCachedData<List<UserInfo>>(cacheItemArgs,
                GetSuperUsersCallBack);
            return retval;
        }

        private static void GetSuperUsersInvalidateCache()
        {
            var cacheKey = GetSuperUsersCacheKey();

            DataCache.RemoveCache(cacheKey);
        }

        private static List<UserInfo> GetSuperUsersCallBack(CacheItemArgs cacheItemArgs)
        {
            List<UserInfo> retval = new List<UserInfo>();

            foreach (var superUser in UserController.GetUsers(false, true, PortalSettings.Current.PortalId))
            {
                retval.Add((UserInfo)superUser);
            }

            return retval;
        }

        #endregion


    }
}