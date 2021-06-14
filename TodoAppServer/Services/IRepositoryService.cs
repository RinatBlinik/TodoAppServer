using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoAppServer.Models.Entities;

namespace TodoAppServer.Services
{
    public interface IRepositoryService
    {
        Task<IEnumerable<TodoItem>> GetAllItems();
        Task<TodoItem> GetItemById(int id);
        Task<IEnumerable<TodoItem>> GetAllItemsByListId(int listId);
        Task<IEnumerable<TodoList>> GetAllLists();
        Task<TodoList> GetListById(int id);
        Task<TodoList> AddNewList(TodoList list);
        Task<TodoItem> AddNewItem(TodoItem item);
        Task<TodoList> UpdateList(TodoList list);
        Task<TodoItem> UpdateItem(TodoItem item);
        Task<TodoItem> DeleteItemById(int id);
        Task<TodoList> DeleteListById(int id);
        Task<IEnumerable<TodoItem>> DeleteItemsByListId(int listId);
    }
}
