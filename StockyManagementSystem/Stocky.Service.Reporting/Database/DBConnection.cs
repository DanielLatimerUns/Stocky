
using System.Configuration;
using System.Net.NetworkInformation;
using System;


namespace Stocky.Service.Reporting.Database
{
    public partial class SSRSDBDataContext
    {
        public SSRSDBDataContext() : base(DBConnection.CurrentCallConnectionString)
        {
            OnCreated();
        }
    }
    
    public static class DBConnection
    {
        public static string CurrentCallConnectionString { get; set; }

        public static void Set(string ServerName, string SQLInstance, string DatabaseName, string LoginName, string Password)
        {
            if (!string.IsNullOrWhiteSpace(ServerName))
            {
                Ping ping = new Ping();
                var exists = ping.Send(ServerName, 60 * 500);

                if (exists.Status == IPStatus.Success)
                {
                    CurrentCallConnectionString
                        = string.Format(@"Data Source={0}\{4};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", ServerName, DatabaseName, LoginName, Password, SQLInstance);
                }
                else { throw new Exception("Server not reachable"); }
            }
            else
            { throw new Exception("Database Server Name invalid"); }
        }

        public static void Set(string ServerName, string DatabaseName)
        {
            if (!string.IsNullOrWhiteSpace(ServerName))
            {
                Ping ping = new Ping();
                var exists = ping.Send(ServerName, 60 * 500);

                if (exists.Status == IPStatus.Success)
                {
                    CurrentCallConnectionString
                        = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", ServerName, DatabaseName);
                }
                else { throw new Exception("Server not reachable"); }
            }
            else
            { throw new Exception("Database Server Name invalid"); }
        }
    }
}
