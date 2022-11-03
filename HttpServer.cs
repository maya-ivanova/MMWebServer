using MayaWebServer.Server.Routing;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace MayaWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        public HttpServer(string ipAddress, int port, Action<IRoutingTable> routingTable)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;

            listener = new TcpListener(this.ipAddress, port);
        }

        public HttpServer(int port, Action<IRoutingTable> routingTable) 
            : this ("127.0.0.1", port, routingTable)
        { }

        public HttpServer(Action<IRoutingTable> routingTable) 
            : this(5050, routingTable) { }

        public async Task Start()
        {

            this.listener.Start();

            Console.WriteLine($"Server started on port {port}");
            Console.WriteLine("Listening for requests ...");

            while (true)
            {
                var connection = await this.listener.AcceptTcpClientAsync();

                var networkStream = connection.GetStream();
                var requestText = await this.ReadRequest(networkStream);

                Console.WriteLine(requestText);

                await WriteResponse(networkStream);
            
                connection.Close();
            }

        }

        private async Task WriteResponse(NetworkStream networkStream)
        {
            var content = "Hello from my server!! \r\n Здрасти :) آني مبسوطة بمعرفتك !!!! مرحبا كيفك؟";
            
            var contentLength = Encoding.UTF8.GetByteCount(content);


            var response = $@"
HTTP/1.1 200 OK
Server: MyWebServer
Date: {DateTime.UtcNow:R}
Content-Length: {contentLength}
Content-Type: text/plain; charset=UTF-8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);

            await networkStream.WriteAsync(responseBytes);

           
        }

        private async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var buffer = new byte[bufferLength];

            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, buffer.Length);

                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            }

            return requestBuilder.ToString();
        }

        
    }
}