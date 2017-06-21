using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using log4net;
using System.Data.SqlClient;
using FolksDataLayer;
using DataContracts;
using CommonLibrary.Base.Database;
using CommonLibrary.Base.Business;
using Data.SingleTable.Dto;

namespace FolksBusinessLayer
{
    /// <summary>
    /// Utilizing the Repository Pattern objects which are Business Objects that can contain business logic and access
    /// to do CRUD operations through the DataLayer.  The Business Layer is in a separate assembly and references the DataLayer
    /// through the relevant DataManager object.  This is the Layer in 
    /// which exceptions are caught, logged to a log file or other logging target and then Friendly messages/exceptions
    /// are then bubbled up the stack as to not accidentally give sensitive information to a consumer.Any Access to 
    /// the underlying Repository Pattern Business objects and logic should go through this class to manage those interactions.
    /// </summary>
    public class FolksBusinessManager:BaseDbSettings
    {
        #region constants

        public const string CONNECTION_STRING_NAME_LOCAL = "FamousFolksConnectionString";

        #endregion constants

        #region module level variables

        static ILog _log = null;       
        
        #endregion module level variables       

        #region constructors

        static FolksBusinessManager()
        {
            _log = _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
        }

        public FolksBusinessManager()
        {            
        }

        #endregion constructors

        #region public methods
        /// <summary>
        /// Returns all Folks in the database
        /// </summary>
        /// <returns></returns>
        public List<FolksModel> GetAllFolks()
        {
            try
            {
                //using the respository pattern which models my data access layer
                Business.SingleTable.Bo.List.Folks boList =
                    new Business.SingleTable.Bo.List.Folks(SmoSettings[CONNECTION_STRING_NAME_LOCAL]);

                boList.FillByGetAll(new Business.SingleTable.Bo.Folks(SmoSettings[CONNECTION_STRING_NAME_LOCAL]));

                //this is a separation of the business model itself from the objects that are to be consumed from 
                //the Controller in the MVC pattern or the WCF Service Layer
                return ConvertBusinessObjectsToDataContracts(boList);
            }
            catch (Exception ex)
            {
                //log the exception information in a log file but do not propogate to the client, send friendly message Application Exception to 
                //bubble up the stack
                _log.Error("Exception in GetAllFolks Method in FolksBusinessManager", ex);
                throw new ApplicationException("Exception Occurred Cannot Load Famous Folks");
            }
        }

        private List<FolksModel> ConvertBusinessObjectsToDataContracts(Business.SingleTable.Bo.List.Folks boList)
        {
            return boList.ConvertAll(new Converter<Folks,FolksModel>(ConvertBoToDataContract));
        }

        private FolksModel ConvertBoToDataContract(Folks folksDto)
        {
            return new FolksModel(folksDto.Bio, folksDto.BirthLocation, folksDto.FirstName, folksDto.ID, folksDto.LastName);
        }       
        #endregion public methods    


    }
}
