using Structurizr;
using System.Reflection;

namespace c4_model_design
{
	public class APIRestComponentDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";
        public Component CommandAssembler { get; private set; }
        public Component QueryAssembler { get; private set; }
		public Component QueryService { get; private set; }
        public Component RESTController { get; private set; }
		public Component CommandService { get; private set; }
		public Component Repositories { get; private set; }
		public Component ResourceAssembler { get; private set; }

        public APIRestComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
		{
			this.c4 = c4;
			this.contextDiagram = contextDiagram;
			this.containerDiagram = containerDiagram;
		}

		public void Generate() {
            AddComponents();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddComponents()
		{
            CommandAssembler = containerDiagram.ApiRest.AddComponent("Command Assembler", "Crea comandos para el sistema", "SpringBoot (Java22)");
            QueryAssembler = containerDiagram.ApiRest.AddComponent("Query Assembler", "Crea consultas para el sistema", "SpringBoot (Java22)");
            QueryService = containerDiagram.ApiRest.AddComponent("Query QueryService", "Provee vistas materializadas y respuestas a queries", "SpringBoot (Java22)");
            RESTController = containerDiagram.ApiRest.AddComponent("REST Controller", "Controlador general para manejar solicitudes REST", "SpringBoot (Java22)");
            CommandService = containerDiagram.ApiRest.AddComponent("Command Service", "Crea comandos para el sistema", "SpringBoot (Java22)");
			Repositories = containerDiagram.ApiRest.AddComponent("Repositories", "Permite consultar y actualizar datos en la db", "SpringBoot (Java22)");
			ResourceAssembler = containerDiagram.ApiRest.AddComponent("Resource Assembler", "Genera recursos para el sistema", "SpringBoot (Java22)");
		}

		private void AddRelationships() {
            RESTController.Uses(contextDiagram.Firebase, "Usa", "");
            RESTController.Uses(contextDiagram.OAuth, "Usa", "");
            RESTController.Uses(this.QueryAssembler, "Envía consultas generales", "");
            RESTController.Uses(this.CommandAssembler, "Envía consultas generales", "");

            CommandAssembler.Uses(this.CommandService, "Genera comandos para el sistema", "");

            QueryAssembler.Uses(this.QueryService, "Genera consultas del sistema", "");

            ResourceAssembler.Uses(this.RESTController, "Envía recursos", "");

            QueryService.Uses(this.Repositories, "Consultas de datos desde la base de datos", "");
            QueryService.Uses(this.ResourceAssembler, "Genera recursos del sistema", "");

            CommandService.Uses(this.ResourceAssembler, "Genera recursos para el sistema", "");

            Repositories.Uses(containerDiagram.Database, "JPA", "Java Persitence API");
            Repositories.Uses(this.QueryService, "Retorna el resultado de las consultas de la bd", "");
        }

        private void ApplyStyles() {
			SetTags();
			Styles styles = c4.ViewSet.Configuration.Styles;
			styles.Add(new ElementStyle(this.componentTag) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
		}

		private void SetTags()
		{
            CommandAssembler.AddTags(this.componentTag);
            QueryAssembler.AddTags(this.componentTag);
            QueryService.AddTags(this.componentTag);
            RESTController.AddTags(this.componentTag);
            CommandService.AddTags(this.componentTag);
            Repositories.AddTags(this.componentTag);
            ResourceAssembler.AddTags(this.componentTag);
		}

		private void CreateView() {
			string title = "API Rest Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
			componentView.Add(containerDiagram.Database);
			componentView.Add(contextDiagram.OAuth);
			componentView.Add(contextDiagram.Firebase);
			componentView.AddAllComponents();
		}
	}
}