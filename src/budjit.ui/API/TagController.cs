using budjit.core.data.Contracts;
using budjit.core.models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace budjit.ui.API
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private ITagRepository tagRepository;
        public TagController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IEnumerable<Tag> GetAllTags()
        {
            return tagRepository.GetAll();
        }
    }
}
