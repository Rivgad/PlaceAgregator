using PlaceAgregator.Shared.Extensions;
using System.ComponentModel;
using Xunit;

namespace PlaceAgregator.UnitTests
{
    public class EnumExtensionsTests
    {
        private enum TestEnum
        {
            [Description("test1")]
            Test1,
            [Description("TEST10")]
            Test10,
        }

        [Fact]
        public void GetDescriptionAttribute_EnumTest1_Returnedtest1()
        {
            string expectedValue = "test1";

            TestEnum value = TestEnum.Test1;
            string actualValue = value.GetDescriptionAttribute();

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void GetDescriptionAttribute_EnumTest10_ReturnsTEST10()
        {
            string expectedValue = "TEST10";

            TestEnum value = TestEnum.Test10;
            string actualValue = value.GetDescriptionAttribute();

            Assert.Equal(expectedValue, actualValue);
        }
    }
}
