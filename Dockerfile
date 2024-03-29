FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

EXPOSE 80/tcp

WORKDIR /app
COPY /backend.person.api/*.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish ./backend.person.api/backend.person.api.csproj  -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "backend.person.api.dll"]
