using MealyStateMachine.ViewModels;
using System.Timers;

namespace MealyStateMachine.Views;

public partial class GraphPage : ContentPage
{
	private static int _fps = 1;
	public static void SetFPS(int fps)
	{
		if (_fps != fps)
		{
			_fps = fps;
			_timer.Interval = 1000 / _fps;
		}
	}

	private static System.Timers.Timer _timer = new(1000 / _fps);

	public GraphPage(GraphViewModel graphViewModel)
	{
		InitializeComponent();
		BindingContext = graphViewModel;

		_timer.Elapsed += new ElapsedEventHandler(Redraw);
		_timer.Start();
	}

	public void Redraw(object source, ElapsedEventArgs e)
	{
		graphGraphicsView.Invalidate();
	}
}
