using Hello.BookStore.Models;
using Hello.BookStore.Repository;
using Hello.BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly NewBookAlertConfiq _newBookAlertconfiguration;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public HomeController(IOptionsSnapshot<NewBookAlertConfiq> newBookAlertconfiguration,
            IMessageRepository messageRepository,
            IUserService userService,
            IEmailService emailService)
        {
            _newBookAlertconfiguration = newBookAlertconfiguration.Value;
            _messageRepository = messageRepository;
            _userService = userService;
            _emailService = emailService;
        }
        public async Task<ViewResult> Index()
        {
            /*UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { "test@gmail.com" },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", "Jule")
                }
            };

            await _emailService.SendTestEmail(options);*/

            /* var userId = _userService.GetUserId();
             var isLoggedIn = _userService.IsAuthenticated();*/

            /*var value = _messageRepository.GetName();*/
            /*bool isDisplay = _newBookAlertconfiguration.DisplayNewBookAlert;*/


            /* var newBookAlert = new NewBookAlertConfiq();
           _configuration.Bind("NewBookAlert", newBookAlert);*/


            //var newBook = _configuration.GetSection("NewBookAlert");
            //var result = newBook.GetValue<bool>("DisplayNewBookAlert"); /* It return actual return type value*/
            //var bookName = newBook.GetValue<string>("BookName");

            //var result = _configuration["AppName"]; /* If I use Iconfiguration interface only and it always return string*/
            //var key1 = configuration["infoObj:key1"];
            //var key2 = configuration["infoObj:key2"];
            //var key3 = configuration["infoObj:key3:key3obj1"];

            return View();
        }
        [Route("contact")]
        public ViewResult Contact()
        {
            return View();
        }
        
    }
}
