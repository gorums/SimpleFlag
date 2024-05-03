using Npgsql;
using SimpleFlag.Core;
using SimpleFlag.Core.DataSource;
using SimpleFlag.PostgreSQL.Migrations;

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

            using (var command = new NpgsqlCommand($"SELECT value FROM {SimpleFlagRepositoryOptions.TablePrefix}_flags WHERE name = @flag", connection))
            {
                command.Parameters.AddWithValue("flag", flag);

                var result = await command.ExecuteScalarAsync(cancellation);

                if (result != null)
                {
                    return result.ToString();
                }
            }
        }

        throw new SimpleFlagDoesNotExistException($"The flag {flag} does not exist.");
    }
}
