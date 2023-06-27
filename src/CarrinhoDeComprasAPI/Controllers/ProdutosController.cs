using Domain.Model;
using Infra.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly DALProdutos _dalProdutos;
        public ProdutosController(DALProdutos dALProdutos)
        {
            _dalProdutos = dALProdutos;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IAsyncEnumerable<Produtos> BuscarTodos()
        {
            try
            {
                return _dalProdutos.BuscarTodosAsync();
                
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> BuscarPorId(int id )
        {
            try
            {
                Produtos? produto = await _dalProdutos.BuscarPorIdAsync(id);

                if(produto is null)
                {
                    return NotFound();
                }

                return Ok(produto);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cadastrar(Produtos produto)
        {
            try
            {
                await _dalProdutos.AddAsync(produto);
                return CreatedAtAction(
                    nameof(BuscarPorId),
                    new {Id = produto.Id},
                    produto);
            }
            catch(Exception erro)
            {
                return BadRequest();
            }
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Produtos produto)
        {
            if(produto is null && id == 0 && !ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                bool status = await _dalProdutos.AtualizarAsync(produto);
                return Ok(new { produto, status });
            }
            catch(Exception)
            {
                throw;
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deletar(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            try
            {
                bool status = await _dalProdutos.DeletarAsync(id);
                return Ok(new {status, descricao = status ? "Excluído" : "Não foi excluído"});
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
