FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY GitWrap.Api/GitWrap.Api.csproj ./GitWrap.Api/
COPY GitWrap.Application/GitWrap.Application.csproj ./GitWrap.Application/
COPY GitWrap.Domain/GitWrap.Domain.csproj ./GitWrap.Domain/
COPY GitWrap.Infrastructure/GitWrap.Infrastructure.csproj ./GitWrap.Infrastructure/

RUN dotnet restore ./GitWrap.Api/GitWrap.Api.csproj
COPY . .
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "GitWrap.Api.dll"]
