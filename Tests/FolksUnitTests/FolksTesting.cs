using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FolksBusinessLayer;
using DataContracts;
using System.Net;
using System.IO;

namespace FolksUnitTests
{
    /// <summary>
    /// More unit tests would be added to test things like failures, successes and the various layers directly
    /// would use more injecton of dependencies and test each domain or separation of concern individual classes
    /// </summary>
    [TestClass]
    public class FolksTesting
    {      
       
        [TestMethod]
        public void GetAllFolksFolksBusinessLayerTest()
        {
            FolksBusinessManager folksBusinessManager = new FolksBusinessManager();
            List<FolksModel> folks = folksBusinessManager.GetAllFolks();

            Assert.IsTrue(folks != null);
            Assert.IsTrue(folks.Count > 0);

        }
    }
}
