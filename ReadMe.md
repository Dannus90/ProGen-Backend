# PROGEN API

This is the backend of the ProGen application that handles requests
from ProGen frontend. 

## Setup and workflow

Down below you can see instructions regarding setup and workflow.

### Local Database setup
The development environment is the default environment and the database is set to
`ProGenLocal`, the password to `progen` and the username to `progen`. In order to be 
able to run the database locally you need to have postgres installed. To work
easier with the database a tool such as `PgAdmin` is recommended. The database will be
created upon application start if it does not exist. In order for it to be able to start
you will need to have a user configured with the username and password stated above.

**Instructions Mac**
- [How to setup](https://www.codementor.io/@engineerapart/getting-started-with-postgresql-on-mac-osx-are8jcopb)
- [Troubles connection?](https://stackoverflow.com/questions/7695962/postgresql-password-authentication-failed-for-user-postgres/7696398#7696398)
- [Make superuser](https://stackoverflow.com/questions/10757431/postgres-upgrade-a-user-to-be-a-superuser)
### Migrations
**OBS ALWAYS ADD MIGRATIONS IN PROD ENVIRONMENT!** 
* `ASPNETCORE_ENVIRONMENT=dotnet ef migrations add NewMigration`
* `dotnet ef database update`

### Seeding
In order to successfully run the command down below you have to make progen to a 
super user. See the link up above to make progen a superuser. 

* Local drop and reseed `dotnet ef database drop -f && dotnet run`

### Reset database
`dotnet ef database drop -f`

### API Documentation