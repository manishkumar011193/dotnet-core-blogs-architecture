using System.Text.RegularExpressions;

namespace dotnet_core_blogs_architecture.blogs.Common;

public static class RegexConstants
{
	public const string DonotAllowSplChars = "^[a-zA-Z0-9 ]*$";
	public const string AllowFewSplChars = "^[a-zA-Z0-9- ,./;]*$";
	public const string OnlyNumerics = "^[0-9 ]*$";
	public const string OnlyAllowForMobileCountryCode = "^[0-9+\\-]+$";
	public const string MobilePattern = @"^\d{10}$";
	public const string OnlyAllowWordsWithSpaceAndHyphen = @"^[a-zA-Z -]+$";
	public const string OnlyAllowWordsWithHyphen = @"^[a-zA-Z0-9-]+$";
	public const string OnlyAllowWordsWithSpace = @"^[a-zA-Z ]+$";
	public const string OnlyAllowWords = @"^[a-zA-Z]+$";
	public const string OnlyAllowWordsWithSlash = @"^[a-zA-Z]+/[a-zA-Z]+$";
	public const string OnlyAlphaNumerics = @"^[a-zA-Z0-9]*$";
	public const string FaxPattern = "^[0-9+ ]*$";
	public const string UNNumberPattern = @"^[0-9]+$";
	public const string OnlyAllowAlphabetsWithNumbers = "^[a-zA-Z0-9]*$";
	public const string OnlyAlphaNumericsWithHyphen = @"^[a-zA-Z0-9-]*$";
}
