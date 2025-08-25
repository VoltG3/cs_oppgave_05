### `C# Intermediate Oppgave 4` and `SQL Oppgave 4`
###### C# / FC CORE / NET 8/ CRUD / MYSQL / SQL MIGRATION / API / WEB SERVER / RestAPI / CURL TESTS / XUNIT TEST / COMPOSE

| Tag              | Description                                                              |
|:-----------------|:-------------------------------------------------------------------------|
| cs_oppgave_05_V1 | [Database schema & SQL scripts (migrations, seed)](#sql-migration)       |
| cs_oppgave_05_V2 | [Minimal web host & health check page](#web-server)                      |
| cs_oppgave_05_V3 | [REST API CRUD for Movies + relations and curl tests](#crud)             |
|                  | [Automated tests with xUnit (unit/integration)](#xunit-tests)            |
| cs_oppgave_05_V4 | [Docker Compose setup for MySQL + API](#compose-db-&-api)                |

## IaC
Todo

## Nginx
Todo

## Nix
InProgress

## Compose DB & API
**Goal:** Spin up the MySQL database and the API with Docker Compose (using `.env.dbase`)

> **Ports & config**
> - `.env.dbase` and `appsettings.json` are committed, so you usually only need to adjust ports if they conflict.
> - Make sure the DB/API ports match between `.env.dbase` and `appsettings.json` (connection string).

#### How to run it
**Option A — Compose DB & API together (recommended):**
```sh
docker compose --env-file .env.dbase up -d --build db api
```
**Option B — DB via Compose, API from IDE:**
```sh
docker compose --env-file .env.dbase up -d db
```
```sh
docker compose --env-file .env.dbase up -d --build db api
```

#### Quick health check
- Open in browser: `curl http://localhost:8080/`
- Or with curl:
```sh
curl -i http://localhost:8080/
```

#### Quick GET Test:
- Open in browser: `http://localhost:8080/api/Movies?page=1&pageSize=50`
- Or with curl:
```sh
curl -sS "http://localhost:8080/api/Movies?page=1&pageSize=50" -H "Accept: application/json" | jq .
```

#### Check if the DB port is free (example: 3309)
```sh
ss -H -ltn 'sport = :3309' | grep -q . && echo busy || echo passable
```

[Diagnostic minikit](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/diag_mkit.md)

#### Stop & remove containers
```sh 
docker stop cs_oppgave_05-db-1 cs_oppgave_05-api-1
docker rm cs_oppgave_05-db-1 cs_oppgave_05-api-1
```

For colored/pretty JSON, install jq:
```sh
sudo snap install jq   # or: sudo apt-get install jq
```
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_jq.png)

## xUnit tests
**Goal:** Verify domain logic, EF Core mappings, and API controllers end-to-end (CRUD, validation, error handling) with fast, repeatable xUnit unit/integration tests to prevent regressions.

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
##### xUnit tests
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_xunit.png)

## CRUD
**Goal:** Implement entities (models) and controllers so full CRUD works for each resource.

#### Quick health check
- Open in browser: `http://localhost:5000`
- Or with curl:
```sh
curl -i http://localhost:5000/
```

#### Run the bundled curl tests
```sh
chmod +x curl_tests_script_relations.sh
sh ./curl_tests_script_relations.sh
```

#### cURL test sequence (creates Movie record 929 and wires up relations)
| Type     | Table          | Relations with    | POST | PATCH | GET | 
|:---------|:---------------|:------------------|:-----|:------|:----|
| Master   | Movies         |                   | X    | X     | X   |
| Child    | Genres         |                   | X    | X     | X   |
| Relation | MovieGenres    | Movie & Genres    | X    |       | X   |
| Child    | Actors         |                   | X    | X     | X   |
| Relation | MovieCast      | Movie & Actor     | X    |       | X   |
| Child    | Reviewers      |                   | X    | X     | X   |
| Relation | Ratings        | Movie & Reviewers | X    |       | X   |
| Child    | Directors      |                   | X    | X     | X   |
| Relation | MovieDirection | Movie & Directors | X    | X     | X   |
| Master   | Movie          | All Relations     |      | X     | X   |

#### Final verification (Movie 929 was patched)
- Open in browser: `http://localhost:5000/api/Movies/929/details`
- Or with curl:
```sh
curl -X GET "http://localhost:5000/api/Movies/929/details" -H "Accept: application/json"
```

#### Request Reference
All test-specific `curl` requests are listed here: [cURL Request Guide](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/curl_tests.md)

#### Relations Diagram
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_migration.png)

## Web Server
**Goal:** Run the app as a web server and confirm it’s alive at `http://localhost:5000`.
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_web.png)

## SQL Migration
**Goal:** Manually create a MySQL database container and apply SQL scripts.
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_diagram.png)

### Start the MySQL container
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

#### If access issue: “Public Key Retrieval is not allowed”
```sh
ALTER USER 'all'@'%' IDENTIFIED WITH mysql_native_password BY 'mysql';
FLUSH PRIVILEGES;
```

###### SQL Migration
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/_doc/a_diagram.png)
