using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.DataAcesse.DataBase;
using Stocky.Data;
using Stocky.Data.Interfaces;
using Stocky.Utility;

namespace Stocky.DataAcesse.Reposetories
{
    public class UserRepo :RepoBase
    {      
        public bool DoesUserExist(string UserName)
        {
            var query = from U in DB.dtUsers
                        where U.UserName == UserName
                        select U;

            if(query.Count() != 0)
            {
                return true;
            }
            else { return false; }
        }

        public string GetPassWord(string UserName)
        {
            var query = from U in DB.dtUsers
                        where U.UserName == UserName
                        select U;

            return query.FirstOrDefault().PassWord;
        }

        public skUser GetUserObject(string UserName)
        {
            var query = (from U in DB.dtUsers
                        join UD in DB.dtUserDetails on U.uID equals UD.UserID
                        where U.UserName == UserName
                        select new skUser
                        {
                            UserID = U.uID,
                            DOB = UD.DateOfBirth,
                            Email = UD.Email,
                            FistName = UD.FirstName,
                            HomePhone = UD.HomePhone,
                            Initials = UD.Initials,
                            IsAdmin = Convert.ToBoolean(U.IsAdmin),
                            LastName = UD.LastName,
                            Mobile = UD.WorkPhone,
                            WorkPhone = UD.WorkPhone,
                            UserName = U.UserName,
                            Password = U.PassWord

                        }).FirstOrDefault();

            return query;
        }
        public skUser GetUserObject(int UserID)
        {
            var query = (from U in DB.dtUsers
                         join UD in DB.dtUserDetails on U.uID equals UD.UserID
                         where U.uID == UserID
                         select new skUser
                         {
                             UserID = U.uID,
                             DOB = UD.DateOfBirth,
                             Email = UD.Email,
                             FistName = UD.FirstName,
                             HomePhone = UD.HomePhone,
                             Initials = UD.Initials,
                             IsAdmin = Convert.ToBoolean(U.IsAdmin),
                             LastName = UD.LastName,
                             Mobile = UD.WorkPhone,
                             WorkPhone = UD.WorkPhone,
                             UserName = U.UserName,
                             Password = U.PassWord

                         }).FirstOrDefault();

            return query;
        }

        public void AddNewUser(skUser UserObj)
        {

                dtUser newuser = new dtUser
                {
                    UserName = UserObj.UserName,
                    PassWord = UserObj.Password,
                    Created = DateTime.Now,
                    IsAdmin = UserObj.IsAdmin
                };

                dtUserDetail newuserdetails = new dtUserDetail
                {
                    FirstName = UserObj.FistName,
                    LastName = UserObj.LastName,
                    DateOfBirth = UserObj.DOB,
                    Initials = UserObj.Initials,
                    Email = UserObj.Email,
                    HomePhone = UserObj.HomePhone,
                    WorkPhone = UserObj.WorkPhone,
                    dtUser = newuser
                };

                dtUserPreferance newuserpref = new dtUserPreferance
                {
                    Code = "MAINTOOLCO",
                    Description = "Main tool bar background colour",
                    Name = "Main ToolBar",
                    UserID = GetLatestUserID() + 1,
                    PreferenceTypeID = 1,
                    Value = "Black"
                };

                DB.dtUserDetails.InsertOnSubmit(newuserdetails);
                DB.dtUserPreferances.InsertOnSubmit(newuserpref);

                DB.SubmitChanges();
        }

        public void UpdateUser(skUser UserObj, bool ResetPassword)
        {
            dtUser user = DB.dtUsers.Single(x => x.uID == UserObj.UserID);
            if (ResetPassword)
            {
                user.PassWord = UserObj.Password;
            }
            user.UserName = UserObj.UserName;

            dtUserDetail userdetail = DB.dtUserDetails.Single(x => x.UserID == UserObj.UserID);

            userdetail.DateOfBirth = UserObj.DOB;
            userdetail.Email = UserObj.Email;
            userdetail.FirstName = UserObj.FistName;
            userdetail.LastName = UserObj.LastName;
            userdetail.HomePhone = UserObj.HomePhone;
            userdetail.WorkPhone = UserObj.WorkPhone;
            userdetail.Initials = UserObj.Initials;

            DB.SubmitChanges();
        }

