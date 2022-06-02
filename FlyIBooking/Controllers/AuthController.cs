using System;
using System.Threading.Tasks;
using FlyIBooking.Dtos;
using FlyIBooking.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FlyIBooking.Controllers
{
    /// <summary>
    /// This controller contains authorization operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public sealed class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        private readonly ILogger<AuthController> _logger;

        private IConfiguration Configuration { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="authService">Auth service</param>
        /// <param name="logger">Logger</param>
        /// <param name="configuration">Configuration</param>
        public AuthController(
            AuthService authService,
            ILogger<AuthController> logger,
            IConfiguration configuration)
        {
            _authService = authService;
            _logger = logger;
            Configuration = configuration;
            _authService.SecurityKey = Configuration.GetSection("AuthKey").GetValue<string>("Secret");
        }

        /// <summary>
        /// Registration action
        /// </summary>
        /// <param name="registerDto">Requested dto for registration on platform</param>
        /// <returns>Returns JWT</returns>
        [HttpPost("register")]
        public async Task<ActionResult<JwtDto>> RegisterAsync([FromBody] AccountRegisterDto registerDto)
        {
            if (!ModelState.IsValid || registerDto == null)
            {
                return BadRequest();
            }

            try
            {
                var token = await _authService.RegisterAsync(registerDto);
                var jwtReturnedDto = new JwtDto
                {
                    JwtString = token
                };

                return CheckTokenAndReturn(jwtReturnedDto, "Register failed!");
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning("Argument null exception. " + e.ParamName);
                return BadRequest();
            }
        }

        /// <summary>
        /// Login action
        /// </summary>
        /// <param name="loginDto">Requested dto for login on platform</param>
        /// <returns>Returns JWT</returns>
        [HttpPost("login")]
        public async Task<ActionResult<JwtDto>> LoginAsync([FromBody] AccountLoginDto loginDto)
        {
            if (!ModelState.IsValid || loginDto == null)
            {
                return BadRequest();
            }

            try
            {
                var token = await _authService.LoginAsync(loginDto);
                var jwtReturnedDto = new JwtDto
                {
                    JwtString = token
                };

                return CheckTokenAndReturn(jwtReturnedDto, "Login failed!");
            }
            catch (ArgumentNullException e)
            {
                _logger.LogWarning(e.Message);
                return BadRequest();
            }
        }

        private ActionResult<JwtDto> CheckTokenAndReturn(JwtDto jwt, string message)
        {
            if (!string.IsNullOrEmpty(jwt.JwtString))
                return Ok(jwt);

            if (!string.IsNullOrEmpty(message))
                _logger.LogWarning(message);

            return BadRequest("Authorization failed");
        }
    }
}