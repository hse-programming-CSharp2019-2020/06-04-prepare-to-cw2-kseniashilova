using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library;
using System.Xml.Serialization;
using System.IO;

namespace Program2DESEREALIZE
{
    class Program
    {
        /// <summary>
        /// Десериализует массив улиц
        /// </summary>
        static Street[] DeserializeStreets(string path)
        {
            using(FileStream fs = new FileStream(path, FileMode.Open))
            {
                XmlSerializer ser = new XmlSerializer(typeof(Street[]),
                    new Type[] { typeof(Street) });
                Street[] arr = (Street[])ser.Deserialize(fs);
                Console.WriteLine("Успешно десериализованно.");
                return arr;
            }
        }

        /// <summary>
        /// Выбирает из массива только волшебные улицы, то есть 
        /// с нечетным количеством домов 
        /// и хотя бы одним домом с "7" в номере
        /// </summary>
        static Street[] MagicStreets(Street[] arr)
        {
            return arr.Where(street =>
            (~street) % 2 == 1 && !street).ToArray();
        }

        static void Main(string[] args)
        {
            const string path = "../../../Var2/bin/Debug/out.ser";
            Street[]streetsArray = DeserializeStreets(path);
            Street[] magic = MagicStreets(streetsArray);
            Console.WriteLine("Волшебные улицы: ");
            foreach (var st in magic) Console.WriteLine(st);
            Console.ReadLine();
        }
    }
}
