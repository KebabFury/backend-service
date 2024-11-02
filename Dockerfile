FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY *.sln .
COPY KebabFury.Innopolice.WebApi/*.csproj ./KebabFury.Innopolice.WebApi/
COPY KebabFury.Innopolice.Docs/*.csproj ./KebabFury.Innopolice.Docs/
COPY KebabFury.Innopolice.Todoist/*.csproj ./KebabFury.Innopolice.Todoist/

RUN dotnet restore

COPY . .

WORKDIR /src/KebabFury.Innopolice.WebApi/
RUN dotnet build "KebabFury.Innopolice.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KebabFury.Innopolice.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KebabFury.Innopolice.WebApi.dll"]
