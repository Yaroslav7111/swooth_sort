using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Створюємо масив чисел
        int[] array = { 90, -1, 34, 0, 56 };

        // Викликаємо демонстраційну версію Smooth Sort
        SmoothSortDemo(array);

        // Виводимо відсортований масив
        Console.WriteLine(string.Join(", ", array));
    }

    // Функція для обчислення чисел Леонардо
    // L(0) = 1
    // L(1) = 1
    // L(n) = L(n-1) + L(n-2) + 1
    static int Leonardo(int n)
    {
        // Базовий випадок рекурсії
        if (n == 0 || n == 1)
            return 1;

        // Рекурсивне обчислення
        return Leonardo(n - 1) + Leonardo(n - 2) + 1;
    }

    static void SmoothSortDemo(int[] array)
    {
        // Список для зберігання розмірів Leonardo-дерев
        List<int> sizes = new List<int>();

        
        // 1 ФАЗА — Побудова дерев
        for (int i = 0; i < array.Length; i++)
        {
            // Кожен новий елемент спочатку вважається деревом розміру 1
            sizes.Add(1);

            // Якщо два останні дерева однакового розміру —
            // об'єднуємо їх у більше дерево
            while (sizes.Count >= 2 &&
                   sizes[sizes.Count - 1] == sizes[sizes.Count - 2])
            {
                int last = sizes[sizes.Count - 1];

                // Видаляємо останнє дерево
                sizes.RemoveAt(sizes.Count - 1);

                // Збільшуємо попереднє дерево
                // (імітація формули Леонардо)
                sizes[sizes.Count - 1] += last + 1;
            }

            // Підтримуємо просту heap-властивість:
            // якщо поточний елемент більший за попередній —
            // міняємо їх місцями
            if (i > 0 && array[i] > array[i - 1])
            {
                Swap(array, i, i - 1);
            }
        }

        
        // 2 ФАЗА — Витягування
        // Працюємо як у Heap Sort:
        // найбільший елемент переносимо в кінець
        for (int end = array.Length - 1; end > 0; end--)
        {
            // Міняємо місцями перший (найбільший) елемент
            // з останнім невідсортованим
            Swap(array, 0, end);

            // Відновлюємо heap-властивість
            Heapify(array, 0, end);
        }
    }

    static void Heapify(int[] array, int root, int size)
    {
        // Відновлення властивості max-heap
        while (true)
        {
            int largest = root;

            // Індекс лівої дитини
            int left = 2 * root + 1;

            // Індекс правої дитини
            int right = 2 * root + 2;

            // Якщо ліва дитина більша за корінь —
            // запам'ятовуємо її як найбільшу
            if (left < size && array[left] > array[largest])
                largest = left;

            // Якщо права дитина більша —
            // оновлюємо найбільший елемент
            if (right < size && array[right] > array[largest])
                largest = right;

            // Якщо корінь вже найбільший — зупиняємось
            if (largest == root)
                break;

            // Міняємо місцями корінь і найбільшу дитину
            Swap(array, root, largest);

            // Продовжуємо перевірку вниз по дереву
            root = largest;
        }
    }

    static void Swap(int[] array, int i, int j)
    {
        // Звичайний обмін значень через тимчасову змінну
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }
}
