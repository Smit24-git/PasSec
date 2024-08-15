FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG CONNECTION="Server=host.docker.internal,3306;uid=sa;Password=notused;Database=passec"
ARG SECRET="AddScretKeyHere"
ARG CONFIGURATION='Test'

RUN dotnet tool install --global dotnet-ef

ENV PATH="${PATH}:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT=${CONFIGURATION}
ENV SECRET=${SECRET}

#restore nuget packages
WORKDIR /source

COPY DatabaseMigration/*.csproj         ./DatabaseMigration/
COPY PasSecWebApi/*.csproj              ./PasSecWebApi/
COPY PasSecWebApi.Application/*.csproj  ./PasSecWebApi.Application/
COPY PasSecWebApi.Persistence/*.csproj  ./PasSecWebApi.Persistence/
COPY PasSecWebApi.Repositories/*.csproj ./PasSecWebApi.Repositories/
COPY PasSecWebApi.Shared/*.csproj       ./PasSecWebApi.Shared/

RUN dotnet restore PasSecWebApi/PasSecWebApi.csproj

# build projects
COPY DatabaseMigration/         ./DatabaseMigration/
COPY PasSecWebApi/              ./PasSecWebApi/
COPY PasSecWebApi.Application/  ./PasSecWebApi.Application/
COPY PasSecWebApi.Persistence/  ./PasSecWebApi.Persistence/
COPY PasSecWebApi.Repositories/ ./PasSecWebApi.Repositories/
COPY PasSecWebApi.Shared/       ./PasSecWebApi.Shared/

RUN dotnet build PasSecWebApi/PasSecWebApi.csproj -c ${CONFIGURATION}

ENV PAS_SEC_ADMIN_CONNECTION_STRING=${CONNECTION}
ENV ConnectionStrings__mysqlDatabase=${CONNECTION}

RUN dotnet publish PasSecWebApi/PasSecWebApi.csproj -c ${CONFIGURATION} -o /publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 as run
ENV ASPNETCORE_ENVIRONMENT='Production'

ARG CONNECTION="Server=host.docker.internal,3306;uid=sa;Password=notused;Database=passec"

WORKDIR /app

copy --from=build /publish .

ENV PAS_SEC_ADMIN_CONNECTION_STRING=${CONNECTION}
ENV ConnectionStrings__mysqlDatabase=${CONNECTION}
ENV "ApiSettings:Secret"=${SECRET}

ENTRYPOINT ["dotnet", "PasSecWebApi.dll"]

# CREATE CONTAINER
# Docker build command
# docker build --build-arg CONNECTION="<provide connection string>" --progress=plain -f Migrations/MigrationBundle.Dockerfile .

# EXECUTE MIGRATION
# docker run --rm --entrypoint /bin/bash migration-bundle:1.0
    # sh run_migration.sh
# OR
# docker run --rm migration-bundle:1.0   