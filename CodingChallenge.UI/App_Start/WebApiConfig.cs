﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace CodingChallenge.UI
{
    public static class WebApiConfig
            // webapi routes 
            config.MapHttpAttributeRoutes();
            // CORS
            var cors = new EnableCorsAttribute("*", "*", "*");

}