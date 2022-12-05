using DemoFK.Helpers;
using DemoFK.Views;

namespace DemoFK;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        RegisterRoutes();
    }

    void RegisterRoutes()
    {
        Routing.RegisterRoute(Constants.DepartmentDetailsRoute,
            typeof(DepartmentDetailsView));

        Routing.RegisterRoute(Constants.EmployeeDetailsRoute,
            typeof(EmployeeDetailsView));
    }
}
