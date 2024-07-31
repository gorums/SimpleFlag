# SimpleFlag.PostgreSQL

This package manages flags using a PostgreSQL database.

## Description

SimpleFlag.PostgreSQL is a .NET package that extends the SimpleFlag service to use PostgreSQL as the backend for flag management. 
It utilizes FluentMigrator for database migrations and Npgsql for PostgreSQL connectivity.

## Installation

To install SimpleFlag.PostgreSQL, add the following package references to your project file:

```csharp
Install-Package SimpleFlag.PostgreSQL
```

## Dependencies

- `FluentMigrator.Runner.Postgres` (Version 5.2.0)
- `Npgsql` (Version 8.0.3)
- `SimpleFlag` (Project Reference)

## Contributing

Contributions are welcome! Please open an issue or submit a pull request on GitHub.

## License

This project is licensed under the MIT License.