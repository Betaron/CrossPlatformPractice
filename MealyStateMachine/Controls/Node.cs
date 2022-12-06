namespace MealyStateMachine.Controls;

public class Node
{
	public List<Edge> Edges { get; set; } = new();
	public PointF Position { get; set; }
	public static int Radius { get; set; } = 16;
	public string Text { get; set; }

	private PointF _nextPosition;
	private int _weightMultiplier = 96;
	private int _velosityMultiplier = 160;

	public Node(string text = "")
	{
		Text = text;
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
			Size vector = this.Position - node.Position;
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
				? this.Position - edge.DestNode.Position
				: this.Position - edge.SourceNode.Position;

			xVelosity -= vector.Width / weight;
			yVelosity -= vector.Height / weight;
		}

		if (Math.Abs(xVelosity) < 0.1 && Math.Abs(yVelosity) < 0.1)
		{
			xVelosity = yVelosity = 0;
		}

		_nextPosition = new PointF(
			(float)xVelosity + Position.X,
			(float)yVelosity + Position.Y);

		int margin = 16;
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

	Random random = new();
	public void Paint(ICanvas canvas)
	{
		var color = App.Colors["Tertiary"] as Color;

		canvas.StrokeColor = color;
		canvas.SetShadow(new SizeF(0, 0), 10, color);
		canvas.StrokeSize = 4;
		canvas.DrawCircle((float)Position.X, (float)Position.Y, Radius);

		canvas.FontSize = Radius;
		canvas.FontColor = App.Current.RequestedTheme == AppTheme.Light
			? Colors.Black
			: Colors.White;
		canvas.DrawString(
			Text,
			(float)Position.X,
			(float)Position.Y + (Radius / 3),
			HorizontalAlignment.Center);
	}
}
