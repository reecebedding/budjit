﻿using budjit.core.data.Contracts;
using budjit.core.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using budjit.ui.API.ViewModel;
using AutoMapper;

namespace budjit.ui.API
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private ITagRepository tagRepository;
        private readonly IMapper mapper;
        public TagController(ITagRepository tagRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult GetAllTags()
        {
            try
            {
                return Ok(mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tagRepository.GetAll()));
            }
            catch (Exception exn)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(mapper.Map<Tag, TagViewModel>(tagRepository.GetById(id)));
            }
            catch (Exception exn)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Create(TagViewModel newTag)
        {
            if (ModelState.IsValid)
            {
                Tag tag = mapper.Map<TagViewModel, Tag>(newTag);

                try
                {
                    tag = tagRepository.Create(tag);

                    string url = Url.Action("GetById", "Tag", new RouteValueDictionary(new { id = tag.ID }));

                    TagViewModel returnTag = mapper.Map<Tag, TagViewModel>(tag);

                    return Created(url, returnTag);
                }
                catch (Exception exn)
                {
                    return StatusCode(500);
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
