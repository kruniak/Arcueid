using System.Runtime.InteropServices;
using Arcueid.Server.Core;
using Arcueid.Server.Database;
using Arcueid.Server.Database.Entities;
using Arcueid.Server.Services;
using Arcueid.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Arcueid.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DogController : BaseController
{
    private readonly IDogService _dogService;

    public DogController(IDogService dogService)
    {
        _dogService = dogService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _dogService.GetDogsAsync();
        var response = CreateResponse(result);

        return response;
    }

    [HttpGet("name")]
    public async Task<IActionResult> GetDogsByName(string name)
    {
        var result = await _dogService.GetDogsByNameAsync(name);
        var response = CreateResponse(result);

        return response;
    }

    [HttpPost]
    public async Task<IActionResult> Add(DogDto dog)
    {
        var result = await _dogService.AddDogAsync(dog);
        var response = CreateResponse(result);

        return response;
    }
}

