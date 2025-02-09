﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyMicroservices.Shared.Services
{
    public sealed class IdentityFakeService : IIdentiyService
    {
        public Guid UserId => Guid.Parse("332ee8cd-f3f6-49fa-92e2-5fdb188b3377");

        public string Username => "Ahmet";
    }
}
