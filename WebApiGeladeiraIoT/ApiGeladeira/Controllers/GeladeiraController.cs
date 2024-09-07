using ApiGeladeira.DTOs;
using ApiGeladeira.Models;
using AutoMapper;
using Domain.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiGeladeira.Controllers
{
    [Route("api/geladeira")]
    [ApiController]
    public class GeladeiraController : ControllerBase
    {
        private readonly IGeladeiraService _service;
        private readonly IMapper _mapper;

        public GeladeiraController(IGeladeiraService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna os métodos HTTP disponíveis para a API.
        /// </summary>
        /// <remarks>
        /// Este endpoint responde a uma requisição OPTIONS e inclui todos os métodos HTTP que a API suporta.
        /// </remarks>
        /// <returns>Uma resposta com o cabeçalho 'Allow' indicando os métodos suportados.</returns>
        [HttpOptions("opcoes-disponiveis")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult OpcoesDisponiveis()
        {
            Response.Headers.Append("Allow", "GET, POST, PATCH, DELETE, OPTIONS");
            return Ok();
        }

        /// <summary>
        /// Obtém todos os itens da geladeira.
        /// </summary>
        /// <remarks>
        /// Este endpoint retorna todos os itens armazenados na geladeira.
        /// </remarks>
        /// <returns>Uma resposta contendo todos os itens e uma mensagem de sucesso.</returns>
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("obter-itens")]
        public async Task<IActionResult> ObterItens()
        {
            try
            {
                var obterItens = await _service.ObterItens();
                return Ok(new { Data = obterItens, Mensagem = "Aproveite seu(s) item(ns)." });
            }
            catch (InvalidDataException)
            {
                return NoContent();
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        /// <summary>
        /// Obtém um item específico da geladeira pelo ID.
        /// </summary>
        /// <param name="id">O ID do item a ser obtido.</param>
        /// <remarks>
        /// Este endpoint retorna um item da geladeira com base no ID fornecido.
        /// </remarks>
        /// <returns>Uma resposta contendo o item encontrado e uma mensagem de sucesso ou uma mensagem de erro se o item não for encontrado.</returns>
        [HttpGet("obter-item/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterItemPorId(int id)
        {
            try
            {
                var obterItem = await _service.ObterItemPorId(id);
                if (obterItem is null)
                    return NotFound(new { Mensagem = $"Item {id} não encontrado." });

                return Ok(new { Data = obterItem, Mensagem = "Aproveite seu item." });
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        /// <summary>
        /// Procura um item na geladeira pelo nome.
        /// </summary>
        /// <param name="nome">O nome do item a ser procurado.</param>
        /// <remarks>
        /// Este endpoint busca um item na geladeira com base no nome fornecido.
        /// </remarks>
        /// <returns>Uma resposta contendo o item encontrado e uma mensagem de sucesso ou uma mensagem de erro se o item não for encontrado.</returns>
        [HttpGet("procurar-na-geladeira/{nome}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ObterItemPorNome(string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return BadRequest("Esqueceu o que ia procurar na geladeira?");

                var item = await _service.ObterItemPorNome(nome);
                if (item is null)
                    return NotFound(new { Mensagem = $"Não tem {nome} na geladeira." });

                return Ok(new { Data = item, Mensagem = $"Aproveite o(a) {nome}." });
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        /// <summary>
        /// Adiciona um novo item à geladeira.
        /// </summary>
        /// <param name="item">O DTO contendo os dados do item a ser adicionado.</param>
        /// <remarks>
        /// Este endpoint adiciona um novo item à geladeira com base nos dados fornecidos.
        /// </remarks>
        /// <returns>Uma resposta confirmando a adição do item e sua localização na geladeira.</returns>
        [HttpPost()]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AdicionarItem([FromBody] CreateGeladeiraDTO item)
        {
            try
            {
                var inserirItem = _mapper.Map<ItemGeladeira>(item);

                await _service.AdicionarItemAsync(inserirItem);
                
                return Ok(new { Mensagem = $"{item.Nome} está guardado(a) no andar {item.Andar}, container {item.Container} e posição {item.Posicao} da geladeira." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        /// <summary>
        /// Atualiza um item existente na geladeira.
        /// </summary>
        /// <param name="item">O DTO contendo os dados do item a ser atualizado.</param>
        /// <remarks>
        /// Este endpoint atualiza um item existente na geladeira com base nos dados fornecidos.
        /// </remarks>
        /// <returns>Uma resposta confirmando a atualização do item e sua nova localização na geladeira.</returns>
        [HttpPatch()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AtualizarItem([FromBody] UpdateGeladeiraDTO item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("As informações de atualização não são válidas.");

                var atualizarItem = _mapper.Map<ItemGeladeira>(item);

                await _service.AtualizarItem(atualizarItem);

                return Ok(new { Mensagem = $"{item.Nome} foi atualizado para andar {item.Andar}, container {item.Container} e posição {item.Posicao} da geladeira." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        /// <summary>
        /// Remove um item específico da geladeira pelo ID.
        /// </summary>
        /// <param name="id">O ID do item a ser removido.</param>
        /// <remarks>
        /// Este endpoint remove um item da geladeira com base no ID fornecido.
        /// </remarks>
        /// <returns>Uma resposta confirmando a remoção do item ou uma mensagem de erro se o item não for encontrado.</returns>
        [HttpDelete("remover-item/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoverItem(int id)
        {
            try
            {
                await _service.RemoverItem(id);

                return Ok(new { Mensagem = "Item removido com sucesso." });
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { Mensagem = $"Item {id} não encontrado." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        /// <summary>
        /// Remove todos os itens da geladeira.
        /// </summary>
        /// <remarks>
        /// Este endpoint realiza uma limpeza completa na geladeira, removendo todos os itens.
        /// </remarks>
        /// <returns>Uma resposta confirmando a remoção de todos os itens ou uma mensagem de erro.</returns>
        [HttpDelete("remover-tudo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(object))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoverTodosElementos()
        {
            try
            {
                int quantItensExcluidos = await _service.RemoverTodosItens();

                return Ok(new { Mensagem = $"Faxina feita. {quantItensExcluidos} foi(foram) removido(s) com sucesso." });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }
    }

// Exercício por Marina Varela