using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Config
{
    public static class SwaggerConfig
    {
        public static void ConfigureSwagger(SwaggerGenOptions options) => options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API Geladeira",
            Version = "v1",
            Description =
            @"<H2>API para gerenciamento de itens da geladeira</H2>
                <H4> IDE e Versão do Framework </H4>
                    <p> O projeto foi desenvolvido utilizando o Visual Studio, com o .NET 8.</p>
                <H4> Persistência e Banco de Dados</H4>
                    <p> Migration adicionada com os comandos:</p>
                    <ul>
                        <li> Add-Migration CriacaoGeladeira: Criação da migração com base na model.</li>
                        <li> Update-Database CriacaoGeladeira: Atualiza o banco de dados com as alterações definidas na migração.</li>
                        <li> Script-Migration: Gera um script SQL para a migração.</li>
                    </ul> 
                <H4> HTTP Codes mapeados nesta aplicação: </H4>
                    <ul>
                        <li><b>200:</b> Requisição realizada com sucesso.</li>
                        <li><b>204:</b> Requisição realizada com sucesso, mas não há conteúdo a ser retornado.</li>
                        <li><b>400:</b> Solicitação inválida, por exemplo, quando há um erro de validação ou informações incorretas.</li>
                        <li><b>404:</b> Não encontrado, por exemplo, quando um item não é encontrado na geladeira.</li>
                        <li><b>500:</b> Erro interno do servidor, por exemplo, quando ocorre uma exceção não tratada.</li>
                    </ul>",
            Contact = new OpenApiContact
            {
                Name = "Marina Varela",
                Email = "marinavareladev@gmail.com",
            }
        });
    }
}
