using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.UnitTests.Connect4Tests
{
    [TestFixture]
    public class JsonHandlerTests
    {

        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Serializ_DataObject_ReturnJsonFormat()
        {
            var json = JsonHandler.Serialize("ABC");
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.Contain("ABC"));
        }

        [Test]
        public void Serializ_DataObjectIsNull_ReturnNoThing()
        {
            var json = JsonHandler.Serialize("");

            Assert.That(() => json, Throws.Nothing);
        }

        [Test]
        public void Deserialize_JsonFormat_ReturnDataOpject()
        {

            string json = @"{'Name': 'C-sharpcorner', 'Description': 'Share Knowledge' }";
            var result = JsonHandler.Deserialize<object>(json).ToString();
            
            Assert.That(result, Does.StartWith("{"));
            Assert.That(result, Does.Contain("Name"));
            Assert.That(result, Does.Contain("C-sharpcorner"));
            Assert.That(result, Does.Contain("Description"));
            Assert.That(result, Does.Contain("Share Knowledge"));
            Assert.That(result, Does.EndWith("}"));

        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        public void Deserialize_JsonFileIsEmpty_ReturnNoData(string json)
        {

            //string json = "";
            var result = JsonHandler.Deserialize<object>(json);

            Assert.That(() => result, Throws.Nothing);
           
        }
    }
}
