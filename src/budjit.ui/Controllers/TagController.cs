using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using budjit.core.data.Contracts;
using budjit.core.models;

namespace budjit.ui.Controllers
{
    public class TagController : Controller
    {
        ITagRepository tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Tag> tags = tagRepository.GetAll();
            return View(tags);
        }
    }
}