using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCStartApp.Models.Db;
using MVCStartApp.Repository.Contracts;

namespace MVCStartApp.Controllers
{
	public class UsersController : Controller
	{
		private readonly IBlogRepository _repo;

		public UsersController(IBlogRepository repo)
		{
			_repo = repo;
		}

		public async Task<IActionResult> Index()
		{
			var authors = await _repo.GetUsers();
			return View(authors);
		}

		[HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }

		[HttpPost]
        public async void Register(User user)
        {
            _repo.AddUser(user);
        }

    }
}
