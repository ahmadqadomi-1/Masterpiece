﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.DTOs;
using MyProject.Models;
using System.Linq;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext _db;
        private readonly TokenGenerator _tokenGenerator;
        public UserController(MyDbContext db, TokenGenerator tokenGenerator)
        {
            _db = db;
            _tokenGenerator = tokenGenerator;
        }

        
        private string ValidatePassword(string password)
        {
            if (password.Length < 8 || password.Length > 20)
                return "Password must be between 8 and 20 characters long.";

            if (password.All(char.IsDigit))
                return "Password must contain letters and symbols, not just numbers.";

            if (password.All(char.IsUpper))
                return "Password must contain lowercase letters, numbers, and symbols.";

            if (password.All(char.IsLower))
                return "Password must contain uppercase letters, numbers, and symbols.";

            if (password.All(char.IsLetter))
                return "Password must contain numbers and symbols.";

            if (!password.Any(char.IsUpper))
                return "Password must contain at least one uppercase letter.";

            if (!password.Any(char.IsLower))
                return "Password must contain at least one lowercase letter.";

            if (!password.Any(char.IsDigit))
                return "Password must contain at least one digit.";

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                return "Password must contain at least one special character.";

            return null; 
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUser()
        {
            var data = _db.Users.ToList();
            return Ok(data);
        }

        [HttpGet("GetAllUsersById/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var data = _db.Users.FirstOrDefault(c => c.Email == name);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }



        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            var data = _db.Users.Find(id);
            if (data == null)
            {
                return NotFound();
            }
            _db.Users.Remove(data);
            _db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromForm] DTOsUser user)
        {
            if (user == null)
            {
                return BadRequest("User data is null");
            }

            var existingUser = _db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return BadRequest("This email is already registered.");
            }

            var passwordValidationResult = ValidatePassword(user.Password);
            if (passwordValidationResult != null)
            {
                return BadRequest(passwordValidationResult);
            }

            byte[] hash, salt;
            passwordhash.CreatePasswordHash(user.Password, out hash, out salt);

            var newUser = new User
            {
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();
            return Ok(newUser);
        }

        [HttpGet("CheckEmail")]
        public IActionResult CheckEmail(string email)
        {
            var existingUser = _db.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                return Ok(true);  
            }
            return Ok(false);  
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] DTOsUser user)
        {
            if (user == null)
            {
                return BadRequest("User data is null");
            }

            
            var storedUser = _db.Users.FirstOrDefault(u => u.Email == user.Email);
            if (storedUser == null || !passwordhash.VerifyPasswordHash(user.Password, storedUser.PasswordHash, storedUser.PasswordSalt))
            {
                return Unauthorized("Invalid email or password.");
            }

            
            var roles = _db.UserRoles.Where(x => x.UserId == storedUser.UserId).Select(ur => ur.Role).ToList();

            
            var token = _tokenGenerator.GenerateToken(user.Email, roles);

            return Ok(new { Token = token, storedUser.UserId });
        }


        [HttpGet("Login")]
        public IActionResult Login([FromQuery] DTOsLogin user)
        {
            if (user == null)
            {
                return BadRequest("User data is null");
            }

            var record = _db.Users.FirstOrDefault(u => u.Email == user.UserName);
            if (record != null && passwordhash.VerifyPasswordHash(user.Password, record.PasswordHash, record.PasswordSalt))
            {
                return Ok("Login successful");
            }

            return Unauthorized("Username or password is wrong");
        }

        
        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword([FromBody] ChangePasswordDTOs changePasswordDto)
        {
            if (changePasswordDto == null)
            {
                return BadRequest("Invalid data.");
            }

            
            var user = _db.Users.FirstOrDefault(u => u.Email == changePasswordDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            
            if (!passwordhash.VerifyPasswordHash(changePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Old password is incorrect.");
            }

            
            var passwordValidationResult = ValidatePassword(changePasswordDto.NewPassword);
            if (passwordValidationResult != null)
            {
                return BadRequest(passwordValidationResult); 
            }

            
            byte[] hash, salt;
            passwordhash.CreatePasswordHash(changePasswordDto.NewPassword, out hash, out salt);

            
            user.PasswordHash = hash;
            user.PasswordSalt = salt;

            _db.SaveChanges();
            return Ok("Password changed successfully.");
        }
    }
}
