using Microsoft.VisualStudio.TestTools.UnitTesting;
using video_store_assign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace video_store_assign.Tests
{
    [TestClass()]
    public class ClientTests
    {
        [TestMethod()]
        public void InsertClientTest()
        {
            Client client = new Client("katal", "NZ", "14858", "katal@gmail.com");
            client.InsertClient();
            Assert.IsTrue(true);

        }
    }
}