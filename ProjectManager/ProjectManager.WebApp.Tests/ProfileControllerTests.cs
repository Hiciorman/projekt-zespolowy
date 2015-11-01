using System.IO;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NUnit.Framework;
using Moq;
using ProjectManager.Domain;
using ProjectManager.WebApp.Controllers;
using ProjectManager.WebApp.Models;

namespace ProjectManager.WebApp.Tests
{
    [TestFixture]
    public class ProfileControllerTests
    {
        ProfileController _sut;
        Mock<IUserStore<User>> _userStore;
        Mock<IAuthenticationManager> _authenticationManager;
        Mock<ApplicationUserManager> _userManager;
        Mock<ApplicationSignInManager> _signInManager;

        [SetUp]
        public void ProfileControllerTestsSetUp()
        {
            _userStore = new Mock<IUserStore<User>>();
            _authenticationManager = new Mock<IAuthenticationManager>();
            _userManager = new Mock<ApplicationUserManager>(_userStore.Object);
            _signInManager = new Mock<ApplicationSignInManager>(_userManager.Object, _authenticationManager.Object);
            _sut = new ProfileController(_userManager.Object, _signInManager.Object);
        }

        [Test]
        public async void ProfileController_RegisterWithValidData()
        {
            //Arange
            var model = new RegisterViewModel { Password = "tester12345", PasswordAgain = "tester12345", Username = "Tester" };

            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            
            //Act
            var result = await _sut.Register(model) as RedirectToRouteResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(0, _sut.ModelState.Count);
            Assert.AreEqual("Start", result.RouteValues["action"]);
            Assert.AreEqual("Main", result.RouteValues["controller"]);
        }

        [Test]
        public async void ProfileController_RegisterWithValidDataWithModelError()
        {
            //Arange
            var model = new RegisterViewModel { Password = "test12345", PasswordAgain = "test12345", Username = "Tester" };

            _sut.ModelState.AddModelError("error", "error");

            //Act
            var result = await _sut.Register(model) as ViewResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(model, result.Model);
            Assert.AreEqual(1, _sut.ModelState.Count);
        }


        [Test]
        public async void ProfileController_RegisterWithInValidData()
        {
            //Arange
            var model = new RegisterViewModel { Password = "short", PasswordAgain = "short", Username = "Tester" };

            _userManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed());
            
            //Act
            var result = await _sut.Register(model) as ViewResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(1, _sut.ModelState.Count);
        }

        [Test]
        public async void ProfileController_RegisterWithInValidDataWithModelError()
        {
            //Arange
            var model = new RegisterViewModel { Password = "short", PasswordAgain = "short", Username = "Tester" };

            _sut.ModelState.AddModelError("error", "error");

            //Act
            var result = await _sut.Register(model) as ViewResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.AreEqual(model, result.Model);
            Assert.AreEqual(1, _sut.ModelState.Count);
        }
    }
}
