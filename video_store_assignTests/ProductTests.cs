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
    public class ProductTests
    {
        [TestMethod()]
        public void InsertProductTest()
        {
            Product product = new Product("peg","4.5",2021,5,4,"action");
            product.InsertProduct();
            Assert.IsTrue(true);
        }
    }
}