using System.Diagnostics;
using MyWorkoutPal.Services;
using MyWorkoutPal.Models;

namespace MyWorkoutPal.Views;

[QueryProperty(nameof(Exercice), "Exercice")]
public partial class MyExercicesPage : ContentPage
{
	private readonly IDataService _dataService;
	Exercice _exercice;
	bool _isNew;

	public Exercice Exercice
	{
		get => _exercice;
		set
		{
			_isNew = IsNew(value);
			_exercice = value;
			OnPropertyChanged();
		}
	}

	public MyExercicesPage(IDataService dataService)
	{
		InitializeComponent();
		_dataService = dataService;
		BindingContext = this;
	}

	bool IsNew(Exercice exercice)
	{
		if( exercice.Id == 0 )
			return true;
		return false;
	}

	async void OnSaveButtonClicked(object sender, EventArgs e)
	{
		if (_isNew)
		{
			await _dataService.AddExerciceAsync(Exercice);
		}
		else
		{
            await _dataService.UpdateExerciceAsync(Exercice);
        }
        await Shell.Current.GoToAsync("..");
    }

	async void OnDeleteButtonClicked(object sender, EventArgs e)
	{
		await _dataService.DeleteExerciceAsync(Exercice.Id);
        await Shell.Current.GoToAsync("..");
    }

	async void OnCancelButtonClicked(object sender, EventArgs e)
	{
		await Shell.Current.GoToAsync("..");
	}
}