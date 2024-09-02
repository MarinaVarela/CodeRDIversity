using ApiGeladeira.DTOs;
using ApiGeladeira.Models;
using Application.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ApiGeladeira.Controllers
{
    [Route("api/geladeira")]
    [ApiController]
    public class GeladeiraController : ControllerBase
    {
        private readonly GeladeiraService _geladeiraService;
        private readonly IMapper _mapper;

        public GeladeiraController(GeladeiraService geladeiraService, IMapper mapper)
        {
            _geladeiraService = geladeiraService;
            _mapper = mapper;
        }

        [HttpOptions("opcoes-disponiveis")]
        public IActionResult OpcoesDisponiveis()
        {
            Response.Headers.Append("Allow", "GET, POST, PATCH, DELETE, OPTIONS");
            return Ok();
        }

        [HttpGet("obter-itens")]
        public async Task<IActionResult> ObterItens()
        {
            try
            {
                var obterItens = await _geladeiraService.ObterItens();
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

        [HttpGet("obter-item/{id:int}")]
        public async Task<IActionResult> ObterItemPorId(int id)
        {
            try
            {
                var obterItem = await _geladeiraService.ObterItemPorId(id);
                if (obterItem is null)
                    return NotFound(new { Mensagem = $"Item {id} não encontrado." });

                return Ok(new { Data = obterItem, Mensagem = "Aproveite seu item." });
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        [HttpGet("procurar-na-geladeira/{nome}")]
        public async Task<IActionResult> ObterItemPorNome(string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                    return BadRequest("Esqueceu o que ia procurar na geladeira?");

                var item = await _geladeiraService.ObterItemPorNome(nome);

                if (item is null)
                    return NotFound(new { Mensagem = $"Não tem {nome} na geladeira." });

                return Ok(new { Data = item, Mensagem = $"Aproveite o(a) {nome}." });
            }
            catch
            {
                return StatusCode(500, "Ops! Acho que a porta da geladeira travou. Chame o técnico para ajustar.");
            }
        }

        [HttpPost("adicionar-item")]
        public async Task<IActionResult> AdicionarItem([FromBody] CreateGeladeiraDTO item)
        {
            try
            {
                var inserirItem = _mapper.Map<ItemGeladeira>(item);

                await _geladeiraService.AdicionarItemAsync(inserirItem);

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

        [HttpPatch("atualizar-item")]
        public async Task<IActionResult> AtualizarItem([FromBody] UpdateGeladeiraDTO item)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest("As informações de atualização não são válidas.");

                var atualizarItem = _mapper.Map<ItemGeladeira>(item);

                await _geladeiraService.AtualizarItem(atualizarItem);

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

        [HttpDelete("remover-item/{id:int}")]
        public async Task<IActionResult> RemoverItem(int id)
        {
            try
            {
                await _geladeiraService.RemoverItem(id);
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

        [HttpDelete("remover-tudo")]
        public async Task<IActionResult> RemoverTodosElementos()
        {
            try
            {
                int quantItensExcluidos = await _geladeiraService.RemoverTodosItens();
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
}

/*
O projeto de api da geladeira foi refatorado completamente para simplificar e enxugar o código.
Ajustado também a organização da api e bibliotecas de classes.

Migration adicionada com os comandos:

- Add-Migration CriacaoGeladeira
- Update-Database (use db_geladeira | select * from ItensGeladeira)
- Script-Migration (Para gerar o script de criação da tabela)

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [ItensGeladeira] (
    [Id] int NOT NULL IDENTITY,
    [Andar] int NOT NULL,
    [Container] int NOT NULL,
    [Posicao] int NOT NULL,
    [Nome] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_ItensGeladeira] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240902223212_CriacaoGeladeira', N'8.0.8');
GO

COMMIT;
GO

 */

// Exercício por Marina Varela