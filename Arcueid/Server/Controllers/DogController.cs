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
    private IDogService dogService;

    public DogController(IDogService dogService)
    {
        this.dogService = dogService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await dogService.GetDogsAsync();
        var response = CreateResponse(result);

        return response;
    }

    [HttpPost]
    public async Task<IActionResult> Add(DogDto dog)
    {
        var result = await dogService.AddDogAsync(dog);
        var response = CreateResponse(result);

        return response;
    }
}

