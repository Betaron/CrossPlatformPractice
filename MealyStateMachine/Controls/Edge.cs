namespace MealyStateMachine.Controls;

public class Edge
{
	public Node SourceNode { get; private set; }
	public Node DestNode { get; private set; }
	public double ArrowSize { get; private set; } = 16;
	public string Text { get; set; }

	private PointF _sourcePoint;
	private PointF _destPoint;
	private PointF[] _arrowPoints = new PointF[2];
	private PointF _textPoint;
	private PathF _arrow = new();
	private int _strokeThikness = 4;
	private int _additionOffset = 4;

	public void Adjust()
	{
		if (SourceNode is null || DestNode is null)
		{
			return;
		}
		var dx = DestNode.Position.X - SourceNode.Position.X;
		var dy = DestNode.Position.Y - SourceNode.Position.Y;
		var len = Math.Sqrt(
			Math.Pow(dx, 2) +
			Math.Pow(dy, 2));

		if (len > Node.Radius * 2)
		{
			SizeF edgeOffset = new(
				(float)(dx * (Node.Radius + _strokeThikness + _additionOffset) / len),
				(float)(dy * (Node.Radius + _strokeThikness + _additionOffset) / len));
			_sourcePoint = SourceNode.Position + edgeOffset;
			_destPoint = DestNode.Position - edgeOffset;

			double angle = Math.Atan2(-dy, dx);
			_arrowPoints[0] = _destPoint + new Size(
				Math.Sin(angle - Math.PI / 3) * ArrowSize,
				Math.Cos(angle - Math.PI / 3) * ArrowSize);
			_arrowPoints[1] = _destPoint + new Size(
				Math.Sin(angle - Math.PI + Math.PI / 3) * ArrowSize,
				Math.Cos(angle - Math.PI + Math.PI / 3) * ArrowSize);

			_arrow = new();
			_arrow.MoveTo(_arrowPoints[0]);
			_arrow.LineTo(_destPoint);
			_arrow.LineTo(_arrowPoints[1]);

			PointF center = new(
				(_sourcePoint.X + _destPoint.X) / 2,
				(_sourcePoint.Y + _destPoint.Y) / 2);
			Size v1 = (_sourcePoint - center) / ((float)len / 2);
			Size v2 = new(v1.Height, -v1.Width);
			_textPoint = center + v2 * Node.Radius * 2;
		}
		else
		{
			_sourcePoint = _destPoint = SourceNode.Position;
		}
	}

	public Edge(Node sourceNode, Node destNode, string text = "")
	{
		SourceNode = sourceNode;
		DestNode = destNode;
		SourceNode.AddEdge(this);
		DestNode.AddEdge(this);
		Text = text;
		Adjust();
	}

	public void Paint(ICanvas canvas)
	{
		Adjust();

		var color = App.Colors["Tertiary"] as Color;

		canvas.StrokeColor = color;
		canvas.SetShadow(new SizeF(0, 0), 10, color);
		canvas.StrokeSize = _strokeThikness;
		canvas.StrokeLineCap = LineCap.Round;
		canvas.StrokeLineJoin = LineJoin.Round;
		canvas.DrawLine(_sourcePoint, _destPoint);
		canvas.DrawPath(_arrow);

		canvas.FontSize = Node.Radius;
		canvas.FontColor = App.Current.RequestedTheme == AppTheme.Light
			? Colors.Black
			: Colors.White;
		canvas.DrawString(
			Text,
			_textPoint.X,
			_textPoint.Y,
			HorizontalAlignment.Center);
	}
}
