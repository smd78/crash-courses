﻿using CrashCourseApi.Web.DataStores;
using CrashCourseApi.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrashCourseApi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostDataStore _blogPostDataStore;

        public BlogPostController(IBlogPostDataStore blogPostDataStore)
        {
            _blogPostDataStore = blogPostDataStore;
        }

        // GET: api/<BlogPostController>
        [HttpGet]
        public IEnumerable<BlogPostResponse> Get()
        {
            var blogPostEntities = _blogPostDataStore.SelectAll();

            return blogPostEntities.Select(x => new BlogPostResponse() {
                Id = x.Id, 
                Title = x.Title,
                Content = x.Content,
                CreationDate = x.CreationDate
            });
        }

        // GET api/<BlogPostController>/5
        [HttpGet("{id}")]
        public BlogPostResponse Get(int id)
        {
            var blogPostEntity = _blogPostDataStore.SelectById(id);
            if (blogPostEntity == null)
            {
                return null;
            }    
            return new BlogPostResponse()
            {
                Id = blogPostEntity.Id,
                Title = blogPostEntity.Title,
                Content = blogPostEntity.Content,
                CreationDate = blogPostEntity.CreationDate
            };
        }

        // POST api/<BlogPostController>
        [HttpPost]
        public void Post([FromBody] BlogPostRequest value)
        {
            _blogPostDataStore.Insert(new BlogPost() {
                Title = value.Title,
                Content = value.Content,
                CreationDate = DateTime.UtcNow
            });
        }

        // PUT api/<BlogPostController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BlogPostRequest value)
        {
            _blogPostDataStore.Update(new BlogPost()
            {
                Id = id,
                Title = value.Title,
                Content = value.Content
            });
        }

        // DELETE api/<BlogPostController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _blogPostDataStore.Delete(id);
        }
    }
}