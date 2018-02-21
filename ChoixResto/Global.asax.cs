﻿using ChoixRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChoixRestaurant
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            IDatabaseInitializer<MyDbContext> init = new InitChoixRestaurant();
            Database.SetInitializer(init);
            init.InitializeDatabase(new MyDbContext());
        }
    }
}
