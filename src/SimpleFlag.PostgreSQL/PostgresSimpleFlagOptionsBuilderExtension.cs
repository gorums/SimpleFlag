using SimpleFlag.Core;

namespace SimpleFlag.PostgreSQL;

public static class PostgresSimpleFlagOptionsBuilderExtension
{
    public static void UsePostgreSQL(this SimpleFlagOptionsBuilder simpleFlagOptionsBuilder, string? connectionString) =>
        simpleFlagOptionsBuilder.UsePostgreSQL(options => options.ConnectionString = connectionString);

    public static void UsePostgreSQL(this SimpleFlagOptionsBuilder simpleFlagOptionsBuilder, Action<PostgresSimpleFlagOptionsBuilder>? postgresSimpleFlagOptionsBuilder) =>
        postgresSimpleFlagOptionsBuilder?.Invoke(new PostgresSimpleFlagOptionsBuilder(simpleFlagOptionsBuilder));
}
