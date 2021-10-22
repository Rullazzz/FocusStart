using System;
using System.Collections.Generic;
using System.IO;

namespace FocusStart
{
	class Program
	{
		public static bool IsSortingMode { get; private set; } = false;

		static void Main(string[] args)
		{
			#region Проверка входных данных
			if ((args[0] == "-a" || args[0] == "-d") && args.Length <= 3)
				CloseApp("Недостаточно параметров");
			if (args.Length < 3)
			{
				CloseApp("Недостаточно параметров");
			}
			#endregion

			try
			{
				const ushort COUNT_LINES_READING = 10000;
				var sortingMode = GetSortingMode(args);
				var typeData = GetTypeArg(args);
				var outputFileName = GetOutputFileName(args);

				var fileNames = new List<string>(args.Length - 2);
				SetFileNames(args, fileNames);

				var fileReader = new FileReader(fileNames);
				
				if (File.Exists(outputFileName))
				{
					File.Delete(outputFileName);
				}

				if (typeData == "-i")
				{
					List<int> data;
					while ((data = fileReader.ReadFilesInt(COUNT_LINES_READING)) != null)
					{
						var sortedData = GetSortData(data, sortingMode);
						FileWriter.WriteFile(outputFileName, sortedData, true);
						data.Clear();
					}
				}
				else
				{
					List<string> data;
					while ((data = fileReader.ReadFilesString(COUNT_LINES_READING)) != null)
					{
						var sortedData = GetSortData(data, sortingMode);
						FileWriter.WriteFile(outputFileName, sortedData, true);
						data.Clear();
					}
				}
				fileReader.Close();
			}
			catch (Exception e)
			{
				CloseApp(e.Message);
			}

			Console.WriteLine("Данные записаны");
		}

		private static string GetOutputFileName(string[] args)
		{
			#region Проверка входных данных
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			#endregion

			if (IsSortingMode)
				return args[2];
			else
				return args[1];
		}

		private static SortingMode GetSortingMode(string[] args)
		{
			#region Проверка входных данных
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			#endregion

			if (args[0] == "-a" || args[0] == "-d")
			{
				IsSortingMode = true;
				if (args[0] == "-a")
					return SortingMode.Ascending;
				else if (args[0] == "-d")
					return SortingMode.Descending;
				else
					throw new Exception("Неккоректный режим сортировки");
			}				
			return SortingMode.Ascending;
		}

		private static List<T> GetSortData<T>(List<T> data, SortingMode sortingMode) where T: IComparable
		{
			#region Проверка входных данных
			if (data is null)
			{
				throw new ArgumentNullException(nameof(data));
			}
			#endregion

			var mergeSort = new MergeSort<T>();
			var sortedData = mergeSort.MakeSort(data, sortingMode);
			return sortedData;
		}		

		private static void SetFileNames(string[] args, List<string> fileNames)
		{
			#region Проверка входных данных
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args));
			}

			if (fileNames is null)
			{
				throw new ArgumentNullException(nameof(fileNames));
			}
			#endregion

			if (IsSortingMode)
			{
				for (var i = 3; i < args.Length; i++)
				{
					fileNames.Add(args[i]);
				}				
			}
			else
			{
				for (var i = 2; i < args.Length; i++)
				{
					fileNames.Add(args[i]);
				}
			}
		}

		private static string GetTypeArg(string[] args)
		{
			#region Проверка входных данных
			if (args is null)
			{
				throw new ArgumentNullException(nameof(args));
			}
			#endregion

			if (IsSortingMode)
			{
				if (args[1] == "-i" || args[1] == "-s")
					return args[1];
				else
					throw new Exception("Указан неизвестный тип данных");
			}
			else
			{
				if (args[0] == "-i" || args[0] == "-s")
					return args[0];
				else
					throw new Exception("Указан неизвестный тип данных");
			}
		}

		private static void CloseApp(string error = "Произошла неожиданная ошибка")
		{
			Console.WriteLine(error);
			Environment.Exit(0);
		}
	}
}
