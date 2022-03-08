# TodoApi

### Add Migrations
```
cd ./TodoApi
dotnet ef migrations add initial -o Data/Migrations
```

<br>


### Subir Container do Banco de Dados
```
docker-compose up -d db
```

<br>


### Subir Container da API
```
docker-compose up -d --build api
```

<http://localhost:6001/swagger/index.html>

##### Variáveis de ambiente

As configurações de conexão com banco de dados na API (**TodoApi**) podem ser definidas no arquivo de configuração **appsettings.json**. Essas informações também podem
ser passadas por variável de ambiente. Declaramos essas variáveis em diversos locais, para serem usadas em ambiente diferentes, como se segue:

- Variáveis de ambiente para execução da aplicação em **Container** via **docker-compose**:  
Essas variáveis estão no arquivo `.env`.

- Variáveis para execução em modo **Debug no VSCode**:  
Verificar em `./vscode/launch.json`.

- Variáveis para execução da aplicação via **linha de comando**:  
Verificar em `TodoApi/Properties/launchSettings.json`.


Obs: Variáveis de ambiente possuem prioridade sobre os arquivos de configuração da aplicação (appsettings.json). Não existindo as variáveis, então os valores definidos nesse arquivo são aplicados.

Obs2: Quando a aplicação API é iniciada ela verifica a conexão com o banco de dados para executar as **Migrations** necessárias. Porém, se o banco de dados não estiver disponível a aplicação irá gerar uma exceção. Quando subimos os containeres juntos localmente, leva alguns segundos para o banco de dados ficar disponível, por isso usamos
no **Dockerfile** da API o arquivo **wait_for_bootstrapping** (`./Infra/docker/wait_for_bootstrapping.sh`). Trata-se de um script `bash` que pega as variáveis de ambiente de
conexão com o banco de dados e fica verificando se o banco está disponível antes de prosseguir com a inicialização da aplicação API.


<br>


### Subir Container do App
```
docker-compose up -d --build app
```

<http://localhost:6002/>

A Aplicação **TodoApp** verifica se existe uma **variável de ambiente** com chave "API_URL_CONNECTION". Existindo ela usa o valor dessa variável para se comunciar com o servidor de API. Do contrário, a conexão é lida do arquivo de configuração `appsettings.json`.


<br>


### Subir Container do Static App
```
docker-compose up -d --build app-static
```

<http://localhost:6003/>

Diferente do App, que roda no framework Blazor Server, o Static App roda no Blazor Wasm, e gera arquivos estáticos para publicação. Sendo assim, ele não tem o roteamento
por si só. Por isso, ao investigarmos seu Dockerfile (verificar em `Infra/TodoStaticApp/`) podemos perceber que uma imagem do **nginx** é utilizada como servidor.

##### Network HOST

Rodamos o Static App em modo HOST. Ou seja, ele roda em container mas é como se tivesse rodando direto no Sistem Operacional, e por isso não precisamos mapear uma porta, 
pois a porta exposta pelo **nginx** é a porta que ficará disponível no HOST.

Para subir o container com rede em modo HOST via linha de comando seria algo assim:
```
sudo docker run --name todoapp.static -d --network host vitormoschetta/todoapp.static
```


<br>


### VOLUMES

Uma alternativa à **variáveis de ambiente** são os VOLUMES. Podemos mudar o arquivo de configuração (`appsettings.json`) a ser usado pela aplicação da seguinte forma:
```
sudo docker run --name todoapi02 -d -v ./TodoApi/appsettings.DockerDevelopment.json:/public/appsettings.json --network todo-network -p 6004:6004 vitormoschetta/todoapi
```
O segredo aqui está no identificador de volume: `-v ./TodoApi/appsettings.DockerDevelopment.json:/public/appsettings.json`.  
O que esse comando faz é criar uma ponte onde o arquivo `appsettings.DockerDevelopment.json` é visto dentro do container como se fosse o `appsettings.json`.

<http://localhost:6004/swagger/index.html>


<br>


### Seeding database 
Ao subir o Container do banco de dados Mysql, usamos também um volume:
```
....
volumes:      
    - ./Infra/docker/MySql/:/tmp/seeds/
...    
```

Nesse volume dizemos que todos os arquivos que estão na pasta local `./Infra/docker/MySql/` devem ser mapeados para a pasta `/tmp/seeds` no Container. 
Perceba que temos um arquivo de scripts SQL inserem dois registros. Queremos executar esse script junto ao Container para que os dados sejam gerados no banco, e fazemos isso usando o seguinte comando:
```
sudo docker exec -it todoapi.db /bin/bash -c 'mysql -h todoapi.db -u root -pMySql2022 todoapidb < /tmp/seeds/seeds.sql'
```


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

As **credenciais e acessos de produção** podem ser setadas via variável de ambiente (arquivo `.env`) e/ou substituindo o arquivo de configuração da aplicação `appsettings.json` no momento de subir o novo container (técnica de uso de volumes).


<br>

Enviar imagens para DockerHub

```
sudo docker push vitormoschetta/todoapp
sudo docker push vitormoschetta/todoapi
```