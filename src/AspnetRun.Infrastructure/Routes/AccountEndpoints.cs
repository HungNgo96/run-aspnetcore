using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetRun.Infrastructure.Routes
{
    public static class AccountEndpoints
    {
        public static string Register = "api/identity/account/register";
        public static string ChangePassword = "api/identity/account/changepassword";
        public static string UpdateProfile = "api/identity/account/updateprofile";

        public static string GetProfilePicture(string userId)
        {
            return $"api/identity/account/profile-picture/{userId}";
        }

        public static string UpdateProfilePicture(string userId)
        {
            return $"api/identity/account/profile-picture/{userId}";
        }
    }
}
