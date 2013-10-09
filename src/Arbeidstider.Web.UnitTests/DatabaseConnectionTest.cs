using Arbeidstider.Business.Interfaces.Database;
using Arbeidstider.Database;

namespace Arbeidstider.Web.UnitTests
{
    public class DatabaseConnectionTest : DatabaseConnection, IDatabaseConnection
    {
        public DatabaseConnectionTest(string connectionString = null) : base(null, "test")
        {
            base._mode = DatabaseMode.Test;
        }
    }
}
