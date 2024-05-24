using ConfigurationManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationUnitTest
{
    public class UnitTest
    {
        [Fact]
        public void ConfigurationReaderUnitTest()
        {
            // Arrange
            var reader = new ConfigurationReader("SERVICE-A", "mongodb://localhost:27017", 2000);

            // Act
            var siteName = reader.GetValue<string>("SiteName");

            // Assert
            Assert.Equal("soty.io", siteName);
        }
    }
}
