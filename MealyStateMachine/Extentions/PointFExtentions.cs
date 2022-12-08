namespace MealyStateMachine.Extentions;

public static class PointFExtentions
{
	public static SizeF Add(this PointF pt1, PointF pt2)
	{
		return new SizeF(pt1.X + pt2.X, pt1.Y + pt2.Y);
	}
}
