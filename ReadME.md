#### [ C# Intermediate Oppgave 4 ] and [ SQL Oppgave 4 ]

/ C# / FC CORE / CRUD / MYSQL / API / WEB SERVER / CURL TESTS / XUNIT TEST

| Tags         | Chaper                                                           |
|:-------------|:-----------------------------------------------------------------|
| oppgave05_V1 | [SQL Migration](#sql-migration)                                  |
|              | [Docker MYSQL container](#docker-mysql-container)                |
|              | [C# Register path for sql script](#register-path-for-sql-script) |
|              | [DBeaver](#dbeaver)                                              |
| oppgave05_V2 | [Simple Web Server](#simple-webserver)                           |
| oppgave05_V3 | [CRUD](#crud)                                                    |
|              | [Issues](#issues)                                                |
| img          | [Relation Diagram](#relation-diagram)                            |
|              | [Curl Tests - Relations](#curl-tests-relations)                  |
|              | [Curl Tests - Options](#curl-tests-options) |                                           
Best for test anyway - Linux / Docker / DBeaver

#### Dependencies

```sh
dotnet add package MySql.Data
dotnet add package Spectre.Console
dotnet add package Spectre.Console.Cli

dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package MySqlConnector
```

## SQL Migration
___
#### Dependencies
```sh
dotnet add package MySql.Data
dotnet add package Spectre.Console
dotnet add package Spectre.Console.Cli
```
#### Docker MYSQL container
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

#### Reminder: Basic SQL Commands
```sql
SHOW DATABASES;
USE movies;
SHOW TABLES;
DESCRIBE <tablename>;
```

#### Register path for sql script
```sh
cs_oppgave_05.csproj

<ItemGroup>
  <Content Include="SqlScripts\**\*.sql">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </Content>
</ItemGroup>
```

#### DBeaver
```sh
if error: Public Key Retrieval is not allowed

ALTER USER 'all'@'%' IDENTIFIED WITH mysql_native_password BY 'mysql';
FLUSH PRIVILEGES;
```

## Simple Webserver
___
#### Add `server.csproj`
```sh
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

</Project>
```

#### Edit `cs_oppgave_05.csproj`
```sh
replace

          <Project Sdk="Microsoft.NET.Sdk">
          <TargetFramework>net7.0</TargetFramework>
          
with

          <Project Sdk="Microsoft.NET.Sdk.Web">
          <TargetFramework>net9.0</TargetFramework> 
          
```

#### Check Connection
```sh
web:

      http://localhost:5000
      
termnall:

      curl http://localhost:5000 
      
```

## CRUD
___

#### Dependencies
```sh
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package MySqlConnector
```

#### Issues

Remove `server.csproj`, then rewrite `cs_oppgave_05.proj`
```sh
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.5" />
    <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.3" />
    <PackageReference Include="MySql.Data" Version="9.4.0" />
    <PackageReference Include="Spectre.Console" Version="0.50.0" />
    <PackageReference Include="Spectre.Console.Cli" Version="0.50.0" />
  </ItemGroup>
</Project>
```

#### If EF Core version crash
```sh
dotnet remove package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.13
```

#### Relation Diagram
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/01.png)

## Curl Tests Relations
___
#### Execute 'cur_test_scripts.sh'
!Imortant 'Run once!'
```sh
chmod +x curl_tests_script_relations.sh
sh ./curl_tests_script_relations.sh
```

#### CURL TEST 1 - MASTER TABLE - MOVIE

```sh
echo ""
echo "POST [ movie ]"
echo ""
curl -X POST "http://localhost:5000/api/Movies" \
  -H "Content-Type: application/json" \
  -d '{"movTitle":"Movie_929","movYear":2025,"movTime":120,"movLang":"EN","movDtRel":"2025-08-08","movRelCountry":"US"}'
echo ""
  
echo ""
echo "PACH [ movie ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Movies/929" \
  -H "Content-Type: application/json" \
  -d '{"movTitle":"Movie_PACHED_929"}'
echo ""
  
echo ""
echo "GET [ movie ]"
echo ""
curl -X GET "http://localhost:5000/api/Movies/929" \
  -H "Accept: application/json"
echo ""
```

#### CURL TEST 2 - SLAVE TABLE - GENRES

```sh
echo ""
echo "POST [ genre ]"
echo ""
curl -X POST "http://localhost:5000/api/Genres" \
  -H "Content-Type: application/json" \
  -d '{"genTitle":"Genre_929"}'
echo ""

echo ""
echo "PATCH [ genre ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Genres/1014" \
  -H "Content-Type: application/json" \
  -d '{"genTitle":"Genre_PACHED_929"}'
echo ""

echo ""
echo "GET [ genre ]"
echo ""
curl -X GET "http://localhost:5000/api/Genres/1014" \
  -H "Accept: application/json"
echo ""
```

#### CURL TEST 2 - RELATIONS - [ movie ] >> [ movie_genres ] << [ genres]

```sh
echo ""
echo "POST [ movie_genres ]"
echo ""
curl -X POST "http://localhost:5000/api/MovieGenres" \
  -H "Content-Type: application/json" \
  -d '{"movId":929,"genId":1014}'
echo ""
  
echo ""
echo "GET [ movie_genres: movie & genres ]"
echo ""
curl -X GET "http://localhost:5000/api/MovieGenres?movId=929&genId=1014" -H "Accept: application/json"
echo ""
```

#### CURL TEST 3 - SLAVE TABLE ACTOR

```sh
echo ""
echo "POST [ actor ]"
echo ""
curl -X POST "http://localhost:5000/api/Actors" \
  -H "Content-Type: application/json" \
  -d '{"actFname":"ActorFname_929","actLname":"ActorLname_929","actGender":"M"}'
echo ""

echo ""
echo "PATCH [ actor ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Actors/125" \
  -H "Content-Type: application/json" \
  -d '{"actFname":"ActorFname_PATCH_929"}'
echo ""

echo ""
echo "GET [ actor ]"
echo ""
curl -X GET "http://localhost:5000/api/Actors/125" \
  -H "Accept: application/json"
echo ""
```

#### CURL TEST 3 - RELATIONS - [ movie ] >> [ movie_cast ] << [ actor ]

```sh
echo ""
echo "POST [ movie_cast ]"
echo ""
curl -X POST "http://localhost:5000/api/MovieCasts" \
  -H "Content-Type: application/json" \
  -d '{"actId":125,"movId":929,"role":"Role_929"}'
echo ""

echo ""
echo "GET [ movie_cast: mmovie & actor ]"
echo ""
curl -X GET "http://localhost:5000/api/MovieCasts?actId=125&movId=929" -H "Accept: application/json"
echo ""
```

#### CURL TEST 4 - SLAVE TABLE - REVIEWER

```sh
echo ""
echo "POST [ reviewer ]"
echo ""
curl -X POST "http://localhost:5000/api/Reviewers" \
  -H "Content-Type: application/json" \
  -d '{"revName":"revName_929"}'
echo ""

echo ""
echo "PATCH [ reviewer ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Reviewers/9021" \
  -H "Content-Type: application/json" \
  -d '{"revName":"revName_PATCHED_929"}'
echo ""

echo ""
echo "GET [ reviewer ]"
echo ""
curl -X GET "http://localhost:5000/api/Reviewers/9021" \
  -H "Accept: application/json"
echo ""
```

#### CURL TEST 4 - RELATIONS - [ movie ] >> [ rating ] << [ reviewer ]

```sh
echo ""
echo "POST [ reviewer ]"
echo ""
curl -X POST "http://localhost:5000/api/Ratings" \
  -H "Content-Type: application/json" \
  -d '{"movId":929,"revId":9021,"revStars":4.8,"numOfRatings":120}'
echo ""

echo ""
echo "GET [ rating ]"
echo ""
curl -X GET "http://localhost:5000/api/Ratings?movId=929&revId=9021" -H "Accept: application/json"
echo ""
```

#### CURL TEST 5 - SLAVE TABLE - DIRECTOR

```sh
echo ""
echo "POST [ director ]"
echo ""
curl -X POST "http://localhost:5000/api/Directors" \
  -H "Content-Type: application/json" \
  -d '{"dirFname":"dirFname_929", "dirLname":"dirLname_929"}'
echo ""

echo ""
echo "PATCH [ director ]"
echo ""
curl -X PATCH "http://localhost:5000/api/Directors/224" \
  -H "Content-Type: application/json" \
  -d '{"dirFname":"dirFname_PATCHED_929"}'
echo ""

echo ""
echo "GET [ director ]"
echo ""
curl -X GET "http://localhost:5000/api/Directors/224" \
  -H "Accept: application/json"
echo ""
```

#### CURL TEST 5 - RELATIONS - [ movie ] >> [ movie_direction ] << [ director ]

```sh
echo ""
echo "POST [ movie_genres ]"
echo ""
curl -X POST "http://localhost:5000/api/MovieDirection" \
  -H "Content-Type: application/json" \
  -d '{"dirId":224,"movId":929}'
echo ""

echo ""
echo "GET [ movie_directions: movie-director ]"
echo ""
curl -X GET "http://localhost:5000/api/MovieDirection?dirId=224&movId=929" -H "Accept: application/json"
echo ""
```

## Curl Tests Options
___
#### Execute 'cur_test_scripts.sh'

```sh
chmod +x curl_tests_script_options.sh
sh ./curl_tests_script_options.sh
```