using Structurizr;

namespace c4_model_design
{
	public class SharedComponentDiagram
    {
		private readonly C4 c4;
        private readonly ContextDiagram contextDiagram;
        private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";

        public Component DomainLayer { get; private set; }
        public Component InterfaceLayer { get; private set; }
        public Component InfrastructureLayer { get; private set; }

		public SharedComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
		{
			this.c4 = c4;
			this.containerDiagram = containerDiagram;
            this.contextDiagram = contextDiagram;
        }

		public void Generate() {
            AddComponents();
            AddRelationships();
            ApplyStyles();
            CreateView();
        }

        private void AddComponents()
        {
            // Definimos las capas de componentes
            DomainLayer = containerDiagram.ApiRest.AddComponent("Domain Layer Shared", "Contiene la lógica de dominio compartida, como entidades y agregados auditables.", "SpringBoot (Java)");
            InterfaceLayer = containerDiagram.ApiRest.AddComponent("Interface Layer Shared", "Gestiona las interfaces REST y OpenAPI para la exposición de servicios.", "SpringBoot (Java)");
            InfrastructureLayer = containerDiagram.ApiRest.AddComponent("Infrastructure Layer Shared", "Gestiona la persistencia de datos y la estrategia de nombramiento en base de datos.", "SpringBoot (Java)");
        }

        private void AddRelationships()
        {
            InfrastructureLayer.Uses(contextDiagram.APIAnuncios, "Carga anuncios", "");

            InfrastructureLayer.Uses(DomainLayer, "Gestiona entidades y agregados", "JPA");
            InterfaceLayer.Uses(InfrastructureLayer, "Interfaz REST que consume la capa de infraestructura", "HTTP");
        }

        private void ApplyStyles() {
            SetTags();
            Styles styles = c4.ViewSet.Configuration.Styles;
        }

		private void SetTags()
		{
            DomainLayer.AddTags(this.componentTag);
            InterfaceLayer.AddTags(this.componentTag);
            InfrastructureLayer.AddTags(this.componentTag);
        }

		private void CreateView() {
			string title = "Shared Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
			componentView.Add(this.DomainLayer);
			componentView.Add(this.InterfaceLayer);
			componentView.Add(this.InfrastructureLayer);
            componentView.Add(contextDiagram.APIAnuncios);
        }
	}
}