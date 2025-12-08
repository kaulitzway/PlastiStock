using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EntradaInventarioController : ControllerBase
{
    private readonly IEntradaInventarioRepository _repository;

    public EntradaInventarioController(IEntradaInventarioRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var entrada = await _repository.GetByIdAsync(id);
        return entrada == null ? NotFound() : Ok(entrada);
    }

    [HttpPost]
    public async Task<IActionResult> Create(EntradaInventario entrada)
    {
        var creado = await _repository.CreateAsync(entrada);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, EntradaInventario entrada)
    {
        entrada.Id = id;
        var updated = await _repository.UpdateAsync(entrada);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted ? Ok(true) : NotFound();
    }
}

