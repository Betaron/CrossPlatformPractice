namespace MealyStateMachine.Controls;

public class Edge
{
	public Node SourceNode { get; private set; }
	public Node DestNode { get; private set; }
	public string Text { get; set; }
	public Edge Pair
	{
		get => _pair;
		set
		{
			if (_pair != value && value != null)
			{
				_pair = value;
				value.Pair = _pair;
			}
		}
	}

	private Edge _pair;
	private PointF _sourcePoint;
	private PointF _destPoint;
	private PointF _textPoint;
	private PointF[] _archPoints = new PointF[2];
	private PointF[] _arrowPoints = new PointF[3];
	private PathF _edge = new();
	private PathF _arrow = new();
	private int _strokeThikness = 4;
	private int _additionOffset = 4;
	private int _arrowSize = 12;

	public void Adjust()
	{
		SizeF CalculateOffset(SizeF delta, double len, params int[] offset)
		{
			float a = (float)(offset.Sum() / len);
			return new SizeF(delta.Width * a, delta.Height * a);
		}

		PointF CalculatеPerpendicularPoint(PointF projection, int distanceFromProjection, double len, bool side = true)
		{
			Size v1 = (_sourcePoint - projection) / ((float)len / 2);
			Size v2 = side ? new(-v1.Height, v1.Width) : new(v1.Height, -v1.Width);
			return projection + v2 * distanceFromProjection;
		}

		PointF Center(PointF pt1, PointF pt2)
		{
			return new PointF((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2);
		}

		if (SourceNode is null || DestNode is null)
		{
			return;
		}

		var delta = DestNode - SourceNode;
		var len = Math.Sqrt(
			Math.Pow(delta.Width, 2) +
			Math.Pow(delta.Height, 2));

		if (len < GraphicsDrawable.Unit * 2)
		{
			_sourcePoint = _destPoint = SourceNode.Position;
			return;
		}

		SizeF edgeOffset = CalculateOffset(delta, len, GraphicsDrawable.Unit, _strokeThikness, _additionOffset);
		_sourcePoint = SourceNode.Position + edgeOffset;
		_destPoint = DestNode.Position - edgeOffset;

		PointF center = Center(_sourcePoint, _destPoint);

		Pair = FindPair(this);
		if (Pair is not null)
		{
			_archPoints[0] = CalculatеPerpendicularPoint(
				Center(_sourcePoint, center), GraphicsDrawable.Unit * 4, len);
			_archPoints[1] = CalculatеPerpendicularPoint(
				Center(_destPoint, center), GraphicsDrawable.Unit, len);

			_edge = new();
			_edge.MoveTo(_sourcePoint);
			_edge.CurveTo(_archPoints[0], _archPoints[1], _destPoint);
		}

		_textPoint = CalculatеPerpendicularPoint(center, GraphicsDrawable.Unit, len);

		double angle = _pair is null
			? Math.Atan2(-delta.Height, delta.Width)
			: Math.Atan2(
			 _archPoints[1].Y - _destPoint.Y,
			 _destPoint.X - _archPoints[1].X);

		_arrowPoints[1] = _destPoint + new Size(
			Math.Sin(angle - Math.PI / 3) * _arrowSize,
			Math.Cos(angle - Math.PI / 3) * _arrowSize);
		_arrowPoints[2] = _destPoint + new Size(
			Math.Sin(angle - Math.PI + Math.PI / 3) * _arrowSize,
			Math.Cos(angle - Math.PI + Math.PI / 3) * _arrowSize);

		_arrow = new();
		_arrow.MoveTo(_arrowPoints[1]);
		_arrow.LineTo(_destPoint);
		_arrow.LineTo(_arrowPoints[2]);
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

	public static Edge FindPair(Edge node) =>
		Items.Edges.FirstOrDefault(it =>
		it.SourceNode == node.DestNode &&
		it.DestNode == node.SourceNode);

	public void Paint(ICanvas canvas)
	{
		Color contrastColor = App.Current.RequestedTheme == AppTheme.Light ?
			Colors.Black : Colors.White;
		var appColor = App.Colors["Tertiary"] as Color;

		canvas.StrokeLineCap = LineCap.Round;
		canvas.StrokeLineJoin = LineJoin.Round;
		canvas.StrokeColor = appColor;
		canvas.StrokeSize = _strokeThikness;
		if (Pair is null)
			canvas.DrawLine(_sourcePoint, _destPoint);
		else
			canvas.DrawPath(_edge);
		canvas.DrawPath(_arrow);

		canvas.FontSize = GraphicsDrawable.Unit;
		canvas.FontColor = contrastColor;
		canvas.DrawString(
			Text,
			_textPoint.X,
			_textPoint.Y,
			HorizontalAlignment.Center);
	}
}
