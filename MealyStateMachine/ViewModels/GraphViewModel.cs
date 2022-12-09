using MealyStateMachine.Controls;
using Microsoft.Maui.Graphics.Skia;
using System.Windows.Input;

namespace MealyStateMachine.ViewModels;

public partial class GraphViewModel : BaseViewModel
{
	public ICommand ShuffleGraphCommand { get; private set; }
	public ICommand СenterGraphCommand { get; private set; }
	public ICommand SaveGraphCommand { get; private set; }

	public GraphViewModel()
	{
		ShuffleGraphCommand = new Command(Shuffle);
		СenterGraphCommand = new Command(CenterGraph);
		SaveGraphCommand = new Command(SaveGraph);
	}

	public static void Shuffle()
	{
		Random random = new();
		for (int i = 0; i < Items.Nodes.Count; i++)
		{
			Node node = Items.Nodes[i];
			node.Position = new Point(
				random.Next(GraphicsDrawable.Unit, 800),
				random.Next(GraphicsDrawable.Unit, 400));
		}
	}

	public static void CenterGraph()
	{
		var graphCenter = new PointF(
				(Items.Nodes.MinBy(node => node.Position.X).Position.X +
				Items.Nodes.MaxBy(node => node.Position.X).Position.X) / 2,
				(Items.Nodes.MinBy(node => node.Position.Y).Position.Y +
				Items.Nodes.MaxBy(node => node.Position.Y).Position.Y) / 2);

		var vector = new SizeF(
			GraphicsDrawable.Bounds.Width / 2 - graphCenter.X,
			GraphicsDrawable.Bounds.Height / 2 - graphCenter.Y);

		if (vector.Width != 0 || vector.Height != 0)
		{
			foreach (var node in Items.Nodes)
				node.Position += vector;
		}
	}

	public async void SaveGraph()
	{
		SkiaBitmapExportContext skiaBitmapExportContext = new(
			(int)GraphicsDrawable.Bounds.Width,
			(int)GraphicsDrawable.Bounds.Height, 4);
		ICanvas canvas = skiaBitmapExportContext.Canvas;

		GraphicsDrawable graphicsDrawable = new();
		graphicsDrawable.Draw(canvas, GraphicsDrawable.Bounds);

		string folderName = "Graph Images";
		string mainDir = string.Empty;
#if WINDOWS
		mainDir = Path.Combine(
			Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures),
			folderName);
#endif
#if ANDROID
		mainDir = $"/storage/emulated/0/Documents/{folderName}";
#endif
		Directory.CreateDirectory(mainDir);
		string filePath = Path.Combine(mainDir, $"Graph{canvas.GetHashCode()}.png");
		skiaBitmapExportContext.WriteToFile(filePath);

		await App.Current.MainPage.DisplayAlert("Picture has been saved", filePath, "Ok");
	}
}
