### `C# Intermediate Oppgave 4` and `SQL Oppgave 4`
###### C# / FC CORE / NET 8/ CRUD / MYSQL / SQL MIGRATION / API / WEB SERVER / RestAPI / CURL TESTS / XUNIT TEST

###### Check OnLive condition
```sh
http://localhost:5000   

curl http://localhost:5000 
```
###### Execute 'curl_tests_script_relations.sh'

```sh
chmod +x curl_tests_script_relations.sh

sh ./curl_tests_script_relations.sh
```

###### Then check new record
```sh
http://localhost:5000/api/Movies/929/details

curl -X GET "http://localhost:5000/api/Movies/929/details" -H "Accept: application/json"
````

###### Run xUnit tests
```sh
dtest
```

### SQL Migration
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/03.png)

### xUnit test
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/02.png)

#### Relations Diagram
![img](https://github.com/VoltG3/cs_oppgave_05/blob/master/01.png)

#### If EF Core version crash
```sh
dotnet remove package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore --version 8.0.13
```

### Dependencies

```sh
dotnet add package MySql.Data
dotnet add package Spectre.Console
dotnet add package Spectre.Console.Cli
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package MySqlConnector
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package MySqlConnector
```

### Dependencies xunit test
```sh
# simple touch
dotnet new xunit -f net8.0 -n cs_oppgave_05.UnitTests -o tests/cs_oppgave_05.UnitTests
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.NET.Test.Sdk
dotnet add tests/cs_oppgave_05.UnitTests reference cs_oppgave_05.csproj
dotnet test

# update sln
dotnet sln add tests/cs_oppgave_05.UnitTests/cs_oppgave_05.UnitTests.csproj

# test it 
dotnet sln list

# forward
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.Data.Sqlite --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.Relational --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.InMemory --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.Data.Sqlite --version 8.0.13

# better output
echo 'alias dtest="dotnet test --settings tests/RunSettings.runsettings"' >> ~/.bashrc
source ~/.bashrc

# refresh it
dotnet clean
dotnet restore
dotnet build
dotnet test

# then run
dtest
````

### Initialize Docker MYSQL container
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

### DBeaver, if access issue
```sh
if classic error: Public Key Retrieval is not allowed

ALTER USER 'all'@'%' IDENTIFIED WITH mysql_native_password BY 'mysql';
FLUSH PRIVILEGES;
```

### project encyclopedia `cs_oppgave_05.csproj`
```sh
<Project Sdk="Microsoft.NET.Sdk.Web">

    <PropertyGroup>
        <OutputType>Exe</OutputType>
        <TargetFramework>net8.0</TargetFramework>
        <ImplicitUsings>enable</ImplicitUsings>
        <Nullable>enable</Nullable>
    </PropertyGroup>

    <ItemGroup>
      <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.13" />
      <PackageReference Include="MySql.Data" Version="9.4.0" />
      <PackageReference Include="MySqlConnector" Version="2.4.0" />
      <PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.3" />
      <PackageReference Include="Spectre.Console" Version="0.50.0" />
      <PackageReference Include="Spectre.Console.Cli" Version="0.50.0" />
      <Content Include="Infrastructure\Presistance\Migrations\SqlScripts\**\*.sql">
    	<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
      </Content>
      <Content Include="Assets/**">
    	<CopyToOutputDirectory>Always</CopyToOutputDirectory>
      </Content>
      <Compile Remove="tests/**" />
      <None Remove="tests/**" />
      <Content Remove="tests/**" />
      <EmbeddedResource Remove="tests/**" />
    </ItemGroup>

    <ItemGroup>
      <Folder Include="Domain\" />
    </ItemGroup>

</Project>
```

### xunit test encyclopedia `cs_oppgave_05.UnitTests.csproj`
```sh
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>

    <IsPackable>false</IsPackable>
    <IsTestProject>true</IsTestProject>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.Data.Sqlite" Version="8.0.13" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.13" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="8.0.13" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="8.0.13" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="8.0.13" />
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.14.1" />
    <PackageReference Include="xunit" Version="2.4.2" />
    <PackageReference Include="xunit.runner.visualstudio" Version="2.4.5">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
    <PackageReference Include="coverlet.collector" Version="6.0.0">
      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
      <PrivateAssets>all</PrivateAssets>
    </PackageReference>
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\..\cs_oppgave_05.csproj" />
  </ItemGroup>

</Project>
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

#### CURL TEST 2 - RELATIONS - [ movie ] ➡️ [ movie_genres ] ⬅️ [ genres]

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

#### CURL TEST 3 - RELATIONS - [ movie ] ➡️ [ movie_cast ] ⬅️ [ actor ]

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

#### CURL TEST 4 - RELATIONS - [ movie ] ➡️ [ rating ] ⬅️ [ reviewer ]

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

#### CURL TEST 5 - RELATIONS - [ movie ] ➡️ [ movie_direction ] ⬅️ [ director ]

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

#### CURL TEST 6 - RELATIONS - [ movie ] ⬅️ [ movie_direction ] ⬅️ [ movie_genres ] ⬅️ [ movie_cast ] ⬅️ [ rating ]
```sh
curl -X PATCH "http://localhost:5000/api/movies/929/details" \
  -H "Content-Type: application/json" \
  -d '{
    "movTitle": "_ovie_PACHED_929",
    "movRelCountry": "NO",
    "genres": [
      { "genId": 1014, "genTitle": "_enre_PACHED_929" }
    ],
    "directors": [
      { "dirId": 224, "dirFname": "_irFname_PATCHED_929", "dirLname": "_irLname_929" }
    ],
    "cast": [
      { "actId": 125, "role": "_ole_929", "actFname": "_ctorFname_PATCH_929", "actLname": "_ctorLname_929" }
    ],
    "ratings": [
      { "revId": 9021, "revStars": 4.8, "numOfRatings": 10 }
    ]
  }'
```
