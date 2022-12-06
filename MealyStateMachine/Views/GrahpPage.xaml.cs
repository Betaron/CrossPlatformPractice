using System.Timers;

namespace MealyStateMachine.Views;

public partial class GraphPage : ContentPage
{
	public GraphPage()
	{
		InitializeComponent();

		var timer = new System.Timers.Timer(1000 / 60);
		timer.Elapsed += new ElapsedEventHandler(Redraw);
		timer.Start();
	}

	public void Redraw(object source, ElapsedEventArgs e)
	{
		graphGraphicsView.Invalidate();
	}
}