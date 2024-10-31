using Loxley.App;
using Loxley.App.Extensions;

namespace Loxley.Droid;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();

		builder
			.UseSharedMauiApp()
			.AddAppServices(typeof(MauiProgram).Assembly);

		return builder.Build();
	}
}
