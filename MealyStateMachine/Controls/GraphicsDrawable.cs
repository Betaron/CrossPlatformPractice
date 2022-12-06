namespace MealyStateMachine.Controls;

public class GraphicsDrawable : IDrawable
{
	public static RectF Bounds { get; set; }

	public GraphicsDrawable()
	{
	}

	public void Draw(ICanvas canvas, RectF bounds)
	{
		Bounds = bounds;

		foreach (var node in Items.Nodes)
		{
			node.CalculateForces();
		}

		foreach (var node in Items.Nodes)
		{
			node.AdvancePosition();
		}

		foreach (var edge in Items.Edges)
		{
			edge.Paint(canvas);
		}

		foreach (var node in Items.Nodes)
		{
			node.Paint(canvas);
		}
	}
}