        public IEnumerable<skPreference> GetPreferanceList(int UserID)
        {
            var query = from U in DB.dtUsers
                        join UP in DB.dtUserPreferances on U.uID equals UP.UserID
                        join UPT in DB.dtUserPreferanceTypes on UP.PreferenceTypeID equals UPT.preftypeID
                        where U.uID == UserID
                        select new skPreference
                        {
                            ID = UP.upID,
                            Name = UP.Name,
                            Type = UPT.Name,
                            Desc = UP.Description,
                            Value = UP.Value,
                            Code = UP.Code,
                            UserID = UP.UserID
                        };

            return query;
        }

        public skPreference GetPreferanceObject(int UserID,string Code)
        {
            var query = (from U in DB.dtUsers
                        join UP in DB.dtUserPreferances on U.uID equals UP.UserID
                        join UPT in DB.dtUserPreferanceTypes on UP.PreferenceTypeID equals UPT.preftypeID
                        where U.uID == UserID && UP.Code == Code
                        select new skPreference
                        {
                            ID = UP.upID,
                            Name = UP.Name,
                            Type = UPT.Name,
                            Desc = UP.Description,
                            Value = UP.Value,
                            Code = UP.Code,
                            UserID = UP.UserID
                        }).FirstOrDefault();

            return query;
        }

        public skPreference GetPreferanceObject(int UserID, int ID)
        {
            var query = (from U in DB.dtUsers
                         join UP in DB.dtUserPreferances on U.uID equals UP.UserID
                         join UPT in DB.dtUserPreferanceTypes on UP.PreferenceTypeID equals UPT.preftypeID
                         where U.uID == UserID && UP.upID == ID
                         select new skPreference
                         {
                             ID = UP.upID,
                             Name = UP.Name,
                             Type = UPT.Name,
                             Desc = UP.Description,
                             Value = UP.Value,
                             Code = UP.Code,
                             UserID = UP.UserID
                         }).FirstOrDefault();

            return query;
        }

        public void UpDatePreferances(int UserID, string Code, object value)
        {
            dtUserPreferance pr = DB.dtUserPreferances.Single(x => x.Code == Code && x.UserID == UserID);
            pr.Value = value.ToString();

            DB.SubmitChanges();
        }


        public void UpDatePreferances(List<skPreference> Preferances ,int UserID)
        {
            foreach (skPreference Pref in Preferances)
            {
                dtUserPreferance pr = DB.dtUserPreferances.Single(x => x.Code == Pref.Code && x.UserID == UserID);
                pr.Value = Pref.Value.ToString();

                DB.SubmitChanges();
            }
        }

        public skTheme GetThemeObject(int ThemeID)
        {
            var query = (from t in DB.dtThemes
                         join u in DB.dtUsers on t.thID equals u.ThemeID
                         where u.uID == ThemeID
                         select new skThemeType
                         {
                            ThemeXML = t.ThemXML
                         }).FirstOrDefault();

            var theme = Utility.Theming.ThemeParser.ParseThemeXML(query.ThemeXML);

            if(theme != null)
                return theme;
            else
                throw new Exception("No Theme Found.");
        }

        public IEnumerable<skThemeType> GetThemeTypeList()
        {
            var qeury = from t in DB.dtThemes
                        select new skThemeType
                        {
                            ID = t.thID,
                            Description = t.Description,
                            Name = t.Name,
                            ThemeXML = t.ThemXML
                        };
            return qeury.ToList<skThemeType>();
        }

        public void AddThemeType(skThemeType ThemeTypeObject)
        {
            dtTheme newthem = new dtTheme
            {
                Description = ThemeTypeObject.Description,
                Name = ThemeTypeObject.Name,
                ThemXML = ThemeTypeObject.ThemeXML
            };

            DB.dtThemes.InsertOnSubmit(newthem);
            DB.SubmitChanges();
        }

        public void UpdateThemeType(skThemeType ThemeTypeObject)
        {
            dtTheme ThemeToUpdate = DB.dtThemes.Single(x => x.thID == ThemeTypeObject.ID);

            ThemeToUpdate.Name = ThemeToUpdate.Name;
            ThemeToUpdate.Description = ThemeToUpdate.Description;
            ThemeToUpdate.ThemXML = ThemeToUpdate.ThemXML;

            DB.SubmitChanges();
        }
    }
}
