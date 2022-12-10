using MealyStateMachine.ViewModels;

namespace MealyStateMachine.Views;

public partial class JsonPage : ContentPage
{
	public JsonPage(JsonViewModel jsonViewModel)
	{
		InitializeComponent();
		BindingContext = jsonViewModel;
	}
}