using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlastiStock.Models;
using PlastiStock.Repositories.Interfaces;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MermaController : ControllerBase
{
    private readonly IMermaRepository _repository;

    public MermaController(IMermaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() =>
        Ok(await _repository.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var merma = await _repository.GetByIdAsync(id);
        return merma == null ? NotFound() : Ok(merma);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Merma merma)
    {
        var creado = await _repository.CreateAsync(merma);
        return Ok(creado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Merma merma)
    {
        merma.Id = id;
        var updated = await _repository.UpdateAsync(merma);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _repository.DeleteAsync(id);
        return deleted ? Ok(true) : NotFound();
    }
}

