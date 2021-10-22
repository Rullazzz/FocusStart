using System;
using System.Collections.Generic;
using System.IO;

namespace FocusStart
{
	public static class FileWriter
	{
		/// <summary>
		/// Записать в выходной файл данные.
		/// </summary>
		/// <typeparam name="T"> Тип данных. </typeparam>
		/// <param name="outputFileName"> Имя выходного файла. </param>
		/// <param name="data"> Данные для записи. </param>
		/// <param name="append"> true для добавления данных в файл. false Перезаписать файл. </param>
		public static void WriteFile<T>(string outputFileName, IEnumerable<T> data, bool append)
		{
			#region Проверка входных данных
			if (string.IsNullOrEmpty(outputFileName))
			{
				throw new ArgumentException($"\"{nameof(outputFileName)}\" не может быть неопределенным или пустым.", nameof(outputFileName));
			}

			if (data is null)
			{
				throw new ArgumentNullException(nameof(data));
			}			

			if (!outputFileName.Contains(".txt"))
			{
				throw new Exception("Неккоректное название выходного файла");
			}
			#endregion

			using (var sw = new StreamWriter(outputFileName, append))
			{
				foreach (var item in data)
				{
					sw.WriteLine(item);
				}
			}
		}
	}
}
