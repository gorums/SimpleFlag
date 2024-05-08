using Npgsql;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;

namespace SimpleFlag.PostgreSQL;
internal class PostgresDataSourceRepository : ISimpleFlagDataSourceRepository
{
    private static readonly Lazy<ISimpleFlagDataSourceRepository> _dataSourceRepository = new Lazy<ISimpleFlagDataSourceRepository>(() => new PostgresDataSourceRepository());

    public SimpleFlagRepositoryOptions SimpleFlagRepositoryOptions { get; set; } = new SimpleFlagRepositoryOptions();

    private PostgresDataSourceRepository()
    {
    }

    public static ISimpleFlagDataSourceRepository Instance => _dataSourceRepository.Value;

    public async Task<string> GetFlagValueAsync(string flag, CancellationToken cancellation = default)
    {
        using (var connection = new NpgsqlConnection(SimpleFlagRepositoryOptions.ConnectionString))
        {
            await connection.OpenAsync(cancellation);

            var schemaPrefix = string.IsNullOrEmpty(SimpleFlagRepositoryOptions.SchemaName) ? "" : $"{SimpleFlagRepositoryOptions.SchemaName}.";
            var query = $"SELECT \"Value\" FROM {schemaPrefix}{SimpleFlagRepositoryOptions.TablePrefix}_flags WHERE \"Key\" = @flag";

            using (var command = new NpgsqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("flag", flag);

                var result = await command.ExecuteScalarAsync(cancellation);

                if (result is not null)
                {
                    return result.ToString()!;
                }
            }
        }

        throw new SimpleFlagDoesNotExistException($"The flag \"{flag}\" does not exist.");
    }
}
