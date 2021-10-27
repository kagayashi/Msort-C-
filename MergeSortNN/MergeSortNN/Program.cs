using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
class Program
{

	//main recursive algorithm
	static Int32[] MergeSort(Int32[] array)
	{
		if (array.Length == 1)
		{
			return array;
		}

		Int32 middle = array.Length / 2;
		return Merge(MergeSort(array.Take(middle).ToArray()), MergeSort(array.Skip(middle).ToArray()));
	}
	//Merge two parts
	static Int32[] Merge(Int32[] arr1, Int32[] arr2)
	{
		Int32 ptr1 = 0, ptr2 = 0;
		Int32[] merged = new Int32[arr1.Length + arr2.Length];

		for (Int32 i = 0; i < merged.Length; ++i)
		{
			if (ptr1 < arr1.Length && ptr2 < arr2.Length)
			{
				merged[i] = arr1[ptr1] > arr2[ptr2] ? arr2[ptr2++] : arr1[ptr1++];
			}
			else
			{
				merged[i] = ptr2 < arr2.Length ? arr2[ptr2++] : arr1[ptr1++];
			}
		}

		return merged;
	}
	//Core
	static async Task Main(string[] args)
	{

		//random array
		Int32[] arr = new Int32[100];

		
		Random rd = new Random();
		for (Int32 i = 0; i < arr.Length; ++i)
		{
			arr[i] = rd.Next(1, 101);
		}
		//dividing array into 4 pieces
		int[] arr1 = new int[arr.Length / 4], arr2 = new int[arr.Length / 4], arr3 = new int[arr.Length / 4], arr4 = new int[arr.Length/4] ;

		System.Console.WriteLine("The array before sorting:");
		foreach (Int32 x in arr)
			System.Console.Write(x + " ");

		//dividing array into 4 pieces
		for (int j = 0; j < arr.Length / 4; j++)
		{
			arr1[j] = arr[j];
			arr2[j] = arr[arr.Length / 4 + j];
			arr3[j] = arr[arr.Length / 4 * 2 + j];
			arr4[j] = arr[arr.Length / 4 * 3 + j];
		}

		//elapsed time
		Stopwatch stopWatch = new Stopwatch();
		
		stopWatch.Start();

		
		//creating 4 tasks to MergeSort 4 pieces of an original array
		Task[] taskArr = new Task[] 
		{Task.Factory.StartNew(() => {Console.WriteLine("\nTask1 Started at elapsed time : "+ stopWatch.Elapsed/100); arr1 = MergeSort(arr1);Console.WriteLine("\nTask1 Ended at elapsed time : "+ stopWatch.Elapsed/100); }),
		Task.Factory.StartNew(() => {Console.WriteLine("\nTask2 Started at elapsed time : "+ stopWatch.Elapsed/100); arr2 = MergeSort(arr2);Console.WriteLine("\nTask2 Ended at elapsed time : "+ stopWatch.Elapsed/100); }),
		Task.Factory.StartNew(() => {Console.WriteLine("\nTask3 Started at elapsed time : "+ stopWatch.Elapsed/100); arr3 = MergeSort(arr3);Console.WriteLine("\nTask3 Ended at elapsed time : "+ stopWatch.Elapsed/100); }),
		Task.Factory.StartNew(() => {Console.WriteLine("\nTask4 Started at elapsed time : "+ stopWatch.Elapsed/100); arr4 = MergeSort(arr4); Console.WriteLine("\nTask4 Ended at elapsed time : "+ stopWatch.Elapsed/100);})
	};
		
		Task.WaitAll(taskArr);
		
	//after all of the pieces are sorted, merging them into 1 array
		arr = Merge(Merge(arr1, arr2), Merge(arr3, arr4));
	
		stopWatch.Stop();
		//timer stop
		TimeSpan ts = stopWatch.Elapsed;
		string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
			ts.Hours, ts.Minutes, ts.Seconds,
			ts.Milliseconds);
		

		
		System.Console.WriteLine("\n\nThe array after sorting:");
		foreach (Int32 x in arr)
			System.Console.Write(x + " ");
		Console.WriteLine("\n\nRunTime " + elapsedTime);

		System.Console.WriteLine("\n\nPress the <Enter> key");
		System.Console.ReadLine();
	}
}