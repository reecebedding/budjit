using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budjit.core.data.Contracts;
using budjit.core.models;
using budjit.ui.API.ViewModel;
using Microsoft.AspNetCore.Http;
using System.IO;
using budjit.core.ImportParsers.Contracts;
using budjit.core.ImportParsers;
using budjit.core.ImportParsers.Exceptions;
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

        [HttpPost]
        [Route("file")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var filePath = Path.GetTempFileName();

            if (file.Length > 0)
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                //Default to Santander CSV importing.
                IImporter importer = new CSVImporter(new FileInfo(filePath));
                IImportParser parser = new SantanderCSVParser(importer);

                IEnumerable<Transaction> data;

                try
                {
                    data = parser.Parse();
                    transactionsRepository.Create(data);

                    return Ok();
                }
                catch (ParsingException e)
                {
                    return StatusCode(500, e.Message);
                }
                catch (Exception e)
                {
                    return StatusCode(500, e.Message);
                }
            }
            else
            {
                return BadRequest("File is empty");
            }
        }

        private class ParserError : Exception { }
        private class SaveError : Exception { }
    }
}
