using System;
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
        private JwtSecurityTokenHandler _tokenHandler = new JwtSecurityTokenHandler();
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

            var token = _tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>() {new Claim(ClaimTypes.Email, user.Email)}),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            regUser.Token = _tokenHandler.WriteToken(token);

            return regUser;
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

            var token = _tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new List<Claim>() { new Claim(ClaimTypes.Email, user.Email) }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });
            loginUser.Token = _tokenHandler.WriteToken(token);
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
    }
}
