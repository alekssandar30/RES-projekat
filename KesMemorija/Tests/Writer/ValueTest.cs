using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Writer
{
    [TestFixture]
    public class ValueTest
    {
        [Test]
        [TestCase("2311", 50)]
        [TestCase("2323", 140)]
        public void Value_GoodParameters(string id, double p)
        {
            Value val = new Value(id, p);
            Assert.AreEqual(val.IDGeoPolozaja, id);
            Assert.AreEqual(val.Potrosnja, p);
        }

        

        [Test]
        [TestCase("22125", 20)]
        [TestCase("2142142124", 20)]
        [TestCase("21", 0)]
        [TestCase("2122", -1)]
        public void Value_BadParameters(string id, double p)
        {
            //Mock<Value> mockVal = new Mock<Value>(id,p);
            //Value val = mockVal.Object;
            Assert.Throws<ArgumentException>(() => {
                Value val = new Value(id, p);
            });

           
        }


        
    }
}
