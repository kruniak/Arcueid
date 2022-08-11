# Notes / TODO

## requirements
[...]

## migration workflow
here's how to setup a local Azure SQL database (using Docker for Windows):
    - https://docs.microsoft.com/en-us/azure/azure-sql/database/local-dev-experience-set-up-dev-environment?view=azuresql&tabs=ads

build then publish the project to deploy an Azure SQL database docker container locally (it usually takes a while, don't panic)

the most basic connection string to an Azure DB is like "Server=localhost; Database=Arcueid; User Id=sa; Password=password".
make sure to set it in appsettings.(Development.)json before doing the following.

first off, make sure dotnet-ef tools are up to date by running ```dotnet tool update --global dotnet-ef```
then update the DB with ```dotnet ef database update```

EF migrations:
docs: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli
[...]

