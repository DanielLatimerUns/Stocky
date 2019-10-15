using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.DataAcesse.DataBase;

namespace Stocky.DataAcesse.Reposetories
{
    public class SettingsRepo :RepoBase
    {
        public void SaveSettingsData(string SettingsXML)
        {
            var query = (from S in DB.dtSettingLookups
                         where S.Code == "APP"
                         select S).FirstOrDefault();
            if(query.Code == "APP")
            {
                query.Value = SettingsXML;
                DB.SubmitChanges();
            }
            else
            {
                dtSettingLookup APP = new dtSettingLookup
                {
                    Code = "APP",
                    Value = SettingsXML

                };
                DB.dtSettingLookups.InsertOnSubmit(APP);
                DB.SubmitChanges();
            }
        }

        public string LoadSettingsData()
        {
            var query = (from S in DB.dtSettingLookups
                        where S.Code == "APP"
                        select new 
                        {
                            Value = S.Value
                        }).FirstOrDefault().Value;

            if(string.IsNullOrEmpty(query))
            {
                dtSettingLookup APP = new dtSettingLookup
                {
                    Code = "APP",
                    Value = "<>"

                };
                DB.dtSettingLookups.InsertOnSubmit(APP);
                DB.SubmitChanges();

                return "<>";
            }

            return query;
        }
    }
}
