using CrmApiV2.Dtos.Account;
using CrmApiV2.Dtos.Response;
using CrmApiV2.Interface;
using CrmApiV2.Mapper;
using CrmApiV2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CrmApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => new ErrorDetailDto
                                              {
                                                  Field = "",
                                                  Message = e.ErrorMessage
                                              })
                                              .ToList();

                    return BadRequest(new ErrorResponseDto
                    {
                        Status = "error",
                        Message = "Validation failed.",
                        Errors = errors
                    });

                }

                var appUser = new ApplicationUser
                {
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                    CompanyId = registerDto.ComapnyId,
                    Name = registerDto.Name,
                    PhoneNumber = registerDto.PhoneNumber,
                    Address = registerDto.Address,
                    CreatedOn = DateTime.Now,
                };

                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, registerDto.Role);
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new ApiResponseDto<SignupResponseDataDto>
                            {
                                Status = "Success",
                                Message = "Account Created Successfully",
                                Data = new SignupResponseDataDto
                                {
                                    UserId = appUser.Id,
                                    Email = appUser.Email,
                                    Username = appUser.UserName,
                                    Token = _tokenService.CreateToken(appUser)
                                }
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, new ErrorResponseDto
                        {
                            Status = "error",
                            Message = "An unexpected error occurred. Please try again later.",
                            Errors = new List<ErrorDetailDto>
                    {
                        new ErrorDetailDto
                        {
                            Field = "internal",
                            Message = createUser.Errors.ToString() // In production, avoid exposing internal messages.
                        }
                    }
                        });
                    }
                }
                else
                {
                    return StatusCode(500, new ErrorResponseDto
                    {
                        Status = "error",
                        Message = "An unexpected error occurred. Please try again later.",
                        Errors = new List<ErrorDetailDto>
                    {
                        new ErrorDetailDto
                        {
                            Field = "internal",
                            Message = createUser.Errors.ToString() // In production, avoid exposing internal messages.
                        }
                    }
                    });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new ErrorResponseDto
                {
                    Status = "error",
                    Message = "An unexpected error occurred. Please try again later.",
                    Errors = new List<ErrorDetailDto>
                    {
                        new ErrorDetailDto
                        {
                            Field = "internal",
                            Message = ex.Message
                        }
                    }
                });
            }
        }



        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Email);
            if (user == null)
            {
                return Unauthorized("Invalid Username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                Unauthorized("Username not found");
            }
            return Ok(
                new ApiResponseDto<SignupResponseDataDto>
                {
                    Status = "Success",
                    Message = "Account Created Successfully",
                    Data = new SignupResponseDataDto
                    {
                        UserId = user.Id,
                        Email = user.Email,
                        Username = user.UserName,
                        Token = _tokenService.CreateToken(user)
                    }
                }
            );
        }


        [HttpGet("roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(s => s.Name).ToListAsync();
            return Ok(new ApiResponseDto<List<string>>
            {
                Status = "Success",
                Message = "Roles retrieved successfully",
                Data = roles
            });
        }

        [HttpGet("users")]
        [Authorize]
        public async Task<ActionResult> GetAllUsers()
        {

            var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            if (loggedInUser == null)
            {
                return NotFound(new ErrorResponseDto
                {
                    Status = "error",
                    Message = "Logged-in user not found",
                    Errors = new List<ErrorDetailDto>
            {
                new ErrorDetailDto
                {
                    Field = "userId",
                    Message = "Logged-in user does not exist"
                }
            }
                });
            }

            var users = await _userManager.Users
                                          .Where(user => user.CompanyId == loggedInUser.CompanyId)
                                          .Select(user => user.ToUserDto()).ToListAsync();

            return Ok(new ApiResponseDto<List<UserDto>>
            {
                Status = "Success",
                Message = "Users retrieved successfully",
                Data = users
            });
        }

    }
}
