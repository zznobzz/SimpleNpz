using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using SimpleNpz.Contracts.TankDtos;
using SimpleNpz.Services.Abstractions;

namespace SimpleNpz.Presentation.Controllers;
[ApiController]
[Route("api/tanks")]
public class TanksController:ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public TanksController(IServiceManager serviceManager) => _serviceManager = serviceManager;

    [HttpGet]
    public async Task<IActionResult> GetTanks(CancellationToken cancellationToken)
    {
        var tanks = await _serviceManager.TankService.GetAllAsync(cancellationToken);
        return Ok(tanks);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetTankById(long id, CancellationToken cancellationToken)
    {
        var tank = await _serviceManager.TankService.GetByIdAsync(id, cancellationToken);
        return Ok(tank);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTank([FromBody] CreateTankRequestDto createTank)
    {
        var tankDto = await _serviceManager.TankService.CreateAsync(createTank);
        return CreatedAtAction(nameof(GetTankById), new {id = tankDto.Id}, tankDto);
    }

    [HttpPut("{tankId:long}")]
    public async Task<IActionResult> UpdateTank(long tankId, [FromBody]UpdateTankRequestDto updateTank,
        CancellationToken cancellationToken)
    {
        await _serviceManager.TankService.UpdateAsync(tankId, updateTank, cancellationToken);
        return NoContent();
    }

    [HttpPut("{tankId:long}/update={inputVolume:float}")]
    public async Task<IActionResult> ChangeTankVolumeAsync(long tankId, [FromBody] string inputVolume,
        CancellationToken cancellationToken)
    {
        float valueToSet;
        float.TryParse(inputVolume, NumberStyles.Number, CultureInfo.InvariantCulture.NumberFormat, out valueToSet);
        await _serviceManager.TankService.ChangeTankVolumeAsync(tankId, valueToSet, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{tankId:long}")]
    public async Task<IActionResult> DeleteTankAsync(long tankId, CancellationToken cancellationToken)
    {
        await _serviceManager.TankService.DeleteAsync(tankId, cancellationToken);
        return NoContent();
    }
}