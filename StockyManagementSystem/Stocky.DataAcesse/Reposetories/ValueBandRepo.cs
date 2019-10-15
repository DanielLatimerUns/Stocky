using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.DataAcesse.DataBase;

namespace Stocky.DataAcesse.Reposetories
{
    public class ValueBandRepo :RepoBase
    {
        public IEnumerable<skValueBands> GetValueBandList()
        {
            var query = from VB in DB.dtValueBands
                        select new skValueBands
                        {
                            Description = VB.Description,
                            HighValue = VB.HighValue,
                            ID = VB.ivID,
                            LowValue = VB.LowValue
                        };
            return query;
        }

        public IEnumerable<skValueBands> GetValueBandList(skValueBands ValueBandObj)
        {
            int tempval;
            int? ConvertedID = int.TryParse(ValueBandObj.ID.ToString(), out tempval) ? tempval : (int?)null;

            var query = from VB in DB.dtValueBands
                        where VB.ivID.Equals(ConvertedID == 0 ? VB.ivID : ValueBandObj.ID)
                        select new skValueBands
                        {
                            Description = VB.Description,
                            HighValue = VB.HighValue,
                            ID = VB.ivID,
                            LowValue = VB.LowValue
                        };
            return query;
        }

        public skValueBands GetValueBand(int ValueBandID)
        {
            var valueobject = (from VB in DB.dtValueBands
                              where VB.ivID == ValueBandID
                              select new skValueBands
                              {
                                  Description = VB.Description,
                                  HighValue = VB.HighValue,
                                  ID = VB.ivID,
                                  LowValue = VB.LowValue
                              }).FirstOrDefault();

            return valueobject;
        }

    }
}
