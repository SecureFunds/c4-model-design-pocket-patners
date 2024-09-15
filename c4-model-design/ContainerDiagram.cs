using Structurizr;

namespace c4_model_design
{
	public class ContainerDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		public Container MobileApplication { get; private set; }
		public Container WebApplication { get; private set; }
		public Container LandingPage { get; private set; }
		public Container ApiRest { get; private set; }
		public Container Database { get; private set; }

		//bounded context
		public Container Groups { get; private set; }
		public Container Operations { get; private set; }
		public Container Users { get; private set; }
		public Container Shared {  get; private set; }


		public ContainerDiagram(C4 c4, ContextDiagram contextDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
		}

		public void Generate() {
			AddContainers();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddContainers()
		{
			WebApplication = contextDiagram.SplitSystem.AddContainer("Web App", "Permite a los usuarios visualizar un dashboard para dividir gastos entre personas.", "Angular");
			LandingPage = contextDiagram.SplitSystem.AddContainer("Landing Page", "", "HTML, CSS y JS");
			ApiRest = contextDiagram.SplitSystem.AddContainer("RESTFul Web Services", "API REST", "Spring Boot");
			Database = contextDiagram.SplitSystem.AddContainer("DB", "", "MySQL Railway");
			//bounded context
			Groups = contextDiagram.SplitSystem.AddContainer("Groups Context", "Maneja los grupos y registra los gastos del grupo.");
            Operations = contextDiagram.SplitSystem.AddContainer("Operations Context", "Permite administrar las operaciones de cada grupo.");
            Users = contextDiagram.SplitSystem.AddContainer("Users Context", "Maneja los usuarios, roles, su información y autenticación.");
            Shared = contextDiagram.SplitSystem.AddContainer("Shared Context", "gestiona entidades comunes y garantiza la auditoría automática de las fechas de creación y modificación (createdAt, updatedAt), proporcionando trazabilidad y control de cambios en el sistema.");
        }

		private void AddRelationships() {
			contextDiagram.GroupMember.Uses(LandingPage, "Consulta");
            contextDiagram.GroupManager.Uses(LandingPage, "Consulta");

            LandingPage.Uses(WebApplication, "Consulta");


			WebApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");

            ApiRest.Uses(Database, "", "");
			ApiRest.Uses(Groups, "", "");
            ApiRest.Uses(Operations, "", "");
            ApiRest.Uses(Users, "", "");
            ApiRest.Uses(Shared, "", "");


            Users.Uses(contextDiagram.OAuth, "API Request", "JSON/HTTPS");
            Users.Uses(contextDiagram.Firebase, "API Request", "JSON/HTTPS");
            Operations.Uses(contextDiagram.Firebase, "API Request", "JSON/HTTPS");
        }

		private void ApplyStyles() {
			SetTags();
			Styles styles = c4.ViewSet.Configuration.Styles;
			styles.Add(new ElementStyle(nameof(MobileApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
			styles.Add(new ElementStyle(nameof(WebApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(LandingPage)) { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
			styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
			//Bounded Context
            styles.Add(new ElementStyle(nameof(Groups)) { Shape = Shape.Hexagon, Background = "#facc2e", Color = "#000000", Icon = "" });
            styles.Add(new ElementStyle(nameof(Operations)) { Shape = Shape.Hexagon, Background = "#facc2e", Color = "#000000", Icon = "" });
            styles.Add(new ElementStyle(nameof(Users)) { Shape = Shape.Hexagon, Background = "#facc2e", Color = "#000000", Icon = "" });
            styles.Add(new ElementStyle(nameof(Shared)) { Shape = Shape.Hexagon, Background = "#facc2e", Color = "#000000", Icon = "" });
        }

		private void SetTags()
		{
			WebApplication.AddTags(nameof(WebApplication));
			LandingPage.AddTags(nameof(LandingPage));
			ApiRest.AddTags(nameof(ApiRest));
			Database.AddTags(nameof(Database));
			//bounded context
			Groups.AddTags(nameof(Groups));
			Operations.AddTags(nameof(Operations));
			Users.AddTags(nameof(Users));
			Shared.AddTags(nameof(Shared));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.SplitSystem, "Contenedor", "Diagrama de Contenedores");
			containerView.AddAllElements();
		}
	}
}