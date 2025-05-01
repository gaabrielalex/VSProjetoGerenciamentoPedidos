# GerenciamentoPedidos

Este é um sistema de gerenciamento de pedidos desenvolvido em C#, dividido em várias camadas para facilitar a manutenção e escalabilidade. O projeto segue uma arquitetura modular, com diferentes responsabilidades separadas em pastas e projetos distintos.

## Funcionalidades do Projeto

O sistema oferece as seguintes funcionalidades principais, focadas em operações CRUD (Criar, Ler, Atualizar e Deletar):

### 1. **Gerenciamento de Pedidos** 📝
   - Criação de novos pedidos.
   - Consulta de pedidos existentes.
   - Atualização de informações de pedidos.
   - Exclusão de pedidos.

### 2. **Gerenciamento de Itens do Pedido** 🍽️
   - Adição de itens a um pedido.
   - Consulta de itens associados a um pedido.
   - Atualização de informações dos itens (ex.: quantidade).
   - Remoção de itens de um pedido.

### 3. **Gerenciamento de Clientes** 👥
   - Cadastro de novos clientes.
   - Consulta de clientes existentes.
   - Atualização de informações dos clientes.
   - Exclusão de registros de clientes.

### 4. **Gerenciamento de Produtos** 🛒
   - Cadastro de novos produtos.
   - Consulta de produtos disponíveis.
   - Atualização de informações dos produtos (ex.: preço, descrição).
   - Exclusão de produtos.

---

## Descrição das Pastas

- **DAOGerenciamentoPedidos/** 📂  
  Contém a lógica de acesso a dados (Data Access Object).

- **ModelsGerenciamentoPedidos/** 📦  
  Define os modelos e entidades do sistema.

- **UtilsGerenciamentoPedidos/** 🔧  
  Contém utilitários, como manipuladores de erro e registro de logs.

- **WebGerenciamentoPedidos/** 🌐  
  Interface web do sistema, incluindo páginas e configurações para interação com o usuário final.

- **TestesGerenciamentoPedidos/** ✅  
  Contém os testes automatizados para garantir a qualidade do sistema.

---

## Pré-requisitos

- **Visual Studio** 🖥️  
  Para abrir e compilar a solução `.sln`.

- **.NET Core/Framework** 🛠️  
  Certifique-se de que a versão necessária está instalada.

- **Banco de Dados** 💾  
  Certifique-se de configurar o banco de dados necessário para o funcionamento do sistema.
