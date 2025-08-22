### `C# Intermediate Oppgave 4` and `SQL Oppgave 4`
###### C# / FC CORE / NET 8/ CRUD / MYSQL / SQL MIGRATION / API / WEB SERVER / RestAPI / CURL TESTS / XUNIT TEST / COMPOSE
<p style="color: red;">INTRO TODO</p>

## IaC
<p style="color: red;">TODO</p>

## Nginx
<p style="color: red;">TODO</p>

## Compose DB & API
<p style="color: red;">INTRO TODO</p>
> `.env.dbase` and `appsettings.json` excluded from .gitignore, so only the port needs to be overwritten

How to run it:

___FIRST___
#### Check is port-passable:
```sh
ss -H -ltn 'sport = :3309' | grep -q . && echo busy || echo passable
```
- Verify port in the `.env.dbase`
- Verify port in the `appsettings.json`

#### Compose db
```sh
docker compose --env-file .env.dbase up -d db
docker compose --env-file .env.dbase ps
```

___SECOND___
```sh
Run IDE
```

___THIRD___
#### Compose api
```sh
docker compose --env-file .env.dbase up -d --build db api
docker compose --env-file .env.dbase ps
```
The API will listening at `http://localhost:8080/`
#### Quick Test:
```sh
curl -sS "http://localhost:8080/api/Movies?page=1&pageSize=50" -H "Accept: application/json" | jq .
```

[Diagnostic minikit](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/diag_mkit.md)

#### Stop and remove containers
```sh 
docker stop cs_oppgave_05-db-1 cs_oppgave_05-api-1
docker rm cs_oppgave_05-db-1 cs_oppgave_05-api-1
```

Curl requests: for colored and better json output `sudo snap install jq`
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/04.png)


## xUnit tests
<p style="color: red;">INTRO TODO</p>

#### Run xUnit tests
```sh
dtest
```

[Installation Remainder](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/rem_xunit.md)

#### If EF Core version crash, reinstall correct version
```sh
dotnet remove package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.13
```
###### xUnit tests
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_xunit.png)

## CRUD 
<p style="color: red;">INTRO TODO</p>
<p style="color: red;">IMG TODO</p>

#### Check OnLive condition
```sh
http://localhost:5000   
curl http://localhost:5000 
```

#### Execute 'curl_tests_script_relations.sh'
```sh
chmod +x curl_tests_script_relations.sh
sh ./curl_tests_script_relations.sh
```
<p style="color: red;">IMG TODO</p>

#### Or do it manually for each table
[Guide for curl request](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/curl_tests.md)
<p style="color: red;">IMG TODO</p>

#### Then check new record
```sh
http://localhost:5000/api/Movies/929/details
curl -X GET "http://localhost:5000/api/Movies/929/details" -H "Accept: application/json"
````
<p style="color: red;">IMG TODO</p>
<p style="color: red;">IMG TODO</p>

## WEB Server
<p style="color: red;">INTRO TODO</p>
<p style="color: red;">IMG TODO</p>

## SQL Migration
<p style="color: red;">INTRO TODO</p>

#### Initialize Docker container
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

#### DBeaver, if access issue
```sh
if classic error: Public Key Retrieval is not allowed

ALTER USER 'all'@'%' IDENTIFIED WITH mysql_native_password BY 'mysql';
FLUSH PRIVILEGES;
```

###### Relations Diagram
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/01.png)

###### SQL Migration
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_diagram.png)
