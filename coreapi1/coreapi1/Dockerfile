FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["coreapi1/coreapi1.csproj", "coreapi1/"]
RUN dotnet restore "coreapi1/coreapi1.csproj"
COPY . .
WORKDIR "/src/coreapi1"
RUN dotnet build "coreapi1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "coreapi1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "coreapi1.dll"]