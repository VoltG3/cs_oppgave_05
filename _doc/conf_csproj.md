###### project config: `cs_oppgave_05.csproj`
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

###### xunit config: `cs_oppgave_05.UnitTests.csproj`
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