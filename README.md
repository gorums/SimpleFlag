# SimpleFlag

[![.NET](https://github.com/gorums/SimpleFlag/actions/workflows/dotnet.yml/badge.svg)](https://github.com/gorums/SimpleFlag/actions/workflows/dotnet.yml)
[![Nuget downloads](https://img.shields.io/nuget/v/SimpleFlag.svg)](https://www.nuget.org/packages/SimpleFlag/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/gorums/SimpleFlag/blob/master/LICENSE)

**SimpleFlag is a simple feature flag package for .Net 8 that can be embedded in your application and provide an easy way to create/edit/delete and evaluate Flags.**

## Installation

```
Install-Package SimpleFlag
```

## Setup

Using Postgres

```csharp
Install-Package SimpleFlag.PostgreSQL
```


**Program.cs**

With ConnectionString and Default Options

```csharp
var builder = WebApplication.CreateBuilder(args);

...

builder.Services.AddSimpleFlag(options =>
{
    options.UsePostgreSQL(builder.Configuration.GetConnectionString("ConnectionString"));     
});

...

```

With ConnectionString and Custom Options

```csharp
var builder = WebApplication.CreateBuilder(args);

...

builder.Services.AddSimpleFlag(options =>
{
    options.UsePostgreSQL(pgOptions =>
    {
        pgOptions.SchemaName = "flag";
        pgOptions.TablePrefix = "sf";
        pgOptions.ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
    });     
});

...

```

Using Custom DataSource with Custom Options

```csharp
var builder = WebApplication.CreateBuilder(args);

...

builder.Services.AddSimpleFlag(options =>
{
   options.UseCustom(customOptions =>
    {
        customOptions.SchemaName = "flag";
        customOptions.TablePrefix = "sf";
        customOptions.ConnectionString = builder.Configuration.GetConnectionString("ConnectionString");
        customOptions.DataSourceMigration = MyDataSourceDatabaseMigration.Instance;
        customOptions.DataSourceRepository = MyDataSourceRepository.Instance;
    });
});

...

```

In this case you need to implement the interface ***IDataSourceMigration*** and ***IDataSourceRepository***

## Setup AspNet Core

 Using SimpleFlag Extension package to provide endpoints for management flags [***!Working in progress***]

```
Install-Package SimpleFlag.AspNetCore
```


**Program.cs**

```csharp
var builder = WebApplication.CreateBuilder(args);

...

builder.Services.AddSimpleFlag(options =>
{
   ...  
});

...

var app = builder.Build();

...

app.MapSimpleFlagEndpoints();

...

```

## Evaluate Flag

You can inject ***ISimpleFlagService*** interface and access different methods to evaluate a flag.

```csharp
public class MyService : IMyService
{
    private readonly ISimpleFlagService _simpleFlagService;

    public MyService(ISimpleFlagService simpleFlagService)
    {
        _simpleFlagService = simpleFlagService;
    }

    public async Task<bool> IsOpenAsync(CancellationToken cancellationToken = default) =>
         // Evaluate the flag "my-service"
        await _simpleFlagService.EvaluateAsync("my-service", cancellationToken);
    ...

}
```

## Supported Provider

- PostgreSQL (SimpleFlag.PostgreSQL)

## Copyright

Copyright (c) Alejandro Ferrandiz. See [LICENSE](https://raw.githubusercontent.com/gorums/SimpleFlag/master/LICENSE.txt) for details.
