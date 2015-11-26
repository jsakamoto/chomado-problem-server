﻿using System;
using System.Linq;
using Nancy;

namespace ChomadProblemServer
{
    public class DefaultModule : NancyModule
    {
        public DefaultModule()
        {
            Get["/"] = _ => View["index.cshtml", new { Context.Request.Url.SiteBase }];

            After += context => {
                if (context.Response.ContentType == "text/html")
                    context.Response.ContentType = "text/html; charset=utf-8";
            };
        }
    }
}