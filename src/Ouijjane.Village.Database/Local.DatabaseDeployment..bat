@echo off

echo *** Setting grate params ***
SET environment=Local
SET connectionString="Server=localhost;Port=5433;Username=postgres;Password=postgres;Database=ouijjane-local-village;"
SET sql.files.directory=.\db\

echo *** Running grate ***
grate --files %sql.files.directory% --connectionstring %connectionString% --environment %environment% --databasetype postgresql --commandtimeout 3600 --warnandignoreononetimescriptchanges --silent

Pause

Rem database name will be replaced with ouijjane-local-village-local when implementing multi-tenancy
Rem –databasetype : mariadb | oracle | postgresql | sqlite | sqlserver