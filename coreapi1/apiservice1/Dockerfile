FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["apiservice1/apiservice1.csproj", "apiservice1/"]
RUN dotnet restore "apiservice1/apiservice1.csproj"
COPY . .
WORKDIR "/src/apiservice1"
RUN dotnet build "apiservice1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "apiservice1.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "apiservice1.dll"]