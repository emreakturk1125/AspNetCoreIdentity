using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreIdentity.Web.Models
{

   
    public class AppIdentityDbContext : IdentityDbContext<AppUser,AppRole,string>   // AppUser ile AppRole Eşleştirmesini string baz lı yapalım demektir.
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }
    }
}
