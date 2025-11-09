```bash
dotnet ef migrations add InitialCreate --startup-project src/Acme.WebAPI --project src/Acme.WebAPI --output-dir Migrations
```

### Command Breakdown:

dotnet ef migrations add InitialCreate: This is the command to scaffold a new migration named "InitialCreate".

`--startup-project src/Acme.WebAPI`: Specifies the project to use for startup configuration (like reading the connection string). This must be your Acme.WebAPI project.

`--project src/Acm.WebAPI`: This flag (or --project src/Acme.WebAPI) tells EF Core where to find the DbContext and where to put the generated migration files. Since we configured MigrationsAssembly("Acme.WebAPI") in Program.cs, we use the Acme.WebAPI project.

`--output-dir Migrations`: This explicitly tells the tool to place the migration files in a new directory named Migrations inside the Acme.WebAPI project.

If successful, you will see a new Migrations folder in src/Acme.WebAPI containing two files:

**[Timestamp]**_InitialCreate.cs (The migration)

`AcmeDbContextModelSnapshot.cs` (The current state of your model)

---

```bash
dotnet ef database update --startup-project src/Acme.WebAPI
```