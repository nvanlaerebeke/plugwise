FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Plugwise/Plugwise.csproj", "Plugwise/"]
RUN dotnet restore "Plugwise/Plugwise.csproj"
COPY . .
WORKDIR "/src/Plugwise"
RUN dotnet build "Plugwise.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Plugwise.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plugwise.dll"]
