using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjectManager.Domain
{
    public class AppUserStore : UserStore<User>
    {
        public AppUserStore(AppContext context) : base(context)
        {
        }
    }
}
