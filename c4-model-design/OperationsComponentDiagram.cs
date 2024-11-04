using Structurizr;

namespace c4_model_design
{
	public class OperationsComponentDiagram
    {
		private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";

        public Component DomainLayer { get; private set; }
        public Component InterfaceLayer { get; private set; }
        public Component ApplicationLayer { get; private set; }
        public Component InfrastructureLayer { get; private set; }

        public OperationsComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            DomainLayer = containerDiagram.ApiRest.AddComponent("Domain Layer Operations", "Gestiona la l�gica de dominio de las operaciones", "SpringBoot (Java22)");
            InterfaceLayer = containerDiagram.ApiRest.AddComponent("Interface Layer Operations", "Proporciona las interfaces para interactuar con el dominio de las operaciones", "SpringBoot (Java22)");
            ApplicationLayer = containerDiagram.ApiRest.AddComponent("Application Layer Operations", "Gestiona el flujo de la aplicaci�n para las operaciones", "SpringBoot (Java22)");
            InfrastructureLayer = containerDiagram.ApiRest.AddComponent("Infrastructure Layer Operations", "Gestiona la interacci�n con las tecnolog�as de infraestructura, como bases de datos", "SpringBoot (Java22)");
        }

        private void AddRelationships()
        {
            InfrastructureLayer.Uses(contextDiagram.Firebase, "Guarda los recibos de los pagos en Firebase", "");
            

            InterfaceLayer.Uses(ApplicationLayer, "Env�a solicitudes de interfaz al Application Layer", "");
            ApplicationLayer.Uses(DomainLayer, "Llama a la l�gica de dominio", "");
            ApplicationLayer.Uses(InfrastructureLayer, "Gestiona interacciones con la infraestructura", "");
            InfrastructureLayer.Uses(DomainLayer, "Consulta y actualiza datos de la l�gica de dominio", "");
            InfrastructureLayer.Uses(containerDiagram.Database, "Accede y modifica la base de datos", "");
        }

        private void ApplyStyles() {
			SetTags();
		}

		private void SetTags()
		{
            DomainLayer.AddTags(this.componentTag);
            InterfaceLayer.AddTags(this.componentTag);
            ApplicationLayer.AddTags(this.componentTag);
            InfrastructureLayer.AddTags(this.componentTag);
        }

		private void CreateView() {
			string title = "Operations Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
			componentView.Add(containerDiagram.Database);
			componentView.Add(this.DomainLayer);
			componentView.Add(this.InterfaceLayer);
			componentView.Add(this.ApplicationLayer);
			componentView.Add(this.InfrastructureLayer);
            componentView.Add(contextDiagram.Firebase);
        
        }
	}
}