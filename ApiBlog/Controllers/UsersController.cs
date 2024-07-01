using ApiBlog.DTOs;
using ApiBlog.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        ///  <remarks>  
        /// Consome dados do usuário do servidor      
        /// <details><summary><strong>Documentação</strong></summary>
        /// <ul><li><a href="https://www.github.com">GET User By Id</a></li>        
        /// </ul></details>   
        ///  <details><summary><strong>Regras</strong></summary>
        /// <ul>        
        /// <li>Necessário ID para consumir.</li>        
        /// </ul></details>
        ///  <details><summary><strong>Fluxo da informação</strong></summary>
        /// <ul>        
        /// <li>Recebe o informações do usuário como parâmetro.</li>                                        
        /// <li>Aplicação acessa banco de ados</li>
        /// </ul>
        /// </details>        
        /// <details><summary><strong>Dados de entrada</strong></summary>
        /// | Nome do Atributo | Formato    | Obrigatoriedade | Observações                      |
        /// |------------------|------------|-----------------|----------------------------------|
        /// | id               | string(?)  | Sim             | Id do usuário                    |        
        /// </details>
        /// <details><summary><strong>Dados da Saída</strong></summary>
        /// | Nome do Atributo | Formato   | Obrigatoriedade | Observações         |
        /// |------------------|-----------|-----------------|---------------------|
        /// | id               | string(?) | Sim             | Id do usuário       |        
        /// | UserName         | string(?)   | Sim             | Nome do usuário     |
        /// </details>
        /// <details><summary><strong>Exemplo Requisição</strong></summary>          
        /// <pre>               
        /// GET /api/Users/123  
        /// </pre>                    
        ///</details>   
        /// </remarks> 
        /// <param name="id"></param>           
        /// <returns></returns>
        /// <response code="200">Sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        /// <response code="500">Erro interno do servidor.</response>     
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        ///  <remarks>  
        /// Cria dados do usuário       
        /// <details><summary><strong>Documentação</strong></summary>
        /// <ul><li><a href="https://www.github.com">POST User</a></li>        
        /// </ul></details>   
        ///  <details><summary><strong>Regras</strong></summary>
        /// <ul>        
        /// <li>Necessário campo usuário e password no body na requisição</li>        
        /// <li>Necessário 10 caracteres no campo password</li>        
        /// </ul></details>
        ///  <details><summary><strong>Fluxo da informação</strong></summary>
        /// <ul>        
        /// <li>Recebe o informações do usuário no body da requisição.</li>                                        
        /// <li>Aplicação acessa banco de ados</li>
        /// </ul>
        /// </details>       
        /// <details><summary><strong>Dados de entrada</strong></summary>
        /// | Nome do Atributo | Formato    | Obrigatoriedade | Observações                      |
        /// |------------------|------------|-----------------|----------------------------------|
        /// | id               | string(?)  | Sim             | Id do usuário                    |        
        /// </details>
        /// <details><summary><strong>Dados da Saída</strong></summary>
        /// | Nome do Atributo | Formato   | Obrigatoriedade | Observações         |
        /// |------------------|-----------|-----------------|---------------------|
        /// | UserName         | string(?) | Sim             | Nome do usuário     |
        /// | Password         | string(10) | Sim            | Pass do usuário    |        
        /// </details>
        /// <details><summary><strong>Exemplo Requisição</strong></summary>          
        /// <pre>          
        ///    {
        ///    "userName": "string",
        ///    "password": "string"
        ///    }       
        /// </pre>                    
        ///</details>   
        /// </remarks> 
        /// <param name="createUserDto"></param>           
        /// <returns></returns>
        /// <response code="201">Sucesso</response>
        /// <response code="500">Erro interno do servidor.</response>  
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto createUserDto)
        {
            var user = await _userService.CreateUserAsync(createUserDto);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPost("auth")]
        public async Task<IActionResult> AuthUser([FromBody] AuthUserDto authenticateUserDto)
        {
            var user = await _userService.AuthUserAsync(authenticateUserDto.UserName, authenticateUserDto.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }
    }
}
