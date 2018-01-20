using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budjit.core.data.Contracts;
using budjit.core.models;
using budjit.ui.API.ViewModel;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace budjit.ui.API
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private ITransactionsRepository transactionsRepository;
        private IMapper mapper;
        public TransactionController(ITransactionsRepository transactionsRepository, IMapper mapper)
        {
            this.transactionsRepository = transactionsRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(
                    mapper.Map<IEnumerable<Transaction>, IEnumerable<TransactionViewModel>>(transactionsRepository.GetAll())
                );
            }
            catch (Exception exn)
            {
                return StatusCode(500);
            }
        }
    }
}
