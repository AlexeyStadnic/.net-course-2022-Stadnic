using Services;
using Xunit;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace PracticeWithEqualsGetHashCode.Tests
{
    public class EquivalenceTestsTests
    {
        [Fact]
        public void GetHashCodeNecessityPositivTest_Test()
        {
            //Arrange
            EquivalenceTests equivalenceTests = new EquivalenceTests();
            
            //Act
            equivalenceTests.GetHashCodeNecessityPositivTest();

            //Assert
            
        }
    }
}