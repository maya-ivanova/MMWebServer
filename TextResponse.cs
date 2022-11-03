using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MayaWebServer.Server.Common;
using MayaWebServer.Server.Http;

namespace MayaWebServer.Server.Responses
{
    public class TextResponse : HttpResponse
    {
        public TextResponse(string text, string contentType)
            : base(HttpStatusCode.OK)
        {
            Guard.AgainstNull(text);
            var textLength = Encoding.UTF8.GetByteCount(text).ToString();
            this.Headers.Add("Content-Type", contentType);
            this.Headers.Add("Content-Length", textLength);

            this.Content = text;
        }
        public TextResponse(string text) 
            : this(text, "text/plain; charset=UTF-8")
        {
         }

    }
}
