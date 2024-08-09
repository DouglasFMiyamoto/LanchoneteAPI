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
    - Arquitetura Hexagonal.
    - APIs para:
        - Cadastro do Cliente.
        - Identificação do Cliente via CPF.
        - Criar, editar e remover produtos.
        - Buscar produtos por categoria.
        - Fake checkout: enviar os produtos escolhidos para a fila (finalização do pedido).
        - Listar os pedidos.
    - Documentação das APIs disponível no Swagger.

3. **Docker**:
    - Dockerfile configurado para a execução da aplicação.
    - `docker-compose.yml` para subir o ambiente completo.

### Limitações de Infraestrutura

- 1 instância para banco de dados.
- 1 instância para executar a aplicação.

## Estrutura do Projeto
![image](https://github.com/user-attachments/assets/2c209ca9-be8c-4061-94e5-20d336cef75b)

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

