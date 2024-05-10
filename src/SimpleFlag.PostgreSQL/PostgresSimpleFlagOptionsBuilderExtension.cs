using SimpleFlag.Core;

namespace SimpleFlag.PostgreSQL;

/// <summary>
/// This class contains the extension methods for the SimpleFlagOptionsBuilder.
/// </summary>
public static class PostgresSimpleFlagOptionsBuilderExtension
{
    /// <summary>
    /// Add the PostgreSQL options.
    /// </summary>
    /// <param name="simpleFlagOptionsBuilder"><see cref="SimpleFlagOptionsBuilder"/></param>
    /// <param name="connectionString">The connection string</param>
    public static void UsePostgreSQL(this SimpleFlagOptionsBuilder simpleFlagOptionsBuilder, string? connectionString) =>
        simpleFlagOptionsBuilder.UsePostgreSQL(options => options.ConnectionString = connectionString);

    /// <summary>
    /// Add the PostgreSQL options.
    /// </summary>
    /// <param name="simpleFlagOptionsBuilder"><see cref="SimpleFlagOptionsBuilder"/></param>
    /// <param name="postgresSimpleFlagOptionsBuilder">Postgres options builder</param>
    public static void UsePostgreSQL(this SimpleFlagOptionsBuilder simpleFlagOptionsBuilder, Action<PostgresSimpleFlagOptionsBuilder>? postgresSimpleFlagOptionsBuilder) =>
        postgresSimpleFlagOptionsBuilder?.Invoke(new PostgresSimpleFlagOptionsBuilder(simpleFlagOptionsBuilder));
}
