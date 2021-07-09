using Hello.BookStore.Models;
using Hello.BookStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuratioin;

        public AccountRepository(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUserService userService,
            IEmailService emailService,
            IConfiguration configuratioin)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuratioin = configuratioin;
        }
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var UserNameFromEmail = GetUserName(userModel.Email);
            var user = new ApplicationUser()
            {
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
                Email = userModel.Email,
                UserName = UserNameFromEmail
            };
            var result = await _userManager.CreateAsync(user, userModel.Password);
            if(result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                if(!string.IsNullOrEmpty(token))
                {
                    await SendEmailConfirmationEmail(user, token);
                }
            }
            return result;
        }

        public async Task<SignInResult> PasswordSignInAsync(SignInModel signInModel)
        {
            var UserNameFromEmail = GetUserName(signInModel.Email);
            return await _signInManager.PasswordSignInAsync(UserNameFromEmail, signInModel.Password, signInModel.RememberMe, false);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<ApplicationUser> GetUserDetails()
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        
        private string GetUserName(string Email)
        {
            var UserNameList = new List<char>();
            foreach (var ch in Email)
            {
                if (ch == '@')
                {
                    UserNameList.Add(ch);
                    break;
                }
                UserNameList.Add(ch);
            }
            return new string(UserNameList.ToArray()); // converting character list to string
        }

        private async Task SendEmailConfirmationEmail(ApplicationUser user , string token)
        {
            string appDomain = _configuratioin.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuratioin.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };

            await _emailService.SendEmailForEmailConfirmation(options);
        }
    }
}