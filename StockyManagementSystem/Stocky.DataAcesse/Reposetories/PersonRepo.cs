using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using System.Data;
using Stocky.Exceptions;
using Stocky.DataAcesse.DataBase;

namespace Stocky.DataAcesse.Reposetories
{
    public class PersonRepo :RepoBase
    {
        public void AddNewPerson(skPerson PersonObject)
        {
            if (PersonObject != null)
            {
                dtPerson newperson = new dtPerson
                {
                    EbayName = PersonObject.EbayName,
                    Email = PersonObject.Email,
                    FirstName = PersonObject.FirstName,
                    HomePhone = PersonObject.HomePhone,
                    SureName = PersonObject.Surname,
                    WorkPhone = PersonObject.WorkPhone,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };

                DB.dtPersons.InsertOnSubmit(newperson);
                DB.SubmitChanges();     
                  
            }
        }

        public void AddNewPerson(skPerson PersonObject,skAddresses AddressObject)
        {
            if (PersonObject != null)
            {
                dtPerson newperson = new dtPerson
                {
                    EbayName = PersonObject.EbayName,
                    Email = PersonObject.Email,
                    FirstName = PersonObject.FirstName,
                    HomePhone = PersonObject.HomePhone,
                    SureName = PersonObject.Surname,
                    WorkPhone = PersonObject.WorkPhone,
                    Created = DateTime.Now,
                    Updated = DateTime.Now

                };

                dtAddress newaddress = new dtAddress
                {
                    AddressLine1 = AddressObject.AddressLine1,
                    AddressLine2 = AddressObject.AddressLine2,
                    AddressLine3 = AddressObject.AddressLine3,
                    Country = AddressObject.Country,
                    County = AddressObject.County,
                    PostCode = AddressObject.PostCode,
                    isActive = true,
                    Town = AddressObject.Town
                };

                dtAddressPersonRelationShip relationship = new dtAddressPersonRelationShip
                {
                    dtAddress = newaddress,
                    dtPerson = newperson
                };

                DB.dtAddressPersonRelationShips.InsertOnSubmit(relationship);
                DB.SubmitChanges();

            }
        }
        public void AddNewPerson(skPerson PersonObject,int AddressID)
        {
            if (PersonObject != null)
            {
                dtPerson newperson = new dtPerson
                {
                    EbayName = PersonObject.EbayName,
                    Email = PersonObject.Email,
                    FirstName = PersonObject.FirstName,
                    HomePhone = PersonObject.HomePhone,
                    SureName = PersonObject.Surname,
                    WorkPhone = PersonObject.WorkPhone,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };

                dtAddressPersonRelationShip relationship = new dtAddressPersonRelationShip
                {
                    AddressID = AddressID,
                    dtPerson = newperson
                };

                DB.dtAddressPersonRelationShips.InsertOnSubmit(relationship);
                DB.SubmitChanges();

            }
        }

        public void UpdatePerson(skPerson PersonObject)
        {
            dtPerson person = DB.dtPersons.SingleOrDefault(x => x.pID == PersonObject.pID);

            person.EbayName = PersonObject.EbayName;
            person.FirstName = PersonObject.FirstName;
            person.SureName = PersonObject.Surname;
            person.Email = PersonObject.Email;
            person.WorkPhone = PersonObject.WorkPhone;
            person.HomePhone = PersonObject.HomePhone;
            person.Updated = DateTime.Now;

            DB.SubmitChanges();
        }

        public skPerson GetPersonObject(int personID)
        {
            AddressRepo repo = new AddressRepo();

            var query = (from A in DB.dtPersons
                         where A.pID == personID
                         select new skPerson
                         {
                             pID = A.pID,
                             EbayName = A.EbayName,
                             Email = A.Email,
                             FirstName = A.FirstName,
                             Surname = A.SureName,
                             HomePhone = A.HomePhone,
                             WorkPhone = A.WorkPhone,
                             LinkedAddresses = repo.GetPersonAddressList(personID).ToList(),
                             Created = A.Created,
                             Updated = A.Updated

                        }).FirstOrDefault();
            if(query == null)
            {
                throw new NoRecordException(typeof(skPerson));
            }
            else
            {
                return query;
            }            
        }

        public IEnumerable<skPerson> GetPersonList()
        {
            AddressRepo repo = new AddressRepo();

            var query = from A in DB.dtPersons
                        select new skPerson
                        {
                            pID = A.pID,
                            EbayName = A.EbayName,
                            Email = A.Email,
                            FirstName = A.FirstName,
                            Surname = A.SureName,
                            HomePhone = A.HomePhone,
                            WorkPhone = A.WorkPhone,
                            LinkedAddresses = repo.GetPersonAddressList(A.pID).ToList(),
                            Created = A.Created,
                            Updated = A.Updated

                        };

            return query;
        }
    }
}
