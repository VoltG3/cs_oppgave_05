## cs_oppgave_05 
#### C# / CRUD / MYSQL / API

| Tags             | Chaper                           |
|:-----------------|:---------------------------------|
| [oppgave05_V1](https://github.com/VoltG3/cs_oppgave_05/tags) | [SQL Migration](#sql-migrationn) |

#### Dependencies
- Linux
- Docker
- DBeaver

```sh
dotnet add package MySql.Data
dotnet add package Spectre.Console
dotnet add package Spectre.Console.Cli
```



#### Create Docker MYSQL container
```sh
docker create
 --name mysql_movies
 -e MYSQL_ROOT_PASSWORD=rootpassword
 -e MYSQL_DATABASE=movies
 -e MYSQL_USER=all
 -e MYSQL_PASSWORD=mysql
 -p 3309:3306
 mysql:8.0
```
#### Reminder: Basic Docker Commands
```sh
docker images
docker ps
docker ps -a 
sudo lsof -i :3306 
docker start oppgave_05 
docker stop oppgave_05
docker logs oppgave_05
docker exec -it mysql_movies mysql -u root -p
```

#### C# Register path for sql script
```sh
cs_oppgave_05.csproj

<ItemGroup>
  <Content Include="SqlScripts\**\*.sql">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

#### Reminder: Basic SQL Commands
```sql
SHOW DATABASES;
USE movies;
SHOW TABLES;
DESCRIBE <tablename>;
```

#### DBeaver
```sh
if error: Public Key Retrieval is not allowed

ALTER USER 'all'@'%' IDENTIFIED WITH mysql_native_password BY 'mysql';
FLUSH PRIVILEGES;
```

# SQL Migration