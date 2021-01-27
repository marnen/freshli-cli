FROM mcr.microsoft.com/dotnet/sdk:5.0-focal as build-env
WORKDIR /app

COPY . ./
RUN dotnet publish Freshli.Cli -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Freshli.Cli.dll"]