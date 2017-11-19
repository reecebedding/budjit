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
        ITransactionsRepository transactionRepository;
        public HomeController(ITransactionsRepository transRepo)
        {
            transactionRepository = transRepo;
        }
        public IActionResult Index()
        {
            IEnumerable<Transaction> data = (transactionRepository.GetAll() ?? new List<Transaction>()).ToList();
            return View(data);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
