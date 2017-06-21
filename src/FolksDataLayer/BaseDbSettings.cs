using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace FolksDataLayer
{
    /// <summary>
    /// abstract base class to give default implementation of the data specific settings to common data consumers
    /// Uses SMO to access the underlying sql server objects
    /// </summary>
    public abstract class BaseDbSettings
    {     
       
        #region constructor

        public BaseDbSettings()
        {
            //initialize database settings
            ConnectionStrings = CommonLibrary.Base.Database.ConnectionStringHelper.GetConnectionStrings();
            populateSmoSettings();
        }

        

        #endregion constructor

        #region module level variables

        ConnectionStringsSection _connectionStrings = null;

       
        Dictionary<string, CommonLibrary.DatabaseSmoObjectsAndSettings> _smoSettings = null;

        #endregion module level variables

        #region public properties

        public Dictionary<string, CommonLibrary.DatabaseSmoObjectsAndSettings> SmoSettings
        {
            get { return _smoSettings; }
            set { _smoSettings = value; }
        }

        public ConnectionStringsSection ConnectionStrings
        {
            get { return _connectionStrings; }
            set { _connectionStrings = value; }
        }

        #endregion public properties

        #region private methods

        private void populateSmoSettings()
        {
            foreach (ConnectionStringSettings conSetting in _connectionStrings.ConnectionStrings)
            {
                if (_smoSettings == null)
                {
                    _smoSettings = new Dictionary<string, CommonLibrary.DatabaseSmoObjectsAndSettings>();
                    SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(conSetting.ConnectionString);

                    _smoSettings.Add(conSetting.Name, new CommonLibrary.DatabaseSmoObjectsAndSettings(
                        sb.InitialCatalog, sb.DataSource, sb.InitialCatalog,
                        sb.UserID, sb.Password, sb.IntegratedSecurity));
                }
                else
                    if (!_smoSettings.ContainsKey(conSetting.Name))
                    {
                        SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(conSetting.ConnectionString);

                        _smoSettings.Add(conSetting.Name, new CommonLibrary.DatabaseSmoObjectsAndSettings(
                            sb.InitialCatalog, sb.DataSource, sb.InitialCatalog,
                            sb.UserID, sb.Password, sb.IntegratedSecurity));

                    }
            }
        }

        #endregion private methods
    }
}
