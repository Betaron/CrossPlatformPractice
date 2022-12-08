using MealyStateMachine.Controls;

namespace MealyStateMachine.Models;

public class EdgeModel
{
	public EdgeModel(Node sourceNode, Node destNode, string text)
	{
		SourceNode = sourceNode;
		DestNode = destNode;
		Text = text;
	}

	public Node SourceNode { get; protected set; }
	public Node DestNode { get; protected set; }
	public string Text { get; set; }
}
