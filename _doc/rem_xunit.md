###### simple touch
```sh
dotnet new xunit -f net8.0 -n cs_oppgave_05.UnitTests -o tests/cs_oppgave_05.UnitTests
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.NET.Test.Sdk
dotnet add tests/cs_oppgave_05.UnitTests reference cs_oppgave_05.csproj
dotnet test
````

###### update sln
```sh
dotnet sln add tests/cs_oppgave_05.UnitTests/cs_oppgave_05.UnitTests.csproj
dotnet sln list
````

###### forward
```sh
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.Data.Sqlite --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.Relational --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.InMemory --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.EntityFrameworkCore.Sqlite --version 8.0.13
dotnet add tests/cs_oppgave_05.UnitTests package Microsoft.Data.Sqlite --version 8.0.13

````

###### better output
```sh
echo 'alias dtest="dotnet test --settings tests/RunSettings.runsettings"' >> ~/.bashrc
source ~/.bashrc
````

###### refresh it
```sh
dotnet clean
dotnet restore
dotnet build
dotnet test
````

###### then run
```sh
dtest
````