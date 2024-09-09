using Structurizr;

namespace c4_model_design
{
	public class MonitoringComponentDiagram
    {
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";
        public Component DomainLayer { get; private set; }
        public Component InterfaceLayer { get; private set; }
        public Component ApplicationLayer { get; private set; }
        public Component InfrastructureLayer { get; private set; }
        public Component GoogleMapsConnector { get; private set; }

        public MonitoringComponentDiagram(C4 c4, ContextDiagram contextDiagram, ContainerDiagram containerDiagram)
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
            DomainLayer = containerDiagram.ApiRest.AddComponent("Domain Layer Monitoring", "", "NodeJS (NestJS)");
            InterfaceLayer = containerDiagram.ApiRest.AddComponent("Interface Layer Monitoring", "", "NodeJS (NestJS)");
            ApplicationLayer = containerDiagram.ApiRest.AddComponent("Application Layer Monitoring", "", "NodeJS (NestJS)");
            InfrastructureLayer = containerDiagram.ApiRest.AddComponent("Infrastructure Layer Monitoring", "", "NodeJS (NestJS)");
            GoogleMapsConnector = containerDiagram.ApiRest.AddComponent("Google Maps Connector", "", "NodeJS (NestJS)");
        }

        private void AddRelationships() {
            InterfaceLayer.Uses(ApplicationLayer, "", "");
            ApplicationLayer.Uses(DomainLayer, "", "");
            ApplicationLayer.Uses(InfrastructureLayer, "", "");
            InfrastructureLayer.Uses(DomainLayer, "", "");
            InfrastructureLayer.Uses(containerDiagram.Database, "Usa", "");
            InfrastructureLayer.Uses(GoogleMapsConnector, "", "");
            GoogleMapsConnector.Uses(contextDiagram.GoogleMaps, "", "JSON/HTTPS");
            InfrastructureLayer.Uses(contextDiagram.AircraftSystem, "JSON/HTTPS");
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
            GoogleMapsConnector.AddTags(this.componentTag);
        }

		private void CreateView() {
			string title = "Monitoring BC Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
			componentView.Add(containerDiagram.Database);
			componentView.Add(contextDiagram.AircraftSystem);
			componentView.Add(contextDiagram.GoogleMaps);
			componentView.Add(this.DomainLayer);
			componentView.Add(this.InterfaceLayer);
			componentView.Add(this.ApplicationLayer);
			componentView.Add(this.InfrastructureLayer);
            componentView.Add(this.GoogleMapsConnector);
        }
	}
}