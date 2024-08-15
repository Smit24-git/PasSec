#!/bin/bash
# dotnet ef migrations bundle -p /source/DatabaseMigration -s /source/PasSecWebApi --configuration Release --self-contained --verbose

./efbundle --connection $PAS_SEC_ADMIN_CONNECTION_STRING