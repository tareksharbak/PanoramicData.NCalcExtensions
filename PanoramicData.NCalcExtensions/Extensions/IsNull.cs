﻿using NCalc;
using PanoramicData.NCalcExtensions.Exceptions;
using System;

namespace PanoramicData.NCalcExtensions.Extensions
{
	internal static class IsNull
	{
		internal static void Evaluate(FunctionArgs functionArgs)
		{
			if (functionArgs.Parameters.Length != 1)
			{
				throw new FormatException($"{ExtensionFunction.IsNull}() requires one parameter.");
			}
			try
			{
				var outputObject = functionArgs.Parameters[0].Evaluate();
				functionArgs.Result = outputObject == null;
				return;
			}
			catch (NCalcExtensionsException)
			{
				throw;
			}
			catch (FormatException)
			{
				throw;
			}
			catch (Exception e)
			{
				throw new FormatException(e.Message);
			}
		}
	}
}