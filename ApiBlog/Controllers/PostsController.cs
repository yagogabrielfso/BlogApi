using ApiBlog.DTOs;
using ApiBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        ///  <remarks>  
        /// Consome dados dos posts do blog do servidor     
        /// <details><summary><strong>Documentação</strong></summary>
        /// <ul><li><a href="https://www.github.com">GET Posts</a></li>        
        /// </ul></details>   
        ///  <details><summary><strong>Regras</strong></summary>
        /// <ul>        
        /// <li>?</li>        
        /// </ul></details>
        ///  <details><summary><strong>Fluxo da informação</strong></summary>
        /// <ul>        
        /// <li>Recebe a requisição para retornar todos os posts</li>                                        
        /// <li>Aplicação acessa banco de ados</li>
        /// </ul>
        /// </details> 
        /// </remarks> 
        /// <returns></returns>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Erro interno do servidor.</response>  
        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }


        ///  <remarks>  
        /// Cria post      
        /// <details><summary><strong>Documentação</strong></summary>
        /// <ul><li><a href="https://teste.com.br">POST Criar Post</a></li>        
        /// </ul></details>   
        ///  <details><summary><strong>Regras</strong></summary>
        /// <ul>        
        /// <li>Necessário campos título, conteúdo e usuário no body da requisição</li>        
        /// </ul></details>
        ///  <details><summary><strong>Fluxo da informação</strong></summary>
        /// <ul>        
        /// <li>Recebe o informações do usuário no body da requisição.</li>                                        
        /// <li>Aplicação acessa banco de ados</li>
        /// </ul>
        /// </details>        
        /// <details><summary><strong>Dados de entrada</strong></summary>
        /// | Nome do Atributo | Formato    | Obrigatoriedade | Observações        |
        /// |------------------|-----------|-----------------|---------------------|
        /// | title            | string(?) | Sim             | Título do Post      |
        /// | content          | string(?) | Sim             | Conteúdo do Post    |
        /// | id               | int(?)    | Sim             | Id do usuário       |
        /// </details>
        /// <details><summary><strong>Exemplo Requisição</strong></summary>          
        /// <pre>              
        ///  {
        ///  "title": "string",
        ///  "content": "string",
        ///  "userId": 0
        ///  }        
        /// </pre>                    
        ///</details>   
        /// </remarks> 
        /// <param name="createPostDto"></param>           
        /// <returns></returns>
        /// <response code="201">Sucesso</response>
        /// <response code="500">Erro interno do servidor.</response>  

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostDto createPostDto)
        {
            var post = await _postService.CreatePostAsync(createPostDto);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, UpdatePostDto updatePostDto)
        {
            await _postService.UpdatePostAsync(id, updatePostDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
