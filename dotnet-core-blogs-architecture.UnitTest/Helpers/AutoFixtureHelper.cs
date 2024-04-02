using AutoFixture;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dotnet_core_blogs_architecture.UnitTest.Helpers;
public sealed class AutoFixtureHelper
{
	private static Fixture instance = null;
	public static Fixture Fixture
	{
		get
		{
			if (instance == null)
			{
				var fixture = new Fixture();
				fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
								 .ForEach(b => fixture.Behaviors.Remove(b));
				fixture.Behaviors.Add(new OmitOnRecursionBehavior());//recursionDepth

				instance = fixture;
			}

			return instance;
		}
	}
}