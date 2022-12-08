using MealyStateMachine.Controls;

namespace MealyStateMachine;

public static class Items
{
	public static List<Node> Nodes { get; set; } = new()
	{
		new Node("a1"),
		new Node("a2"),
		new Node("a3"),
		new Node("a4"),
		new Node("a5"),
		new Node("a6"),
		new Node("a7"),
		new Node("a8"),
	};
	public static List<Edge> Edges { get; set; } = new()
	{
		new Edge(Nodes[0], Nodes[1], "0/1"),
		new Edge(Nodes[1], Nodes[2], "0/0"),
		new Edge(Nodes[2], Nodes[0], "1/1"),
		new Edge(Nodes[3], Nodes[0], "0/1"),
		new Edge(Nodes[3], Nodes[2], "1/0"),
		new Edge(Nodes[1], Nodes[0], "0/0"),
		new Edge(Nodes[0], Nodes[4], "1/1"),
		new Edge(Nodes[0], Nodes[5], "0/1"),
		new Edge(Nodes[0], Nodes[6], "1/0"),
		new Edge(Nodes[0], Nodes[7], "0/0"),
		new Edge(Nodes[5], Nodes[6], "0/0"),
	};

	static Items()
	{
		Random random = new();
		for (int i = 0; i < Nodes.Count; i++)
		{
			Node node = Nodes[i];
			node.Position = new Point(random.Next(16, 800), random.Next(16, 400));
		}
	}
}
