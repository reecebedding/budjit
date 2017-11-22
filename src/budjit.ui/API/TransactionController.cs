using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budjit.core.data.Contracts;
using budjit.core.models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace budjit.ui.API
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        private ITransactionsRepository transactionsRepository;
        public TransactionController(ITransactionsRepository transactionsRepository)
        {
            this.transactionsRepository = transactionsRepository;
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id, [FromBody]int? tagValue)
        {
            Transaction transaction = transactionsRepository.GetTransactionById(id);
            transaction.TagID = tagValue == 0 ? null : tagValue;

            try
            {
                transactionsRepository.SaveTransaction(transaction);
                return StatusCode(500);
            }
            catch (Exception ex)
            {
                return Ok();
            }
        }
    }
}
