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
    public class ProjectService : IProjectService
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
            var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var tableFont = FontFactory.GetFont("Arial", 11, Font.NORMAL);
            var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            var headerFont = FontFactory.GetFont("Verdana", 16, Font.NORMAL);

            var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
            

            var writer = PdfWriter.GetInstance(document, output);
            document.Open();
            #region logo
            var logoParagraph = new Paragraph("Project Manager",logoFont);
            logoParagraph.IndentationLeft = 280;
            document.Add(logoParagraph);
            #endregion
            #region projectName
            var projectNameParagraph = new Paragraph(project.Name, projectNameFont);
            projectNameParagraph.IndentationLeft = 200;
            document.Add(projectNameParagraph);
            #endregion
            #region description
            var descriptionParagraph = new Paragraph(project.Description, bodyFont);
            descriptionParagraph.IndentationLeft = 5;
            descriptionParagraph.PaddingTop = 20;
            document.Add(descriptionParagraph);
            #endregion
            #region assignmentsTable

            var assignmentsParagraph = new Paragraph("Assingnments", headerFont);
            assignmentsParagraph.PaddingTop = 20;
            assignmentsParagraph.IndentationLeft = 200;
            document.Add(assignmentsParagraph);


            var asssignmentsTable = new PdfPTable(6);
            asssignmentsTable.HorizontalAlignment = 0;
            asssignmentsTable.SpacingBefore = 10;
            asssignmentsTable.SpacingAfter = 10;
            asssignmentsTable.DefaultCell.Border = 1;
            asssignmentsTable.WidthPercentage = 100;
            string[] properties = { "Name:", "Owner:", "Assigned to:", "Status:", "Priority:", "Category:" };
            foreach (var property in properties)
            {
                var phrase = new Phrase(property, boldTableFont);
                var cell = new PdfPHeaderCell();
                cell.Phrase = phrase;
                asssignmentsTable.AddCell(cell);
            }
            foreach (var assignment in project.Assignemnts)
            {
            # region properites
                var name = assignment.Name ?? "";

                string owner;
                if (assignment.Owner != null)
                    owner = assignment.Owner.UserName;
                else
                    owner = "";

                string assignedTo;
                if (assignment.AssignedTo != null)
                    assignedTo = assignment.AssignedTo.UserName;
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
                string[] asignmentProps = new string[6] { name, owner, assignedTo, status, priority, category };
                #endregion
                foreach (var property in asignmentProps)
                {
                    var phrase = new Phrase(property, tableFont);
                    var cell = new PdfPHeaderCell();
                    cell.Phrase = phrase;
                    cell.PaddingBottom = 5;
                    asssignmentsTable.AddCell(cell);
                }
            }
            document.Add(asssignmentsTable);
            #endregion

            document.Close();
            return output;
        }
    }
}
