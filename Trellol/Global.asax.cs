using EntitiesLogic.Entities;
using Logic;
using Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Trellol
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Cards",
                "Boards/{boardId}/Lists/{listId}/Cards/{action}/{cardId}",
                new { controller = "Cards", cardId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Lists",
                "Boards/{boardId}/Lists/{action}/{listId}",
                new { controller = "Lists", listId = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
			PopulateMemoryData();
            AreaRegistration.RegisterAllAreas();

            // Use LocalDB for Entity Framework by default
            Database.DefaultConnectionFactory = new SqlConnectionFactory(@"Data Source=(localdb)\v11.0; Integrated Security=True; MultipleActiveResultSets=True");

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
		
		private static void PopulateMemoryData()
        {
            AppServices.AddBoard(new Board { Name = "teste", Description = "blablabla" });
            AppServices.AddBoard(new Board { Name = "jose", Description = "blablabla" });
            AppServices.AddList(new List { BoardName = "teste", Name = "Fazendo", Position = 1 });
            AppServices.AddList(new List { BoardName = "teste", Name = "acabando", Position = 2 });
            AppServices.AddList(new List { BoardName = "teste", Name = "Quase a acabar", Position = 3 });
            AppServices.AddList(new List { BoardName = "jose", Name = "acabou", Position = 1 });
            AppServices.AddList(new List { BoardName = "jose", Name = "Fazendo", Position = 2 });
            AppServices.AddList(new List { BoardName = "jose", Name = "lolando", Position = 3 });
            AppServices.AddCard(new Card
            {
                Id = 1,
                BoardName = "teste",
                Name = "cartão",
                Description = "Descrição do cartão 1",
                ListName = "Fazendo",
                CreationDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Position = 1,
                isArchived = false,
            });
            AppServices.AddCard(new Card
            {
                Id = 2,
                BoardName = "teste",
                Name = "cartão",
                Description = "Descrição do cartão 2",
                ListName = "Fazendo",
                CreationDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Position = 2,
                isArchived = false
            });
            AppServices.AddCard(new Card
            {
                Id = 3,
                BoardName = "teste",
                Name = "cartão",
                Description = "Descrição do cartão 3",
                ListName = "acabando",
                CreationDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Position = 1,
                isArchived = false
            });
            AppServices.AddCard(new Card
            {
                Id = 1,
                BoardName = "jose",
                Name = "cartão",
                Description = "Descrição do cartão 1",
                ListName = "acabou",
                CreationDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Position = 1,
                isArchived = false
            });
            AppServices.AddCard(new Card
            {
                Id = 2,
                BoardName = "jose",
                Name = "cartão",
                Description = "Descrição do cartão 2",
                ListName = "arquivado",
                CreationDate = DateTime.Today,
                DueDate = DateTime.Today.AddDays(5),
                Position = 1,
                isArchived = false
            });


            TrellolUserProvider.CreateUser(new User
            {
                Username = "Admin",
                Email = "xpto@sapo.pt",
                Password = "123",
                isConfirmed = true,
            });

            TrellolUserProvider.AddBoardForUser(AppServices.GetBoard("jose"), TrellolUserProvider.GetUser("Admin"));
            TrellolRoleProvider.AddUsersToRoles(new string[] { "Admin" }, new string[] { TrellolRoleProvider.Admin });
        }
    }
}