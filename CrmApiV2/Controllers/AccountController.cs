using CrmApiV2.Data;
using CrmApiV2.Dtos.Account;
using CrmApiV2.Dtos.Email;
using CrmApiV2.Dtos.Response;
using CrmApiV2.Interface;
using CrmApiV2.Mapper;
using CrmApiV2.Models;
using CrmApiV2.Models.Register;
using CrmApiV2.Utilities;
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
        private readonly ApplicationDbContext _db;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<ApplicationUser> userManager, 
            ITokenService tokenService, 
            SignInManager<ApplicationUser> signInManager, 
            RoleManager<IdentityRole> roleManager, 
            ApplicationDbContext db,
            IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _db = db;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
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

            var existingUser = await _userManager.FindByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                return BadRequest(new ErrorResponseDto
                {
                    Status = "error",
                    Message = "Email is already taken. Please use a different email.",
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

            try
            {
                var createUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (!createUser.Succeeded)
                {
                    var errorMessage = string.Join("; ", createUser.Errors.Select(e => e.Description));
                    return BadRequest(new ErrorResponseDto
                    {
                        Status = "error",
                        Message = $"Failed to create the user: {errorMessage}",
                    });
                }

                try
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, registerDto.Role);
                    if (!roleResult.Succeeded)
                    {
                        var deleteResult = await _userManager.DeleteAsync(appUser);
                        if (!deleteResult.Succeeded)
                        {
                            return StatusCode(500, new ErrorResponseDto
                            {
                                Status = "error",
                                Message = "Failed to assign the role and unable to delete the user. Please contact support.",
                            });
                        }

                        return StatusCode(500, new ErrorResponseDto
                        {
                            Status = "error",
                            Message = "Failed to assign the role to the user. The user has been deleted.",
                        });
                    }
                }
                catch (Exception ex)
                {
                    // Handle any exceptions during role assignment or deletion
                    await _userManager.DeleteAsync(appUser); // Attempt to delete the user in case of failure
                    return StatusCode(500, new ErrorResponseDto
                    {
                        Status = "error",
                        Message = $"An error occurred during role assignment: {ex.Message}",
                    });
                }

                var emailDto = new EmailDto
                {
                    To = appUser.Email,
                    Subject = "Your Access Credentials For NexaCrm",
                    Body = $@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Access Credentials</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            color: #333;
            margin: 0;
            padding: 20px;
        }}
        .container {{
            max-width: 600px;
            background-color: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            margin: auto;
        }}
        h1 {{
            font-size: 24px;
            color: #0056b3;
        }}
        p {{
            font-size: 16px;
            line-height: 1.6;
        }}
        .credentials {{
            background-color: #f8f9fa;
            padding: 15px;
            border-left: 4px solid #007bff;
            margin: 20px 0;
            border-radius: 4px;
        }}
        .credentials p {{
            margin: 0;
            font-size: 16px;
        }}
        .footer {{
            margin-top: 20px;
            font-size: 14px;
            color: #777;
        }}
        .footer a {{
            color: #0056b3;
            text-decoration: none;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Access Credentials for NexaCrm</h1>

        <p>Dear {appUser.Name},</p>

        <p>I hope this message finds you well.</p>

        <p>As part of our ongoing collaboration, we are providing you with access credentials to <strong>NexaCrm</strong>. Please find the details below:</p>

        <div class=""credentials"">
            <p><strong>Email:</strong> {appUser.Email}</p>
            <p><strong>Password:</strong> {registerDto.Password}</p>
        </div>

        <p><strong>Instructions:</strong></p>
        <ol>
            <li><strong>Login:</strong> Please use the above credentials to log in to <strong>NexaCrm</strong> via <strong>https://shubham.com/</strong>.</li>
            
            <li style=""margin-top: 13px;"" ><strong>Support:</strong> Should you encounter any issues or require assistance, please do not hesitate to contact our support team at <strong>+91 8617742849</strong>.</li>
        </ol>

        <p>We are committed to ensuring that you have a smooth experience using our services. If you have any questions or require further clarification, feel free to reach out to me directly.</p>

        <p>Thank you for your continued trust and partnership.</p>

        <p>Best regards,</p>
        <p><strong>Shubham Gupta</strong><br>
        <br>
        NexaCrm<br>
        +91 8617742849<br>
        shubhamgupta123@gmail.com</p>

        <div class=""footer"">
            <p><strong>Security Note:</strong> Please ensure that these credentials are stored securely and not shared with unauthorized personnel. We advise enabling two-factor authentication (2FA) to further protect your account.</p>
        </div>
    </div>
</body>
</html>
"
                };

                _emailService.SendEmail(emailDto);


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

            
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            // strart login time
            var timeLog = new UserTimeLog
            {
                ApplicationUserId = user.Id,
                LoginTime = DateTime.UtcNow
            };

            _db.UserTimeLogs.Add(timeLog);
            await _db.SaveChangesAsync();

            return Ok(
                new ApiResponseDto<SignupResponseDataDto>
                {
                    Status = "success",
                    Message = "Login successful.",
                    Data = new SignupResponseDataDto
                    {
                        Name = user.Name,
                        Role = role,
                        UserId = user.Id,
                        Email = user.Email,
                        Mobile = user.PhoneNumber,
                        Username = user.UserName,
                        Token = _tokenService.CreateToken(user)
                    }
                }
            );
        }

        [HttpPost("logout/{userId}")]
        [Authorize]
        public async Task<IActionResult> Logout([FromRoute] string userId)
        {
            var timeLog = await _db.UserTimeLogs
                .Where(tl => tl.ApplicationUserId == userId && tl.LogoutTime == null)
                .OrderByDescending(tl => tl.LoginTime)
                .FirstOrDefaultAsync();

            if (timeLog == null)
            {
                return BadRequest("No active login session found.");
            }

            timeLog.LogoutTime = DateTime.UtcNow;

            // Calculate break time if there was a previous session
            var previousLog = await _db.UserTimeLogs
                .Where(tl => tl.ApplicationUserId == userId && tl.LogoutTime != null)
                .OrderByDescending(tl => tl.LogoutTime)
                .FirstOrDefaultAsync();

            TimeSpan breakTime = TimeSpan.Zero;


            if (previousLog != null)
            {
                breakTime = timeLog.LoginTime - previousLog.LogoutTime ?? TimeSpan.Zero;
                timeLog.BreakTime = breakTime;
            }

            // Calculate working time
            TimeSpan workingTime = timeLog.LogoutTime.Value - timeLog.LoginTime;

            // Update daily summary
            var summary = await _db.DailyUserSummaries
                .Where(s => s.ApplicationUserId == userId && s.Date == DateTime.UtcNow.Date)
                .FirstOrDefaultAsync();

            if (summary == null)
            {
                summary = new DailyUserSummary
                {
                    ApplicationUserId = userId,
                    Date = DateTime.UtcNow.Date,
                    TotalWorkingTime = workingTime,
                    TotalBreakTime = breakTime
                };
                _db.DailyUserSummaries.Add(summary);
            }
            else
            {
                summary.TotalWorkingTime += workingTime;
                summary.TotalBreakTime += breakTime;
                _db.DailyUserSummaries.Update(summary);
            }

            await _db.SaveChangesAsync();

            return Ok("Logout recorded successfully.");
        }

        [HttpGet("roles")]
        public async Task<ActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.Select(s => s.Name).ToListAsync();
            return Ok(new ApiResponseDto<List<string>>
            {
                Status = "success",
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
                    Address = user.Address,
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

            var users = usersInRole.Where(user => user.CompanyId == loggedInUser.CompanyId)
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
                Status = "success",
                Message = "Users retrieved successfully",
                Data = users
            });
        }

        [HttpGet("userWorkingStatus/{userId}")]
        [Authorize]
        public async Task<ActionResult> UserWorkingStatus([FromRoute] string userId)
        {
            var workingStatusList = await _db.DailyUserSummaries.Where(u => u.ApplicationUserId == userId).ToListAsync();

            if (!workingStatusList.Any())
            {
                return NotFound(new ErrorResponseDto
                {
                    Status = "error",
                    Message = $"No Record Found.",
                });
            }

            return Ok(new ApiResponseDto<List<DailyUserSummary>>
            {
                Status = "success",
                Message = "Users retrieved successfully",
                Data = workingStatusList
            });
        }

        [HttpPost("startBreakTime/{userId}")]
        [Authorize]
        public async Task<IActionResult> StartBreakTime([FromRoute] string userId)
        {
            var timeLog = await _db.UserTimeLogs
                .Where(tl => tl.ApplicationUserId == userId && tl.LogoutTime == null)
                .OrderByDescending(tl => tl.LoginTime)
                .FirstOrDefaultAsync();

            if (timeLog == null)
            {
                return BadRequest("No active login session found.");
            }

            timeLog.LogoutTime = DateTime.UtcNow;

            // Calculate break time if there was a previous session
            var previousLog = await _db.UserTimeLogs
                .Where(tl => tl.ApplicationUserId == userId && tl.LogoutTime != null)
                .OrderByDescending(tl => tl.LogoutTime)
                .FirstOrDefaultAsync();

            TimeSpan breakTime = TimeSpan.Zero;


            if (previousLog != null)
            {
                breakTime = timeLog.LoginTime - previousLog.LogoutTime ?? TimeSpan.Zero;
                timeLog.BreakTime = breakTime;
            }

            // Calculate working time
            TimeSpan workingTime = timeLog.LogoutTime.Value - timeLog.LoginTime;

            // Update daily summary
            var summary = await _db.DailyUserSummaries
                .Where(s => s.ApplicationUserId == userId && s.Date == DateTime.UtcNow.Date)
                .FirstOrDefaultAsync();

            if (summary == null)
            {
                summary = new DailyUserSummary
                {
                    ApplicationUserId = userId,
                    Date = DateTime.UtcNow.Date,
                    TotalWorkingTime = workingTime,
                    TotalBreakTime = breakTime
                };
                _db.DailyUserSummaries.Add(summary);
            }
            else
            {
                summary.TotalWorkingTime += workingTime;
                summary.TotalBreakTime += breakTime;
                _db.DailyUserSummaries.Update(summary);
            }

            await _db.SaveChangesAsync();

            return Ok(new ApiResponseDto<string>
            {
                Status = SD.Success,
                Message = "Breaktime Start"
            }
                  );
        }

        [HttpPost("endBreakTime/{userId}")]
        [Authorize]
        public async Task<IActionResult> EndBreakTime([FromRoute] string userId)
        {
            var timeLog = new UserTimeLog
            {
                ApplicationUserId = userId,
                LoginTime = DateTime.UtcNow
            };

            _db.UserTimeLogs.Add(timeLog);
            await _db.SaveChangesAsync();

            return Ok(new ApiResponseDto<string>
            {
                Status = SD.Success,
                Message = "Breaktime End"
            }
                 );
        }

    }
}