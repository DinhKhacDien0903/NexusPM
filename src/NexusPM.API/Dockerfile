# ===== BUILD =====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY src/NexusPM.API/NexusPM.API.csproj ./src/NexusPM.API/
COPY src/NexusPM.Application/NexusPM.Application.csproj ./src/NexusPM.Application/
COPY src/NexusPM.Infrastructure/NexusPM.Infrastructure.csproj ./src/NexusPM.Infrastructure/
COPY src/NexusPM.Domain/NexusPM.Domain.csproj ./src/NexusPM.Domain/

RUN dotnet restore src/NexusPM.API/NexusPM.API.csproj

COPY . .

# Publish
WORKDIR /src/src/NexusPM.API
RUN dotnet publish NexusPM.API.csproj -c Release -o /app/publish /p:UseAppHost=false

# ===== RUNTIME =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .

RUN useradd -m appuser
USER appuser

ENTRYPOINT ["dotnet", "NexusPM.API.dll"]