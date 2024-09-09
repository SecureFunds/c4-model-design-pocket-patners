using Structurizr;

namespace c4_model_design
{
	public class APIRestComponentDiagram
	{
		private readonly C4 c4;
		private readonly ContextDiagram contextDiagram;
		private readonly ContainerDiagram containerDiagram;
        private readonly string componentTag = "Component";
        public Component Aircrafts { get; private set; }
        public Component Airports { get; private set; }
		public Component Flights { get; private set; }
        public Component Monitoring { get; private set; }
		public Component Vaccines { get; private set; }
		public Component Security { get; private set; }
		public Component SharedKernel { get; private set; }

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
			Aircrafts = containerDiagram.ApiRest.AddComponent("Aircrafts", "", "NodeJS (NestJS)");
            Airports = containerDiagram.ApiRest.AddComponent("Airports", "", "NodeJS (NestJS)");
            Flights = containerDiagram.ApiRest.AddComponent("Flights", "", "NodeJS (NestJS)");
            Monitoring = containerDiagram.ApiRest.AddComponent("Monitoring", "", "NodeJS (NestJS)");
			Vaccines = containerDiagram.ApiRest.AddComponent("Vaccines", "", "NodeJS (NestJS)");
			Security = containerDiagram.ApiRest.AddComponent("Security", "", "NodeJS (NestJS)");
			SharedKernel = containerDiagram.ApiRest.AddComponent("Shared Kernel", "", "NodeJS (NestJS)");
		}

		private void AddRelationships() {
			Aircrafts.Uses(containerDiagram.Database, "Usa", "");
			Aircrafts.Uses(this.SharedKernel, "Usa", "");

			Airports.Uses(containerDiagram.Database, "Usa", "");
			Airports.Uses(this.SharedKernel, "Usa", "");
			
			Flights.Uses(containerDiagram.Database, "Usa", "");
			Flights.Uses(this.SharedKernel, "Usa", "");

			Monitoring.Uses(containerDiagram.Database, "Usa", "");
			Monitoring.Uses(this.SharedKernel, "Usa", "");
			Monitoring.Uses(contextDiagram.GoogleMaps, "Usa", "");
			Monitoring.Uses(contextDiagram.AircraftSystem, "Usa", "");
			
			Vaccines.Uses(containerDiagram.Database, "Usa", "");
			Vaccines.Uses(this.SharedKernel, "Usa", "");

			Security.Uses(containerDiagram.Database, "Usa", "");
			Security.Uses(this.SharedKernel, "Usa", "");
        }

        private void ApplyStyles() {
			SetTags();
			Styles styles = c4.ViewSet.Configuration.Styles;
			styles.Add(new ElementStyle(this.componentTag) { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
		}

		private void SetTags()
		{
			Aircrafts.AddTags(this.componentTag);
            Airports.AddTags(this.componentTag);
            Flights.AddTags(this.componentTag);
            Monitoring.AddTags(this.componentTag);
			Vaccines.AddTags(this.componentTag);
			Security.AddTags(this.componentTag);
			SharedKernel.AddTags(this.componentTag);
		}

		private void CreateView() {
			string title = "API Rest Component Diagram";
			ComponentView componentView = c4.ViewSet.CreateComponentView(containerDiagram.ApiRest, title, title);
			componentView.Title = title;
			componentView.Add(containerDiagram.Database);
			componentView.Add(contextDiagram.AircraftSystem);
			componentView.Add(contextDiagram.GoogleMaps);
			componentView.AddAllComponents();
		}
	}
}