using System;
using Microsoft.AspNetCore.Mvc;
using CompulsoryREST.Services;
using CompulsoryREST.Models;
using Microsoft.AspNetCore.Authorization;

namespace CompulsoryREST.Controllers;

[Controller]
[Route("api/[controller]")]
public class CreatureController : Controller
{
    private readonly MongoDBService _mongoDBService;

    public CreatureController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    [HttpGet]
    public async Task<List<Creature>> Get()
    {
        return await _mongoDBService.GetAsync();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Creature creature)
    {
        await _mongoDBService.CreateAsync(creature);
        return CreatedAtAction(nameof(Get), new { id = creature.Id }, creature);
    }


    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Creature creature)
    {
        await _mongoDBService.UpdateAsync(id, creature);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}