# Sistema de Controle de Pedidos para Lanchonete

## Visão Geral

Uma lanchonete de bairro está expandindo devido ao seu grande sucesso. No entanto, a ausência de um sistema de controle de pedidos está causando caos e confusão no atendimento aos clientes. A lanchonete enfrenta problemas como pedidos mal interpretados, perdidos ou esquecidos, resultando em atrasos e insatisfação dos clientes. 

Para resolver esse problema, será desenvolvido um sistema de autoatendimento que permitirá aos clientes fazerem seus pedidos diretamente, sem a necessidade de interagir com um atendente.

A implementação desse sistema de controle de pedidos visa melhorar significativamente a eficiência do serviço e a satisfação dos clientes. Com ele, a lanchonete poderá gerenciar os pedidos de forma organizada, evitando erros e garantindo que cada pedido seja preparado corretamente.

## Solução Proposta

A lanchonete investirá em um sistema de autoatendimento de fast food, composto por dispositivos e interfaces que permitem aos clientes selecionar e fazer pedidos. O sistema incluirá:

### Funcionalidades

1. **Documentação do Sistema (DDD)**:
    - Realização do pedido e pagamento;
    - Preparação e entrega do pedido.
    ```bash
    https://miro.com/app/board/uXjVK3gCvoA=/
    ```

2. **Aplicação Backend (Monolito)**:
    - Clean architecture.
    - Clean code.
    - APIs para:
        - Cadastro do Cliente.
        - Identificação do Cliente via CPF.
        - Checkout do pedido.
        - Listar os pedidos.
        - Consultar o status do pedido.
        - Listar os pedidos ordenados por status e data.
        - Atualizar o status do pedido
        - Criar, editar, listar e remover produtos.
        - Buscar produtos por categoria.
        - Buscar produtos pelo id.
        - Webhook para confirmação do pagamento do pedido.
    - Documentação das APIs disponível no Swagger.

3. **Docker**:
    - Dockerfile configurado para a execução da aplicação.
    - `docker-compose.yml` para subir o ambiente completo.

## Estrutura do Projeto
![image](https://github.com/user-attachments/assets/a4a7e2ec-bebb-4938-aa3d-013733e6313b)

## Arquitetura
Essa aplicação segue um modelo de arquitetura limpa com a seguinte estrutura:
- Api: O ponto de entrada para solicitações HTTP.
- Application: Contém a lógica de negócios (Use Cases).
- Domain: Contém a lógica de negócios mais interna (Entidades).
- Infrastructure: Implementa os mecanismos de persistência e acesso aos dados.

## Arquitetura da solução
![image](https://github.com/user-attachments/assets/c7da6148-26ae-4b75-a9cd-1c36d1727bd1)


## Como Executar o Projeto

### Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Passos para Execução

1. Clone o repositório:

```bash
git clone https://github.com/DouglasFMiyamoto/LanchoneteAPI
```
```bash
cd nome-do-repositorio
```
2. Construa e execute a aplicação usando Docker Compose:
```bash
docker-compose up --build
```
3. Acesse a documentação das APIs via Swagger em:
```bash
http://localhost:8080/swagger/index.html
```
4. Ou se preferir execute o projeto pelo visual studio.

