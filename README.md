### Add Migrations
```
cd ./TodoApi
dotnet ef migrations add initial -o Data/Migrations
```

<br>


### Subir Container do Banco de Dados
```
docker-compose up -d --build db
```

<br>


### Subir Container da Aplicação

A aplicação **TodoApi** verifica se existe uma **variável de ambiente** com chave "CONNECTION_STRING". Existindo ela usa o valor dessa variável para setar a conexão com o banco de dados. Do contrário, a conexão é lida do arquivo de configuração `appsettings.json`.

Nosso Dockerfile também possibilita a passagem dessa variável de ambiente:
```
ARG CONNECTION_STRING
ENV CONNECTION_STRING=$CONNECTION_STRING
```

Dessa forma, podemos rodar o seguintes comandos para Construir a imagem e para Subir o container, na sequência:
```
docker build -t vitormoschetta/todoapi -f Dockerfile .

sudo docker run --name todoapi.app1 -d -e CONNECTION_STRING="server=todoapi.db;user=root;password=MySql2022;database=todoapidb" --network todoapi -p 5050:8080 vitormoschetta/todoapi
```

Veja que conectamos na mesma **rede/network** que o container de banco de dados está (todoapi). **Containeres se comunicam pelo NOME**, e precisam estar na mesma Rede/Network.

Por padrão a aplicação roda na porta 8080, e portanto mapeamos essa porta do container para a porta 5050 do HOST. Podemos acessar a aplicação no seguinte endereço:

<http://localhost:5050/swagger/index.html>


<br>


### docker-compose

Porque não subimos o container da aplicação pelo `docker-compose`? Porque queremos apresentar as possibilidades existentes. O `docker-compose` também faz usso dessa variável de ambiente CONNECTION_STRING, ou seja, ele passa a variável para a imagem da aplicação, e aplicação consegue identificar o valor. Logo, poderíamos subir o container da aplicação assim:
```
docker-compose up -d --build app
```

Poderíamos subir ambos os containeres (banco e aplicação) pelo `docker-compose`, assim:
```
docker-compose up -d --build
```

Rodando a aplicação pelo compose, ficará acessível no seguinte endereço: 

<http://localhost:5000/swagger/index.html>


<br>


### VOLUME

Uma alternativa à **variáveis de ambiente** são os VOLUMES. Veja que podemos mudar o arquivo de configuração (`appsettings.json`) a ser usado pela aplicação da seguinte forma:
```
sudo docker run --name todoapi.app2 -d -v ~/Desktop/GitHub/TodoApi/TodoApi/appsettings.DockerDevelopment.json:/public/appsettings.json --network todoapi -p 6060:8080 vitormoschetta/todoapi
```
Veja que  substituímos o conteúdo do arquivo de configuração (`appsettings.json`) com o conteúdo que está no `appsettings.DockerDevelopment.json`.

Acessar: <http://localhost:6060/swagger/index.html>


<br>


### Network HOST

Outra forma de subir o App pela linha de comando seria setando a **rede/network como HOST**. Neste caso a aplicação vai se comportar como se estivesse rodando na maquina local e não em um container:
```
sudo docker run --name todoapi.app3 -d --network host vitormoschetta/todoapi
```
No comando acima já não foi necessário setar a porta, pois quando falamos que nossa rede é `--network host`, não há o que mapear para o host. Neste caso a porta será a que a própria aplicação expõe (8080).

Acessar: <http://localhost:8080/swagger/index.html>


<br>


### Deploy em produção

Atualmente se usa muito o que chamamos de Continuous Deployment (CD). Isso pode ser feito através de uma ferramente, como por exemplo o **Jenkis**, que basicamente se conecta no servidor de aplicação via SSH e executa uma sequência de comandos Bash/Shell, como uma espécie de esteira. Entre as acões realizadas estão:
- Subir uma máquina temporária;
- Instalar ferramentas e SKDs necessários nessa máquina;
- Baixar código fonte publicado;
- Executar comandos de Build sobre o código fonte;
- Executar comandos de Teste sobre o código fonte;
- Gerar uma imagem da aplicação e subir para um repositório de imagens (DockerHub por exemplo);
- Se conectar via SSH ao Servidor em que a aplicação está rodando;
- Baixar a nova imagem da aplicação de um repositório de imagens (DockerHub por exemplo);
- Parar o container da aplicação;
- Subir novo container com imagem atualizada.

As **credenciais e acessos de produção** podem ser setadas via variável de ambiente e/ou substituindo o arquivo de configuração da aplicação (appsettings.json) no momento de subir o novo container.

A abordagem de VOLUME me parece mais segura do que Variáveis de Ambiente.