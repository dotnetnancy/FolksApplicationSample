//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3623
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Business.SingleTable.Bo.List
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using CommonLibrary;
    using System.Reflection;
    
    /// <summary>
    /// This class is meant to Model a set of rows from the database and give a consistent access to the underlying database as well as do any transformations necessary
    /// to map DTO to the Business Model.  In practice the usage of these classes could be for new Business Model classes
    /// to derive from these classes and override the existing behavior when necessary and add new busines logic.  Inherits
    /// from the DTOs to give inherent access to the database and additionally add its own behavior that is business specific.
    /// </summary>
    public class Folks : List<Data.SingleTable.Dto.Folks>
    {
        
        public const string FILL_DB_SETTINGS_EXCEPTION = "Please Fill the DatabaseSmoObjectsAndSettings_Property with a filled DatabaseSmoO" +
            "bjectsAndSettings class and try again";
        
        public const string PRIMARY_KEY_NOT_FOUND_EXCEPTION_VAR_NAME = "PRIMARY_KEY_NOT_FOUND_EXCEPTION_VAR_NAME";
        
        private CommonLibrary.DatabaseSmoObjectsAndSettings _databaseSmoObjectsAndSettings;
        
        private CommonLibrary.Base.Business.BaseBusiness<Business.SingleTable.Bo.Folks, Data.SingleTable.Dto.Folks> _baseBusiness;
        
        private CommonLibrary.Base.Database.BaseDataAccess<Data.SingleTable.Dto.Folks> _baseDataAccess;
        
        public Folks()
        {
        }
        
        public Folks(CommonLibrary.DatabaseSmoObjectsAndSettings databaseSmoObjectsAndSettings)
        {
            _databaseSmoObjectsAndSettings = databaseSmoObjectsAndSettings;
            _baseDataAccess = 
                new CommonLibrary.Base.Database.BaseDataAccess<Data.SingleTable.Dto.Folks>(_databaseSmoObjectsAndSettings);
            _baseBusiness = 
                new CommonLibrary.Base.Business.BaseBusiness<Business.SingleTable.Bo.Folks, Data.SingleTable.Dto.Folks>(_databaseSmoObjectsAndSettings);
        }
        
        public virtual CommonLibrary.DatabaseSmoObjectsAndSettings DatabaseSmoObjectsAndSettings
        {
            get
            {
                return this._databaseSmoObjectsAndSettings;
            }
            set
            {
                this._databaseSmoObjectsAndSettings = value;
            }
        }
        
        private bool BaseDataAccessAvailable()
        {
            bool baseDataAccessAvailable = false;
            if ((_baseDataAccess == null))
            {
                if ((_databaseSmoObjectsAndSettings != null))
                {
                    _baseDataAccess = 
                new CommonLibrary.Base.Database.BaseDataAccess<Data.SingleTable.Dto.Folks>(_databaseSmoObjectsAndSettings);
                }
                baseDataAccessAvailable = true;
            }
            else
            {
                baseDataAccessAvailable = true;
            }
            return baseDataAccessAvailable;
        }
        
        private void FillByGetPermutation(CommonLibrary.Enumerations.GetPermutations getPermutation, Business.SingleTable.Bo.Folks filledBo)
        {
            if (this.BaseDataAccessAvailable())
            {
                this.Clear();
                Data.SingleTable.Dto.Folks dto = ((Data.SingleTable.Dto.Folks)(filledBo));
                List<Data.SingleTable.Dto.Folks> returnDto = _baseDataAccess.Get(dto, getPermutation);
                int control = returnDto.Count;
                if ((control > 0))
                {
                    int counter;
                    for (counter = 0; (counter < control); counter = (counter + 1))
                    {
                        Business.SingleTable.Bo.Folks boToFill = new Business.SingleTable.Bo.Folks(_databaseSmoObjectsAndSettings);
                        _baseBusiness.FillThisWithDto(returnDto[counter], boToFill);
                        this.Add(boToFill);
                    }
                }
            }
            else
            {
                throw new System.ApplicationException(FILL_DB_SETTINGS_EXCEPTION);
            }
        }
        
        public virtual void FillByPrimaryKey(Business.SingleTable.Bo.Folks filledBo)
        {
            CommonLibrary.Enumerations.GetPermutations getPermutation = CommonLibrary.Enumerations.GetPermutations.ByPrimaryKey;
            this.FillByGetPermutation(getPermutation, filledBo);
        }
        
        public virtual void FillByCriteriaFuzzy(Business.SingleTable.Bo.Folks filledBo)
        {
            CommonLibrary.Enumerations.GetPermutations getPermutation = CommonLibrary.Enumerations.GetPermutations.ByFuzzyCriteria;
            this.FillByGetPermutation(getPermutation, filledBo);
        }
        
        public virtual void FillByCriteriaExact(Business.SingleTable.Bo.Folks filledBo)
        {
            CommonLibrary.Enumerations.GetPermutations getPermutation = CommonLibrary.Enumerations.GetPermutations.ByExplicitCriteria;
            this.FillByGetPermutation(getPermutation, filledBo);
        }
        
        public virtual void FillByGetAll(Business.SingleTable.Bo.Folks filledBo)
        {
            CommonLibrary.Enumerations.GetPermutations getPermutation = CommonLibrary.Enumerations.GetPermutations.AllByColumnMappings;
            this.FillByGetPermutation(getPermutation, filledBo);
        }
    }
}
