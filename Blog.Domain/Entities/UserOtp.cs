using Blog.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class UserOtp
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset ExpiredAt { get; set; }
        public OptUses UsedFor { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
