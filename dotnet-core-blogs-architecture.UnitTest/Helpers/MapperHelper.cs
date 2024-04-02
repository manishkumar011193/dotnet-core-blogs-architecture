using AutoMapper;

namespace dotnet_core_blogs_architecture.UnitTest.Helpers;
public sealed class MapperHelper
{
	private static IMapper instance = null;
	public static IMapper Instance
	{
		get
		{
			if (instance == null)
			{
				//auto mapper configuration
				var mockMapper = new MapperConfiguration(cfg =>
				{
					var profiles = typeof(dotnet_core_blogs_architecture.blogs.Mediator.Post.MapperProfile).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x));
					foreach (var profile in profiles)
					{
						cfg.AddProfile(Activator.CreateInstance(profile) as Profile);
					}
				});
				instance = mockMapper.CreateMapper();
			}

			return instance;
		}
	}
}