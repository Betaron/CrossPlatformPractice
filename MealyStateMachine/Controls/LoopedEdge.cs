using MealyStateMachine.Extentions;

namespace MealyStateMachine.Controls;

public class LoopedEdge : Edge
{
	private InvisibleNode _fakeNode;
	private PointF[] _archPoints = new PointF[2];


	public LoopedEdge(Node sourceNode, Node destNode, string text = "")
	: base(sourceNode, destNode, text)
	{
		_fakeNode = new();
		Items.Nodes.Add(_fakeNode);
		SourceNode.AddEdge(this);
		_fakeNode.AddEdge(this);
	}

	~LoopedEdge()
	{
		if (_fakeNode is not null)
			Items.Nodes.Remove(_fakeNode);
	}

	public override void Adjust()
	{

		if (SourceNode is null || DestNode is null)
		{
			return;
		}

		var delta = _fakeNode - SourceNode;
		var len = Math.Sqrt(
			Math.Pow(delta.Width, 2) +
			Math.Pow(delta.Height, 2));

		if (len < GraphicsDrawable.Unit * 2)
		{
			_sourcePoint = _destPoint = SourceNode.Position;
			return;
		}

		SizeF edgeOffset = CalculateOffset(
			delta, len, GraphicsDrawable.Unit, StrokeThikness, AdditionOffset);
		_sourcePoint = _destPoint = SourceNode.Position + edgeOffset;

		PointF center = _sourcePoint.Center(_fakeNode.Position);

		_archPoints[0] = _sourcePoint.CalculatеPerpendicularPoint(
			center, GraphicsDrawable.Unit * 4, len, true);
		_archPoints[1] = _sourcePoint.CalculatеPerpendicularPoint(
			center, GraphicsDrawable.Unit * 4, len, false);

		_edge = new();
		_edge.MoveTo(_sourcePoint);
		_edge.CurveTo(_archPoints[0], _archPoints[1], _destPoint);

		_textPoint = _sourcePoint.CalculatеPerpendicularPoint(
			center, GraphicsDrawable.Unit, len);

		double angle = Math.Atan2(
			 _archPoints[1].Y - _destPoint.Y,
			 _destPoint.X - _archPoints[1].X);

		_arrowPoints[1] = _destPoint + new Size(
			Math.Sin(angle - Math.PI / 3) * ArrowSize,
			Math.Cos(angle - Math.PI / 3) * ArrowSize);
		_arrowPoints[2] = _destPoint + new Size(
			Math.Sin(angle - Math.PI + Math.PI / 3) * ArrowSize,
			Math.Cos(angle - Math.PI + Math.PI / 3) * ArrowSize);

		_arrow = new();
		_arrow.MoveTo(_arrowPoints[1]);
		_arrow.LineTo(_destPoint);
		_arrow.LineTo(_arrowPoints[2]);
	}
}
