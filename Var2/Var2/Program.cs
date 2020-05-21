using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Library;
using System.Xml.Serialization;


namespace Var2
{
    class Program
    {
        /// <summary>
        /// Проверяет строку из файла на корректность
        /// </summary>
        public static bool CheckString(string s)
        {
            //пробелы нельзя пропускать - по условию между номера ровно 1 пробел
            string[] arr =
                s.Split(new char[] { ' ' });
            //первый элемент arr - название улицы
            if (arr.Length <= 1) return false; //означает, что нет ни одного дома
            int numb;
            for (int i = 1; i < arr.Length; i++)
                if (!int.TryParse(arr[i], out numb) || numb > 100 || numb <= 0)
                    return false;
            return true;

        }

        /// <summary>
        /// Проверяет файл на корректность
        /// </summary>
        public static bool CheckFile(string path)
        {
            string[] arr =
                File.ReadAllLines(path); //считываем все строки
            foreach (string s in arr)
                if (!CheckString(s)) return false;
            return true;
        }

        /// <summary>
        /// Считывает положительное целое число
        /// </summary>
        public static int ReadInt()
        {
            int res;
            Console.WriteLine("Введите число улиц: ");
            while (!int.TryParse(Console.ReadLine(), out res) || res <= 0)
            {
                Console.WriteLine("Неверно введено количество улиц." +
                    " Введите целое положительное число.");
            }
            return res;
        }


        public static Random rnd = new Random();

        /// <summary>
        /// Шенерирует список улиц и номера домов
        /// </summary>
        /// <param name="amount">количество улиц</param>
        public static List<Street> StreetsGenerator(int amount)
        {
            List<Street> lst = new List<Street>();
            for (int i = 0; i < amount; i++)
            {
                //генерируем улицу
                string name = "" + (char)rnd.Next('A', 'Z' + 1); //первая буква
                int len = rnd.Next(1, 14); //длина названия -1
                for (int j = 0; j < len; j++)
                {
                    name += (char)rnd.Next('a', 'z' + 1);
                }

                //генерируем дома
                int[] houses = new int[rnd.Next(2, 10)];
                for (int j = 0; j < houses.Length; j++)
                    houses[j] = rnd.Next(1, 101);

                //добавляем улицу
                lst.Add(new Street(name, houses));
            }
            return lst;
        }

        /// <summary>
        /// Преобразует файл к списку улиц
        /// </summary>
        public static List<Street> GetStreets(string path, int amount)
        {
            List<Street> lst = new List<Street>();
            string[] arr = File.ReadAllLines(path, Encoding.GetEncoding(1251));
            for (int i = 0; i < arr.Length && i < amount; i++)
            {
                string[] arr2 = arr[i].Split(' ');
                string name = arr2[0]; //название улицы
                int[] houses = new int[arr2.Length - 1]; //дома
                for (int j = 1; j < arr2.Length; j++)
                {
                    int numb;
                    int.TryParse(arr2[j], out numb);
                    houses[j - 1] = numb;
                }
                lst.Add(new Street(name, houses));
            }
            return lst;

        }


        /// <summary>
        /// Сериализует улицы в файл
        /// </summary>
        /// <param name="streets">сериализуемый объект</param>
        /// <param name="path">выходной файл</param>
        public static void SerializeStreets(Street[] streets, string path)
        {
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                XmlSerializer xmlSerializer = 
                    new XmlSerializer(typeof(Street[]),
                    new Type[] { typeof(Street) });
                xmlSerializer.Serialize(fs, streets);
            }
            Console.WriteLine("Успешно сериализованно.");
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            const string path1 = "data.txt";
            const string path2 = "out.ser";
            int N = ReadInt(); //считываем число улиц
            List<Street> streetsList;
            if (CheckFile(path1)) //если можно взять из файла
                streetsList = GetStreets(path1, N);
            else
                streetsList = StreetsGenerator(N);
            Street[] streetsArray = streetsList.ToArray();
            //Вывод информации
            foreach (Street st in streetsArray)
                Console.WriteLine(st);
            //Сериализуем
            SerializeStreets(streetsArray, path2);
        }
    }
}
