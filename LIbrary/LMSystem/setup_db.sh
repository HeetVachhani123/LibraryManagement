#!/bin/bash

echo "Running init.sql against local SQL Server..."

# We use sqlcmd inside the container since the tools might not be installed on the host
docker exec -i sql-server /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P "YourStrong@Passw0rd" -C < init.sql

echo "Database LMS successfully created and seeded!"
