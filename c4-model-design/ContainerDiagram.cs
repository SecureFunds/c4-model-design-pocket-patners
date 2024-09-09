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
			ApiRest = contextDiagram.SplitSystem.AddContainer("API REST", "API REST", "Spring Boot");
			Database = contextDiagram.SplitSystem.AddContainer("DB", "", "MySQL Railway");
		}

		private void AddRelationships() {
			contextDiagram.Roomate.Uses(WebApplication, "Consulta");
			contextDiagram.Roomate.Uses(LandingPage, "Consulta");


			WebApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");

            ApiRest.Uses(Database, "", "");
            ApiRest.Uses(contextDiagram.Firebase, "API Request", "JSON/HTTPS");
            ApiRest.Uses(contextDiagram.OAuth, "API Request", "JSON/HTTPS");
		}

		private void ApplyStyles() {
			SetTags();
			Styles styles = c4.ViewSet.Configuration.Styles;
			styles.Add(new ElementStyle(nameof(MobileApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
			styles.Add(new ElementStyle(nameof(WebApplication)) { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(LandingPage)) { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
			styles.Add(new ElementStyle(nameof(ApiRest)) { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
			styles.Add(new ElementStyle(nameof(Database)) { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
		}

		private void SetTags()
		{
			WebApplication.AddTags(nameof(WebApplication));
			LandingPage.AddTags(nameof(LandingPage));
			ApiRest.AddTags(nameof(ApiRest));
			Database.AddTags(nameof(Database));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.SplitSystem, "Contenedor", "Diagrama de Contenedores");
			containerView.AddAllElements();
		}
	}
}