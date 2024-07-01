using ApiBlog.DTOs;

namespace ApiBlog.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<PostDto> GetPostByIdAsync(int id);
        Task<PostDto> CreatePostAsync(CreatePostDto createPostDto);
        Task UpdatePostAsync(int id, UpdatePostDto updatePostDto);
        Task DeletePostAsync(int id);
    }
}
