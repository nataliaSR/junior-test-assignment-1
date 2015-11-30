using System;

namespace ThirdTask
{
    class Node<T> : IComparable<T> where T : IComparable<T>
    {
        /// <summary>
        /// Реализует представление узла двоичного дерева.
        /// </summary>
        /// <param name="value"></param>
        public Node(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Правый потомок
        /// </summary>
        public Node<T> Left
        {
            get;
            set;
        }

        /// <summary>
        /// Левый потомок
        /// </summary>
        public Node<T> Right
        {
            get;
            set;
        }

        /// <summary>
        /// Значение узла
        /// </summary>
        public T Value
        {
            get;
            set;
        }

        /// <summary>
        /// Сравнивает значение текущего объекта со значением объекта того же типа.
        /// </summary>
        /// <param name="other">Объект для сравнения</param>
        /// <returns>Если значение текущего объекта больше значения other возвращается 1, 
        ///             иначе -1. Если объекты равны возвращмется 0.</returns>
        public int CompareTo(T other)
        {
            return Value.CompareTo(other);
        }
    }
}
