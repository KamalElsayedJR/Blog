using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ExternalService
{
    public interface IAuthClient
    {
        Task<UserInfoDto?> Getme(string token); 
    }
}
