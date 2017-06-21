using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using DataContracts;

namespace FolksServiceLayer
{
    [ServiceContract]
    public interface IFolksService
    {
        [OperationContract(Name = "GetAllFolks")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        List<FolksModel> GetAllFolks();
    }
}
