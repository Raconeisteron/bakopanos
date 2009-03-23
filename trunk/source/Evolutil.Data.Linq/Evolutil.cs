using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Practices.Unity;

namespace Evolutil.Data.Linq
{
    public class EvolutilDatabase : EvolutilDataContext
    {
        public EvolutilDatabase([Dependency("ConnectionString")]string connectionString)
            : base(connectionString)
        {
        }
    }
    partial class EvolutilDataContext
    {
        
    }
}
