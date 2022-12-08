using MealyStateMachine.Views;

namespace MealyStateMachine.Controls;

public class GraphicsDrawable : IDrawable
{
	public static RectF Bounds { get; set; }
	public static int Unit { get; set; } = 16;

	public void Draw(ICanvas canvas, RectF bounds)
	{
		Bounds = bounds;
		bool allStanding = true;

		foreach (var node in Items.Nodes)
		{
			node.CalculateForces();
			if (node.AdvancePosition())
			{
				allStanding = false;
				GraphPage.SetFPS(240);

				foreach (var edge in node.Edges)
				{
					edge.Adjust();
				}
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
		}
	}
}
