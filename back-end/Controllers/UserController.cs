using Azure.Communication;
using Azure.Communication.Identity;
using back_end.Services;
using Microsoft.AspNetCore.Mvc;

namespace back_end.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CommunicationIdentityClient _client;
        private readonly bool _enableMultipleUsers;
        private readonly UserIdentityStore _userIdentityStore;
        private readonly ILogger<UserController> _logger;

        public UserController(IConfiguration configuration, UserIdentityStore userIdentityStore, ILogger<UserController> logger)
        {
            _client = new CommunicationIdentityClient(configuration["ResourceConnectionString"]);
            _enableMultipleUsers = configuration.GetValue<bool>("EnableMultipleUsers");
            _userIdentityStore = userIdentityStore;
            _logger = logger;
        }

        [HttpGet("identity")]
        public async Task<IActionResult> GetIdentity()
        {
            var identity = _userIdentityStore.IsPrimaryUserTokenAcquired && _enableMultipleUsers
                ? _userIdentityStore.SecondaryUserId
                : _userIdentityStore.UserId;

            if (string.IsNullOrEmpty(identity))
            {
                var createUserResponse = await _client.CreateUserAsync();
                identity = createUserResponse.Value.Id;
                if (_userIdentityStore.IsPrimaryUserTokenAcquired)
                    _userIdentityStore.SecondaryUserId = identity;
                else
                    _userIdentityStore.UserId = identity;

                _logger.LogInformation($"New identity with ID: {identity}");
            }
            return Ok(identity);
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            var userId = _userIdentityStore.IsPrimaryUserTokenAcquired && _enableMultipleUsers
                ? _userIdentityStore.SecondaryUserId
                : _userIdentityStore.UserId;

            // Issue an access token with a validity of 24 hours and the "voip" scope for an identity
            var tokenResponse = await _client.GetTokenAsync(new CommunicationUserIdentifier(userId), scopes: new[] { CommunicationTokenScope.VoIP });
            _userIdentityStore.IsPrimaryUserTokenAcquired = true;
            // Get the token from the response
            var token = tokenResponse.Value.Token;
            var expiresOn = tokenResponse.Value.ExpiresOn;
            _logger.LogInformation($"Issued an access token with 'voip' scope that expires at {expiresOn}:");
            _logger.LogInformation($"Toke: {token}");
            return Ok(token);
        }
    }
}