using System.Diagnostics;
using MyWorkoutPal.Services;
using MyWorkoutPal.Models;
using MyWorkoutPal.Views;

namespace MyWorkoutPal;

public partial class MainPage : ContentPage
{
	private readonly IDataService _dataService;

	public MainPage(IDataService dataService)
	{
		InitializeComponent();
		_dataService = dataService;
	}

	protected async override void OnAppearing()
	{
		base.OnAppearing();
		collectionView.ItemsSource = await _dataService.GetAllExercicesAsync();
	}

	async void OnAddExerciceClicked(object sender, EventArgs e)
	{
		Debug.WriteLine("---> Add button clicked!");

		var navigationParameter = new Dictionary<string, object>
		{
			{ nameof(Exercice), new Exercice() }
		};

		await Shell.Current.GoToAsync(nameof(MyExercicesPage), navigationParameter);
	}

	async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
	{
        Debug.WriteLine("---> Item changed clicked!");

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Exercice), e.CurrentSelection.FirstOrDefault() as Exercice }
        };

        await Shell.Current.GoToAsync(nameof(MyExercicesPage), navigationParameter);
    }

}

