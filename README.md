# Host

## Instalação

Instale e configure uma instância de postgresql.
O arquivo `appsettings.json` em `ConnectionStrings:DefaultConnection` possui os dados de conexão.

Clone o repositório com:
`git clone https://github.com/jiom-0/dotnetcrud-01.git`

Acesse-o com `cd ngdotnetcrud`

### Migration
Execute a ferramenta de migração com:
`dotnet ef database update`

### Execução
Rode o programa com `dotnet run`

O endereço será `http://localhost:5106/index.html`

## Docker
Caso esteja utilizando Docker pode contriuir sua imagem com a DockerFile do repositório

`docker build -t dotnetcrud .`

`docker run -it --rm -p 5000:5000 --name dotnetcrud_app dotnetcrud`

Neste caso a porta para acesso da aplicação será `5000`

## Troubleshouting
Certifique-se que, caso esteja usando docker, sua base dados aceite conexões externas (de outros ips).