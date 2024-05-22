FROM mcr.microsft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AcmeCorp.Api/AcmeCorp.Api.csproj", "AcmeCorp.Api/"]
COPY ["AcmeCorp.Application/AcmeCorp.Application.csproj", "AcmeCorp.Application/"]
COPY ["AcmeCorp.Domain/AcmeCorp.Domain.csproj", "AcmeCorp.Domain/"]
COPY ["AcmeCorp.Infrastructure/AcmeCorp.Infrastructure.csproj", "AcmeCorp.Infrastructure/"]
RUN dotnet restore "AcmeCorp.Api/AcmeCorp.Api.csproj"
COPY . .
WORKDIR "/src/AcmeCorp.Api"
RUN dotnet build "AcmeCorp.Api.csproj" -c Release -o /app/build

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcmeCorp.Api.dll"]