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
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        IRepositoryService _repo;

        public ItemsController(IRepositoryService repo)
        {
            _repo = repo;
        }

        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get([FromQuery] int? listId = null)
        {
            try
            {
                if(listId == null)
                {
                    var items = await _repo.GetAllItems();
                    return Ok(items);
                }
                var items1 = await _repo.GetAllItemsByListId((int)listId);
                return Ok(items1);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound();
}
        }

        // GET api/items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            try
            {
                var item = await _repo.GetItemById(id);
                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        // POST api/items
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post([FromBody] TodoItem item)
        {
            try
            {
                var newItem = await _repo.AddNewItem(item);
                return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Item {item.Id} already exist.");
            }
        }

        // PUT api/items/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> Put(int id, [FromBody] TodoItem item)
        {
            try
            {
                var item1 = await _repo.GetItemById(id);
                if(item1 != null)
                {
                    item1 = item1 with { Caption = item.Caption, IsCompleted = item.IsCompleted, ListId = item.ListId };
                    await _repo.UpdateItem(item1);
                    return Ok(item);
                }
                return NotFound();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> Delete(int id)
        {
            try
            {
                var item = await _repo.DeleteItemById(id);

                return Ok(item);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Item {id} not found.");
            }
        }
    }
}
