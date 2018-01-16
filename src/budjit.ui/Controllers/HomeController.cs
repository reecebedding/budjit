using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budjit.ui.Models;
using budjit.core.data;
using budjit.core.models;
using budjit.core.data.SQLite;
using budjit.core.data.Contracts;

namespace budjit.ui.Controllers
{
    public class HomeController : Controller
    {
        ITransactionsRepository transactionsRepository;
        ITagRepository tagRepository;

        public HomeController(ITransactionsRepository transRepo, ITagRepository tagRepository)
        {
            transactionsRepository = transRepo;
            this.tagRepository = tagRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
