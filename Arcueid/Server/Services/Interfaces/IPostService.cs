using Arcueid.Server.Core;
using Arcueid.Shared.Dto;

namespace Arcueid.Server.Services.Interfaces;

public interface IPostService
{
    Task<OperationResult<IEnumerable<PostDto>>> GetPostsAsync();

}
