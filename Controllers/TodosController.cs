using Microsoft.AspNetCore.Mvc;
using TodoBackend.Models;
using TodoBackend.Services;

namespace TodoBackend.Controllers
{
[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _service;

    public TodosController(ITodoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var todo = await _service.GetByIdAsync(id);
        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TodoItem todo)
    {
        await _service.CreateAsync(todo);
        return Ok(todo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem todo)
    {
        await _service.UpdateAsync(id, todo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string keyword)
        => Ok(await _service.SearchAsync(keyword));
}
}
