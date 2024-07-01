using ApiBlog.Data;
using ApiBlog.DTOs;
using ApiBlog.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiBlog.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PostService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> CreatePostAsync(CreatePostDto createPostDto)
        {
            var post = _mapper.Map<Post>(createPostDto);
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return _mapper.Map<PostDto>(post);
        }

        public async Task UpdatePostAsync(int id, UpdatePostDto updatePostDto)
        {
            var post = await _context.Posts.FindAsync(id);
            _mapper.Map(updatePostDto, post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
