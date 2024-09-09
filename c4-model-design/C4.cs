using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
	public class C4
	{
		//Los datos del Structurizr
		private readonly long workspaceId = 94980;
		private readonly string apiKey = "03be1661-8b60-40e7-aa20-7cebea48fd58";
		private readonly string apiSecret = "8ba85afa-ec5e-4bf7-bcc2-4a968ae2e7ba";

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
            
			APIRestComponentDiagram apiRestComponentDiagram = new APIRestComponentDiagram(this, contextDiagram, containerDiagram);
			
			contextDiagram.Generate();
			containerDiagram.Generate();
			apiRestComponentDiagram.Generate();
			StructurizrClient.PutWorkspace(workspaceId, Workspace);
		}
	}
}