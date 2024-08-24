using ApiGeladeira.DTOs;
using ApiGeladeira.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGeladeira.Controllers
{
    [Route("api/geladeira")]
    [ApiController]
    public class GeladeiraController : ControllerBase
    {
        private readonly Geladeira _geladeira;

        public GeladeiraController(Geladeira geladeira)
        {
            _geladeira = geladeira;
        }

        [HttpOptions("opcoes-disponiveis")]
        public IActionResult OpcoesDisponiveis()
        {
            Response.Headers.Add("Allow", "GET, POST, DELETE, OPTIONS");
            return Ok();
        }

        [HttpGet("obter-itens")]
        public IActionResult ObterItens()
        {
            try
            {
                var obterItens = _geladeira.ObterItens();
                if (obterItens is null)
                    return NoContent();

                return Ok(obterItens);
            }
            catch { return StatusCode(500, "Não foi possível obter os itens da geladeira."); }
        }

        [HttpPost("novo-item")]
        public IActionResult AdicionarItem([FromBody] CreateItemGeladeiraDTO item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("As informações do item adicionado não são válidas.");

                var itemGeladeira = new ItemGeladeira(item.Andar, item.Container, item.Posicao, item.Nome);
                var resultado = _geladeira.AdicionarElemento(itemGeladeira);

                if (resultado.Contains("inválido") || resultado.Contains("ocupada"))
                    return Conflict(new { Mensagem = resultado });

                return Ok(new
                {
                    Data = resultado,
                    Mensagem = "Item adicionado com sucesso."
                });
            }
            catch { return StatusCode(500, "Não foi possível adicionar itens na geladeira."); }
        }

        [HttpDelete("remover-item")]
        public IActionResult RemoverItem(int andar, int container, int posicao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("As informações do item a ser removido não são válidas.");

                var resultado = _geladeira.RemoverElemento(andar, container, posicao);
                if (resultado.Contains("não existe"))
                    return NotFound(new { Mensagem = resultado });

                return Ok(new { Mensagem = resultado });
            }
            catch
            {
                return StatusCode(500, "Não foi possível remover itens da geladeira.");
            }
        }

        [HttpDelete("remover-tudo")]
        public IActionResult RemoverTodosElementos()
        {
            try
            {
                var resultado = _geladeira.RemoverTodosElementos();

                if (resultado.Contains("Nenhum item"))
                    return NoContent();

                return Ok(new { Mensagem = resultado });
            }
            catch
            {
                return StatusCode(500, "Não foi possível remover todos os itens da geladeira.");
            }
        }
    }
}
