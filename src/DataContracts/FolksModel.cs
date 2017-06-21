using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DataContracts
{
    /// <summary>
    /// This class is the Data Contract for the return of Repository Items without exposing the underlying domain/business object
    /// This is also datacontractserializable for other purposes including usage with WCF transparently
    /// </summary>
    [DataContract(Name="FolksModel", Namespace="http://FamousFolks.com/")]
    public class FolksModel
    {
        #region module level variables

        int _id = default(int);       
        string _bio = string.Empty;     
        string _birthLocation = string.Empty;       
        string _firstName = string.Empty;        
        string _lastName = string.Empty;
       
        #endregion module level variables

        #region constructors

        public FolksModel()
        {
        }
        public FolksModel(string bio, string birthLocation, string firstName, int id, string lastName)
        {
            _id = id;
            _bio = bio;
            _birthLocation = birthLocation;
            _firstName = firstName;
            _lastName = lastName;
        }

        #endregion constructors

        #region public properties / Data Members

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember]
        public string Bio
        {
            get { return _bio; }
            set { _bio = value; }
        }

        [DataMember]
        public string BirthLocation
        {
            get { return _birthLocation; }
            set { _birthLocation = value; }
        }

        [DataMember]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [DataMember]
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        #endregion public properties / Data Members

    }
}
