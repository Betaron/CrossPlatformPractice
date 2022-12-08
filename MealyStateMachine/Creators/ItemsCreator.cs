using MealyStateMachine.Controls;

namespace MealyStateMachine.Creators;

public static class ItemsCreator
{
	public static Edge CreateEdge(Node sourceNode, Node destNode, string text = "")
	{
		if (sourceNode == destNode)
			return new LoopedEdge(sourceNode, destNode, text);
		else if (FindPair(sourceNode, destNode))
			return new ArcuateEdge(sourceNode, destNode, text);
		else
			return new StraightEdge(sourceNode, destNode, text);
	}

	public static bool FindPair(Node sourceNode, Node destNode) =>
		Items.EdgeModels.FirstOrDefault(it =>
		it.SourceNode == destNode &&
		it.DestNode == sourceNode) is not null;
}
