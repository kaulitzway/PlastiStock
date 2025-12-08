using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdenProduccionController : ControllerBase
{
    private readonly IOrdenProduccionRepository _repository;

    public OrdenProduccionController(IOrdenProduccionRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var orden = await _repository.GetByIdAsync(id);
        return orden == null ? NotFound() : Ok(orden);
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrdenProduccion orden)
    {
        var creado = await _repository.CreateAsync(orden);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, OrdenProduccion orden)
    {
        orden.Id = id;
        var updated = await _repository.UpdateAsync(orden);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted ? Ok(true) : NotFound();
    }
}

