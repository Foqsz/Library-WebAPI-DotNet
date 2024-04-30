# Desafio: Desenvolvimento de API para Gerenciamento de Biblioteca

Este projeto consiste na criação de uma API utilizando .NET Core para gerenciar um sistema de biblioteca. A API permite aos usuários realizar diversas operações relacionadas ao gerenciamento de usuários e livros.
Também foi utilizado JWT Token.

## Funcionalidades

### Gerenciamento de Usuários

- Cadastro de usuários com informações como nome, email e senha.
- Visualização e edição das informações cadastradas pelos usuários.
- Remoção e manipulação de usuários por administradores do sistema.

### Gerenciamento de Livros

- Cadastro de novos livros com informações como título, autor, gênero, ISBN e ano de publicação.
- Empréstimo de livros, registrando o livro emprestado, o usuário que o emprestou e a data de empréstimo.
- Devolução de livros emprestados.
- Pesquisa de livros por título, autor ou gênero.
- Visualização de todos os livros disponíveis para empréstimo.
- Remoção e manipulação de livros por administradores do sistema.

## Itens Obrigatórios

- Implementação de autenticação e autorização para proteger as rotas da API, garantindo acesso apenas a usuários autenticados.
- Adição de validações para garantir a integridade dos dados, como verificar empréstimos duplicados.
- Implementação de paginação e ordenação para listagens de livros.
- Criação de endpoints para CRUD de usuários e livros.
- Implementação de tratamento de erros adequado, fornecendo respostas HTTP apropriadas e mensagens de erro claras.
- Estruturação do projeto usando o modelo MVC, incluindo controladores, serviços e repositórios.

## Como Executar

Para executar a API, siga estas etapas:

1. Clone este repositório para o seu ambiente local.
2. Certifique-se de ter o .NET Core SDK instalado.
3. Navegue até o diretório do projeto e execute o comando `dotnet build` para compilar o projeto.
4. Após a compilação, execute o comando `dotnet run` para iniciar a API.
5. A API estará disponível localmente e você poderá acessá-la através do navegador ou de ferramentas como Postman.

## Contribuição

Contribuições são bem-vindas! Se você encontrar algum problema ou tiver sugestões para melhorar este projeto, sinta-se à vontade para abrir uma issue ou enviar um pull request.

## Contato

Para mais informações ou contato direto, você pode me encontrar no [LinkedIn](https://www.linkedin.com/in/victor-vinicius-2a9166255/) ou enviar um email para contatovictorvinicius05@gmail.com.

## Licença

Este projeto está licenciado sob a [MIT License](LICENSE).
