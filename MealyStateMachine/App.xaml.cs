namespace MealyStateMachine
{
	public partial class App : Application
	{
		public static ResourceDictionary Colors
		{
			get => Current.Resources.MergedDictionaries.ToArray()[0];
		}

		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();
		}
	}
}