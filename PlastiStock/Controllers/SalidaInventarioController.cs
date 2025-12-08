using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalidaInventarioController : ControllerBase
{
    private readonly ISalidaInventarioRepository _repository;

    public SalidaInventarioController(ISalidaInventarioRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var salida = await _repository.GetByIdAsync(id);
        return salida == null ? NotFound() : Ok(salida);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SalidaInventario salida)
    {
        var creado = await _repository.CreateAsync(salida);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SalidaInventario salida)
    {
        salida.Id = id;
        var updated = await _repository.UpdateAsync(salida);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted ? Ok(true) : NotFound();
    }
}

