using MealyStateMachine.Controls;
using MealyStateMachine.Models;

namespace MealyStateMachine;

public static class Items
{
	public static List<Edge> Edges { get; set; } = new();
	public static List<EdgeModel> EdgeModels { get; set; } = new();
	public static List<Node> Nodes { get; set; } = new();
}
