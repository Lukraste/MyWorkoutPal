using MyWorkoutPal.Views;
namespace MyWorkoutPal;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MyExercicesPage), typeof(MyExercicesPage));
	}
}
