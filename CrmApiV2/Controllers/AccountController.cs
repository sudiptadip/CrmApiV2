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
using System.Data;
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
                    var errorMessage = string.Join("; ", ModelState.Values
                                               .SelectMany(v => v.Errors)
                                               .Select(e => e.ErrorMessage));

                    return BadRequest(new ErrorResponseDto
                    {
                        Status = "error",
                        Message = $"Validation failed: {errorMessage}",
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
                                Status = "success",
                                Message = "Account created successfully.",
                                Data = new SignupResponseDataDto
                                {
                                    Name = appUser.Name,
                                    Username = appUser.UserName,
                                    UserId = appUser.Id,
                                    Email = appUser.Email,
                                    Mobile = appUser.PhoneNumber,
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
                            Message = "Failed to assign the role to the user. Please try again later.",
                        });
                    }
                }
                else
                {
                    var errorMessage = string.Join("; ", createUser.Errors.Select(e => e.Description));
                    return BadRequest(new ErrorResponseDto
                    {
                        Status = "error",
                        Message = $"Failed to create the user: {errorMessage}",
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponseDto
                {
                    Status = "error",
                    Message = $"An internal server error occurred: {ex.Message}",
                });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = string.Join("; ", ModelState.Values
                                           .SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage));

                return BadRequest(new ErrorResponseDto
                {
                    Status = "error",
                    Message = $"Validation failed: {errorMessage}",
                });
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Email);
            if (user == null)
            {
                return Unauthorized(new ErrorResponseDto
                {
                    Status = "error",
                    Message = "Invalid username or email."
                });
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized(new ErrorResponseDto
                {
                    Status = "error",
                    Message = "Incorrect password. Please try again."
                });
            }

            return Ok(
                new ApiResponseDto<SignupResponseDataDto>
                {
                    Status = "success",
                    Message = "Login successful.",
                    Data = new SignupResponseDataDto
                    {
                        Name = user.Name,
                        UserId = user.Id,
                        Email = user.Email,
                        Mobile = user.PhoneNumber,
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
                });
            }

            var users = await _userManager.Users
                                          .Where(user => user.CompanyId == loggedInUser.CompanyId)
                                          .ToListAsync();

            if (!users.Any())
            {
                return NotFound(new ErrorResponseDto
                {
                    Status = "error",
                    Message = "No users found in your company.",
                });
            }

            var usersWithRoles = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                usersWithRoles.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber,
                    Address =user.Address,
                    UserName = user.UserName,
                    CompanyId = user.CompanyId,
                    Roles = roles.ToList()
                });
            }

            return Ok(new ApiResponseDto<List<UserDto>>
            {
                Status = "success",
                Message = "Users retrieved successfully",
                Data = usersWithRoles
            });
        }


        [HttpGet("usersByRole/{role}")]
        [Authorize]
        public async Task<ActionResult> GetAllUsersByRole([FromRoute] string role)
        {

            var loggedInUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var loggedInUser = await _userManager.FindByIdAsync(loggedInUserId);
            if (loggedInUser == null)
            {
                return NotFound(new ErrorResponseDto
                {
                    Status = "error",
                    Message = "Logged-in user not found",
                });
            }

            var usersInRole = await _userManager.GetUsersInRoleAsync(role);
            if (usersInRole == null || !usersInRole.Any())
            {
                return NotFound(new ErrorResponseDto
                {
                    Status = "error",
                    Message = $"No users found with the role '{role}'",
                });
            }

            var users =  usersInRole.Where(user => user.CompanyId == loggedInUser.CompanyId)
                                          .Select(user => user.ToUserDto()).ToList();

            if (!users.Any())
            {
                return NotFound(new ErrorResponseDto
                {
                    Status = "error",
                    Message = $"No users found with the role '{role}' in your company.",
                });
            }


            return Ok(new ApiResponseDto<List<UserDto>>
            {
                Status = "Success",
                Message = "Users retrieved successfully",
                Data = users
            });
        }

    }
}
