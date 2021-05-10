using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using Dapper;
using sin_manager_soft.net.pbt.sql.connector;
using sin_manager_soft.net.pbt.sql.sqlessences;

namespace sin_platform_soft_unit_tests.net.pbt.sql.dao
{
    public sealed class MailServiceDao : Dao
    {
        public override void CreateList(IDbConnection connection)
        {
            ObservableCollection<MailService> mailServices =
                new ObservableCollection<MailService>(connection.Query<MailService>(Query.GET_MAIL_SERVICE_LIST)
                    .AsEnumerable());
            serverInstance.MailServiceList = mailServices;
        }

        public override void SendList(IDbConnection connection)
        {
            ObservableCollection<MailService> mailServices = localInstance.MailServiceList;
            foreach (MailService mailService in mailServices)
            {
                serverInstance.MailServiceList.Add(mailService);
                connection.Query(Query.ADD_MAIL_SERVICE, mailService);
            }
        }

        public override void Update(IDbConnection connection, object obj)
        {

        }

        public override void Delete(IDbConnection connection, object obj)
        {

        }
    }
}
