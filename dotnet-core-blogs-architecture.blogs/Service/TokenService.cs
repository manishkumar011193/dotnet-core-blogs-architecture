using dotnet_core_blogs_architecture.Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnet_core_blogs_architecture.blogs.Service
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;
        private readonly UserManager<Data.Models.User> _userManager;
        private readonly SignInManager<Data.Models.User> _signInManager;
        private readonly IRepository<Data.Models.User> _userRepository;


        public TokenService(
            IConfiguration config,
            UserManager<Data.Models.User> userManager,
            SignInManager<Data.Models.User> signInManager,
            IRepository<Data.Models.User> userRepository
          )
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;

        }        

        public async Task<string> BuidToken(Data.Models.User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,  Guid.NewGuid().ToString())
            };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              _config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(int.Parse(_config["Jwt:AccessTokenDurationInMinutes"])),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
