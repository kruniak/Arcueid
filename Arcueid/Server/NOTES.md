# Notes / TODO

## requirements
[...]

## migration workflow
here's how to setup a local azure sql database (using docker for windows):
    - https://docs.microsoft.com/en-us/azure/azure-sql/database/local-dev-experience-set-up-dev-environment?view=azuresql&tabs=ads

build then publish the project to deploy an azure sql database docker container locally (it usually takes a while, don't panic)

the most basic connection string to an azure sql database is like "Server=localhost; Database=Arcueid; User Id=sa; Password=password".
make sure to set it in appsettings.(Development.)json before doing the following.

first off, make sure dotnet-ef tools are up to date by running ```dotnet tool update --global dotnet-ef```
then update db with ```dotnet ef database update```

migrations:
[...]

