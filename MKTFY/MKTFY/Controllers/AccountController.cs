//using AutoMapper.Configuration;
//using AutoMapper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization; // for Authorization for the Registeration
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using MKTFY.App.Repositories;
using MKTFY.App.Repositories.Interfaces;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<User> _signinManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;

        //private readonly IMapper _mapper;       //private readonly UserRepository _userRepository;

        public AccountController(SignInManager<User> signinManager, IConfiguration configuration,
            IUserRepository userRepository, UserManager<User> userManager, IMailService mailService) // , IMapper mapper 
        {
            _signinManager = signinManager;
            _configuration = configuration;
            _userRepository = userRepository;
            _userManager = userManager;
            _mailService = mailService;
            //_mapper = mapper;
        }

        //api/account/login
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseVM>> Login([FromBody] LoginVM login)
        {
            // Validate the user login
            var result = await _signinManager.PasswordSignInAsync(login.Email, login.Password, false, true).ConfigureAwait(false);

            if (result.IsLockedOut)
                return BadRequest("This user account has been locked out, please try again later");
            else if (!result.Succeeded)
                return BadRequest("Invalid username/password");

            // Get the user profile
            var user = await _userRepository.GetByEmail(login.Email);

            // Get a token from the identity server
            using (var httpClient = new HttpClient())
            {
                var authority = _configuration.GetSection("Identity").GetValue<string>("Authority");

                // Make the call to our identity server
                var tokenResponse = await httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = authority + "/connect/token",
                    UserName = login.Email,
                    Password = login.Password,
                    ClientId = login.ClientId,
                    ClientSecret = "UzKjRFnAHffxUFati8HMjSEzwMGgGHmN",
                    //Scope = "mktfyapi.scope"
                    Scope = "mktfyapi.scope" //  roles

                }).ConfigureAwait(false);

                if (tokenResponse.IsError)
                {
                    return BadRequest(tokenResponse.Error);
                    //return BadRequest("Unable to grant access to user account");
                }

                return Ok(new LoginResponseVM(tokenResponse, new UserVM(user)));
            }

        }

        // Below code is to REGISTER a new User

        //api/account/register
        [AllowAnonymous]
        [HttpPost("register")]
        //public async Task<ActionResult<RegisterVM>> Register([FromBody] RegisterVM model)
        public async Task<IActionResult> Register([FromBody] RegisterVM model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest("Encouuntered some Errors or it's possible the User exist");
            }
            await _signinManager.SignInAsync(user, isPersistent: false); // This can be used to sign in the created user

            return Ok(user);
        }

        // For users forgetting their PASSWORDS:
        //api/account/forgotpassword
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgetPasswordVM model) //string email
        {
            var email = model.Email;

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return RedirectToAction(nameof(ForgotPasswordConfirmation));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            // var link = Url.Action("ResetPassword", "Account", new { token, email = user.Email }, Request.Scheme);
            var encodedToken = Encoding.UTF8.GetBytes(token);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);

            string link = $"{_configuration["applicationUrl"]}/ResetPassword?email={email}&token={validToken}";

            //EmailHelper emailHelper = new EmailHelper();

            await _mailService.SendEmailAsync(email, "Reset Password", "<h2>Follow the LINK for Password reset</h2>" +
                $"<p>You need to click here <a href='{link}'>to reset your password</a></p>");


            return Ok($"Please check your email {email} for reset link");
        }

        public IActionResult ForgotPasswordConfirmation()
        {
            return BadRequest("User does not exist");
        }


        //// Logout Action if required
        //[HttpPost("logout")]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signinManager.SignOutAsync();

        //    return RedirectToAction("login");
        //}
        //api/account/login
        [HttpPut("profile")]
        public async Task<ActionResult<UserProfileEditVM>> EditProfile([FromBody] User user)
        {
            //var profile = await _userRepository.EditProfile(user);
            var profile = await _userManager.FindByEmailAsync(user.Email);
            if (profile == null)
                return BadRequest($"There is no user with this email address = {user} ");

            var model = new UserProfileEditVM
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                PhoneNumber = profile.PhoneNumber,
            };
            //else
            //{

            //    //profile.Email = user.Email; // not sure if it's needful to edit email field when User is logged in
            //    profile.FirstName = user.FirstName;
            //    profile.LastName = user.LastName;
            //    //profile.PhoneNumber = user.PhoneNumber;
            //}
            var result = await _userManager.UpdateAsync(profile);

            //if (result.Succeeded)
            //{
            //    return model;
            //}
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError("There is no user", error.Description);
            //}

            //if (result.Succeeded)
            //{
            //    return model;
            //}
            return Ok(result);
        }

        [HttpPut("resetpassword")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return BadRequest(model);

            if (model.Password != model.ConfirmPassword)
                return BadRequest(model);

            var decodedToken = WebEncoders.Base64UrlDecode(model.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, model.Password);

            if (result.Succeeded)
                return Ok($"Your password {result} has been reset");

            return BadRequest(model);
        }
    }
}
