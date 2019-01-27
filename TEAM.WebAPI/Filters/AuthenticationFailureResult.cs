﻿using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace TEAM.WebAPI.Filters
{
    /// <summary>
    /// Authentication Failure IHttpActionResult
    /// Source : https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/authentication-filters
    /// </summary>
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request, bool isInvalidSession = true)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
            IsInvalidSession = isInvalidSession;
        }

        public bool IsInvalidSession { get; private set; }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
            {
                RequestMessage = Request,
                ReasonPhrase = ReasonPhrase
            };
            if (IsInvalidSession)
            {
                response.Headers.Add("IsInvalidSession", "true");
            }
            return response;
        }
    }
}