using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Infrastructure.ExternalService
{
    public class UserInfoDto
    {
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Verified { get; set; }
        public List<string> Roles { get; set; }
    }
}
