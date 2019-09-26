FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["Vueling.Api/Vueling.Api.csproj", "Vueling.Api/"]
RUN dotnet restore "Vueling.Api/Vueling.Api.csproj"
COPY . .
WORKDIR "/src/Vueling.Api"
RUN dotnet build "Vueling.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Vueling.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Vueling.Api.dll"]