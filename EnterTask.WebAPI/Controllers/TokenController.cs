using AutoMapper;
using EnterTask.Application.Services.Interfaces;
using EnterTask.Data.DataEntities;
using EnterTask.Data.Services;
using EnterTask.WebAPI.DTOs;
using EnterTask.WebAPI.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EnterTask.WebAPI.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<TokenController> _logger;
        private readonly IMapper _mapper;

        public TokenController(IPersonService personService,
            ILogger<TokenController> logger,
            IMapper mapper)
        {
            _personService = personService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("get", Name = "GetToken")]
        public async Task<IActionResult> Token([FromBody] LoginDTO model)
        {
            var res = await _personService.EnsureLoginAsync(model.Login, model.Password);
            _logger.LogInformation(res.Message);

            var identity = GetIdentity(res.Value!);
            var encodedJwt = GetEncodedJwtToken(identity);

            var response = new {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        [HttpPost("create/admin", Name = "CreateAdmin")]
        public async Task<IActionResult> CreateAdmin([FromBody] PersonDTO model)
        {
            var entity = _mapper.Map<Person>(model);
            entity.Role = "admin";

            var res = await _personService.AddLoginToParticipantAsync(entity.ParticipantId, entity);
            _logger.LogInformation(res.Message);

            return Ok();
        }

        [HttpPost("create", Name = "CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] PersonDTO model)
        {
            var entity = _mapper.Map<Person>(model);
            entity.Role = "user";

            var res = await _personService.AddLoginToParticipantAsync(entity.ParticipantId, entity);
            _logger.LogInformation(res.Message);

            return Ok();
        }

        [HttpDelete("delete/{participantId}", Name = "DeletePerson")]
        public async Task<IActionResult> Remove(int participantId)
        {
            var res = await _personService.DeleteLoginAsync(participantId);

            _logger.LogInformation(res.Message);
            return Ok();
        }

        private ClaimsIdentity GetIdentity(Person person)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Login),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role),
                    new Claim("custom_claim", "true"),
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }

        private string? GetEncodedJwtToken(ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
