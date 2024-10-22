using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_Management_System.DTOs;
using Task_Management_System.Model;

namespace Task_Management_System.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration configuration) {
        
        
          this._userManager = userManager;
            this._configuration = configuration;
        
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<ActionResult> Adduser(UserDto user)
        {

            if (user == null)
            {
                return BadRequest();
            }

            if(ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    UserName = user.UserName,
                    Email=user.UserEmail
                };

           IdentityResult result =     await _userManager.CreateAsync(applicationUser, user.Password);
                 if (result.Succeeded)
                {
                    return Ok();
                }
            }

            return BadRequest(ModelState);
        }


        [HttpGet("api/[controller]")]
        public ActionResult gatall()
        {
            var users = _userManager.Users.ToList();

            return Ok(users);

        }

        [HttpPost("api/[controller]/login")]
       
        public async Task<ActionResult> login(loginDtos login)
        {
            if (ModelState.IsValid)
            {
               ApplicationUser user  =    await   _userManager.FindByNameAsync(login.Username);
             
                if (user != null) { 
                 

                    bool found = await _userManager.CheckPasswordAsync(user, login.Password);
                    if (found)
                    {
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name,login.Username));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sdfghjllrtyyuuqwqeretrtqweerttyuuioplkjhgfdsaaZZXCCCVVBBN"));
                        SigningCredentials signing = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken token = new JwtSecurityToken(

                          issuer: _configuration["Jwt:validissuer"],
                          audience: _configuration["Jwt:validaudience"],
                          claims: claims,
                          expires: DateTime.Now.AddDays(1),
                          signingCredentials: signing




                          );
                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            fullname=user.UserName,
                            ID=user.Id,
                      
                        
                        
                        });

                    }



                }
                return BadRequest(ModelState);
            }
            return Unauthorized();
        }

        [HttpGet("api/[controller]/get")]
        [Authorize]
       
        public ActionResult get()
        {
            return Ok();
        }
    }
}
