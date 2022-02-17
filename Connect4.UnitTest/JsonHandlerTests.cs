using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4.UnitTest
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

            //string input = "ABC.json";
            //object t = JsonHandler.Deserialize(input).ToString();
            //object dataOpject = new object();
            //string StrJson = "ABC";
            //var dataOpject = JsonConvert.DeserializeObject<dynamic>(StrJson);
            //Assert.That(dataOpject, Is.EqualTo(JsonHandler.Deserialize<dynamic>(StrJson)));

            object dataOpject = new object();
            string strJson = "ABC";
            
            dataOpject = JsonHandler.Deserialize<dynamic>(strJson);
            
            Assert.That(dataOpject, 



        }
    }
}
