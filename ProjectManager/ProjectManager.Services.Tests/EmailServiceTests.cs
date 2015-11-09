using Microsoft.AspNet.Identity;
using NUnit.Framework;

namespace ProjectManager.Services.Tests
{
    [TestFixture]
    public class EmailServiceTests
    {
        EmailService _sut;

        [SetUp]
        public void EmailServiceTestsSetUp()
        {

            _sut = new EmailService();
        }

        [Ignore]
        [Test]
        public void EmailService_ShouldSendEmail()
        {
            //Arrange
            //Act
            _sut.SendEmail("test@outlook.com", "TestSubject", "testBody");

            //Assert
        }
    }
}
