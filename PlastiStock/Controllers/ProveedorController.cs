using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProveedorController : ControllerBase
{
    private readonly IProveedorRepository _repository;

    public ProveedorController(IProveedorRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var proveedor = await _repository.GetByIdAsync(id);
        return proveedor == null ? NotFound() : Ok(proveedor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Proveedor proveedor)
    {
        var creado = await _repository.CreateAsync(proveedor);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Proveedor proveedor)
    {
        proveedor.Id = id;
        var updated = await _repository.UpdateAsync(proveedor);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted ? Ok(true) : NotFound();
    }
}

