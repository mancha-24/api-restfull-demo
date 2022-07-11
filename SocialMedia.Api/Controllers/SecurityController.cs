using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerators;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Interfaces;
using SocialMediaApi.Responses;

namespace SocialMediaApi.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class SecurityController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        private readonly ISecurityService _securityService;
        private readonly IPasswordService _passwordService;
        private readonly IMapper _mapper;

        public SecurityController(ILogger<PostController> logger, 
                              ISecurityService securityService,
                              IMapper mapper,
                              IPasswordService passwordService)
        {
            _logger = logger;
            _securityService = securityService;
            _mapper = mapper;
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(SecurityDto securityDto)
        {
            var security = _mapper.Map<Security>(securityDto);

            security.Password = _passwordService.Hash(security.Password);
            await _securityService.RegisterUser(security);

            securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }
    }
}