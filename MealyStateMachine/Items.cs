using MealyStateMachine.Controls;

namespace MealyStateMachine;

public static class Items
{
	public static List<Node> Nodes { get; set; } = new()
	{
		new Node(),
		new Node(),
		new Node()
	};
	public static List<Edge> Edges { get; set; } = new()
	{
		new Edge(Nodes[0], Nodes[1]),
		new Edge(Nodes[1], Nodes[2]),
		new Edge(Nodes[2], Nodes[0]),
	};

	static Items()
	{
		Random random = new();
		for (int i = 0; i < Nodes.Count; i++)
		{
			Node node = Nodes[i];
			node.Position = new Point(random.Next(16, 400), random.Next(16, 800));
		}
	}
}
