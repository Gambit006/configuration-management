using ConfigurationManagement.Extceptions;
using ConfigurationManagement.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationUnitTest
{
    public class ConvertTypeUnitTest
    {
        [Theory, InlineData("unknowntype")]
        public void ConvertToType_UndefinedDataTypeException_WhenDataTypeIsNotExist(string input)
        {
            var ex = Assert.Throws<UndefinedDataTypeException>(() => ConvertType.ConvertToType("value", input));

            Assert.Contains("Unknown type", ex.Message);

        }

        [Fact]
        public void ConvertToType_ObjectOfParameter_WhenParamaterTypeIsString()
        {
            //Arrange
            string variable = "sitename.domain";
            string typeOfVariable = "string";
            string expectedVariable = "sitename.domain";

            //Act
            var result = ConvertType.ConvertToType(variable, typeOfVariable);

            //Assert
            Assert.Equal(expectedVariable, result);
        }

        [Fact]
        public void ConvertToType_ObjectOfParameter_WhenParamaterTypeIsInt()
        {
            //Arrange
            var variable = "12";
            string typeOfVariable = "int";
            var expectedVariable = 12;

            //Act
            var result = ConvertType.ConvertToType(variable, typeOfVariable);

            //Assert
            Assert.Equal(expectedVariable, result);
        }
    }
}
