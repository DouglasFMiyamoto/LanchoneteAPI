FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["LanchoneteApi/LanchoneteApi.csproj", "LanchoneteApi/"]

RUN dotnet restore "LanchoneteApi/LanchoneteApi.csproj"

COPY . .

WORKDIR "/src/LanchoneteApi"
RUN dotnet build "LanchoneteApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LanchoneteApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LanchoneteApi.dll"]
