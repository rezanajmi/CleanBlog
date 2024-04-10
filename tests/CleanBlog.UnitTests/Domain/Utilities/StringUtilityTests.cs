using CleanBlog.Domain.Abstractions;
using CleanBlog.Domain.SharedKernel.Utilities;

namespace CleanBlog.UnitTests.Domain.Utilities
{
    public class StringUtilityTests
    {
        [Fact]
        public void GenerateCacheKeyWithObject_Should_Return_Correct_Key()
        {
            // Arrange
            var obj = new MyCustomObject
            {
                Infixes = new List<string> { "abc", "def" },
                SomeProperty = "Hello",
                AnotherProperty = 42
            };

            // Act
            var result = StringUtility.GenerateChacheKeyWithObject(obj);

            // Assert
            Assert.Equal("MyCustomObject#abc&def#SomeProperty:Hello&AnotherProperty:42&", result);
        }
    }

    internal class MyCustomObject : IUseCache
    {
        public List<string> Infixes { get; set; }
        public string SomeProperty { get; set; }
        public int AnotherProperty { get; set; }
    }
}
