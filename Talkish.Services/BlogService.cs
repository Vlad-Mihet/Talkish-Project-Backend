using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talkish.Domain.Interfaces;
// using Talkish.Domain.Models;

namespace Talkish.Services
{
    public class BlogService
    {
        private readonly IBlogService _repo;

        public BlogService(IBlogService repo) {
            _repo = repo;
        }

        /*
         * TODO:
         * Add Reading Time Estimation Here 
         */

        // public List<Blog> ProcessReadingTime(Blog[] blogs) { }
    }
}
