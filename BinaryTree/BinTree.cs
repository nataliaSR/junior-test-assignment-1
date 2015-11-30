using System;
using System.Collections;
using System.Collections.Generic;

namespace ThirdTask
{
    class BinTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private Node<T> root;
        private int counter;

        /// <summary>
        /// Количество узлов в дереве.
        /// </summary>
        public int Count
        {
            get { return counter; }
        }

        /// <summary>
        /// Корень дерева.
        /// </summary>
        public Node<T> Root
        {
            get { return root; }
        }

        /// <summary>
        /// Очищат дерево.
        /// </summary>
        public void Clear()
        {
            counter = 0;
            root = null;
        }

        /// <summary>
        /// Добавляет новый узел со значением value в дерево.
        /// </summary>
        /// <param name="value">Значение нового узла</param>
        public void Add(T value)
        {
            if (root == null)
            {
                root = new Node<T>(value);
            }
            else
            {
                AddNode(root, value);

                #region Не рекурсивная реализация
                //bool stop = false;
                //Node<T> tmpRoot = root;
                //while (!stop)
                //{
                //    if (tmpRoot.CompareTo(value) > 0)
                //    {
                //        if (tmpRoot.Left != null)
                //        {
                //            tmpRoot = tmpRoot.Left;
                //        }
                //        else
                //        {
                //            tmpRoot.Left = new Node<T>(value);
                //            stop = true;
                //        }
                //    }
                //    else
                //    {
                //        if (tmpRoot.Right != null)
                //        {
                //            tmpRoot = tmpRoot.Right;
                //        }
                //        else
                //        {
                //            tmpRoot.Right = new Node<T>(value);
                //            stop = true;
                //        }
                //    }
                //}
                #endregion
            }
            counter++;
        }

        /// <summary>
        /// Производит рекурсивно поиск места для новго узла.
        /// </summary>
        /// <param name="root">текущий корень</param>
        /// <param name="value">значение новго узла</param>
        private void AddNode(Node<T> root, T value)
        {
            // Значение нового узла меньше значения текущего
            // переходим к левому поддереву.
            if (root.CompareTo(value) > 0)
            {
                if (root.Left != null)
                {
                    AddNode(root.Left, value);
                }
                else
                {
                    root.Left = new Node<T>(value);
                }
            }
            else // Значение новго узла больше или равно значение текущего, переходим в правое поддерево
            {
                if (root.Right != null)
                {
                    AddNode(root.Right, value);
                }
                else
                {
                    root.Right = new Node<T>(value);
                }
            }
        }

        /// <summary>
        /// Вычисляет высоту указанного узла
        /// </summary>
        /// <param name="root">узел для которого необходимо вычислить высоту</param>
        /// <returns>целое число - высота</returns>
        public int GetHeight(Node<T> root)
        {
            int leftHeigt = 0;
            int rightHeight = 0;

            if (root == null)
            {
                return (0);
            }

            if (root.Left != null)
            {
                leftHeigt = GetHeight(root.Left);
            }
            if (root.Right != null)
            {
                rightHeight = GetHeight(root.Right);
            }
            return (Math.Max(leftHeigt, rightHeight) + 1);

        }

        /// <summary>
        /// Определяет содержится ли в дереве узел со значением value
        /// </summary>
        /// <param name="value">значение искомого узла</param>
        /// <returns>ture если узел найден, иначе false</returns>
        public bool Contains(T value)
        {
            Node<T> parentNode;
            return Find(value, out parentNode) != null;
        }

        /// <summary>
        /// Осуществляет поиск узла с заданым знаением
        /// </summary>
        /// <param name="value">значие узлаен</param>
        /// <param name="parent">родитель искомого узла</param>
        /// <returns>искомый узел</returns>
        private Node<T> Find(T value, out Node<T> parent)
        {
            Node<T> current = root;
            parent = null;

            while (current != null)
            {
                int comparResult = current.CompareTo(value);
                if (comparResult < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else if (comparResult > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else
                {
                    break;
                }
            }
            return current;
        }

        /// <summary>
        /// Удаляет узел с указанным значением
        /// </summary>
        /// <param name="value">значение удаляемого узла</param>
        /// <returns>успех удаления</returns>
        public bool Delete(T value)
        {
            Node<T> current;
            Node<T> parent;

            current = Find(value, out parent);
            // Если мы удаляем корень 
            if (current == null)
            {
                return false;
            }

            counter--;

            // Удаляемый узел не имеет правого потомка. 
            if (current.Right == null)
            {
                if (parent == null)
                {
                    root = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Left;
                    }
                }
            }
            // Удаляемый узел имеет правого потомка, у которого нет левого потомка.
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                // Если мы удаляем корень 
                if (parent == null)
                {
                    root = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        parent.Right = current.Right;
                    }
                }
            }
            // Удаляемый узел имеет правого потомка, у которого есть левый потомок.
            else
            {

                Node<T> leftmost = current.Right.Left;
                Node<T> leftmostParent = current.Right;

                // Ищем крайнего левого потомка
                while (leftmost.Left != null)

                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }

                // Правое поддерево leftmost становится левым поддеревом родителя leftmost       
                leftmostParent.Left = leftmost.Right;

                // Присваиваем потомки удаляемого узла
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;

                if (parent == null)
                {
                    root = leftmost;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);

                    if (result > 0)
                    {
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        parent.Right = leftmost;
                    }
                }
            }

            return true;
        }

        #region Энумератор
        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerator<T> InOrderTraversal()
        {
            if (root != null)
            {
                // Узлы буду хранить в стэке.
                Stack<Node<T>> stack = new Stack<Node<T>>();
                Node<T> currentNode = root;

                bool isLeftNext = true;

                stack.Push(currentNode);
                while (stack.Count > 0)
                {
                    if (isLeftNext)
                    {
                        // Помещаем в стэк всех левых потомков.
                        while (currentNode.Left != null)
                        {
                            stack.Push(currentNode);
                            currentNode = currentNode.Left;
                        }
                    }

                    yield return currentNode.Value;

                    if (currentNode.Right != null)
                    {
                        currentNode = currentNode.Right;
                        isLeftNext = true;
                    }
                    else
                    {
                        currentNode = stack.Pop();
                        isLeftNext = false;
                    }
                }
            }
        }
        #endregion
    }
}
