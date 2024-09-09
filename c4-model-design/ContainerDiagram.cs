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
			MobileApplication = contextDiagram.MonitoringSystem.AddContainer("Mobile App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la información del traslado de los lotes de vacunas.", "Swift UI");
			WebApplication = contextDiagram.MonitoringSystem.AddContainer("Web App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la información del traslado de los lotes de vacunas.", "React");
			LandingPage = contextDiagram.MonitoringSystem.AddContainer("Landing Page", "", "React");
			ApiRest = contextDiagram.MonitoringSystem.AddContainer("API REST", "API REST", "NodeJS (NestJS) port 8080");
			Database = contextDiagram.MonitoringSystem.AddContainer("DB", "", "MySQL Server RDS AWS");
		}

		private void AddRelationships() {
			contextDiagram.Ciudadano.Uses(MobileApplication, "Consulta");
			contextDiagram.Ciudadano.Uses(WebApplication, "Consulta");
			contextDiagram.Ciudadano.Uses(LandingPage, "Consulta");

			contextDiagram.Admin.Uses(MobileApplication, "Consulta");
			contextDiagram.Admin.Uses(WebApplication, "Consulta");
			contextDiagram.Admin.Uses(LandingPage, "Consulta");

			MobileApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");
			WebApplication.Uses(ApiRest, "API Request", "JSON/HTTPS");

            ApiRest.Uses(Database, "", "");
            ApiRest.Uses(contextDiagram.GoogleMaps, "API Request", "JSON/HTTPS");
            ApiRest.Uses(contextDiagram.AircraftSystem, "API Request", "JSON/HTTPS");
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
			MobileApplication.AddTags(nameof(MobileApplication));
			WebApplication.AddTags(nameof(WebApplication));
			LandingPage.AddTags(nameof(LandingPage));
			ApiRest.AddTags(nameof(ApiRest));
			Database.AddTags(nameof(Database));
		}

		private void CreateView() {
			ContainerView containerView = c4.ViewSet.CreateContainerView(contextDiagram.MonitoringSystem, "Contenedor", "Diagrama de Contenedores");
			containerView.AddAllElements();
		}
	}
}