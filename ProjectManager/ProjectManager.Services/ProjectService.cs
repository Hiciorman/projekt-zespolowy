using System;
using System.Collections.Generic;
using System.Linq;
using ProjectManager.Domain;
using ProjectManager.Repositories.Interfaces;
using ProjectManager.Services.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace ProjectManager.Services
{
    public class ProjectService : PdfPageEventHelper, IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IList<Project> GetAll()
        {
            return _projectRepository.GetAll().ToList();
        }

        public IList<Project> GetAllByUserId(string id)
        {
            return _projectRepository.GetAllByUserId(id).ToList();
        }

        public Project FindById(Guid id)
        {
            return _projectRepository.FindById(id);
        }

        public void Add(Project project, string userId)
        {
            _projectRepository.Add(project, userId);
        }

        public void AddMember(Guid projectId, string userId)
        {
            _projectRepository.AddMember(projectId, userId);
        }
        public void RemoveMember(Guid projectId, string userId)
        {
            _projectRepository.RemoveMember(projectId, userId);
        }
        public void Update(Project project)
        {
            _projectRepository.Update(project);
        }

        public bool Remove(Guid id)
        {
            return _projectRepository.Remove(id);
        }

        public MemoryStream GenerateReport(Project project, string serverPath)
        {
            var document = new Document(PageSize.A4, 50, 50, 25, 25);
            var date = System.DateTime.Now.ToLongDateString();
            var documentName = project.Name + " Report " + date ;

            var output = new MemoryStream();
            var logoFont = FontFactory.GetFont("Verdana", 26, Font.BOLD);
            var projectNameFont = FontFactory.GetFont("Arial", 20, Font.BOLD);
            var boldTableFont = FontFactory.GetFont("Arial", 9, Font.BOLD);
            var tableFont = FontFactory.GetFont("Arial", 9, Font.NORMAL);
            var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            var headerFont = FontFactory.GetFont("Verdana", 16, Font.NORMAL);

            

            var writer = PdfWriter.GetInstance(document, output);
            writer.PageEvent = this;
            document.Open();
            #region logo
            var logoParagraph = new Paragraph("Project Manager", logoFont);
            logoParagraph.Alignment = Element.ALIGN_RIGHT;
            logoParagraph.SpacingAfter = 15;
            document.Add(logoParagraph);
            #endregion
            #region projectName
            var projectNameParagraph = new Paragraph(project.Name, projectNameFont);
            projectNameParagraph.Alignment = Element.ALIGN_CENTER;
            projectNameParagraph.SpacingAfter = 15;
            document.Add(projectNameParagraph);
            #endregion
            #region description
            var descriptionParagraph = new Paragraph(project.Description, bodyFont);
            descriptionParagraph.Alignment = Element.ALIGN_JUSTIFIED;
            descriptionParagraph.SpacingAfter = 20;
            document.Add(descriptionParagraph);
            #endregion
            #region sprintsTable
            if (project.Sprints.Count() != 0)
            {
                var sprintsParagraph = new Paragraph("Sprints", headerFont);
                sprintsParagraph.PaddingTop = 20;
                sprintsParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(sprintsParagraph);


                var sprintsTable = new PdfPTable(3);
                sprintsTable.HorizontalAlignment = 0;
                sprintsTable.SpacingBefore = 10;
                sprintsTable.SpacingAfter = 10;
                sprintsTable.DefaultCell.Border = 1;
                sprintsTable.WidthPercentage = 100;
                string[] SprintProperties = { "Name:", "Start date:", "End date:" };
                foreach (var property in SprintProperties)
                {
                    var phrase = new Phrase(property, boldTableFont);
                    var cell = new PdfPHeaderCell();
                    cell.Phrase = phrase;
                    sprintsTable.AddCell(cell);
                }
                foreach (var sprint in project.Sprints)
                {
                    #region properites
                    var name = sprint.Name ?? "";

                    string startDate;
                    if (sprint.StartDate != null)
                        startDate = sprint.StartDate.ToString();
                    else
                        startDate = "";

                    string endDate;
                    if (sprint.EndDate != null)
                        endDate = sprint.EndDate.ToString();
                    else
                        endDate = "";


                    string[] sprintProps = new string[3] { name, startDate, endDate };
                    #endregion
                    foreach (var property in sprintProps)
                    {
                        var phrase = new Phrase(property, tableFont);
                        var cell = new PdfPCell();
                        cell.Phrase = phrase;
                        cell.PaddingBottom = 5;
                        sprintsTable.AddCell(cell);
                    }
                }
                sprintsTable.SpacingAfter = 20;
                document.Add(sprintsTable);
            }
            #endregion
            #region assignmentsTable
            if (project.Assignemnts.Count() != 0)
            {
                var assignmentsParagraph = new Paragraph("Assignments", headerFont);
                assignmentsParagraph.PaddingTop = 20;
                assignmentsParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(assignmentsParagraph);


                var asssignmentsTable = new PdfPTable(9);
                asssignmentsTable.HorizontalAlignment = 0;
                asssignmentsTable.SpacingBefore = 10;
                asssignmentsTable.SpacingAfter = 10;
                asssignmentsTable.DefaultCell.Border = 1;
                asssignmentsTable.WidthPercentage = 100;
                string[] properties = { "Name:", "Owner:", "Assigned to:", "Status:", "Priority:", "Category:", "Start:", "Stop:", "Estimation:" };
                foreach (var property in properties)
                {
                    var phrase = new Phrase(property, boldTableFont);
                    var cell = new PdfPHeaderCell();
                    cell.Phrase = phrase;
                    asssignmentsTable.AddCell(cell);
                }
                foreach (var assignment in project.Assignemnts)
                {
                    #region properites
                    var name = assignment.Name ?? "";

                    string owner;
                    if (assignment.Owner != null)
                        owner = assignment.Owner.FirstName + " " + assignment.Owner.LastName;
                    else
                        owner = "";

                    string assignedTo;
                    if (assignment.AssignedTo != null && assignment.Owner != null)
                        assignedTo = assignment.AssignedTo.FirstName + " " + assignment.Owner.LastName;
                    else
                        assignedTo = "";

                    string status;
                    if (assignment.Status != null)
                        status = assignment.Status.Description;
                    else
                        status = "";

                    string priority;
                    if (assignment.Priority != null)
                        priority = assignment.Priority.Description;
                    else
                        priority = "";

                    string category;
                    if (assignment.Category != null)
                        category = assignment.Category.Description;
                    else
                        category = "";

                    string startDateTime;
                    if (assignment.StartDateTime != null)
                        startDateTime = assignment.StartDateTime.ToString();
                    else
                        startDateTime = "";

                    string stopDateTime;
                    if (assignment.StopDateTime != null)
                        stopDateTime = assignment.StopDateTime.ToString();
                    else
                        stopDateTime = "";

                    string estimation;
                    if (assignment.Estimation != null)
                        estimation = assignment.Estimation.ToString();
                    else
                        estimation = "";

                    string[] asignmentProps = new string[9] { name, owner, assignedTo, status, priority, category, startDateTime, stopDateTime, estimation };
                    #endregion
                    foreach (var property in asignmentProps)
                    {
                        var phrase = new Phrase(property, tableFont);
                        var cell = new PdfPCell();
                        cell.Phrase = phrase;
                        cell.PaddingBottom = 5;
                        asssignmentsTable.AddCell(cell);
                    }
                }
                asssignmentsTable.SpacingAfter = 20;
                document.Add(asssignmentsTable);
            }
            #endregion
            #region membersTable
            if (project.Members.Count() != 0)
            {
                var membersParagraph = new Paragraph("Members", headerFont);
                membersParagraph.PaddingTop = 20;
                membersParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(membersParagraph);


                var membersTable = new PdfPTable(3);
                membersTable.HorizontalAlignment = 0;
                membersTable.SpacingBefore = 10;
                membersTable.SpacingAfter = 10;
                membersTable.DefaultCell.Border = 1;
                membersTable.WidthPercentage = 100;
                string[] membersProperties = { "First name:", "Last name:", "E-mail adress:" };
                foreach (var property in membersProperties)
                {
                    var phrase = new Phrase(property, boldTableFont);
                    var cell = new PdfPHeaderCell();
                    cell.Phrase = phrase;
                    membersTable.AddCell(cell);
                }

                foreach (var member in project.Members)
                {
                    #region properites
                    var firstName = member.FirstName ?? "";
                    var lastName = member.LastName ?? "";
                    var eMail = member.Email ?? "";
                    string[] memberProps = new string[] { firstName, lastName, eMail };
                    #endregion
                    foreach (var property in memberProps)
                    {
                        var phrase = new Phrase(property, tableFont);
                        var cell = new PdfPCell();
                        cell.Phrase = phrase;
                        cell.PaddingBottom = 5;
                        membersTable.AddCell(cell);
                    }
                }

                document.Add(membersTable);
            }
            #endregion
           

            document.Close();
            return output;
        }

        public override void OnStartPage(PdfWriter writer, Document document)
        {

            var footerFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            PdfPTable footerTable = new PdfPTable(1);
            footerTable.DefaultCell.Border = 0;
            footerTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            var cellfooter2 = new PdfPCell(new Phrase("Raport generated: " + DateTime.Now.ToString(),footerFont));
            cellfooter2.Border = 0;
            cellfooter2.HorizontalAlignment = Element.ALIGN_CENTER;
            footerTable.AddCell(cellfooter2);
            footerTable.WriteSelectedRows(0, -1, document.LeftMargin, document.BottomMargin, writer.DirectContent);
   
        }

        public IList<User> GetUsersInProject(Guid? projectId)
        {
            if (projectId == null)
            {
                return new List<User>();
            }

            return _projectRepository.FindById((Guid) projectId).Members.ToList();
        }
    }
}
