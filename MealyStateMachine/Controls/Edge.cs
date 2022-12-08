using MealyStateMachine.Models;

namespace MealyStateMachine.Controls;

public abstract class Edge : EdgeModel
{
	protected PointF _sourcePoint;
	protected PointF _destPoint;
	protected PointF _textPoint;
	protected PointF[] _arrowPoints;
	protected PathF _arrow;
	protected PathF _edge;

	protected const int StrokeThikness = 4;
	protected const int AdditionOffset = 4;
	protected const int ArrowSize = 12;

	protected Color AppColor { get => App.Colors["Tertiary"] as Color; }
	protected Color ContrastColor
	{
		get =>
			App.Current.RequestedTheme == AppTheme.Light
			? Colors.Black
			: Colors.White;
	}

	public Edge(Node sourceNode, Node destNode, string text = "")
		: base(sourceNode, destNode, text)
	{
		_arrowPoints = new PointF[3];
		_arrow = new();
	}

	public SizeF CalculateOffset(SizeF delta, double len, params int[] offset)
	{
		float a = (float)(offset.Sum() / len);
		return new SizeF(delta.Width * a, delta.Height * a);
	}

	public abstract void Adjust();

	public virtual void Paint(ICanvas canvas)
	{
		canvas.StrokeLineCap = LineCap.Round;
		canvas.StrokeLineJoin = LineJoin.Round;
		canvas.StrokeColor = AppColor;
		canvas.StrokeSize = StrokeThikness;
		canvas.DrawPath(_edge);
		canvas.DrawPath(_arrow);

		canvas.FontSize = GraphicsDrawable.Unit;
		canvas.FontColor = ContrastColor;
		canvas.DrawString(
			Text,
			_textPoint.X,
			_textPoint.Y,
			HorizontalAlignment.Center);
	}
}
