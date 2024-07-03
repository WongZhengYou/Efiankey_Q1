using Efiankey_Q1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Efiankey_Q1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static UserDownloadStatus? _userStatus;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public IActionResult CheckDownload(string memberType)
        {
            if (_userStatus == null || _userStatus.MemberType != memberType)
            {
                _userStatus = new UserDownloadStatus(memberType);
            }

            var currentTime = DateTime.Now;
            var timeDifference = (currentTime - _userStatus.LastDownloadTime).TotalSeconds;

            if (_userStatus.MemberType == "nonmember")
            {
                if (timeDifference < 5)
                {
                    return Json(new { message = "Too many downloads" });
                }
                else
                {
                    _userStatus.LastDownloadTime = currentTime;
                    return Json(new { message = "Your download is starting..." });
                }
            }
            else if (_userStatus.MemberType == "member")
            {
                if (_userStatus.DownloadCount < 2 || timeDifference >= 5)
                {
                    _userStatus.DownloadCount = (_userStatus.DownloadCount < 2) ? _userStatus.DownloadCount + 1 : 1;
                    _userStatus.LastDownloadTime = currentTime;
                    return Json(new { message = "Your download is starting..." });
                }
                else
                {
                    return Json(new { message = "Too many downloads" });
                }
            }

            return Json(new { message = "Invalid member type" });
        }
    }
}
