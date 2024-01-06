FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./

RUN dotnet restore
RUN dotnet build -c Release
RUN dotnet publish -c Release -o out --no-build

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80;

ENTRYPOINT [ "dotnet", "RichillCapital.Identity.Api.dll" ]