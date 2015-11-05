using System;
using Microsoft.AspNet.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ProjectManager.Services.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           EmailService emailService = new EmailService();
            var msg = new IdentityMessage();
            msg.Body = "TestBody";
            msg.Destination = "michal@edarex.pl";
            msg.Subject = "TestSub";
            emailService.SendAsync(msg);
        }
    }
}
