using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PDRViewer.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PDRViewer.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var connector = new JiraApiConnector();
            ViewBag.PdrList = connector.GetPDRs();

            return View();
        }

        public IActionResult Detail(string id)
        {
            var connector = new JiraApiConnector();
            var pdrList = connector.GetPDRs();

            ViewBag.Pdr = pdrList.FirstOrDefault(x => x.Id == id);

            return View();
        }

        public IActionResult Vote(string id, int votes)
        {
            var connector = new JiraApiConnector();
            connector.UpVote(id, votes);

            return RedirectToAction("Index", "Home")
            ;
        }
    }
}
