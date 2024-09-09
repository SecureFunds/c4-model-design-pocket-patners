using Structurizr;
using System.Runtime.InteropServices;

namespace c4_model_design
{
	public class ContextDiagram
	{
		private readonly C4 c4;
		public SoftwareSystem SplitSystem { get; private set; }
		public SoftwareSystem Firebase { get; private set; }
		public SoftwareSystem OAuth { get; private set; }
		public Person Roomate { get; private set; }

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
            Roomate = c4.Model.AddPerson("Roomate", "Persona que registra y divide gastos.");
		}

		private void AddSoftwareSystems()
		{
			SplitSystem = c4.Model.AddSoftwareSystem("PocketPartners", "Aplicación para fraccionar gastos compartidos y calcular el saldo de cada persona.");
            Firebase = c4.Model.AddSoftwareSystem("Firebase", "Plataforma en la nube que ofrece almacenamiento de datos, autenticación, hosting, y notificaciones para aplicaciones.");
            OAuth = c4.Model.AddSoftwareSystem("OAuth", "Proveedor de autenticación.");
		}

		private void AddRelationships() {
            Roomate.Uses(SplitSystem, "Registra gastos y comprueba su balance"); 
			Roomate.Uses(SplitSystem, "Cambio de moneda");

            SplitSystem.Uses(OAuth, "Autentica la cuenta de usuario");
            SplitSystem.Uses(Firebase, "Usa la plataforma de firebase para la gestión de notificaciones en tiempo real y utilizarlo como storage para alojar las imágenes");
		}

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;
			
			styles.Add(new ElementStyle(nameof(Roomate)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });

			styles.Add(new ElementStyle(nameof(SplitSystem)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(Firebase)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(OAuth)) { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
		}

		private void SetTags()
		{
            Roomate.AddTags(nameof(Roomate));

            SplitSystem.AddTags(nameof(SplitSystem));
            Firebase.AddTags(nameof(Firebase));
            OAuth.AddTags(nameof(OAuth));
		}

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(SplitSystem, "Contexto", "Diagrama de Contexto");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
		}
	}
}