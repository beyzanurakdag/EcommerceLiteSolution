﻿using EcommerceLiteBLL.Account;
using EcommerceLiteBLL.Settings;
using EcommerceLiteEntity.Enums;
using EcommerceLiteEntity.IdentityModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace EcommerceLiteUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var myRoleManager = MembershipTools.NewRoleManager();
            var theRoles = Enum.GetNames(typeof(TheIdentityRoles));
            foreach (var therole in theRoles)
            {
                if (!myRoleManager.RoleExists(therole))
                    myRoleManager.Create(new ApplicationRole()
                    {
                        Name = therole
                    });
            }
            LogManager.LogMessage("*-*-*-*-*\tAPPLICATION INITIATED!\t*-*-*-*-*");
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
    }
}
