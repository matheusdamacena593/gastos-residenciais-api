FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /repo

COPY . .

RUN dotnet restore "GastosResidenciais/src/GastosResidenciais.Api/GastosResidenciais.Api.csproj"
RUN dotnet publish "GastosResidenciais/src/GastosResidenciais.Api/GastosResidenciais.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GastosResidenciais.Api.dll"]