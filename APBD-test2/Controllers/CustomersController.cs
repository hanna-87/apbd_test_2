using APBD_test2.DTOs;
using APBD_test2.Services;
using Microsoft.AspNetCore.Mvc;
using TestFinal_APBD.Exceptions;

namespace APBD_test2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CustomersController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet("{id}/purchases")]
    public async Task<IActionResult> GetCustomerInfo(int id)
    {
        try
        { var res = await _dbService.GetCustomerInfoAsync(id);
           return Ok(res);
        }
        catch (NotFoundException e )
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerDto request)
    {

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newId = await _dbService.CreateCustomerAsync(request);
            var location = $"/api/customers/{newId}/purchases";
            return Created(location, new { id = newId });
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ConflictException e)
        {
            return Conflict(e.Message);
        }
        catch (BadRequestException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    
}