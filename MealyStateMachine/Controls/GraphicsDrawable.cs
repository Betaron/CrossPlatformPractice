using MealyStateMachine.ViewModels;
using MealyStateMachine.Views;

namespace MealyStateMachine.Controls;

public class GraphicsDrawable : IDrawable
{
	public static RectF Bounds { get; set; }
	public static ICanvas Canvas { get; set; }
	public static int Unit { get; set; } = 16;

	public void Draw(ICanvas canvas, RectF bounds)
	{
		Bounds = bounds;
		Canvas = canvas;
		bool allStanding = true;

		if (Items.Nodes.Count == 0)
			return;

		foreach (var node in Items.Nodes)
		{
			node.CalculateForces();
			if (node.AdvancePosition())
			{
				allStanding = false;
				GraphPage.SetFPS(300);
			}
			foreach (var edge in node.Edges)
			{
				edge.Adjust();
			}
			node.Paint(canvas);
		}

		foreach (var edge in Items.Edges)
		{
			edge.Paint(canvas);
		}

		if (allStanding)
		{
			GraphPage.SetFPS(1);
			GraphViewModel.CenterGraph();
		}
	}
}
