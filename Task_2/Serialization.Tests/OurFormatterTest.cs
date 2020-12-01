using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serialization.Tests.ExampleModel;
using Serialization;

namespace Serialization.Tests
{
    [TestClass]
    public class OurFormatterTest
    {
        private ClassA _classA;
        private ClassB _classB;
        private ClassC _classC;
        private Stream _deserializeStream;
        private string _fileName;
        private Stream _serializeStream;
        private ClassA _testA;
        private ClassB _testB;
        private ClassC _testC;

        [TestInitialize]
        public void TestInitialize()
        {
            _classA = new ClassA("Klasa A", 2.1, null);
            _classB = new ClassB("Klasa B", 3.7f, null);
            _classC = new ClassC("Klasa C", new DateTime(2020, 12, 24), null);
            _classA.B = _classB;
            _classB.C = _classC;
            _classC.A = _classA;

            _testC = new ClassC("", new DateTime(), null);

            _fileName = "own_results.txt";
            OurFormatter formatter = new OurFormatter();
            File.Delete(_fileName);
            _serializeStream = File.Open(_fileName, FileMode.Create, FileAccess.ReadWrite);
            formatter.Serialize(_serializeStream, _classC);
            _serializeStream.Close();

            _deserializeStream = File.Open(_fileName, FileMode.Open, FileAccess.ReadWrite);
            _testC = (ClassC)formatter.Deserialize(_deserializeStream);
            _deserializeStream.Close();

            _testA = _testC.A;
            _testB = _testA.B;
        }
        [TestMethod]
        public void FormatterProperty_Test()
        {
            Assert.AreEqual("Klasa C", _testC.ClassName);
            Assert.AreEqual(new DateTime(2020, 12, 24), _testC.DateTimeValue);
            Assert.AreEqual("Klasa A", _testA.ClassName);
            Assert.AreEqual(2.1, _testA.DoubleValue);
            Assert.AreEqual("Klasa B", _testB.ClassName);
            Assert.AreEqual(3.7f, _testB.FloatValue);
        }

        [TestMethod]
        public void FormatterReference_Test_Objects()
        {
            Assert.AreSame(_testA, _testC.A);
            Assert.AreSame(_testB, _testC.A.B);
            Assert.AreSame(_testC, _testC.A.B.C);
        }

        [TestMethod]
        public void FormatterReference_Test_Fields()
        {
            Assert.AreEqual(_classC.ClassName, _testC.ClassName);
            Assert.AreEqual(_classC.DateTimeValue, _testC.DateTimeValue);
            Assert.AreEqual(_classC.A.ClassName, _testC.A.ClassName);
            Assert.AreEqual(_classC.A.DoubleValue, _testC.A.DoubleValue);
            Assert.AreEqual(_classC.A.B.ClassName, _testC.A.B.ClassName);
            Assert.AreEqual(_classC.A.B.FloatValue, _testC.A.B.FloatValue);
            Assert.AreEqual(_classC.A.B.C.DateTimeValue, _testC.DateTimeValue);
        }
    }
}