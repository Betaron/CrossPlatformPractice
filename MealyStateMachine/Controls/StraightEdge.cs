using MealyStateMachine.Extentions;

namespace MealyStateMachine.Controls;

public class StraightEdge : Edge
{
	public StraightEdge(Node sourceNode, Node destNode, string text = "")
		: base(sourceNode, destNode, text)
	{
		SourceNode.AddEdge(this);
		DestNode.AddEdge(this);
	}

	public override void Adjust()
	{
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

		SizeF edgeOffset = CalculateOffset(
			delta, len, GraphicsDrawable.Unit, StrokeThikness, AdditionOffset);
		_sourcePoint = SourceNode.Position + edgeOffset;
		_destPoint = DestNode.Position - edgeOffset;

		PointF center = _sourcePoint.Center(_destPoint);

		_textPoint = _sourcePoint.CalculatеPerpendicularPoint(center, GraphicsDrawable.Unit, len);

		double angle = Math.Atan2(-delta.Height, delta.Width);

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

		_edge = new();
		_edge.MoveTo(_sourcePoint);
		_edge.LineTo(_destPoint);
	}
}
