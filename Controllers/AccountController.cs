using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NGK_Assignment_3.Areas.Database;
using NGK_Assignment_3.Areas.Database.Models;

namespace NGK_Assignment_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private NGKDbContext _context;
        private IMapper _mapper;
        byte[] key = Encoding.ASCII.GetBytes("12312dklfjdhgkh3892345867234987!¤%#%¤&3123123");

        public AccountController(NGKDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        [Authorize]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register([FromBody]UserDto regUser)
        {
            var userExists = await _context.Users.Where(user => user.Email == regUser.Email).FirstOrDefaultAsync();
            if (userExists != null) return BadRequest("User already exists");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(regUser.Password);
            var user = _mapper.Map<User>(regUser);
            user.PasswordHash = hashedPassword;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return await Login(regUser);
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login([FromBody]UserDto loginUser)
        {
            loginUser.Email = loginUser.Email.ToLower();
            var user = await _context.Users.Where(user => user.Email == loginUser.Email).FirstOrDefaultAsync();
            if (user == null) return BadRequest("User doesn't exist.");
            
            var validPwd = BCrypt.Net.BCrypt.Verify(loginUser.Password, user.PasswordHash);
            if (!validPwd) return BadRequest("Invalid login");

            //Authentication successful, Issue Token with user credentials
            //Provide the security key which was given in the JWToken configuration in Startup.cs
            var key = Encoding.ASCII.GetBytes
                ("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            //Generate Token for user 
            var JWToken = new JwtSecurityToken(
                issuer: "https://localhost:44399/",
                audience: "https://localhost:44399/",
                claims: GetUserClaims(user),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                //Using HS256 Algorithm to encrypt Token
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            );
            loginUser.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);

            return loginUser;
        }

        // PUT: api/Account/5
        [HttpPut("{id}")]
        public void Put([FromBody] UserDto regUser)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete([FromBody] UserDto regUser)
        {
        }

        private IEnumerable<Claim> GetUserClaims(User userDto)
        {
            return new[]
            {
                new Claim(ClaimTypes.Email, userDto.Email)
            };
        }
    }
}
