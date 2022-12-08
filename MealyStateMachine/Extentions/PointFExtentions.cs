namespace MealyStateMachine.Extentions;

public static class PointFExtentions
{
	public static SizeF Add(this PointF pt1, PointF pt2)
	{
		return new SizeF(pt1.X + pt2.X, pt1.Y + pt2.Y);
	}

	public static PointF CalculatеPerpendicularPoint(this PointF start, PointF projection, int distanceFromProjection, double len, bool side = true)
	{
		Size v1 = (start - projection) / ((float)len / 2);
		Size v2 = side ? new(-v1.Height, v1.Width) : new(v1.Height, -v1.Width);
		return projection + v2 * distanceFromProjection;
	}

	public static PointF Center(this PointF pt1, PointF pt2)
	{
		return new PointF((pt1.X + pt2.X) / 2, (pt1.Y + pt2.Y) / 2);
	}
}
