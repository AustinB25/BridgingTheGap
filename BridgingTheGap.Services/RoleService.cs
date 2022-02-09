using BridgingTheGap.Data;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BridgingTheGap.Services
{
    public class RoleService
    {
        private readonly Guid _userId;
        public RoleService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateRole(IdentityRole role)
        {
            var roleEntity =
                new IdentityRole
                {
                    Name = role.Name
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Roles.Add(role);
                return ctx.SaveChanges() == 1;
            }
        }
        public List<IdentityRole> GetRoles()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Roles
                    .ToList();
                return query;
            }
        }
    }
}
