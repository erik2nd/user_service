using System.Threading.Tasks;
using System.Transactions;
using Npgsql;
using UsersService.DAL.Infrastructure;
using UsersService.DAL.Repositories.Interfaces;

namespace UsersService.DAL.Repositories;

public abstract class PgRepository : IPgRepository
{
    private readonly DalOptions _dalSettings;

    protected const int DefaultTimeoutInSeconds = 5;

    protected PgRepository(DalOptions dalSettings)
    {
        _dalSettings = dalSettings;
    }

    protected async Task<NpgsqlConnection> GetConnection()
    {
        if (Transaction.Current is not null &&
            Transaction.Current.TransactionInformation.Status is TransactionStatus.Aborted)
        {
            throw new TransactionAbortedException("Transaction was aborted (probably by user cancellation request)");
        }

        var connection = new NpgsqlConnection(_dalSettings.PostgresConnectionString);
        await connection.OpenAsync();

        // Due to in-process migrations
        connection.ReloadTypes();

        return connection;
    }
}
