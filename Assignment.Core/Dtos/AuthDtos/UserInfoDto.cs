using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.Dtos.AuthDtos
{
    public class UserInfoDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class UserFullInfoDto : UserInfoDto
    {
        public bool IsDeleted { get; set; }
    }
}
