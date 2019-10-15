using MVCTest.Models;
using Stocky.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCTest.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult StockManager()
        {
            var Model = new StockManagerModel();
            Model.StockObject = new skStock();
          //  Model.StockList = Model.StockObject.GetStockList();

            return View(Model);
        }

        public ActionResult GoToDetails(int id)
        {
            var Model = skStock.GetStockObject(id);

            return View(Model);
        }
    }
}