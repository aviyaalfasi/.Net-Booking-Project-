using System;
using System.Reflection;
namespace BE
{
    public static class BE_Tools
    {
   
        
        /// <summary>
        /// jeneric function that get an object and return a string with discrabtion about it(use by to string)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>string with the detales of the object</returns>
        internal static string GetPropertyValues(object obj)
        {
            string result;
            Type t = obj.GetType();
            result = "Type is: " + t.Name +'\n';
            PropertyInfo[] props = t.GetProperties();// returns array with all the properties that exist in that type
            foreach (var prop in props)
                if (prop.GetIndexParameters().Length == 0)
                {
                    result += string.Format("\n{0} : {1}", prop.Name, prop.GetValue(obj));
                }
                else
                {
                    int indexes = prop.GetIndexParameters().Length;
                    for (int i = 0; i < indexes; i++)
                    {
                        result += string.Format("\n{0} : {1}", prop.Name, prop.GetValue(obj, new object[] { i}));
                    }

                }
            return result;
        }
        public static T[] Flatten<T>(this T[,] arr)
        {
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    var test = arr[i, j];
                    arrFlattened[i * rows + j] = arr[i, j];
                }
            }
            return arrFlattened;
        }

        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    arrExpanded[i, j] = arr[i * rows + j];
                }
            }
            return arrExpanded;
        }

    }
}
