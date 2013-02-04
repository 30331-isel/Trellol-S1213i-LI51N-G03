using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepositoryFactory;
using EntitiesLogic.Entities;
using RepositoryInterfaces;
using System.Text.RegularExpressions;
using Security;

namespace Trellol.Infrastructure.Filters
{
    public class Authentication : AuthorizeAttribute
    {
        private readonly IRoleRepository _roleRepo = RoleRepositoryLocator.Get();

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool hasUsers = !string.IsNullOrEmpty(Users);
            bool hasRoles = !string.IsNullOrEmpty(Roles);

            string userName = httpContext.User.Identity.Name;

            bool userExists = !hasUsers || SplitString(Users).Contains(userName);

            string[] roles = SplitString(Roles);
            bool isInRole = !hasRoles || roles.Intersect(_roleRepo.GetRolesForUser(userName)).Any();

            object o;
            httpContext.Request.RequestContext.RouteData.Values.TryGetValue("controller", out o);
            string controller = o == null ? null : o.ToString();
            string boardId = null;
            if (controller == "Boards")
            {
                httpContext.Request.RequestContext.RouteData.Values.TryGetValue("id", out o);
            }
            else
            {
                httpContext.Request.RequestContext.RouteData.Values.TryGetValue("boardId", out o);
            }
            boardId = o == null ? null : o.ToString();

            bool hasBoard = (boardId == null) || 
                TrellolUserProvider.GetBoardsFromUser(userName).FirstOrDefault(b => b.Name == boardId) != null;

            return httpContext.Request.IsAuthenticated && userExists && isInRole && hasBoard;
        }

        private string[] SplitString(string s)
        {
            s = Regex.Replace(s, @"\s", string.Empty);
            return s.Split(',');
        }
    }
}
