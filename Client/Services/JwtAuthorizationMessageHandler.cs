﻿using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace RealWorldApp.Client.Services
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public JwtAuthorizationMessageHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine(request.Headers.Authorization?.ToString());
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
