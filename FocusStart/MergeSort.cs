using System;
using System.Collections.Generic;
using System.Linq;

namespace FocusStart
{
	public enum SortingMode
	{
		Ascending = 0,
		Descending
	}

	public class MergeSort<T> where T : IComparable
	{
		/// <summary>
		/// Сделать сортировку слиянием.
		/// </summary>
		/// <param name="items"> Сортируемые данные. </param>
		/// <param name="sortingMode"> Тип сортировки, по возрастанию или по убыванию. </param>
		/// <returns> Отсортированные список. </returns>
		public List<T> MakeSort(List<T> items, SortingMode sortingMode = SortingMode.Ascending)
		{
			#region Проверка входных данных
			if (items is null)
			{
				throw new ArgumentNullException(nameof(items));
			}
			#endregion

			return Sort(items, sortingMode);
		}

		private List<T> Sort(List<T> items, SortingMode sortingMode)
		{
			#region Проверка входных данных
			if (items is null)
			{
				throw new ArgumentNullException(nameof(items));
			}

			if (items.Count == 1)
			{
				return items;
			}
			#endregion

			var mid = items.Count / 2;

			var left = items.Take(mid).ToList();
			var right = items.Skip(mid).ToList();

			if (sortingMode == SortingMode.Ascending)
				return MergeAscending(Sort(left, sortingMode), Sort(right, sortingMode));
			else
				return MergeDescending(Sort(left, sortingMode), Sort(right, sortingMode));
		}

		protected int Compare(T a, T b)
		{
			return a.CompareTo(b);
		}

		private List<T> MergeAscending(List<T> left, List<T> right)
		{
			if (left is null)
			{
				throw new ArgumentNullException(nameof(left));
			}

			if (right is null)
			{
				throw new ArgumentNullException(nameof(right));
			}

			var length = left.Count + right.Count;
			var leftPointer = 0;
			var rightPointer = 0;

			var result = new List<T>(length);

			for (int i = 0; i < length; i++)
			{
				if (leftPointer < left.Count && rightPointer < right.Count)
				{
					if (Compare(left[leftPointer], right[rightPointer]) == -1)
					{
						result.Add(left[leftPointer]);
						leftPointer++;
					}
					else
					{
						result.Add(right[rightPointer]);
						rightPointer++;
					}
				}
				else
				{
					if (rightPointer < right.Count)
					{
						result.Add(right[rightPointer]);
						rightPointer++;
					}
					else
					{
						result.Add(left[leftPointer]);
						leftPointer++;
					}
				}
			}

			return result;
		}

		private List<T> MergeDescending(List<T> left, List<T> right)
		{
			#region Проверка входных данных
			if (left is null)
			{
				throw new ArgumentNullException(nameof(left));
			}

			if (right is null)
			{
				throw new ArgumentNullException(nameof(right));
			}
			#endregion

			var length = left.Count + right.Count;
			var leftPointer = 0;
			var rightPointer = 0;

			var result = new List<T>(length);

			for (int i = 0; i < length; i++)
			{
				if (leftPointer < left.Count && rightPointer < right.Count)
				{
					if (Compare(left[leftPointer], right[rightPointer]) == 1)
					{
						result.Add(left[leftPointer]);
						leftPointer++;
					}
					else
					{
						result.Add(right[rightPointer]);
						rightPointer++;
					}
				}
				else
				{
					if (rightPointer < right.Count)
					{
						result.Add(right[rightPointer]);
						rightPointer++;
					}
					else
					{
						result.Add(left[leftPointer]);
						leftPointer++;
					}
				}
			}

			return result;
		}
	}
}
