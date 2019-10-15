using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Configuration;
using System;

//using eBay.Service.Call;
//using eBay.Service.Core.Sdk;
//using eBay.Service.Core.Soap;
//using eBay.Service.Util;


namespace EbayAPI
{
    public class BasicCommands
    {
  //      public static ApiContext apiContext = null;

//        public BasicCommands()
  //      {
//            GetAPiContext();
  //      }
        //public string GeteBayOfficialTime()
        //{
        //    //string endpoint = "https://api.ebay.com/wsapi";
        //    //string callName = "GeteBayDetails";
        //    //string siteId = "3";
        //    //string appId = "DanielUn-eb65-4cfd-bdfd-3dd153588842";      // use your app ID
        //    //string devId = "ef2014e5-387a-481e-baa4-546ee35035fd";     // use your dev ID
        //    //string certId = "418484df-f84d-472e-9508-d57ebac62b3a";   // use your cert ID
        //    //string version = "405";
        //    //// Build the request URL
        //    //string requestURL = endpoint
        //    //+ "?callname=" + callName
        //    //+ "&siteid=" + siteId
        //    //+ "&appid=" + appId
        //    //+ "&version=" + version
        //    //+ "&routing=default";
        //    //// Create the service
        //    //EbayAPI.BaySoap.eBayAPIInterfaceClient service = new eBayAPIInterfaceClient("eBayAPI", requestURL);
        //    //// Assign the request URL to the service locator.
            
        //    //// Set credentials
        //    //EbayAPI.BaySoap.CustomSecurityHeaderType RequesterCredentials = new EbayAPI.BaySoap.CustomSecurityHeaderType();
        //    //RequesterCredentials.eBayAuthToken = @"AgAAAA**AQAAAA**aAAAAA**IrEBVg**nY+sHZ2PrBmdj6wVnY+sEZ2PrA2dj6AGkoSgAZSHowmdj6x9nY+seQ**DwADAA**AAMAAA**JSobdJgkfoMB7trxuVKS5maKuQ8SzOWchkzeAfXbfyPFKXqpZTKZnnp//bI1gCjWQqi2A6S1I66blF8ze6T0GtFeZN4yuXqtqV5F1+tutkwq/XMRlUzQ/3orP00E5a4Kx4vxkuFWCSt9cUD0QJUDIUkJDg5YB7RV4Y7GIa3Ulzt8AqoNLQAxWq8I8aHj+UxPboJVxr8gPzvn6OELVrU66KVFgBL+v3Ljd8wCcs7/wpjH+cMOB1uopwxVGZBDmYNS59h1O9F/qMfmjnvYt4VqIEMNtbfxcB8OM6SegA5Y19Uer8jRDVsut6S0dHdlGkAoIWksGTSl58agyUqT3DwicfDn6loblI/eBr3ANEWdUhPXnwcepIOp9iZS0YLSYWOdKOlYYjJDAJRTQANBk6huoMxOnaCH+80vbbazjHALHGVEH6fOcyKGHmhQ9THuV+vgLsmqoqHGHzihIjPCIHzrnOt1UqBa7p5jf03nyCGdQoswQFX/tJhTVYym4/M1hXtAhSix3t3JOwv4M8weHS+mWh1ypbojm6Lh60jm0iRtRr9b/US0/AUHEXxwTGE/WFsqlyujUH4Zm7bm+YGXwsvvNc3lejMYbS/nej6ocvH0jUYA8mIVDjVyY3vt4OCmOMs0Yct/XgmIiKuilquWzQbVxECAVl4V/hSHgFaq0vUVpHGUtEzBgR7pwXEQuM2f311qnJCKhNnNY/9oS3egq/cm3zGqWgyLD5BXkRdOhEcRtkYCI3Uerna6LMvOMl810vyt";
        //    //RequesterCredentials.Credentials = new EbayAPI.BaySoap.UserIdPasswordType();
        //    //RequesterCredentials.Credentials.AppId = appId;
        //    //RequesterCredentials.Credentials.DevId = devId;
        //    //RequesterCredentials.Credentials.AuthCert = certId;
        //    //// Make the call to GeteBayOfficialTimek
        //    //EbayAPI.BaySoap.GeteBayDetailsRequestType request = new EbayAPI.BaySoap.GeteBayDetailsRequestType();
        //    //request.Version = "405";
        //    //EbayAPI.BaySoap.GeteBayDetailsResponseType response = service.GeteBayDetails(ref RequesterCredentials, request);
        //    //return response.CountryDetails.FirstOrDefault().Description;
        //}
     //   public string blah { get; set; }

   //    public static ApiContext GetAPiContext ()
    //    {
    //        if (apiContext != null)
    //        {
   //             return apiContext;
    //        }
  //          else
  //          {
//                apiContext = new ApiContext();

                //set Api Server Url
  //              apiContext.SoapApiServerUrl =
 ///                   ConfigurationManager.AppSettings["Environment.ApiServerUrl"];
                //set Api Token to access eBay Api Server
 //               ApiCredential apiCredential = new ApiCredential();
  //              apiCredential.eBayToken =
   //                 ConfigurationManager.AppSettings["UserAccount.ApiToken"];
    //            
    //            apiContext.ApiCredential = apiCredential;
    //            //set eBay Site target to US
    //            apiContext.Site = eBay.Service.Core.Soap.SiteCodeType.UK;

                //set Api logging
    //            apiContext.ApiLogManager = new ApiLogManager();
    //                new FileLogger("listing_log.txt", true, true, true)
    //                );
     //           apiContext.ApiLogManager.EnableLogging = true;


   //             return apiContext;
////
   //         }
  //      }

   ///    public List<CategoryType> GetEbayCategories()
    //   {
//           var Categories = new GetCategoriesCall(apiContext);
    //       Categories.Site = SiteCodeType.UK;
   //        Categories.ApiContext = apiContext;
     //     
       //    Categories.Execute();
         //  CategoryTypeCollection cat = Categories.CategoryList;
//
//
  //         List<CategoryType> Catlist = new List<CategoryType>();
//
   //        foreach (CategoryType Cat in cat)
    //        {
  //              Catlist.Add(Cat);
    //        }
      //      return Catlist;

           
      // }
        
    }
}
