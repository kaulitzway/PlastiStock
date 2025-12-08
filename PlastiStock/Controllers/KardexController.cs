using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class KardexController : ControllerBase
{
    private readonly IKardexRepository _repository;

    public KardexController(IKardexRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var kardex = await _repository.GetByIdAsync(id);
        return kardex == null ? NotFound() : Ok(kardex);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Kardex kardex)
    {
        var creado = await _repository.CreateAsync(kardex);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Kardex kardex)
    {
        kardex.Id = id;
        var updated = await _repository.UpdateAsync(kardex);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted ? Ok(true) : NotFound();
    }
}

