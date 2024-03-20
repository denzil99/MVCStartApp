using Microsoft.AspNetCore.Mvc;
using MVCStartApp.Models;
using MVCStartApp.Models.Db;
using MVCStartApp.Repository.Contracts;
using MVCStartApp.Repository.Repositories;
using System.Diagnostics;

namespace MVCStartApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IBlogRepository _blogRepository;
		private readonly IRequestRepository _requestRepository;
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger, IBlogRepository blogRepository, IRequestRepository requestRepository)
		{
			_logger = logger;
			_blogRepository = blogRepository;
			_requestRepository = requestRepository;

        }

		public async Task<IActionResult> Index()
		{
			return View();
		}

		public async Task<IActionResult> Authors()
		{
			var authors = await _blogRepository.GetUsers();
			return View(authors);
		}

		public async Task<IActionResult> RequestHistory()
        {
			var request = await _requestRepository.GetRequests();
			return View(request);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
