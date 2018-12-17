FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 5721
EXPOSE 44375

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY Task/Task.csproj Task/
RUN dotnet restore Task/Task.csproj
COPY . .
WORKDIR /src/Task
RUN dotnet build Task.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Task.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Task.dll"]
