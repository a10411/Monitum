## Colocar Web API e SQL Server em funcionamento
- Criar no SQL Server Management Studio uma base de dados chamada "MonitumDB"
- Executar, nesta BD, as queries de criação de tabelas presentes no ficheiro DBCreateQueries.SQL (presente em G04_10411_21136_21149_CODIGO.zip\Monitum\MonitumAPI_v2\DB_Queries)
- Povoar a BD com as queries presentes no ficheiro PopulateMonitumDB.sql, um insert de cada vez. (estão na mesma pasta deste README.md)
- É agora possível correr o código da Web API RESTful

### Parte 2 Web API
- Registar um Administrador (correr o código, registar administrador no próprio request)
- Fazer login com administrador e copiar a token
- Fazer "authorize" no topo da página inserindo "Bearer {token_copiado}"
- Com este authorize, é possível agora registar um gestor
- Registar um gestor
- Fazer login de gestor e copiar a token
- Fazer authorize com este novo token ("Bearer {token}")
- Consegue agora fazer tudo relativo ao Gestor (adicionar comunicados, adicionar sala, métricas, etc.)

## Colocar SOAP Service e Cliente em funcionamento
- Extender a pasta "Services" do projeto Monitum_SOAP_Service no Visual Studio e com o botão direito no service fazer "View in Browser"
- É agora possível testar o SOAP no browser
- Para adicionar um gestor, escrever os credenciais pretendidos para este novo gestor e escrever os credenciais anteriormente criados para administrador (o código deve retornar true)

### SOAP Client
- Abrir o projeto presente na pasta G04_10411_21136_21149_CODIGO.zip\Monitum\MonitumAPI_v2\Monitum_SOAP_Client
- Correr o código
- Testar a interface (utilizar o administrador anteriormente criado)


*apesar de apenas mencionarmos o funcionamento com base de dados local, no relatório mencionamos a publicação da Web API e Base de Dados SQL Server na Cloud.*

Link API e BD publicadas: https://monitumapi.azurewebsites.net  
(exemplo: https://monitumapi.azurewebsites.net/estabelecimento/1 (GET))


