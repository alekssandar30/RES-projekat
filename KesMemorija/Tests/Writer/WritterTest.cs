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
    public class WritterTest
    {
        private Writter writter;
        Mock<Writter> mockWritter;
        Mock<Value> mockValue;
        
        [SetUp]
        public void SetUp()
        {
            this.writter = new Writter();
            this.mockWritter = new Mock<Writter>();
            this.mockValue = new Mock<Value>();
        }

        //************* ManualWriteToHistory *************
        [Test]
        [TestCase("CODE_ANALOG")]
        [TestCase("CODE_DIGITAL")]
        [TestCase("CODE_CUSTOM")]
        [TestCase("CODE_LIMITSET")]
        [TestCase("CODE_SINGLENODE")]
        [TestCase("CODE_MULTIPLENODE")]
        [TestCase("CODE_CONSUMER")]
        [TestCase("CODE_SOURCE")]
        [TestCase("CODE_MOTION")]
        [TestCase("CODE_SENSOR")]
        public void ManualWriteToHistory_GoodParameters(string code)
        {
            var valueMock = new Mock<Value>("3333", 50);
            var prosledjenaVrednost = valueMock.Object;
            var writerMock = mockWritter.Object;


            Assert.DoesNotThrow(() =>
            {
                writerMock.ManualWriteToHistory(code, prosledjenaVrednost);
            });
        }

        //[Test]
        //public void ManualWriteToHistory_BadParameters_Returns_CodeError()
        //{
        //    string expectedResult = "Code ne moze biti prazan string.\n";
        //    bool exceptionTrown = false;
        //    var valueMock = new Mock<Value>("3333", 50);
        //    var val = valueMock.Object;

        //    try
        //    {
        //        writter.ManualWriteToHistory("", val);
        //    }catch(ArgumentException e)
        //    {
        //        exceptionTrown = true;
        //        Assert.AreEqual(expectedResult, e.ParamName);
        //    }

        //    Assert.That(exceptionTrown);
        //}

        //[Test]
        //public void ManualWriteToHistory_BadParameters_Returns_NullException()
        //{
        //    var expectedParamName = "CODE_ANALOG";
        //    bool exceptionThrown = false;

        //    try
        //    {
        //        writter.ManualWriteToHistory(null, null);
        //    }
        //    catch (ArgumentNullException e)
        //    {
        //        exceptionThrown = true;
        //        Assert.AreEqual(expectedParamName, e.ParamName);
        //    }

        //    Assert.That(exceptionThrown);
        //}

      


        //************* ManualWriteToDumpingBuffer *************





        // ************* ReadDataFromFile *************

    }
}
