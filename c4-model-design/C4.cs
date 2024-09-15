using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
	public class C4
	{
		//Los datos del Structurizr
		private readonly long workspaceId = 95259;
		private readonly string apiKey = "e488851e-362a-4899-81c7-248a15194df6";
		private readonly string apiSecret = "34529c42-3fad-469c-81a6-fe67d6d3c78b";

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
            
			GroupsComponentDiagram apiRestComponentDiagram = new GroupsComponentDiagram(this, contextDiagram, containerDiagram);
			
			contextDiagram.Generate();
			containerDiagram.Generate();
			apiRestComponentDiagram.Generate();
			StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}