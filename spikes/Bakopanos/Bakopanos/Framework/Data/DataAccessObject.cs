using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity;

namespace Bakopanos.Framework.Data
{
    public abstract class DataAccessObject
    {
        [Dependency]
        public Database Database { get; set; }
    }
}