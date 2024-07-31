# SimpleFlag

[![.NET](https://github.com/gorums/SimpleFlag/actions/workflows/dotnet.yml/badge.svg)](https://github.com/gorums/SimpleFlag/actions/workflows/dotnet.yml)
[![Nuget downloads](https://img.shields.io/nuget/v/SimpleFlag.svg)](https://www.nuget.org/packages/SimpleFlag/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/gorums/SimpleFlag/blob/master/LICENSE)

**SimpleFlag is a simple feature flag package for .Net 8 that can be embedded in your application and provide an easy way to manage and evaluate Flags.**

## Installation

```
Install-Package SimpleFlag
```

## Usage

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
        pgOptions.Domain = "demo"; // this is not required, this is only to group feature flag on domains if this project is going to have a static domain
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

### Expose endpoints on AspNet Core 

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

### Evaluate Flag

You can inject ***ISimpleFlagClient*** interface and access different methods to evaluate a flag.

```csharp

using SimpleFlag.Core;
using SimpleFlag.Core.Models;

public class MyService : IMyService
{
    private readonly ISimpleFlagClient _simpleFlagClient;

    public MyService(ISimpleFlagClient simpleFlagClient)
    {
        _simpleFlagClient = simpleFlagClient;
    }

    /// Evaluate the flag "my-service", if the flag does not exist return false as default value
    public async Task<bool> IsOpenAsync(CancellationToken cancellationToken = default) =>         
        await _simpleFlagClient.GetValueAsync("my-service", false, cancellationToken: cancellationToken);

    /// Evaluate the flag "my-service" for that user, if the flag does not exist or the user is not part of the segment of that flag return false as default value
    public async Task<bool> IsOpenByUserAsync(FeatureFlagUser user, CancellationToken cancellationToken = default) =>         
        await _simpleFlagClient.GetValueAsync("my-service", false, user, cancellationToken: cancellationToken);
    ...

}
```

## Supported Provider

- PostgreSQL (SimpleFlag.PostgreSQL)

## Example 

[TODO](https://github.com/gorums/SimpleFlag/tree/master/samples/DemoApi.AppHost)


## Copyright

Copyright (c) Alejandro Ferrandiz. See [LICENSE](https://raw.githubusercontent.com/gorums/SimpleFlag/master/LICENSE.txt) for details.
