using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FocusStart
{
	public class FileReader
	{
		private List<StreamReader> streamReaders;

		public List<string> FileNames { get; set; }

		/// <summary>
		/// Инициализирует новый экземпляр класса FileReader с указанными файлами.
		/// </summary>
		/// <param name="fileNames"> Имена файлов. </param>
		public FileReader(IEnumerable<string> fileNames)
		{
			#region Проверка входных данных
			if (fileNames is null)
			{
				throw new System.ArgumentNullException(nameof(fileNames));
			}
			#endregion

			FileNames = new List<string>(fileNames.Count());
			FileNames.AddRange(fileNames);

			streamReaders = new List<StreamReader>(fileNames.Count());
			foreach (var fileName in FileNames)
			{
				streamReaders.Add(new StreamReader(fileName));
			}
		}

		/// <summary>
		/// Прочитать файлы и вернуть список чисел.
		/// </summary>
		/// <param name="countLines"> Кол-во читаемых строк за раз. </param>
		/// <returns> Список строк или null, если список пуст. </returns>
		public List<string> ReadFilesString(ushort countLines)
		{
			var readedData = new List<string>();

			for (var i = 0; i < FileNames.Count(); i++)
			{
				var reader = streamReaders[i];
				
				string line;
				for (var j = 0; j < countLines; j++)
				{
					if ((line = reader.ReadLine()) != null && line != "")
					{
						readedData.Add(line);						
					}
				}
			}

			if (readedData.Count() > 0)
			{
				return readedData;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Прочитать файлы и вернуть список чисел.
		/// </summary>
		/// <param name="countLines"> Кол-во читаемых строк за раз. </param>
		/// <returns> Список чисел или null, если список пуст. </returns>
		public List<int> ReadFilesInt(ushort countLines)
		{
			var readedData = new List<int>();

			for (var i = 0; i < FileNames.Count(); i++)
			{
				var reader = streamReaders[i];

				string line;
				for (var j = 0; j < countLines; j++)
				{
					if ((line = reader.ReadLine()) != null && line != "" && int.TryParse(line, out int number))
					{
						readedData.Add(number);
					}
				}
			}

			if (readedData.Count() > 0)
			{
				return readedData;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Закрывает все потоки.
		/// </summary>
		public void Close()
		{
			foreach (var stream in streamReaders)
			{
				stream.Close();
			}
		}
	}
}
