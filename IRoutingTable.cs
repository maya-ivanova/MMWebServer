﻿using MayaWebServer.Server.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpMethod = MayaWebServer.Server.Http.HttpMethod;

namespace MayaWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(string url, HttpMethod method, HttpResponse response);

        IRoutingTable MapGet(string url, HttpResponse response);
    }
}