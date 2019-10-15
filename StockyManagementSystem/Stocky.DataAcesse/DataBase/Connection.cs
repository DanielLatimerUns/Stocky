
using System.Configuration;
using System.Net.NetworkInformation;
using System;

namespace Stocky.DataAcesse.DataBase
{
    public partial class StockyDataDataContext
    {   
        public StockyDataDataContext():

        base(DBConnection.CurrentCallConnectionString)
        {
            OnCreated();
        }
    }

    public static class DBConnection
    {
        public static string CurrentCallConnectionString { get; set; }
        public static void Set(string ServerName,string SQLInstance, string DatabaseName, string LoginName, string Password)
        {
            if (!string.IsNullOrWhiteSpace(ServerName))
            {
                Ping ping = new Ping();
                PingReply result;
                try
                {
                    result = ping.Send(ServerName, 60 * 500);
                }
                catch (PingException e)
                {
                    throw new Exception("There was an error connecting to the service , Please try again or contact support", e);
                }

                if (result.Status == IPStatus.Success)
                {
                    CurrentCallConnectionString
                        = string.Format(@"Data Source={0}\{4};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", ServerName, DatabaseName,LoginName,Password,SQLInstance);
                }
                else { throw new Exception("There was an error connecting to the service , Please try again or contact support"); }
            }
            else
            { throw new Exception("There was an error connecting to the service , Please try again or contact support"); }
        }

        public static void Set(string ServerName, string DatabaseName, string LoginName, string Password)
        {
            if (!string.IsNullOrWhiteSpace(ServerName))
            {
                Ping ping = new Ping();
                PingReply result;
                try
                {
                     result = ping.Send(ServerName, 60 * 500);
                }
                catch(PingException e)
                {
                    throw new Exception("There was an error connecting to the service , Please try again or contact support", e);
                }

                if (result.Status == IPStatus.Success)
                {
                    CurrentCallConnectionString
                        = string.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", ServerName, DatabaseName, LoginName, Password);
                }
                else { throw new Exception("There was an error connecting to the service , Please try again or contact support"); }
            }
            else
            { throw new Exception("There was an error connecting to the service , Please try again or contact support"); }
        }

        public static void Set(string ServerName,string DatabaseName)
        {
            if (!string.IsNullOrWhiteSpace(ServerName))
            {
                Ping ping = new Ping();
                PingReply result;
                try
                {
                    result = ping.Send(ServerName, 60 * 500);
                }
                catch (PingException e)
                {
                    throw new Exception("There was an error connecting to the service , Please try again or contact support", e);
                }

                if (result.Status == IPStatus.Success)
                {
                    CurrentCallConnectionString 
                        = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", ServerName,DatabaseName);
                }
                else { throw new Exception("There was an error connecting to the service , Please try again or contact support"); }
            }
            else
            { throw new Exception("There was an error connecting to the service, Please try again or contact support"); }
        }
    }  
}
