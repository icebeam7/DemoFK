using DemoFK.Services;

namespace DemoFK;

public partial class App : Application
{
    public static ILocalDatabaseService LocalDb { get; private set; }
    
	public App(ILocalDatabaseService localDb)
	{
		InitializeComponent();

        LocalDb = localDb;
        MainPage = new AppShell();
	}
}