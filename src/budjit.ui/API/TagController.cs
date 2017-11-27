using budjit.core.data.Contracts;
using budjit.core.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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

        [HttpGet("{id}")]
        public Tag GetById(int id)
        {
            return tagRepository.GetById(id);
        }
        
        [HttpPost]
        public IActionResult Create(Tag newTag)
        {   
            if (string.IsNullOrEmpty(newTag.Name))
                return BadRequest("Name cannot be empty");
            
            newTag = tagRepository.Create(newTag);

            string url = Url.Action("GetById", "Tag", new RouteValueDictionary(new { id = newTag.ID }));
            
            return Created(url, newTag);
        }
    }
}
