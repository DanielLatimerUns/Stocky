using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.DataAcesse.DataBase;
using Stocky.Exceptions;

namespace Stocky.DataAcesse.Reposetories
{
    public class AddressRepo : RepoBase
    {
        public IEnumerable<skAddresses> GetAddressList()
        {
            var query = from A in DB.dtAddresses
                        select new skAddresses
                        {
                            AddressLine1 = A.AddressLine1,
                            AddressLine2 = A.AddressLine2,
                            AddressLine3 = A.AddressLine3,
                            Country = A.Country,
                            County = A.County,
                            PostCode = A.PostCode,
                            Town = A.Town,
                            Active = A.isActive,
                            AddressID = A.aID,
                            Created = A.Created,
                            Updated = A.Updated

                        };

            return query;
        }
        public IEnumerable<skAddresses> GetPersonAddressList(int PersonID)
        {
            var query = from A in DB.dtAddresses
                        join REL in DB.dtAddressPersonRelationShips on A.aID equals REL.AddressID
                        where REL.PersonID == PersonID
                        select new skAddresses
                        {
                            AddressLine1 = A.AddressLine1,
                            AddressLine2 = A.AddressLine2,
                            AddressLine3 = A.AddressLine3,
                            Country = A.Country,
                            County = A.County,
                            PostCode = A.PostCode,
                            Town = A.Town,
                            Active = A.isActive,
                            AddressID = A.aID,
                            Created = A.Created,
                            Updated = A.Updated
                        };

            return query;
        }

        public skAddresses GetAddressObject(int AddressID)
        {
            var query = (from A in DB.dtAddresses
                        where A.aID == AddressID
                        select new skAddresses
                        {
                            AddressLine1 = A.AddressLine1,
                            AddressLine2 = A.AddressLine2,
                            AddressLine3 = A.AddressLine3,
                            Country = A.Country,
                            County = A.County,
                            PostCode = A.PostCode,
                            Town = A.Town,
                            Active = A.isActive,
                            AddressID = A.aID,
                            Created = A.Created,
                            Updated = A.Updated
                        }).FirstOrDefault();

            if (query == null)
            {
                throw new NoRecordException(typeof(skAddresses));
            }
            else
            {
                return query;
            }
        }

        public void AddNewAddress(skAddresses AddressObject)
        {
            if (AddressObject != null)
            {
                dtAddress NewAdress = new dtAddress
                {
                    AddressLine1 = AddressObject.AddressLine1,
                    AddressLine2 = AddressObject.AddressLine2,
                    AddressLine3 = AddressObject.AddressLine3,
                    Country = AddressObject.Country,
                    County = AddressObject.County,
                    PostCode = AddressObject.PostCode,
                    Town = AddressObject.Town,
                    isActive = true,
                    Created = DateTime.Now,
                    Updated = DateTime.Now                    
                };

                DB.dtAddresses.InsertOnSubmit(NewAdress);
                DB.SubmitChanges();           
            }          
        }

        public void AddNewAddress(skAddresses AddressObject,int ObjectID,Type ObjectType)
        {
            if (ObjectType.Name == "skPerson")
            {

                    dtAddress NewAdress = new dtAddress
                    {
                        AddressLine1 = AddressObject.AddressLine1,
                        AddressLine2 = AddressObject.AddressLine2,
                        AddressLine3 = AddressObject.AddressLine3,
                        Country = AddressObject.Country,
                        County = AddressObject.County,
                        PostCode = AddressObject.PostCode,
                        Town = AddressObject.Town,
                        isActive = true,
                        Created = DateTime.Now,
                        Updated = DateTime.Now
                    };

                    dtAddressPersonRelationShip relationship = new dtAddressPersonRelationShip
                    {
                        dtAddress = NewAdress,
                        PersonID = ObjectID
                    };

                    DB.dtAddressPersonRelationShips.InsertOnSubmit(relationship);
                    DB.SubmitChanges();
            }
            else
            {
                dtAddress NewAdress = new dtAddress
                {
                    AddressLine1 = AddressObject.AddressLine1,
                    AddressLine2 = AddressObject.AddressLine2,
                    AddressLine3 = AddressObject.AddressLine3,
                    Country = AddressObject.Country,
                    County = AddressObject.County,
                    PostCode = AddressObject.PostCode,
                    Town = AddressObject.Town,
                    isActive = true,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };


                DB.dtAddresses.InsertOnSubmit(NewAdress);
                DB.SubmitChanges();

                dtVendor vendor = DB.dtVendors.Where(x => x.vID == ObjectID).Single();
                vendor.AddresseID = DB.dtAddresses.OrderByDescending(x => x.Created).Single().aID;

                DB.SubmitChanges();
            }
        }

        public void LinkAddressToPerson(int AddressID,int PersonID)
        {
            if(AddressID != 0 && PersonID != 0)
            {
                dtAddressPersonRelationShip relationship = new dtAddressPersonRelationShip
                {
                    AddressID = AddressID,
                    PersonID = PersonID
                };

                DB.dtAddressPersonRelationShips.InsertOnSubmit(relationship);
                DB.SubmitChanges();
            }
        }

        public void RemoveLinktoPerson(int AddressID, int PersonID)
        {
            dtAddressPersonRelationShip relationship = DB.dtAddressPersonRelationShips.SingleOrDefault(x => x.AddressID == AddressID && x.PersonID == PersonID);

            if (relationship != null)
            {
                DB.dtAddressPersonRelationShips.DeleteOnSubmit(relationship);
                DB.SubmitChanges();
            }
            
        }

        public void UpdateAddress(skAddresses AddressObject)
        {
            if(AddressObject != null)
            {
                dtAddress addressobject = DB.dtAddresses.Single(x => x.aID == AddressObject.AddressID);

                addressobject.AddressLine1 = addressobject.AddressLine1;
                addressobject.AddressLine2 = addressobject.AddressLine2;
                addressobject.AddressLine3 = addressobject.AddressLine3;
                addressobject.Country = addressobject.Country;
                addressobject.County = addressobject.County;
                addressobject.PostCode = addressobject.PostCode;
                addressobject.Town = addressobject.Town;
                addressobject.Updated = DateTime.Now;

                DB.SubmitChanges();
            }
        }
    }
}
