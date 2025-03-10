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

/// <summary>
    /// Retrieves all creatures from the database.
    /// </summary>
    /// <returns>The list of creatures.</returns>
    /// 
    [HttpGet]
    public async Task<List<Creature>> Get()
    {
        return await _mongoDBService.GetAsync();
    }

/// <summary>
    /// Creates a new creature in the database.
    /// </summary>
    /// <returns>The created creature.</returns>
    /// 
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Creature creature)
    {
        await _mongoDBService.CreateAsync(creature);
        return CreatedAtAction(nameof(Get), new { id = creature.Id }, creature);
    }

/// <summary>
    /// Updates an existing creature in the database.
    /// </summary>
    /// <param name="id">The id of the creature to update.</param>
    /// <param name="creature"></param>
    /// <returns>The updated creature.</returns>
    /// 
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Creature creature)
    {
        await _mongoDBService.UpdateAsync(id, creature);
        return NoContent();
    }

/// <summary>
    /// Deletes a creature from the database.
    /// </summary>
    /// <returns>The deleted creature.</returns>
    /// 
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}