FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["PatientService.csproj", "."]
RUN dotnet restore "PatientService.csproj"
COPY . .
RUN dotnet publish "PatientService.csproj" -c Release -o ../app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PatientService.dll"]