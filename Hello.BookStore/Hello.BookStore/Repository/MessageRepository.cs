using Hello.BookStore.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hello.BookStore.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private NewBookAlertConfiq _newBookAlertconfiguration;

        public MessageRepository(IOptionsMonitor<NewBookAlertConfiq> newBookAlertconfiguration)
        {
            _newBookAlertconfiguration = newBookAlertconfiguration.CurrentValue;
            newBookAlertconfiguration.OnChange(config =>
            {
                _newBookAlertconfiguration = config;
            });
        }
        public string GetName()
        {
            return _newBookAlertconfiguration.BookName; 
        }
    }
}
