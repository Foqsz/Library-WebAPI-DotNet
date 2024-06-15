﻿using BibliotecaApi.Library.Application.DTOs;
using BibliotecaApi.Library.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiCatalogo.Catalogo.Core.Model;

namespace WebApiCatalogo.Catalogo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<LibraryUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ITokenService tokenService, UserManager<LibraryUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        [Route("CreateRole")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);

            if (!roleExist)
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));

                if (roleResult.Succeeded)
                {
                    _logger.LogInformation(1, "Roles Added");
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Sucess", Message = $"Role {roleName} added successfully" });
                }
            }
            else
            {
                _logger.LogInformation(2, "Error");
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Status = "Error", Message = $"Issue adding the new {roleName} role" });
            }
            return StatusCode(StatusCodes.Status400BadRequest,
                new Response { Status = "Error", Message = "Role already exist." });
        }

        [HttpPost]
        [Route("AddUserToRole")]
        //[Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> AddUserToRole(string email, string roleName)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);

                if (result.Succeeded)
                {
                    _logger.LogInformation(1, $"User {user.Email} addded to the {roleName} role");
                    return StatusCode(StatusCodes.Status200OK,
                        new Response { Status = "Success", Message = $"User {user.Email} added to the {roleName} role" });
                }
                else
                {
                    _logger.LogInformation(1, $"Error: unable to add user {user.Email} to the {roleName} role");
                    return StatusCode(StatusCodes.Status400BadRequest,
                        new Response { Status = "Error", Message = $"Error: unable to add user {user.Email} to the {roleName} role" });
                }
            }
            return BadRequest(new { error = "Unable to find user" });
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModelDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Name!);

            if (user is not null && await _userManager.CheckPasswordAsync(user, model.Password!))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim("id", user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GenerateAcessToken(authClaims, _configuration);

                var refreshToken = _tokenService.GenerateRefreshToken();

                _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInMinutes"], out int refreshTokenValidityInMinutes);

                user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInMinutes);

                user.RefreshToken = refreshToken;

                await _userManager.UpdateAsync(user);

                return Ok(new
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                });
            }
            return Unauthorized();

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModelDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Name!);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            }

            LibraryUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Name,
            };

            var result = await _userManager.CreateAsync(user, model.Password!);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed." });
            }
            return Ok(new Response { Status = "Sucess", Message = "User created sucessefully!" });
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(TokenModelDTO tokenModel)
        {
            if (tokenModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? acessToken = tokenModel.AcessToken ?? throw new ArgumentException(nameof(tokenModel));

            string? refreshToken = tokenModel.RefreshToken ?? throw new ArgumentException(nameof(tokenModel));

            var principal = _tokenService.GetPrincipalFromExperiredToken(acessToken!, _configuration);

            if (principal == null)
            {
                return BadRequest("Invalid acess token/refresh token");
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username!);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return BadRequest("Invalid acess token/refresh token");
            }

            var newAcessToken = _tokenService.GenerateAcessToken(principal.Claims.ToList(), _configuration);

            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;

            await _userManager.UpdateAsync(user);

            return new ObjectResult(new
            {
                acessToken = new JwtSecurityTokenHandler().WriteToken(newAcessToken),
                refreshToken = newRefreshToken
            });
        }
         
        [HttpPost]
        [Route("revoke/{username}")]
        public async Task<IActionResult> Revoke(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return BadRequest("Invalid user name");
            }

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return Ok("Execução realizada com sucesso.");
        }
         
    }
}