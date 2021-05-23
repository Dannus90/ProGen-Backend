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
**OBS ALWAYS ADD MIGRATIONS IN AN ENVIRONMENT THAT IS NOT DEVELOPMENT!**
**IF YOU ADD ADDITIONAL ENVIRONMENTS TO SEED DON'T CREATE MIGRATIONS IN THEM EITHER.**
* `ASPNETCORE_ENVIRONMENT=Prod dotnet ef migrations add NewMigration`
* `dotnet ef database update`

It is really important to not add migrations in an environment that 
adds the seed data. Look in AppDbContext to see more. The environment that
you should **not** create migrations from is development. 
If you want to connect to the production database locally you have to add a
an appsettings file that has the connectionstring and other required environment data.
It should look exactly as the other files but with production data. 
The one I use locally I call appsettings.LocalProd.json.

If you just use the Prod file for seeding you can temporarily give it the connectionstring
of the development appsettings because it cannot be empty or the program won't start. 

### Seeding
In order to successfully run the command down below you have to make progen to a 
super user. See the link up above to make progen a superuser. 

* Local drop and reseed `dotnet ef database drop -f && dotnet run`

The database is reseeded automatically only in development. This is also dependent 
on the migrations file however, so if you have an incorrect migration that was created in
development the database will become seeded in all environments. 
Therefore always make sure that you do not create migrations in development and by doing so
add the seed data to the migrations file. 

### Reset database
`cd API`
`dotnet ef database drop -f`

### Production database
To drop the production database you have to do that through heroku commands.
This information will of course not be stated here. 

The production database is accessed through heroku. 

### Environment variables
Some of the environment variables that does not need to be secret are set directly in the appsettings files.¨
Other variables are set either in the production environment (HEROKU) or locally
with dotnet user secrets. In the startup class you can see what variables has to be set up locally
to make the backend fully functional.

A the moment the following have to be set:
**SendGrid:ApiKey**
**CloudinaryConfig:CloudName**
**CloudinaryConfig:ApiSecret**
**CloudinaryConfig:ApiKey**

**How to set**
`dotnet user-secrets init`
`dotnet user-secrets set "SendGrid:ApiKey" "THEKEY"`

Verify by listing:
`dotnet user-secrets list`

More information:
[User secrets](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=windows)

### Tests
The tests run while being pushed to gitlab by the config in gitlab-ci.yml.
To run the tests locally you have to go to the root and then run the command `dotnet test`

If you implement more tests and a test break you might have created stuff during the 
testing that is not removed from the database. You then have to drop the local database
and run dotnet run again in development. 

### API Documentation
[Postman API documentation](https://documenter.getpostman.com/view/10905719/TzXtKM79)