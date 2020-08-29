﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject_Devskill.Membership.Extensions
{
    public static class IdentityExtension
    {
        public static string FullName(this IIdentity identity)
        {
            var value = ((ClaimsIdentity)identity)?.FindFirst("FullName")?.Value;
            return value ?? string.Empty;
        }

        public static string Email(this IIdentity identity)
        {
            var value = ((ClaimsIdentity)identity)?.FindFirst(ClaimTypes.Email)?.Value;
            return value ?? string.Empty;
        }

        public static string ImageUrl(this IIdentity identity)
        {
            var value = ((ClaimsIdentity)identity)?.FindFirst("ImageUrl")?.Value;
            return value ?? string.Empty;
        }

        public static string RoleName(this IIdentity identity)
        {
            var value = ((ClaimsIdentity)identity)?.FindFirst(ClaimTypes.Role)?.Value;
            return value ?? string.Empty;
        }

        public static bool HasRole(this IIdentity identity, string role)
        {
            var hasClaim = ((ClaimsIdentity)identity)?.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == role);
            return hasClaim ?? false;
        }

        //public static bool HasPermission(this IIdentity identity, string permission)
        //{
        //    var hasClaim = ((ClaimsIdentity)identity)?.HasClaim(x => x.Type == CustomClaimType.Permission && x.Value == permission);
        //    return hasClaim ?? false;
        //}

        //public static bool HasPermission(this IIdentity identity, params string[] permissions)
        //{
        //    var hasClaim = ((ClaimsIdentity)identity)?.Claims.Any(cl => cl.Type == CustomClaimType.Permission && permissions.Any(per => per == cl.Value));
        //    return hasClaim ?? false;
        //}
    }
}
