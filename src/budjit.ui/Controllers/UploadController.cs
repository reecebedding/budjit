using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budjit.core.data.Contracts;
using Microsoft.AspNetCore.Http;
using System.IO;
using budjit.core.ImportParsers.Contracts;
using budjit.core.ImportParsers;
using budjit.core.models;
using System;
using budjit.core.ImportParsers.Exceptions;

namespace budjit.ui.Controllers
{
    public class UploadController : Controller
    {
        private ITransactionsRepository transactionsRepository;

        public UploadController(ITransactionsRepository repository)
        {
            transactionsRepository = repository;
        }
        
        [HttpPost]
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
            return RedirectToAction("Index", "Home");
        }

        private class ParserError : Exception { }
        private class SaveError: Exception { }
        
    }
}