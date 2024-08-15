FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG CONNECTION="Server=host.docker.internal,3306;uid=sa;Password=notused;Database=passec"
ARG CONFIGURATION='Test'

RUN dotnet tool install --global dotnet-ef

ENV PATH="${PATH}:/root/.dotnet/tools"
ENV ASPNETCORE_ENVIRONMENT=${CONFIGURATION}

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

# build the migration bundle
ENV PAS_SEC_ADMIN_CONNECTION_STRING=${CONNECTION}
ENV ConnectionStrings__mysqlDatabase=${CONNECTION}
RUN dotnet ef migrations bundle -p ./DatabaseMigration -s ./PasSecWebApi --configuration ${CONFIGURATION} --self-contained --verbose



FROM mcr.microsoft.com/dotnet/runtime:8.0 as run
ARG CONNECTION="Server=host.docker.internal,3306;uid=sa;Password=notused;Database=passec"

WORKDIR /home

COPY --from=build /source/efbundle .

COPY PasSecWebApi/appsettings.json .
COPY Migrations/run_migration.sh .

ENV PAS_SEC_ADMIN_CONNECTION_STRING=${CONNECTION}
ENV ConnectionStrings__mysqlDatabase=${CONNECTION}

ENTRYPOINT ["./run_migration.sh"]