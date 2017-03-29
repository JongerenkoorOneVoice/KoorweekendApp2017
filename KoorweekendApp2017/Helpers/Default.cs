using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace KoorweekendApp2017.Helpers
{
	public static class Default<T>
	{
		private static readonly T _value;

		static Default()
		{
			if (typeof(T).IsArray)
			{
				if (typeof(T).GetArrayRank() > 1)
					_value = (T)(object)Array.CreateInstance(typeof(T).GetElementType(), new int[typeof(T).GetArrayRank()]);
				else
					_value = (T)(object)Array.CreateInstance(typeof(T).GetElementType(), 0);
				return;
			}

			if (typeof(T) == typeof(string))
			{
				// string is IEnumerable<char>, but don't want to treat it like a collection
				_value = default(T);
				return;
			}

			if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
			{
				// check if an empty array is an instance of T
				if (typeof(T).GetTypeInfo().IsAssignableFrom(typeof(object[]).GetTypeInfo()))
				{
					_value = (T)(object)new object[0];
					return;
				}

				var genericArguments = typeof(T).GetTypeInfo().IsGenericTypeDefinition ? typeof(T).GetTypeInfo().GenericTypeParameters : typeof(T).GetTypeInfo().GenericTypeArguments;

				if (typeof(T).GetTypeInfo().IsGenericType && genericArguments.Length == 1)
				{
					Type elementType = genericArguments[0];
					if (typeof(T).GetTypeInfo().IsAssignableFrom(elementType.MakeArrayType().GetTypeInfo()))
					{
						_value = (T)(object)Array.CreateInstance(elementType, 0);
						return;
					}
					else if (typeof(T).ToString().ToLower().Contains("system.collections.generic.list"))
					{
						var listType = typeof(List<>);
						var constructedListType = listType.MakeGenericType(elementType);
						_value = (T)Activator.CreateInstance(constructedListType);
						return;
					}
				}

				throw new NotImplementedException("No default value is implemented for type " + typeof(T).FullName);
			}

			_value = default(T);
		}

		public static T Value
		{
			get
			{
				return _value;
			}
		}
	}
}


