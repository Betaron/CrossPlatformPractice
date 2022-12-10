namespace MealyStateMachine.Models;

public class NodeModel
{
	public string NodeText { get; set; } = string.Empty;
	public Dictionary<string, string> ConnectedNodes { get; set; } = new();

	public NodeModel() { }

	public NodeModel(string nodeText, Dictionary<string, string> connectedNodes)
	{
		NodeText = nodeText;
		ConnectedNodes = connectedNodes;
	}
}
