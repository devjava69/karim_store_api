using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Karim_eshop.Data.Entity.Model
{
    public class User : IdentityUser<int>
    {
        public UserAddress Address { get; set; }
    }
}
