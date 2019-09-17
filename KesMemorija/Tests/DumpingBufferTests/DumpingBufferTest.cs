using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DumpingBufferTests
{
    [TestFixture]
    public class DumpingBufferTest
    {
        Mock<DumpingBuffer> dbMock=new Mock<DumpingBuffer>();

        #region SendToHistoricalConverter testovi
        [Test]
        [TestCase ("ADD", 1)]
        [TestCase ("ADD", 2)]
        [TestCase ("ADD", 3)]
        [TestCase ("ADD", 4)]
        public void SendToHistoricalConverterAddGoodParameters(string type, int dataset)
        {
            //dbMock = new Mock<DumpingBuffer>();
            DumpingBuffer dbObj = dbMock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(dataset);
            CollectionDescription cdObj = cdMock.Object;
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";
            cdObj.Dpc.dumpingPropertyList[0].DumpingValue = new Value("2222", 100);
            cdObj.Dpc.dumpingPropertyList[1].DumpingValue = new Value("1312", 500);

            dbObj.SendToHistoricalConverter(cdObj, type, dataset);

            Assert.AreEqual(dbObj.DeltaCD.AddDic[dataset].Dpc.dumpingPropertyList[0].Code, cdObj.Dpc.dumpingPropertyList[0].Code);
            Assert.AreEqual(dbObj.DeltaCD.AddDic[dataset].Dpc.dumpingPropertyList[1].Code, cdObj.Dpc.dumpingPropertyList[1].Code);
        }

        [Test]
        [TestCase("UPDATE", 1)]
        [TestCase("UPDATE", 2)]
        [TestCase("UPDATE", 3)]
        [TestCase("UPDATE", 4)]
        public void SendToHistoricalConverterUpdateGoodParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(dataset);
            CollectionDescription cdObj = cdMock.Object;
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";
            cdObj.Dpc.dumpingPropertyList[0].DumpingValue = new Value();
            cdObj.Dpc.dumpingPropertyList[1].DumpingValue = new Value();

            dbObj.SendToHistoricalConverter(cdObj, type, dataset);

            Assert.AreEqual(dbObj.DeltaCD.UpdateDic[dataset].Dpc.dumpingPropertyList[0].Code, cdObj.Dpc.dumpingPropertyList[0].Code);
            Assert.AreEqual(dbObj.DeltaCD.UpdateDic[dataset].Dpc.dumpingPropertyList[1].Code, cdObj.Dpc.dumpingPropertyList[1].Code);
        }

        [Test]
        [TestCase("REMOVE", 1)]
        [TestCase("REMOVE", 2)]
        [TestCase("REMOVE", 3)]
        [TestCase("REMOVE", 4)]
        public void SendToHistoricalConverterRemoveGoodParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(dataset);
            CollectionDescription cdObj = cdMock.Object;
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";
            cdObj.Dpc.dumpingPropertyList[0].DumpingValue = new Value();
            cdObj.Dpc.dumpingPropertyList[1].DumpingValue = new Value();

            dbObj.SendToHistoricalConverter(cdObj, type, dataset);

            Assert.AreEqual(dbObj.DeltaCD.RemoveDic[dataset].Dpc.dumpingPropertyList[0].Code, cdObj.Dpc.dumpingPropertyList[0].Code);
            Assert.AreEqual(dbObj.DeltaCD.RemoveDic[dataset].Dpc.dumpingPropertyList[1].Code, cdObj.Dpc.dumpingPropertyList[1].Code);
        }

        [Test]
        [TestCase("RMV", -1)]
        [TestCase("REMO", 2)]
        [TestCase(null, 3)]
        public void SendToHistoricalConverterRemoveBadParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(2);
            CollectionDescription cdObj = cdMock.Object;
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";

            Assert.Throws<ArgumentException>(() =>
            {
                dbObj.SendToHistoricalConverter(cdObj, type, dataset);
            });
        }

        [Test]
        [TestCase("UPDATE", -1)]
        [TestCase("UPDT", 2)]
        [TestCase(null, 3)]
        public void SendToHistoricalConverterUpdateBadParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(2);
            CollectionDescription cdObj = cdMock.Object;
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";

            Assert.Throws<ArgumentException>(() =>
            {
                dbObj.SendToHistoricalConverter(cdObj, type, dataset);
            });
        }

        [Test]
        [TestCase("AD", -1)]
        [TestCase("A", 2)]
        [TestCase("ADD", -4)]
        public void SendToHistoricalConverterAddBadParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(2);
            CollectionDescription cdObj = cdMock.Object;
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";

            Assert.Throws<ArgumentException>(() =>
            {
                dbObj.SendToHistoricalConverter(cdObj, type, dataset);
            });
        }

        [Test]
        [TestCase("ADD", 2)]
        [TestCase("ADD", 3)]
        public void SendToHistoricalConverterAddNullParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                dbObj.SendToHistoricalConverter(null, type, dataset);
            });
        }

        [Test]
        [TestCase("UPDATE", 2)]
        [TestCase("UPDATE", 3)]
        public void SendToHistoricalConverterUpdateNullParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                dbObj.SendToHistoricalConverter(null, type, dataset);
            });
        }

        [Test]
        [TestCase("REMOVE", 2)]
        [TestCase("REMOVE", 3)]
        public void SendToHistoricalConverterRemoveNullParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                dbObj.SendToHistoricalConverter(null, type, dataset);
            });
        }

        #endregion

        #region ReadFromWritter testovi

        [Test]
        [TestCase(false, "CODE_ANALOG", false)]
        [TestCase(false, null, false)]
        [TestCase(false, "CODE_SINGLENODE", false)]
        public void ReadFromWritterBadParameters(bool directly, string code, bool remove)
        {
            Mock<Value> valMock = new Mock<Value>("1515", 1000);
            DumpingBuffer dbObj = dbMock.Object;
            Value valObj = valMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            { 
                dbObj.ReadFromWritter(directly, code, null, remove);
            });
        }

        [Test]
        [TestCase(false, "CODE_DIGITAL", false, 0)]
        [TestCase(false, "CODE_CUSTOM", false, 1)]
        public void ReadFromWritterGoodParameters(bool directly, string code, bool remove, int dataset)
        {
            Mock<Value> valMock = new Mock<Value>("1312", 500);
            DumpingBuffer dbObj = dbMock.Object;
            Value valObj = valMock.Object;
            valObj.Timestamp = DateTime.Now;

            dbObj.ReadFromWritter(directly, code, valObj, remove);
            Assert.AreEqual(dbObj.CdDic[dataset].Dpc.dumpingPropertyList[0].DumpingValue.Potrosnja, valObj.Potrosnja);
        }

        [Test]
        [TestCase(false, "CODE_DIGITAL", false, 0)]
        [TestCase(false, "CODE_SENSOR", false, 4)]
        public void ReadFromWritterGoodParameters2(bool directly, string code, bool remove, int dataset)
        {
            Mock<Value> valMock = new Mock<Value>("1337", 2142);
            DumpingBuffer dbObj = dbMock.Object;
            Value valObj = valMock.Object;

            dbObj.ReadFromWritter(directly, code, valObj, remove);
            Assert.AreEqual(dbObj.CdDic[dataset].Dpc.dumpingPropertyList[0].DumpingValue.IDGeoPolozaja, valObj.IDGeoPolozaja);
        }

        #endregion

        #region CheckDeltaCD testovi
        [Test]
        [TestCase("2131", 1000)]
        [TestCase("3444,", 2000)]
        public void CheckDeltaCDDobriParametri(string koordinate, int potrosnja)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<DeltaCD> dCD_mock = new Mock<DeltaCD>();
            Mock<Value> valMock = new Mock<Value>(koordinate, potrosnja);

            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";
            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;
            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;

            Assert.True(dbObj.CheckDeltaCD(dCD_mock.Object));
        }

        [Test]
        [TestCase("2", 1000)]
        [TestCase("3,", 2000)]
        public void CheckDeltaCDLosiParametri(string koordinate, int potrosnja)
        {
            DumpingBuffer dbObj = dbMock.Object;

            Assert.False(dbObj.CheckDeltaCD(null));
        }

        [Test]
        [TestCase("2444", 1000)]
        [TestCase("3789,", 2000)]
        public void CheckDeltaCDLosiParametri2(string koordinate, int potrosnja)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<DeltaCD> dCD_mock = new Mock<DeltaCD>();
            Mock<Value> valMock = new Mock<Value>(koordinate, potrosnja);

            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[1].Code = null;
            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;
            dCD_mock.Object.AddDic[0].Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;

            Assert.False(dbObj.CheckDeltaCD(null));
        }
        #endregion

        #region FillDeltaCD
        [Test]
        [TestCase("ADD", 0)]
        public void FillDeltaCDGoodParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<DeltaCD> dCD_mock = new Mock<DeltaCD>();
            DeltaCD dCD_obj = dCD_mock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(dataset);
            CollectionDescription cdObj = cdMock.Object;
            Mock<Value> valMock = new Mock<Value>("7414", 204);
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";
            cdObj.Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;
            cdObj.Dpc.dumpingPropertyList[1].DumpingValue = valMock.Object;

            dbObj.FillDeltaCD(dCD_obj, type, cdObj, dataset);
            Assert.AreEqual(dCD_obj.AddDic[dataset].Dataset, cdObj.Dataset);
        }

        [Test]
        [TestCase("AD", 1)]
        [TestCase("ADD", -2)]
        [TestCase("REMO", 2)]
        [TestCase("UPDATE", 10)]
        public void FillDeltaCDBadParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<DeltaCD> dCD_mock = new Mock<DeltaCD>();
            DeltaCD dCD_obj = dCD_mock.Object;
            Mock<CollectionDescription> cdMock = new Mock<CollectionDescription>(2);
            CollectionDescription cdObj = cdMock.Object;
            Mock<Value> valMock = new Mock<Value>("7414", 204);
            cdObj.Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            cdObj.Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";
            cdObj.Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;
            cdObj.Dpc.dumpingPropertyList[1].DumpingValue = valMock.Object;

            Assert.Throws<ArgumentException>(() =>
            {
                dbObj.FillDeltaCD(dCD_obj, type, cdObj, dataset);
            });      
        }
        [Test]
        [TestCase("REMOVE", 2)]
        [TestCase("UPDATE", 1)]
        public void FillDeltaCDNUllParameters(string type, int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<DeltaCD> dCD_mock = new Mock<DeltaCD>();
            DeltaCD dCD_obj = dCD_mock.Object;
            Assert.Throws<ArgumentNullException>(() =>
            {
                dbObj.FillDeltaCD(dCD_obj, type, null, dataset);
            });
        }
        #endregion

        #region ClearDumpingBuffer testovi
        [Test]
        public void ClearDumpingBufferTest()
        {
            DumpingBuffer db_Obj = dbMock.Object;

            Mock<Value> valMock = new Mock<Value>("3441", 1000);

            db_Obj.CdDic[0].Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            db_Obj.CdDic[0].Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";
            db_Obj.CdDic[0].Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;
            db_Obj.CdDic[0].Dpc.dumpingPropertyList[1].DumpingValue = valMock.Object;

            db_Obj.ClearDumpingBuffer();
            Assert.AreEqual(db_Obj.CdDic[0].Dpc.dumpingPropertyList[0].Code, null);
            Assert.AreEqual(db_Obj.CdDic[0].Dpc.dumpingPropertyList[1].Code, null);
        }
        #endregion

        #region CheckDatasetValues
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void CheckDatasetValuesGoodParameters(int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;
            dbObj.CdDic[dataset].Dpc.dumpingPropertyList[0].Code = "CODE_ANALOG";
            dbObj.CdDic[dataset].Dpc.dumpingPropertyList[1].Code = "CODE_DIGITAL";

            Mock<Value> valMock = new Mock<Value>("2313", 700);

            dbObj.CdDic[dataset].Dpc.dumpingPropertyList[0].DumpingValue = valMock.Object;
            dbObj.CdDic[dataset].Dpc.dumpingPropertyList[1].DumpingValue = valMock.Object;

            Assert.True(dbObj.CheckDatasetValues(dataset));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(7)]
        public void CheckDatasetValuesBadParameters(int dataset)
        {
            DumpingBuffer dbObj = dbMock.Object;

            Assert.False(dbObj.CheckDatasetValues(dataset));
        }

        #endregion

        #region DumpingBufferDicFill testovi
        [Test]
        [TestCase(1, "")]
        [TestCase(2, null)]
        [TestCase(-10, "CODE_ANALOG")]
        public void DumpingBufferDicFillBadParameters(int dataset, string code)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<Value> mockVal = new Mock<Value>("1337", 100);

            Assert.Throws<ArgumentException>(() =>
            {
                dbObj.DumpingBufferDicFill(false, mockVal.Object, false, dataset, code);
            });
        }

        [Test]
        [TestCase(0, "CODE_ANALOG")]
        public void DumpingBufferDicFillNullParameters(int dataset, string code)
        {
            DumpingBuffer dbObj = dbMock.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                dbObj.DumpingBufferDicFill(false, null, false, dataset, code);
            });
        }

        [Test]
        [TestCase(0, "CODE_ANALOG")]
        [TestCase(1, "CODE_CUSTOM")]
        [TestCase(4, "CODE_CONSUMER")]
        public void DumpingBufferDicFillGoodParameters(int dataset, string code)
        {
            DumpingBuffer dbObj = dbMock.Object;
            Mock<Value> mockVal = new Mock<Value>("1337", 100);

            dbObj.DumpingBufferDicFill(false, mockVal.Object, false, dataset, code);
            Assert.AreEqual(dbObj.CdDic[dataset].Dpc.dumpingPropertyList[0].Code, code);
        }
        #endregion
    }
}
