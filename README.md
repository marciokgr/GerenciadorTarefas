# Gerenciador de Tarefas

Este repositório possui uma solução para uma Web API Restful, construída utilizando .NET 8, utilizando os princípios da arquitetura Clean Code para garantir alta manutenibilidade e escalabilidade. O projeto segue um design baseado em camadas, implementando diversos Padrões de Projeto, incluindo Unit of Work, Facade, Factory, Repository e Injeção de Dependência, assegurando coesão modular e uma clara separação de responsabilidades.
O domínio da aplicação foi modelado para atender a um cenário de gerenciamento de tarefas, com os recursos centrais "Projetos" e "Tarefas". A API oferece endpoints bem definidos que suportam operações CRUD completas, permitindo o cadastro, atualização, exclusão e consulta dos dados. 

# Ferramentas

Você irá precisar das ferramentas abaixo para rodar o serviço:
- [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/downloads/);
- [.NET 8.0](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

## Rodar os Serviços - WebAPI no Docker:

Você poderá usar o projeto "docker-compose" na solução do Visual Studio (Set Startup Project) e criar um profile. Caso queira rodar o comando manualmente use:

```
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

## Testando o Projeto

Você pode utilizar a interface do Swagger para testar alguns endpoints: http://localhost:5055/swagger/index.html
