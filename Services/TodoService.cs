using TodoBackend.Models;
using TodoBackend.Repositories;

namespace TodoBackend.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public Task<List<TodoItem>> GetAllAsync()
            => _repository.GetAllAsync();

        public Task<TodoItem?> GetByIdAsync(int id)
            => _repository.GetByIdAsync(id);

        public Task CreateAsync(TodoItem todo)
            => _repository.AddAsync(todo);

        public async Task UpdateAsync(int id, TodoItem todo)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return;

            existing.Title = todo.Title;
            existing.Description = todo.Description;
            existing.IsCompleted = todo.IsCompleted;

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteAsync(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            if (todo != null)
                await _repository.DeleteAsync(todo);
        }

     
        public Task<List<TodoItem>> SearchAsync(string keyword)
        {
            return _repository
                .GetAllAsync()
                .ContinueWith(t =>
                    t.Result
                        .Where(x => x.Title.Contains(keyword))
                        .ToList());
        }
    }
}


