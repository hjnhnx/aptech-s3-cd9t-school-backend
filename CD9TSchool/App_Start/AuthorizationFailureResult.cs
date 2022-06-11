using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace CD9TSchool.App_Start
{
    public class AuthorizationFailureResult : IHttpActionResult
    {
        public AuthorizationFailureResult(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            response.ReasonPhrase = Message;
            return response;
        }
    }
}