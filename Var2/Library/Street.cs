using System;

namespace Library
{
    [Serializable]
    public class Street
    {
        public string name;
        public int[] houses;

        public Street() { }
        public Street(string name, int[]housesArr)
        {
            this.name = name;
            houses = new int[housesArr.Length];
            for(int i = 0; i < houses.Length; i++)
            {
                houses[i] = housesArr[i];
            }
        }
        /// <summary>
        /// Возвращает количество домов на улице или -1,
        /// если список домов не был создан
        /// </summary>
        public static int operator ~(Street st)
        {
            try
            {
                return st.houses.Length;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Список из номеров домов не был создан");
                return -1;
            }
        }

        /// <summary>
        /// Проверяет, есть ли хотя бы один дом, содержащий в своем номере "7"
        /// </summary>
        public static bool operator !(Street st)
        {
            foreach(var house in st.houses)
            {
                if (house.ToString().Contains("7"))
                    return true;
            }
            return false;
        }

        public override string ToString()
        {
            string res = this.name + " ";
            foreach (var house in this.houses)
                res += house.ToString() + " ";
            return res;
        }
    }
}
