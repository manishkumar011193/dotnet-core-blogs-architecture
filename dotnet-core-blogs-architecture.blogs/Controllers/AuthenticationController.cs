using Ardalis.Specification;
using dotnet_core_blogs_architecture.blogs.Service;
using dotnet_core_blogs_architecture.blogs.Specification;
using dotnet_core_blogs_architecture.blogs.ViewModels;
using dotnet_core_blogs_architecture.Data.Models;
using dotnet_core_blogs_architecture.infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace dotnet_core_blogs_architecture.blogs.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IRepository<User> _userRepository;
        private readonly ITokenService _tokenService;
        const int keySize = 64;
        byte[] salt = RandomNumberGenerator.GetBytes(keySize);
        const int iterations = 350000;

        public AuthenticationController(SignInManager<User> signInManager, IRepository<User> userRepository, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password)))
            {
                var user = await _userRepository.FirstOrDefaultAsync(new UserWithEmailSpecification(model.Email));
                if (user == null)
                {
                    return BadRequest(new { Message = "Incorrect username or password" });
                }

                var result = VerifyPassword(model.Password, user.PasswordHash, salt);
                if (result)
                {
                    var token = await _tokenService.BuidToken(user);
                    return Json(token);
                }
                else
                {
                    return BadRequest(new { Message = "Incorrect username or password" });
                }
            }

            return BadRequest(new { Message = "Please provide both username and password" });
        }


        bool VerifyPassword(string password, string hash, byte[] salt)
        {
            HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;
            salt = RandomNumberGenerator.GetBytes(keySize);
            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithm, keySize);

            return CryptographicOperations.FixedTimeEquals(hashToCompare, Convert.FromHexString(hash));
        }
    }
}
