using Talkish.Domain.Interfaces;

namespace Talkish.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _repo;

        public AuthorService(IAuthorRepository repo)
        {
            _repo = repo;
        }
    }
}
