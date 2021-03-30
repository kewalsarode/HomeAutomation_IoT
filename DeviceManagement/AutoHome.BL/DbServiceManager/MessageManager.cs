using AutoHome.Models.EF;
using AutoHome.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoHome.BL.DbServiceManager
{
    public class MessageManager
    {
        IRepository<Message> objMessageRepo = new Repository<Message>(new DeviceManagementEntities());

        public void PushMessage(string key, string value)
        {
            var message = new Message
            {
                MessageKey = key,
                Value = value
            };

            objMessageRepo.Insert(message);
        }
    }
}
