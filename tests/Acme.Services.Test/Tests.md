```bash
dotnet add tests/Acme.Services.Test/Acme.Services.Test.csproj package Moq
```

```bash
dotnet add tests/Acme.Services.Test/Acme.Services.Test.csproj package Microsoft.EntityFrameworkCore.InMemory
```
---
Note: We use Microsoft.EntityFrameworkCore.InMemory not to mock the DbContext itself (Moq handles that), 
but to provide a functional, in-memory DbSet implementation that Moq can use to simulate database behavior, 
especially for methods like Find and basic queries.

---


