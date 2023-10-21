﻿using Microsoft.AspNetCore.Identity;

namespace MvcWebSchool_Identity.Areas.Admin.Models
{
    public class RoleEdit
    {
        public IdentityRole? Role{ get; set; }

        public IEnumerable<IdentityUser>? Members{ get; set; }

        public IEnumerable<IdentityUser>? NonMembers { get; set; }
    }
}
