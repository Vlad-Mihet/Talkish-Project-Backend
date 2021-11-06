﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkish.Domain.DTOs
{
    public class BlogDTO
    {
        public int BlogId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public BlogAuthorDTO Author { get; set; }
    }
}
