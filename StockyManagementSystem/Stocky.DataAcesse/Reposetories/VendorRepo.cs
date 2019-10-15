using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.DataAcesse.DataBase;

namespace Stocky.DataAcesse.Reposetories
{
    public class VendorRepo :RepoBase
    {
        public IEnumerable<skvendors> GetVendorList()
        {
            var query = from V in DB.dtVendors
                        join A in DB.dtAddresses on V.AddresseID equals A.aID
                        select new skvendors
                        {
                            VendorID = V.vID,
                            VendorsName = V.VendorsName,
                            VendorsDescption = V.VendorsDescption,
                            onlineVendor = V.OnlineVender,
                            AddresseID = V.AddresseID,
                            CurrentAddress = new skAddresses
                            {
                                Active = A.isActive,
                                AddressID = A.aID,
                                AddressLine1 = A.AddressLine1,
                                AddressLine2 = A.AddressLine2,
                                AddressLine3 = A.AddressLine3,
                                Country = A.Country,
                                County = A.County,
                                PostCode = A.PostCode,
                                Town = A.Town
                            }
                        };
            return query;
        }

        public IEnumerable<skvendors> GetVendorList(skvendors vendorOBJ)
        {
            long tempval;
            long? sIDv = long.TryParse(vendorOBJ.VendorID.ToString(), out tempval) ? tempval : (long?)null;
            var query = from V in DB.dtVendors
                        join A in DB.dtAddresses on V.AddresseID equals A.aID
                        where V.vID.Equals(sIDv
                        == 0 ? V.vID : sIDv)
                        && V.VendorsName.IndexOf(vendorOBJ.VendorsName == null ? V.VendorsName : vendorOBJ.VendorsName) >= 0
                        select new skvendors
                        {
                            VendorID = V.vID,
                            VendorsName = V.VendorsName,
                            VendorsDescption = V.VendorsDescption,
                            CurrentAddress = new skAddresses
                            {
                                Active = A.isActive,
                                AddressID = A.aID,
                                AddressLine1 = A.AddressLine1,
                                AddressLine2 = A.AddressLine2,
                                AddressLine3 = A.AddressLine3,
                                Country = A.Country,
                                County = A.County,
                                PostCode = A.PostCode,
                                Town = A.Town
                            }
                        };
            return query;
        }

        public skvendors GetVendorDetails(int VendorId)
        {
            var query = (from V in DB.dtVendors
                        where V.vID == VendorId
                        select new skvendors
                        {
                            VendorID = V.vID,
                            VendorsName = V.VendorsName,
                            VendorsDescption = V.VendorsDescption,
                            onlineVendor = V.OnlineVender,
                            AddresseID = V.AddresseID
                        }).FirstOrDefault();

            var addressquery = (from A in DB.dtAddresses
                         where A.aID == query.AddresseID
                         select new skAddresses
                         {
                             Active = A.isActive,
                             AddressID = A.aID,
                             AddressLine1 = A.AddressLine1,
                             AddressLine2 = A.AddressLine2,
                             AddressLine3 = A.AddressLine3,
                             Country = A.Country,
                             County = A.County,
                             PostCode = A.PostCode,
                             Town = A.Town
                         }).FirstOrDefault();

            query.CurrentAddress = addressquery;
          

            return query;
        }

        public void UpdateVendor(skvendors VendorObj)
        {
            dtVendor Vendor = DB.dtVendors.Single(x => x.vID == VendorObj.VendorID);

            Vendor.VendorsName = VendorObj.VendorsName;
            Vendor.VendorsDescption = VendorObj.VendorsDescption;
            Vendor.OnlineVender = VendorObj.onlineVendor;
            Vendor.AddresseID = VendorObj.CurrentAddress.AddressID;

            DB.SubmitChanges();
        }

        public bool AddNewVendor(skvendors VendorObj)
        {
            if (VendorObj != null)
            {
                dtVendor NewVendor = new dtVendor
                {
                    VendorsName = VendorObj.VendorsName,
                    VendorsDescption = VendorObj.VendorsDescption,
                    OnlineVender = VendorObj.onlineVendor,
                    AddresseID = Convert.ToInt32(VendorObj.AddresseID)
                };

                DB.dtVendors.InsertOnSubmit(NewVendor);
                DB.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AddNewVendor(skvendors VendorObj, skAddresses AddressObject)
        {
            if (VendorObj != null)
            {
                dtVendor NewVendor = new dtVendor
                {
                    VendorsName = VendorObj.VendorsName,
                    VendorsDescption = VendorObj.VendorsDescption,
                    OnlineVender = VendorObj.onlineVendor,

                };

                dtAddress NewAdress = new dtAddress
                {
                    AddressLine1 = AddressObject.AddressLine1,
                    AddressLine2 = AddressObject.AddressLine2,
                    AddressLine3 = AddressObject.AddressLine3,
                    Country = AddressObject.Country,
                    County = AddressObject.County,
                    PostCode = AddressObject.PostCode,
                    Town = AddressObject.Town,
                    isActive = true
                };

                NewAdress.dtVendors.Add(NewVendor);


                DB.dtAddresses.InsertOnSubmit(NewAdress);
                DB.SubmitChanges();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
