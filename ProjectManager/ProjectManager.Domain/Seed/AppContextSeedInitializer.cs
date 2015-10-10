using System.Linq;
using System.Data.Entity;

namespace ProjectManager.Domain.Seed
{
    public class AppContextSeedInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            #region Users
            User user = new User
            {
                UserName = "Tester",
                Email = "test@test.com"
            };

            context.Users.Add(user);
            context.SaveChanges();
            #endregion

            #region Projects
            Project project = new Project
            {
                Name = "Test",
                Description = "Projekt testowy",
                OwnerId = context.Users.FirstOrDefault().Id
            };

            context.Projects.Add(project);
            context.SaveChanges();
            #endregion

            #region Assignments
            for (int i = 0; i < 5; i++)
            {
                Assignment assignment = new Assignment
                {
                    Name = "Task " + i,
                    Description = "Opis taska " + i,
                    OwnerId = context.Users.FirstOrDefault().Id,
                    ProjectId = context.Projects.FirstOrDefault().Id
                };

                context.Assignments.Add(assignment);
            }

            context.SaveChanges();
            #endregion
        }
    }
}