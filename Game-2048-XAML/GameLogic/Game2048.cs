using System;
using System.Collections.Generic;
using System.Linq;

namespace Game2048XAML.GameLogic
{
    public static class Game2048
    {
        private static int score = 0;
        private const int Size = 4;
        private static int[,] matrix = new int[Size, Size];
        private static readonly Random random = new Random();

        public static int Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

        public static int[,] Matrix
        {
            get
            {
                return matrix;
            }
            set
            {
                matrix = value;
            }
        }

        public static void Move(Direction direction)
        {
            for (int i = 0; i < Size; i++)
            {
                int[] array = new int[Size];
                for (int j = 0; j < Size; j++)
                {
                    if (direction == Direction.Left)
                    {
                        array[j] = matrix[i, j];
                    }
                    else if (direction == Direction.Right)
                    {
                        array[j] = matrix[i, Size - 1 - j];
                    }
                    else if (direction == Direction.Up)
                    {
                        array[j] = matrix[j, i];
                    }
                    else if (direction == Direction.Down)
                    {
                        array[j] = matrix[Size - 1 - j, i];
                    }
                }
                array = NewArray(array);
                if (direction == Direction.Right || direction == Direction.Down)
                {
                    Array.Reverse(array);
                }
                for (int j = 0; j < Size; j++)
                {
                    if (direction == Direction.Left || direction == Direction.Right)
                    {
                        matrix[i, j] = array[j];
                    }
                    else if (direction == Direction.Up || direction == Direction.Down)
                    {
                        matrix[j, i] = array[j];
                    }
                }
            }
            CreateRandomNumber();
        }

        public static void CreateRandomNumber()
        {
            while (true)
            {
                int currRow = random.Next(Size);
                int currCol = random.Next(Size);
                if (matrix[currRow, currCol] == 0)
                {
                    matrix[currRow, currCol] = 2;
                    break;
                }
            }
        }

        private static int[] NewArray(int[] array)
        {
            List<int> list = new List<int>();
            List<int> newList = new List<int>();
            int[] newArray = new int[Size];
            for (int i = 0; i < Size; i++)
            {
                if (array[i] != 0)
                {
                    list.Add(array[i]);
                }
            }
            if (list.Count == 0)
            {
                return newArray;
            }
            else if (list.Count == 1)
            {
                newArray[0] = list[0];
                return newArray;
            }
            else
            {
                int index = 0;
                while (index < list.Count - 1)
                {
                    if (list[index] == list[index + 1])
                    {
                        newList.Add(2 * list[index]);
                        score += 2 * list[index];
                        index += 2;
                    }
                    else if (list[index] != list[index + 1])
                    {
                        newList.Add(list[index]);
                        index++;
                    }
                    if (index == list.Count - 1)
                    {
                        newList.Add(list[list.Count - 1]);
                    }
                }
                for (int i = 0; i < newList.Count; i++)
                {
                    newArray[i] = newList[i];
                }
            }
            return newArray;
        }
    }
}