using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
	public class C4
	{
		//Los datos del Structurizr
		private readonly long workspaceId=83843;
		private readonly string apiKey= "7525d691-9c80-4e72-971d-05252cd2ce8e";
		private readonly string apiSecret= "5bbb343a-be87-4dbe-b367-836c580ccca1";


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
            SharedComponentDiagram sharedComponentDiagram = new SharedComponentDiagram(this, containerDiagram);


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