using Structurizr;

namespace c4_model_design
{
	public class ContextDiagram
	{
		private readonly C4 c4;
		public SoftwareSystem MonitoringSystem { get; private set; }
		public SoftwareSystem GoogleMaps { get; private set; }
		public SoftwareSystem AircraftSystem { get; private set; }
		public Person Ciudadano { get; private set; }
		public Person Admin { get; private set; }

		public ContextDiagram(C4 c4)
		{
			this.c4 = c4;
		}

		public void Generate() {
			AddElements();
			AddRelationships();
			ApplyStyles();
			CreateView();
		}

		private void AddElements() {
			AddPeople();
			AddSoftwareSystems();
		}

		private void AddPeople()
		{
			Ciudadano = c4.Model.AddPerson("Ciudadano", "Ciudadano peruano.");
			Admin = c4.Model.AddPerson("Admin", "User Admin.");
		}

		private void AddSoftwareSystems()
		{
			MonitoringSystem = c4.Model.AddSoftwareSystem("Monitoreo del Traslado Aéreo de Vacunas SARS-CoV-2", "Permite el seguimiento y monitoreo del traslado aéreo a nuestro país de las vacunas para la COVID-19.");
			GoogleMaps = c4.Model.AddSoftwareSystem("Google Maps", "Plataforma que ofrece una REST API de información geo referencial.");
			AircraftSystem = c4.Model.AddSoftwareSystem("Aircraft System", "Permite transmitir información en tiempo real por el avión del vuelo a nuestro sistema");
		}

		private void AddRelationships() {
			Ciudadano.Uses(MonitoringSystem, "Realiza consultas para mantenerse al tanto de la planificación de los vuelos hasta la llegada del lote de vacunas al Perú");
			Admin.Uses(MonitoringSystem, "Realiza consultas para mantenerse al tanto de la planificación de los vuelos hasta la llegada del lote de vacunas al Perú");

			MonitoringSystem.Uses(AircraftSystem, "Consulta información en tiempo real por el avión del vuelo");
			MonitoringSystem.Uses(GoogleMaps, "Usa la API de google maps");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;
			
			styles.Add(new ElementStyle(nameof(Ciudadano)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
			styles.Add(new ElementStyle(nameof(Admin)) { Background = "#aa60af", Color = "#ffffff", Shape = Shape.Person });

			styles.Add(new ElementStyle(nameof(MonitoringSystem)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(GoogleMaps)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(AircraftSystem)) { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
		}

		private void SetTags()
		{
			Ciudadano.AddTags(nameof(Ciudadano));
			Admin.AddTags(nameof(Admin));

			MonitoringSystem.AddTags(nameof(MonitoringSystem));
			GoogleMaps.AddTags(nameof(GoogleMaps));
			AircraftSystem.AddTags(nameof(AircraftSystem));
		}

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(MonitoringSystem, "Contexto", "Diagrama de Contexto");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
		}
	}
}