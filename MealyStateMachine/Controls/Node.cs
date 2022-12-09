namespace MealyStateMachine.Controls;

public class Node
{
	public List<Edge> Edges { get; set; } = new();
	public PointF Position { get; set; }
	public string Text { get; set; }

	private PointF _nextPosition;
	private int _weightMultiplier = 20;
	private int _velosityMultiplier = 250;

	public Node(string text = "")
	{
		Text = text;
	}

	public static SizeF operator -(Node n1, Node n2)
	{
		return n1.Position - n2.Position;
	}

	public void AddEdge(Edge edge)
	{
		Edges.Add(edge);
	}

	public void CalculateForces()
	{
		double xVelosity = 0;
		double yVelosity = 0;

		foreach (var node in Items.Nodes)
		{
			Size vector = this - node;
			var dx = vector.Width;
			var dy = vector.Height;

			double l = 2.0 * (dx * dx + dy * dy);
			if (l > 0)
			{
				xVelosity += (dx * _velosityMultiplier) / l;
				yVelosity += (dy * _velosityMultiplier) / l;
			}
		}

		double weight = (Edges.Count + 1) * _weightMultiplier;
		foreach (var edge in Edges)
		{
			Size vector = edge.SourceNode == this
				? this - edge.DestNode
				: this - edge.SourceNode;

			xVelosity -= vector.Width / weight;
			yVelosity -= vector.Height / weight;
		}

		if (Math.Abs(xVelosity) < 0.4 && Math.Abs(yVelosity) < 0.4)
		{
			xVelosity = yVelosity = 0;
		}

		_nextPosition = new PointF(
			(float)xVelosity + Position.X,
			(float)yVelosity + Position.Y);

		int margin = GraphicsDrawable.Unit * 2;
		RectF bounds = GraphicsDrawable.Bounds;
		_nextPosition.X =
			Math.Min(Math.Max(_nextPosition.X, bounds.Left + margin), bounds.Right - margin);
		_nextPosition.Y =
			Math.Min(Math.Max(_nextPosition.Y, bounds.Top + margin), bounds.Bottom - margin);
	}

	public bool AdvancePosition()
	{
		if (_nextPosition == Position)
		{
			return false;
		}

		Position = _nextPosition;
		return true;
	}

	public virtual void Paint(ICanvas canvas)
	{
		var color = App.Colors["Tertiary"] as Color;

		canvas.StrokeColor = color;
		canvas.StrokeSize = 4;
		canvas.DrawCircle(Position, GraphicsDrawable.Unit);

		canvas.FontSize = GraphicsDrawable.Unit;
		canvas.FontColor = App.Current.RequestedTheme == AppTheme.Light
			? Colors.Black
			: Colors.White;
		canvas.DrawString(
			Text,
			(float)Position.X,
			(float)Position.Y + (GraphicsDrawable.Unit / 3),
			HorizontalAlignment.Center);
	}
}
