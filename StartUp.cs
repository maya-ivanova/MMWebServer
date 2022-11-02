using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using MayaWebServer.Server;
using MayaWebServer.Server.Responses;

namespace MayaWebServer
{
    public class StartUp
    {
        public static async Task Main()
            => await new HttpServer(9090, routes => routes
            .MapGet("/", new TextResponse("Hello from 2022-11-02 meeeee!"))
            .MapGet("/Cats", new TextResponse("<h2>Hello from tthe CATS!</h2>", "text/html"))
            .MapGet("/Dogs", new TextResponse("<h1>Hello from the digs :)</h1>", "text/html")))
            .Start();
       
        //{
        //   var server = new HttpServer("127.0.0.1", 9090);
           // await server.Start();
        //}
    }
}