﻿namespace dotnet_core_blogs_architecture.UnitTest.Helpers;
public static class CommonUtils
{
	private static Random random = new Random();
	public static string RandomNumber(int length)
	{
		const string chars = "0123456789";
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[random.Next(s.Length)]).ToArray());
	}
	public static string RandomCharacters(int length)
	{
		const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		return new string(Enumerable.Repeat(chars, length)
			.Select(s => s[random.Next(s.Length)]).ToArray());
	}
}
