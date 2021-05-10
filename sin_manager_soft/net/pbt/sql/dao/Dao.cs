using System.Collections;
using System.Data;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_platform_soft_unit_tests.net.pbt.sql.dao
{
    public abstract class Dao
    {
        protected readonly SinCollection serverInstance;
        protected readonly SinCollection localInstance;

        protected Dao()
        {
            serverInstance = SinCollection.GetServerCollection();
            localInstance = SinCollection.GetLocalCollection();
        }

        public abstract void CreateList(IDbConnection connection);
        public abstract void SendList(IDbConnection connection);
        public abstract void Update(IDbConnection connection, object obj);
        public abstract void Delete(IDbConnection connection, object obj);
    }
}