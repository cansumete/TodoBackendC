using TodoBackend.Models;

namespace TodoBackend.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(int id);
        Task CreateAsync(TodoItem todo);
        Task UpdateAsync(int id, TodoItem todo);
        Task DeleteAsync(int id);

        Task<List<TodoItem>> SearchAsync(string keyword);
    }
}
