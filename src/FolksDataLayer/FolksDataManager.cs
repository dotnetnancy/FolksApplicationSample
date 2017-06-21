using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using log4net;
using System.Reflection;
using System.Data.SqlClient;
using Data.SingleTable.Dto;
using CommonLibrary.Base.Database;

namespace FolksDataLayer
{
    /// <summary>
    /// Class that manages the Data Layer utilizing the objects of the Active Record Pattern.  This is the Layer in 
    /// which exceptions are caught, logged to a log file or other logging target and then Friendly messages/exceptions
    /// are then bubbled up the stack as to not accidentally give sensitive information to a consumer.  Any Access to 
    /// the underlying data should go through this class to manage those interactions.
    /// </summary>
    public class FolksDataManager:BaseDbSettings
    {

        #region constants

        public const string CONNECTION_STRING_NAME_LOCAL = "FamousFolksConnectionString";

        #endregion constants

        #region module level variables

        static ILog _log = null;

        #endregion module level variables

        #region constructors

        static FolksDataManager()
        {
            _log = _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
        }

        public FolksDataManager()
        {
            
        }

        #endregion constructors

        #region public methods

        /// <summary>
        /// Retrieves all Folks from the Database
        /// </summary>
        /// <returns></returns>
        public List<Folks> GetAllFolks()
        {
            List<Folks> folksToReturn = null;

            try
            {

                //using the Active Record Pattern to model the database objects and backend CRUD ops
                BaseDataAccess<Folks> bdAccount = new BaseDataAccess<Folks>(SmoSettings[CONNECTION_STRING_NAME_LOCAL]);

                folksToReturn = bdAccount.Get(new Folks(), CommonLibrary.Enumerations.GetPermutations.AllByColumnMappings);
            }
            catch (Exception ex)
            {
                //log the exception information in a log file but do not propogate to the client, send friendly message Application Exception to 
                //bubble up the stack
                _log.Error("Exception Occurred in GetAllFolks method in FolksDataManager", ex);
                throw new ApplicationException("Exception Occurred Could not Load Famous Folks from the database");
            }
            return folksToReturn;
        }

        #endregion public methods
    }
}
