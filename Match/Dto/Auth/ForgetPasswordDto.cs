using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Dto
{
    public class ForgetPasswordDto
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public int BirthYear { get; set; }

    }
}
