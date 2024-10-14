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

## Arquitetura da solução (Kubernetes)
![image](https://github.com/user-attachments/assets/c7da6148-26ae-4b75-a9cd-1c36d1727bd1)

- Clients: Representam os usuários ou sistemas externos que se conectam à API. Eles se comunicam com a aplicação através da porta 30080.
- Cluster: Responsável por orquestrar os contêineres, escalá-los, balancear carga e manter a saúde dos serviços em execução.
- Node : Um Node é uma máquina física ou virtual que faz parte do cluster Kubernetes e hospeda os Pods.
- API Service: Este é um objeto Kubernetes do tipo svc que atua como um proxy para enviar tráfego de rede para um ou mais pods executando a aplicação API. O Service garante que a API possa ser acessada de maneira estável e confiável.
- Pod: Aqui temos um Pod que representa a unidade de implantação da aplicação API dentro do cluster Kubernetes. O Pod encapsula um ou mais contêineres da aplicação, isolando-a em seu próprio ambiente de execução.
- HPA (Horizontal Pod Autoscaler): O HPA permite o escalonamento automático do número de réplicas do Pod baseado em métricas específicas, como CPU ou tráfego de rede, para garantir a disponibilidade e a eficiência da aplicação conforme a demanda muda.
- Payment: Responsável por processar pagamentos (ainda não foi implementa a integração com o mercado livre).
- DB Service: Serviço com a porta 5432 é um banco de dados PostgreSQL.
- PVC (Persistent Volume Claim): Este é um recurso de armazenamento persistente que está associado ao banco de dados PostgreSQL, garantindo que os dados persistam além do ciclo de vida dos contêineres e Pods.


## Como Executar o Projeto

### Pré-requisitos

- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Passos para Execução (Docker-Compose)

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

### Passos para Execução (Kubernetes K8s)

1. Clone o repositório:

```bash
git clone https://github.com/DouglasFMiyamoto/LanchoneteAPI
```
```bash
cd nome-do-repositorio
```
2. Habilitar o Kubernetes nas configurações do Docker Desktop:
   ![image](https://github.com/user-attachments/assets/774277e6-629c-41ac-8542-fbff34ed6a09)
   
3. Execute os comandos abaixo para aplicar todos os manifestos do Kubernetes:
```bash
kubectl apply -f postgres-deployment.yaml
```
```bash
kubectl apply -f postgres-pvc.yaml
```
```bash
kubectl apply -f postgres-service.yaml
```
```bash
kubectl apply -f metrics.yaml
```
```bash
kubectl apply -f lanchoneteapi-deployment.yaml
```
```bash
kubectl apply -f lanchoneteapi-hpa.yaml
```
```bash
kubectl apply -f lanchoneteapi-service.yaml
```
4. Acesse a documentação das APIs via Swagger em:
```bash
http://localhost:30080/swagger/index.html
```
5. Ou se preferir execute o projeto pelo visual studio.
