using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Reflection;
using log4net;
using DataContracts;
using FolksBusinessLayer;

namespace FolksServiceLayer
{
    [ServiceBehavior(Namespace = "http://FamousFolks.com/services/FolksServices",
           InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FolksService : IFolksService
    {
         static ILog _log = null;

         static FolksService()
         {
             _log = _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().GetType());
         }

         public List<FolksModel> GetAllFolks()
         {
             FolksBusinessManager folksBusinessManager = new FolksBusinessManager();
             return folksBusinessManager.GetAllFolks();
         }
        
    }
}
