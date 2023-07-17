﻿namespace PanoramicData.NCalcExtensions.Test;
public class IndexOfTests
{
	[Theory]
	[InlineData("abc", "a", 0)]
	[InlineData("abc", "b", 1)]
	[InlineData("abc", "c", 2)]
	[InlineData("abc", "d", -1)]
	public void IndexOf_Succeeds(string list, string item, object expected)
	{
		var expr = new ExtendedExpression($"indexOf('{list}','{item}')");
		var result = expr.Evaluate();
		Assert.Equal(expected, result);
	}
}
