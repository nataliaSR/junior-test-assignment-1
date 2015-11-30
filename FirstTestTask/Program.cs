using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstSecondTask
{
    class Program
    {
        static int maxGroupLength = 3;  // Максимально возможная длинна группы
        static List<int> bufer;
        static int[] sequence;          // Входная последовательность

        /// <summary>
        /// Выводит на экран содержимое массива
        /// с сопроводительным текстом.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="text"></param>
        static void printSequence(int[] array, string text)
        {
            Console.Write(text);
            foreach (int element in array)
            {
                Console.Write(element.ToString() + ' ');
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Вычисляет длинну группы
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        static int getGroupLenght(int start, int end)
        {
            return end - start + 1; // +1 т.к. нумерация с 0.
        }

        /// <summary>
        /// Записывает в буфер переданное значение указанное число раз
        /// </summary>
        /// <param name="groupLength">кол-во повторений символа</param>
        /// <param name="elemForCopy">символ для записи</param>
        static void copyToBufer(int groupLength, int elemForCopy)
        {
            if (groupLength < maxGroupLength)
            {
                for (int j = 0; j < groupLength; j++)
                {
                    bufer.Add(elemForCopy);
                }
            }
        }

        /// <summary>
        /// Очищает последовательность от групп одинаковых чисел длинной более maxGroupLength
        /// </summary>
        /// <returns>очищенная последовательность</returns>
        static int[] clearSequence()
        {
            int[] result;
            int count = 0;
            int prev = sequence[0];
            int start = 0, end = 0;
            bufer = new List<int>();
            List<int> notNeededNumbers = new List<int>();
            for (int i = 0; i < sequence.Length; i++)
            {
                // Если попадаются одинаковые элементы, то
                // передвигаем границу группы.
                if (prev == sequence[i])
                {
                    end = i;
                }
                else // иначе вычисляем длинну группы
                {
                    count = getGroupLenght(start, end);
                    // копируем в буфер, если нужно.
                    copyToBufer(count, prev);

                    prev = sequence[i];
                    count = 0;
                    start = i; end = i; // указатели на текущий элемент
                }
            }
            // Проверяем завершающую группу
            count = getGroupLenght(start, end);
            copyToBufer(count, prev);

            int index = 0;
            // Переводим в массив.
            result = new int[bufer.Count];

            foreach (int item in bufer)
            {
                result[index++] = item;
            }

            return result;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите кол-во элементов в последовательности:");
            int length = -1;
            if (int.TryParse(Console.ReadLine(), out length))
            {
                #region Получаем элементы последовательности
                Console.WriteLine();
                sequence = new int[length];
                for (int i = 0; i < length; i++)
                {
                    Console.Write("Введите {0}-й элемент:", i + 1);
                    try
                    {
                        sequence[i] = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine("Ошибка! " + ex.Message);
                    }
                }

                printSequence(sequence, "Введёная последовательность:");
                #endregion

                #region Удаление только групп
                int[] resultGroupErase;
                if (sequence.Length > 0 && sequence.Length > maxGroupLength)
                {
                    resultGroupErase = clearSequence();
                }
                else
                {   // Если длинна последовательности меньше maxRepeatsNumber
                    // то просто выводим её.
                    resultGroupErase = sequence;
                }

                printSequence(resultGroupErase, string.Format("Удалены группы длинной >= {0}:", maxGroupLength));
                #endregion
            }
        }
    }
}