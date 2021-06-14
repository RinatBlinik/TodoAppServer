using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Models.Entities;
using TodoAppServer.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoAppServer.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/lists")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        IRepositoryService _repo;

        public ListsController(IRepositoryService repo)
        {
            _repo = repo;
        }
        // GET: api/lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoList>>> Get()
        {
            try
            {
                var lists = await _repo.GetAllLists();
                return Ok(lists);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        // GET api/lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoList>> GetById(int id)
        {
            try
            {
                var list = await _repo.GetListById(id);
                return Ok(list);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        // POST api/lists
        [HttpPost]
        public async Task<ActionResult<TodoList>> Post([FromBody] TodoList list)
        {
            try
            {
                var newlist = await _repo.AddNewList(list);
                return CreatedAtAction(nameof(GetById), new { id = newlist.Id }, newlist);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"List {list.Id} already exist.");
            }
        }

        // PUT api/lists/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoList>> Put(int id, [FromBody] TodoList list)
        {
            try
            {
                var list1 = await _repo.GetListById(id);
                if (list1 != null)
                {
                    list1 = list1 with { Caption = list.Caption, Description = list.Description, Icon = list.Icon, Color = list.Color };
                    await _repo.UpdateList(list1);
                    return Ok(list1);
                }
                return NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        // DELETE api/lists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoList>> Delete(int id)
        {
            try
            {
                var list = await _repo.DeleteListById(id);

                return Ok(list);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"list {id} not found.");
            }
        }
    }
}
