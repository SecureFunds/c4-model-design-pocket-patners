using Structurizr;
using System.Runtime.InteropServices;

namespace c4_model_design
{
	public class ContextDiagram
	{
		private readonly C4 c4;
		public SoftwareSystem SplitSystem { get; private set; }
		
		//APIS
		public SoftwareSystem Firebase { get; private set; }
		public SoftwareSystem OAuth { get; private set; }
     

		public SoftwareSystem Twillo { get; private set; }

		public SoftwareSystem  APIAnuncios { get; private set; }

		

        //Users
        public Person GroupMember { get; private set; }

        public Person GroupManager { get; private set; }

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
            GroupMember = c4.Model.AddPerson("Group Member", "Usuario que participa en el grupo, añade gastos y visualiza sus propias divisiones sin acceso a funciones administrativas.");
            GroupManager = c4.Model.AddPerson("Group Manager", "Usuario con privilegios administrativos que gestiona las divisiones de gastos, supervisa el balance del grupo y realiza ajustes segun sea necesario.");
        }

		private void AddSoftwareSystems()
		{
			SplitSystem = c4.Model.AddSoftwareSystem("PocketPartners", "Aplicacion para fraccionar gastos compartidos y calcular el saldo de cada persona.");
            Firebase = c4.Model.AddSoftwareSystem("Firebase", "Plataforma en la nube que ofrece almacenamiento de datos, autenticacion, hosting, y notificaciones para aplicaciones.");
            OAuth = c4.Model.AddSoftwareSystem("OAuth", "Proveedor de autenticación.");
            
			Twillo = c4.Model.AddSoftwareSystem("Twillo", "Plataforma de comunicación en la nube que permite a los desarrolladores integrar mensajes de texto y llamadas telefonicas en sus aplicaciones.");
			APIAnuncios = c4.Model.AddSoftwareSystem("API Externa", "API externa para obtener Anuncios de terceros");
			

        }

		private void AddRelationships() {
            GroupMember.Uses(SplitSystem, "Registra gastos y comprueba su balance");
            GroupMember.Uses(SplitSystem, "Cambio de moneda");
			GroupManager.Uses(SplitSystem, "Gestiona las divisiones de gastos");
            GroupManager.Uses(SplitSystem, "Supervisa el balanceo del grupo");
            GroupManager.Uses(SplitSystem, "Realiza ajustes");

            SplitSystem.Uses(OAuth, "Autentica la cuenta de usuario");
            SplitSystem.Uses(Firebase, "Usa la plataforma de firebase para la gestion de notificaciones en tiempo real y utilizarlo como storage para alojar las imagenes");
			SplitSystem.Uses(Twillo, "Envía mensajes de texto para notificaciones");
			SplitSystem.Uses(APIAnuncios, "Obtiene anuncios de terceros para mostrar en la aplicacion");
			
        }

		private void ApplyStyles() {
			SetTags();

			Styles styles = c4.ViewSet.Configuration.Styles;


			//Users
			styles.Add(new ElementStyle(nameof(GroupMember)) { Background = "#0ad6ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle(nameof(GroupManager)) { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });

			//APIs externas
            styles.Add(new ElementStyle(nameof(SplitSystem)) { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(Firebase)) { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(OAuth)) { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });
            
			styles.Add(new ElementStyle(nameof(Twillo)) { Background = "#ffcbdb", Color = "#ffffff", Shape = Shape.RoundedBox });
			styles.Add(new ElementStyle(nameof(APIAnuncios)) { Background = "#FFA500", Color = "#ffffff", Shape = Shape.RoundedBox });
			

        }

		private void SetTags()
		{
			//Users tags para los estilos
            GroupMember.AddTags(nameof(GroupMember));
			GroupManager.AddTags(nameof(GroupManager));

            //APIs tags para los estilos
            SplitSystem.AddTags(nameof(SplitSystem));
            Firebase.AddTags(nameof(Firebase));
            OAuth.AddTags(nameof(OAuth));
            
			Twillo.AddTags(nameof(Twillo));
			APIAnuncios.AddTags(nameof(APIAnuncios));
		

        }

		private void CreateView() {
			SystemContextView contextView = c4.ViewSet.CreateSystemContextView(SplitSystem, "Contexto", "Diagrama de Contexto");
			contextView.AddAllSoftwareSystems();
			contextView.AddAllPeople();
		}
	}
}