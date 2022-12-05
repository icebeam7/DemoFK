using DemoFK.Views;
using DemoFK.Models;
using DemoFK.Helpers;
using DemoFK.Services;
using DemoFK.ViewModels;

using Microsoft.Extensions.Logging;
using DemoFK.Models.Views;

namespace DemoFK;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        var dbPath = FileAccessHelper.GetLocalFilePath(Constants.LocalDbFile);

        builder.Services.AddSingleton<ILocalDatabaseService>(
            s => ActivatorUtilities.CreateInstance<LocalDatabaseService>(s, dbPath));

        builder.Services.AddTransient(
            s => ActivatorUtilities.CreateInstance<GenericListViewModel<Department>>(
                s, Constants.AllDepartmentsQuery, Constants.DepartmentDetailsRoute));

        builder.Services.AddTransient(
            s => ActivatorUtilities.CreateInstance<GenericListViewModel<EmployeeWithDepartment>>(
                s, Constants.EmployeesAndDepartmentQuery, Constants.EmployeeDetailsRoute));

        builder.Services.AddTransient<DepartmentDetailsViewModel>();
        builder.Services.AddTransient<EmployeeDetailsViewModel>();

        builder.Services.AddTransient<DepartmentListView>();
        builder.Services.AddTransient<DepartmentDetailsView>();
        builder.Services.AddTransient<EmployeeListView>();
        builder.Services.AddTransient<EmployeeDetailsView>();

        return builder.Build();
	}
}
