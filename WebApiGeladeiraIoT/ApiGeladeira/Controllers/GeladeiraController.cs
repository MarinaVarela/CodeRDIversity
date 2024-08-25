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
            Response.Headers.Append("Allow", "GET, POST, PUT, DELETE, OPTIONS");
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

                return Ok(new { Data = obterItens, Mensagem = $"Aproveite seu(s) item(ns)." });
            }
            catch { return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar."); }
        }

        [HttpGet("procurar-na-geladeira")]
        public IActionResult ObterItemPorNome(string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return BadRequest("Esqueceu o que ia procurar na geladeira?");

                var item = _geladeira.ObterItemPorNome(nome);
                if (item is null)
                    return NotFound(new { Mensagem = $"Não tem {nome} na geladeira." });

                return Ok(new { Data = item, Mensagem = $"Aproveite o(a) {nome}." });
            }
            catch { return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar."); }
        }

        [HttpPost("adicionar-item")]
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

                return Ok(new { Mensagem = resultado });
            }
            catch { return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar."); }
        }

        [HttpPut("atualizar-item")]
        public IActionResult AtualizarItem([FromBody] UpdateItemGeladeiraDTO atualizarItem)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("As informações de atualização não são válidas.");

                var itemGeladeira = new ItemGeladeira(atualizarItem.Andar, atualizarItem.Container, atualizarItem.Posicao, atualizarItem.Nome);


                var resultado = _geladeira.AtualizarElemento(atualizarItem);

                if (resultado.Contains("Não existe"))
                    return NotFound(new { Mensagem = resultado });

                return Ok(new { Mensagem = resultado });
            }
            catch { return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar."); }
        }

        [HttpDelete("remover-item")]
        public IActionResult RemoverItem(int andar, int container, int posicao)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("As informações do item a ser removido não são válidas.");

                var resultado = _geladeira.RemoverElemento(andar, container, posicao);

                if (resultado.Contains("Não existe"))
                    return NotFound(new { Mensagem = resultado });

                return Ok(new { Mensagem = resultado });
            }
            catch { return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar."); }
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
            catch { return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar."); }
        }
    }
}

/*
O projeto de api da geladeira foi pensado aproveitando algumas classes da aplicação de console, criada anteriormente.
No entanto, o serviço, responsável pela regra de negócio, foi simplificado.

As mensagens de erro foram melhoradas para serem passadas de uma forma mais amigável.

Ao program foi adicionando um Singleton para a geladeira, para que a mesma instância seja utilizada em todas as requisições.

Além disso, ao invés de um Get by ID, pensei que ficaria mais interessante um get por nome.
Assim permitiria ao usuário vasculhar a geladeira.
 */

// Exercício por Marina Varela