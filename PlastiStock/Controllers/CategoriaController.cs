using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _repository;

    public CategoriaController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var categoria = await _repository.GetByIdAsync(id);
        return categoria == null ? NotFound() : Ok(categoria);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Categoria categoria)
    {
        var creado = await _repository.CreateAsync(categoria);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Categoria categoria)
    {
        categoria.Id = id;
        var updated = await _repository.UpdateAsync(categoria);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted ? Ok(true) : NotFound();
    }
}
