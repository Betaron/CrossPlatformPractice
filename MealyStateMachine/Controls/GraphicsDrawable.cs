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
			var graphCenter = new PointF(
				(Items.Nodes.MinBy(node => node.Position.X).Position.X +
				Items.Nodes.MaxBy(node => node.Position.X).Position.X) / 2,
				(Items.Nodes.MinBy(node => node.Position.Y).Position.Y +
				Items.Nodes.MaxBy(node => node.Position.Y).Position.Y) / 2);

			var vector = new SizeF(
				Bounds.Width / 2 - graphCenter.X,
				Bounds.Height / 2 - graphCenter.Y);

			if (vector.Width != 0 || vector.Height != 0)
			{
				foreach (var node in Items.Nodes)
					node.Position += vector;
			}
		}
	}
}
