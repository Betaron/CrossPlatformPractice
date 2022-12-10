using CommunityToolkit.Mvvm.ComponentModel;
using MealyStateMachine.Controls;
using MealyStateMachine.Creators;
using MealyStateMachine.Models;
using Newtonsoft.Json;
using System.Windows.Input;

namespace MealyStateMachine.ViewModels;

public partial class JsonViewModel : BaseViewModel
{
	[ObservableProperty]
	public string jsonString;
	[ObservableProperty]
	public string currentFile = null;
	public ICommand FileOpenCommand { get; private set; }
	public ICommand FileSaveCommand { get; private set; }
	public ICommand FileSaveAsCommand { get; private set; }
	public ICommand ApplyJsonCommand { get; private set; }


	public JsonViewModel()
	{
		FileOpenCommand = new Command(FileOpen);
		FileSaveCommand = new Command(FileSave);
		FileSaveAsCommand = new Command(FileSaveAs);
		ApplyJsonCommand = new Command(ApplyJson);
	}

	public async void FileOpen()
	{
		try
		{
			var result = await FilePicker.Default.PickAsync();
			if (result != null)
			{
				if (result.FileName.EndsWith("json", StringComparison.OrdinalIgnoreCase))
				{
					JsonString = File.ReadAllText(result.FullPath);
					CurrentFile = result.FullPath;
				}
			}
		}
		catch
		{
			// The user canceled or something went wrong
		}
	}

	public async void FileSave()
	{
		if (string.IsNullOrEmpty(CurrentFile))
		{
			FileSaveAs();
		}
		else
		{
			File.WriteAllText(CurrentFile, JsonString);
			await App.Current.MainPage.DisplayAlert("Source has been saved (╹ڡ╹ )", CurrentFile, "Ok");
		}
	}

	public async void FileSaveAs()
	{
		string folderName = "Graph Sources";
		string mainDir = string.Empty;
#if WINDOWS
		mainDir = Path.Combine(
			Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments),
			folderName);
#endif
#if ANDROID
		mainDir = $"/storage/emulated/0/Documents/{folderName}";
#endif

		string res = await App.Current.MainPage.DisplayPromptAsync("Save file ༼ つ ◕_◕ ༽つ", "File name?");
		if (string.IsNullOrEmpty(res))
			return;

		Directory.CreateDirectory(mainDir);
		string filePath = Path.Combine(mainDir, $"{res}.json");

		File.WriteAllText(filePath, JsonString);

		CurrentFile = filePath;
		await App.Current.MainPage.DisplayAlert("Source has been saved ╰(*°▽°*)╯", filePath, "Ok");
	}

	public async void ApplyJson()
	{
		List<NodeModel> nodeModels;
		try
		{
			nodeModels = JsonConvert.DeserializeObject<List<NodeModel>>(JsonString);
			Items.Nodes.Clear();
			Items.Edges.Clear();
			Items.EdgeModels.Clear();

			Items.Nodes = nodeModels.Select(x => new Node(x.NodeText)).ToList();

			foreach (var sourceNode in nodeModels)
			{
				var source = Items.Nodes.FirstOrDefault(x => x.Text == sourceNode.NodeText);
				foreach (var destNode in sourceNode.ConnectedNodes)
				{
					var dest = Items.Nodes.FirstOrDefault(x => x.Text == destNode.Key);
					Items.EdgeModels.Add(new EdgeModel(source, dest, destNode.Value));
				}
			}
		}
		catch (Exception)
		{
			await App.Current.MainPage.DisplayAlert("Failure (┬┬﹏┬┬)", "Json is not valid", "Ok(");
			return;
		}

		foreach (var item in Items.EdgeModels)
		{
			Items.Edges.Add(ItemsCreator.CreateEdge(
				item.SourceNode, item.DestNode, item.Text));
		}

		GraphViewModel.Shuffle();
		await App.Current.MainPage.DisplayAlert("Success q(≧▽≦q)", "Graph is built let's go look", "Go!");
	}
}
