﻿using System.Collections;
using System.Collections.Generic;

namespace PanoramicData.NCalcExtensions.Extensions;

internal static class Select
{
	internal static void Evaluate(FunctionArgs functionArgs)
	{
		var enumerable = functionArgs.Parameters[0].Evaluate() as IList
			?? throw new FormatException($"First {ExtensionFunction.Select} parameter must be an IList.");

		var predicate = functionArgs.Parameters[1].Evaluate() as string
			?? throw new FormatException($"Second {ExtensionFunction.Select} parameter must be a string.");

		var lambdaString = functionArgs.Parameters[2].Evaluate() as string
			?? throw new FormatException($"Third {ExtensionFunction.Select} parameter must be a string.");

		var type = functionArgs.Parameters.Length == 4
			? functionArgs.Parameters[3].Evaluate() as string
				?? throw new FormatException($"Fourth {ExtensionFunction.Select} parameter must be a string.")
			: "object";

		var lambda = new Lambda(predicate, lambdaString, functionArgs.Parameters[0].Parameters);

		switch (type)
		{
			case "object":
				var result = new List<object?>();
				foreach (var value in enumerable)
				{
					result.Add(lambda.Evaluate(value));
				}

				functionArgs.Result = result;
				return;
			case "JObject":
				var jResult = new List<JObject?>();
				foreach (var value in enumerable)
				{
					var jObject = lambda.Evaluate(value) switch
					{
						null => null,
						JObject valueAsJObject => valueAsJObject,
						_ => JObject.FromObject(value)
					};

					jResult.Add(jObject);
				}

				functionArgs.Result = jResult;
				return;
			default:
				throw new FormatException($"Fourth {ExtensionFunction.Select} parameter must be either 'object' (default) or 'JObject'.");
		}
	}
}

