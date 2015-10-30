using System.Net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using ProjectManager.Domain;
using ProjectManager.Repositories;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services;
using ProjectManager.Services.Interfaces;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(ProjectManager.WebApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(ProjectManager.WebApp.App_Start.NinjectWebCommon), "Stop")]

namespace ProjectManager.WebApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                //repositories
                kernel.Bind<IProjectRepository>().To<ProjectRepository>();

                //services
                kernel.Bind<IProjectService>().To<ProjectService>();

                //user
                kernel.Bind<IUserStore<User>>().To<AppUserStore>();
                kernel.Bind<UserManager<User>>().ToSelf();
                kernel.Bind<IAuthenticationManager>().ToMethod(
                    c =>
                        HttpContext.Current.GetOwinContext().Authentication).InRequestScope();


                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }
    }
}
