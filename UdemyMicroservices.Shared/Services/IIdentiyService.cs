using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyMicroservices.Shared.Services
{
    public interface IIdentiyService
    {
        public Guid UserId { get; }
        public string Username { get; }
    }
}
