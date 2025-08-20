# build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish *.csproj -c Release -o /out

# run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=build /out .
EXPOSE 8080
ENTRYPOINT ["dotnet","cs_oppgave_05.dll"] 
