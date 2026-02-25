using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int[] array = { 90, -1, 34, 0, 56 };

        SmoothSortDemo(array);

        Console.WriteLine(string.Join(", ", array));
    }

    // Leonardo numbers
    static int Leonardo(int n)
    {
        if (n == 0 || n == 1)
            return 1;

        return Leonardo(n - 1) + Leonardo(n - 2) + 1;
    }

    static void SmoothSortDemo(int[] array)
    {
        List<int> sizes = new List<int>();

        // 1️⃣ Build Leonardo trees
        for (int i = 0; i < array.Length; i++)
        {
            sizes.Add(1); // create tree of size 1

            // merge trees if possible
            while (sizes.Count >= 2 &&
                   sizes[sizes.Count - 1] == sizes[sizes.Count - 2])
            {
                int last = sizes[sizes.Count - 1];
                sizes.RemoveAt(sizes.Count - 1);

                sizes[sizes.Count - 1] += last + 1;
            }

            // maintain simple heap property (demo)
            if (i > 0 && array[i] > array[i - 1])
            {
                Swap(array, i, i - 1);
            }
        }

        // 2️⃣ Extract phase (heap-style)
        for (int end = array.Length - 1; end > 0; end--)
        {
            Swap(array, 0, end);
            Heapify(array, 0, end);
        }
    }

    static void Heapify(int[] array, int root, int size)
    {
        while (true)
        {
            int largest = root;
            int left = 2 * root + 1;
            int right = 2 * root + 2;

            if (left < size && array[left] > array[largest])
                largest = left;

            if (right < size && array[right] > array[largest])
                largest = right;

            if (largest == root)
                break;

            Swap(array, root, largest);
            root = largest;
        }
    }

    static void Swap(int[] array, int i, int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}

