using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Dto
{
    public class PhotoCreateDto
    {
        public string photoUrl { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile File { get; set; }
        public string descriptions { get; set; }
        public DateTime dateAdded { get; set; }
        public string publicId { get; set; }

        public PhotoCreateDto()
        {
            dateAdded = DateTime.Now;
        }
    }
}
