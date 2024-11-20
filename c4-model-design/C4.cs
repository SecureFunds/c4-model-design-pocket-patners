using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
	public class C4
	{
		//Los datos del Structurizr
		private readonly long workspaceId = 97755;
		private readonly string apiKey = "099b8a3a-828c-4f33-b75c-cf95633c3aa6";
		private readonly string apiSecret = "758535b4-719c-435b-815c-8c17609fb8d0";


		public StructurizrClient StructurizrClient { get; }
		public Workspace Workspace { get; }
		public Model Model { get; }
		public ViewSet ViewSet { get; }

		public C4()
		{
			string workspaceName = "C4 Model - PocketPatners";
			string workspaceDescription = "Sistema de división de gastos entre personas que conviven";
			StructurizrClient = new StructurizrClient(apiKey, apiSecret);
			Workspace = new Workspace(workspaceName, workspaceDescription);
			Model = Workspace.Model;
			ViewSet = Workspace.Views;
		}

		public void Generate() {
			ContextDiagram contextDiagram = new ContextDiagram(this);
			ContainerDiagram containerDiagram = new ContainerDiagram(this, contextDiagram);
            
			UsersComponentDiagram usersComponentDiagram = new UsersComponentDiagram(this, contextDiagram, containerDiagram);
            OperationsComponentDiagram operationsComponentDiagram = new OperationsComponentDiagram(this, contextDiagram, containerDiagram);
            GroupsComponentDiagram groupsComponentDiagram = new GroupsComponentDiagram(this, contextDiagram, containerDiagram);
            SharedComponentDiagram sharedComponentDiagram = new SharedComponentDiagram(this, contextDiagram, containerDiagram);


            contextDiagram.Generate();
			containerDiagram.Generate();
            usersComponentDiagram.Generate();
            operationsComponentDiagram.Generate();
            groupsComponentDiagram.Generate();
            sharedComponentDiagram.Generate();

            StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}