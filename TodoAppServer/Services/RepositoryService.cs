using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Contexts;
using TodoAppServer.Models.Entities;

namespace TodoAppServer.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly TodoDbContext _todoDbContext;
        public RepositoryService(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }


        public async Task<IEnumerable<TodoItem>> GetAllItems()
        {
            return (await Task.FromResult(_todoDbContext.Items.ToList()));
        }

        public async Task<TodoItem> GetItemById(int id)
        {
            return (await Task.FromResult(_todoDbContext.Items.First(i => i.Id == id)));
        }

        public async Task<IEnumerable<TodoItem>> GetAllItemsByListId(int listId)
        {
            return await Task.FromResult(_todoDbContext.Items.Where(i => i.ListId == listId).ToList());
        }
        public async Task<IEnumerable<TodoList>> GetAllLists()
        {
            return (await Task.FromResult(_todoDbContext.Lists.ToList()));
        }
        public async Task<TodoList> GetListById(int id)
        {
            return (await Task.FromResult(_todoDbContext.Lists.First(l => l.Id == id)));
        }
        public async Task<TodoList> AddNewList(TodoList list)
        {
            await Task.FromResult(_todoDbContext.Lists.Add(list));
            await Task.FromResult(_todoDbContext.SaveChanges());
            return list;
        }

        public async Task<TodoItem> AddNewItem(TodoItem item)
        {
            await Task.FromResult(_todoDbContext.Items.Add(item));
            await Task.FromResult(_todoDbContext.SaveChanges());
            return item;
        }
        public async Task<TodoList> UpdateList(TodoList list)
        {
            TodoList list1 = await Task.FromResult(_todoDbContext.Lists.FirstOrDefault(l => l.Id == list.Id));
            if (list1 != null)
            {
                await Task.FromResult(_todoDbContext.Lists.Remove(list1));
                await Task.FromResult(_todoDbContext.Lists.Add(list));
                await Task.FromResult(_todoDbContext.SaveChanges());
                return list;
            }
            else
            {
                throw new KeyNotFoundException($"List not found. list id ={list.Id}");
            }
        }
        public async Task<TodoItem> UpdateItem(TodoItem item)
        {
            TodoItem item1 = await Task.FromResult(_todoDbContext.Items.FirstOrDefault(i => i.Id == item.Id));
            if (item1 != null)
            {
                await Task.FromResult(_todoDbContext.Items.Remove(item1));
                await Task.FromResult(_todoDbContext.Items.Add(item));
                await Task.FromResult(_todoDbContext.SaveChanges());
                return item;
            }
            else
            {
                throw new KeyNotFoundException($"Item not found. Item id ={item.Id}");
            }
        }

        public async Task<TodoItem> DeleteItemById(int id)
        {
            TodoItem item1 = await Task.FromResult(_todoDbContext.Items.FirstOrDefault(i => i.Id == id));
            if (item1 != null)
            {
                await Task.FromResult(_todoDbContext.Items.Remove(item1));
                await Task.FromResult(_todoDbContext.SaveChanges());
                return item1;
            }
            else
            {
                throw new KeyNotFoundException($"Item not found. Item id ={id}");
            }
        }
        public async Task<TodoList> DeleteListById(int id)
        {
            TodoList list1 = await Task.FromResult(_todoDbContext.Lists.FirstOrDefault(l => l.Id == id));
            if (list1 != null)
            {
                await Task.FromResult(_todoDbContext.Lists.Remove(list1));
                await Task.FromResult(_todoDbContext.SaveChanges());
                return list1;
            }
            else
            {
                throw new KeyNotFoundException($"List not found. list id ={id}");
            }
        }

        public async Task<IEnumerable<TodoItem>> DeleteItemsByListId(int listId)
        {
            IEnumerable<TodoItem> itemsTodelete = await Task.FromResult(_todoDbContext.Items.Where(i => i.ListId == listId).ToList());
            if(itemsTodelete != null)
            {
                _todoDbContext.Items.RemoveRange(itemsTodelete);
                await Task.FromResult(_todoDbContext.SaveChanges());            
            }
            return itemsTodelete;
        }


        

        




    }
}
