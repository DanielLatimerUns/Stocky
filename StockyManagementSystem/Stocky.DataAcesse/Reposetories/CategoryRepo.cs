using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stocky.Data;
using Stocky.DataAcesse.DataBase;

namespace Stocky.DataAcesse.Reposetories
{
    public class CategoryRepo : RepoBase
    {
        public IEnumerable<skCategory> GetCategoryList(skCategory CategoryOBJ)
        {
            var query = from STL in DB.dtCategories
                        where STL.Title.IndexOf(CategoryOBJ.SearchFilter.ObjectName == null ? STL.Title : CategoryOBJ.SearchFilter.ObjectName) >= 0
                                  && STL.Description.IndexOf(CategoryOBJ.SearchFilter.ObjectDescription == null ? STL.Description : CategoryOBJ.SearchFilter.ObjectDescription) >= 0
                                  && STL.CatID.ToString().Equals(CategoryOBJ.SearchFilter.ObjectID == null ? STL.CatID.ToString() : CategoryOBJ.SearchFilter.ObjectID)

                        select new skCategory
                        {
                            StockTypeID = STL.CatID,
                            Name = STL.Title,
                            Description = STL.Description
                        };
            return query;
        }

        public IEnumerable<skCategory> GetCategoryList()
        {
            var query = from STL in DB.dtCategories

                        select new skCategory
                        {
                            StockTypeID = STL.CatID,
                            Name = STL.Title,
                            Description = STL.Description
                        };
            return query;
        }

        public skCategory GetCategoryObject(int CategoryID)
        {
            var query = (from C in DB.dtCategories
                        where  C.CatID == CategoryID

                        select new skCategory
                        {
                            StockTypeID = C.CatID,
                            Name = C.Title,
                            Description = C.Description
                        }).FirstOrDefault();

            return query;
        }
        public skCategory GetCategoryObjectByStockID(int StockID)
        {
            var query = (from C in DB.dtCategories
                         join S in DB.dtStocks on C.CatID equals S.CategoryID
                         where S.sID == StockID
                         select new skCategory
                         {
                             StockTypeID = C.CatID,
                             Name = C.Title,
                             Description = C.Description
                         }).FirstOrDefault();

            return query;
        }
        public void AddNewCategory(skCategory categoryobj)
        {
            dtCategory newcat = new dtCategory
            {
                Description = categoryobj.Description,
                Title = categoryobj.Name

            };

            DB.dtCategories.InsertOnSubmit(newcat);
            DB.SubmitChanges();
        }

        public void UpdateCategory(skCategory CategoryObj)
        {

            dtCategory category = DB.dtCategories.Single(x => x.CatID == CategoryObj.StockTypeID);

            category.Description = CategoryObj.Description;
            category.Title = CategoryObj.Name;

            DB.SubmitChanges();

        }
    }
}
