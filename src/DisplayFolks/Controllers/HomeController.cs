using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using famousfolks.com;
using CommonLibrary.Utilty.Sort;
using FolksBusinessLayer;
using DataContracts;
using System.ServiceModel;
using System.Reflection;
using log4net;
using System.Threading;

namespace DisplayFolks.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

         #region module level variables

        static ILog _log = null;       
        
        #endregion module level variables       

        #region constructors

        static HomeController()
        {
            _log = _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
        }

        public HomeController()
        {
            this.ActionInvoker = new CustomErrorActionInvoker();
        }
        //
        // GET: /Home/
        #endregion constructors

        #region action methods

        
        public virtual ActionResult Index()
        {
            //no try catch because we are relying on the [HandleError] OnException handling
            //customerrors in config set to "on", but no error page exists yet, did not create one
            
            List<FolksModel> allFolksReturn = new List<FolksModel>();

            #region implementation with the wcf service commented out

            /*
            bool success = false;

             GenericList<FolksModel> genericList = new CommonLibrary.Utilty.Sort.GenericList<FolksModel>();

            //implementation using the WCF Service - do not use using block, best practice check underlying connecton
            //state, sometimes using block fails to close connection successfully to use this: remove reference to 
            //data contracts project directly and Add the FolksService.cs class in the Models directory
            //back into include in the project'you will need to change namespace from DataContracts to famousfolks.com

            try
            {
                FolksServiceClient client = new FolksServiceClient();
            
                FolksModel[] allFolks = client.GetAllFolks();
               
                genericList.AddRange(allFolks);
                genericList.Sort("LastName", CollectionSortDirection.Ascending);
                return View(genericList);

            }
            catch (TimeoutException te)
            {
                _log.Fatal(te);

            }
            catch (FaultException fe)
            {
                _log.Fatal(fe);

            }
            catch (CommunicationException ce)
            {
                _log.Fatal(ce);
            }

            catch (Exception ex)
            {

                _log.Fatal(ex);
            }

            finally
            {
                if (client != null)
                {
                    client.CloseProxy();
                }
            }

            if (!success)
            {
                throw new ApplicationException("Exception Loading FolksModel from Service Layer");
            }
             
           */

            #endregion implementation with the wcf service commented out

            #region implemented using business layer directly for simplicity

            
                FolksBusinessManager businessManager = new FolksBusinessManager();

                allFolksReturn = businessManager.GetAllFolks();
                GenericList<FolksModel> genericList = new CommonLibrary.Utilty.Sort.GenericList<FolksModel>();
                genericList.AddRange(allFolksReturn);
                genericList.Sort("LastName", CollectionSortDirection.Ascending);
            
                return View(genericList);        

            #endregion implemented using business layer directly
        }


        //public virtual ActionResult Index()
        //{
        //    throw new NotImplementedException("Index Not Implemented");
        //}

        #endregion action methods

    }
}
       

        

           