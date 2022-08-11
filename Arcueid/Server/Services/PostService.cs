using Arcueid.Server.Core;
using Arcueid.Server.Database;
using Arcueid.Server.Services.Interfaces;
using Arcueid.Shared.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Arcueid.Server.Services;

public class PostService : IPostService
{
    private readonly ArcueidContext _dbContext;
    private readonly IMapper _mapper;

    public PostService(ArcueidContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<OperationResult<IEnumerable<PostDto>>> GetPostsAsync()
    {
        var posts = await _dbContext.Posts
            .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return posts;
    }
}
