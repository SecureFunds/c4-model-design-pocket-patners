using Structurizr;

namespace c4_model_design
{
	public class GroupsComponentDiagram
	{
		private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";

        public Component DomainLayer { get; private set; }
        public Component InterfaceLayer { get; private set; }
		public Component ApplicationLayer { get; private set; }
        public Component InfrastructureLayer { get; private set; }

        public GroupsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            DomainLayer = containerDiagram.ApiRest.AddComponent("Domain Layer Groups", "Gestiona la lógica de dominio de los grupos", "SpringBoot (Java22)");
            InterfaceLayer = containerDiagram.ApiRest.AddComponent("Interface Layer Groups", "Proporciona las interfaces para interactuar con el dominio de los grupos", "SpringBoot (Java22)");
            ApplicationLayer = containerDiagram.ApiRest.AddComponent("Application Layer Groups", "Gestiona el flujo de la aplicación para los grupos", "SpringBoot (Java22)");
            InfrastructureLayer = containerDiagram.ApiRest.AddComponent("Infrastructure Layer Groups", "Gestiona la interacción con las tecnologías de infraestructura, como bases de datos", "SpringBoot (Java22)");
        }

        private void AddRelationships()
        {
            InfrastructureLayer.Uses(contextDiagram.Firebase, "Guarda imágenes de los grupos en Firebase", "");
            
            InterfaceLayer.Uses(contextDiagram.Twillo, "Envía mensaje de textos", "");
            
            InterfaceLayer.Uses(ApplicationLayer, "Envía solicitudes de interfaz al Application Layer", "");
            ApplicationLayer.Uses(DomainLayer, "Llama a la lógica de dominio", "");
            ApplicationLayer.Uses(InfrastructureLayer, "Gestiona interacciones con la infraestructura", "");
            InfrastructureLayer.Uses(DomainLayer, "Consulta y actualiza datos de la lógica de dominio", "");
            InfrastructureLayer.Uses(containerDiagram.Database, "Accede y modifica la base de datos", "");
        }

        private void ApplyStyles() {
			SetTags();
			Styles styles = c4.ViewSet.Configuration.Styles;
			//styles.Add(new ElementStyle(this.componentTag) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
		}

		private void SetTags()
		{
			DomainLayer.AddTags(this.componentTag);
            InterfaceLayer.AddTags(this.componentTag);
            ApplicationLayer.AddTags(this.componentTag);
            InfrastructureLayer.AddTags(this.componentTag);
		}

		private void CreateView() {
			string title = "Groups Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
			componentView.Add(containerDiagram.Database);
			componentView.Add(this.DomainLayer);
			componentView.Add(this.InterfaceLayer);
			componentView.Add(this.ApplicationLayer);
			componentView.Add(this.InfrastructureLayer);
            componentView.Add(contextDiagram.Firebase);
            componentView.Add(contextDiagram.Twillo);
        }
	}
}