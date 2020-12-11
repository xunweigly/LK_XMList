using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fuzhu
{

    public  class AnyConvert
    {
        /// <summary>
        ///转换任意进制
        /// </summary>
        /// <param name="v">转换源数值</param>
        /// <param name="fromBase">目标进制</param>
        /// <returns></returns>
        public static string ConvertToString(int v, int fromBase)
        {
            if (fromBase < 2 || fromBase > 36) throw new ArgumentException();
            List<char> cs = new List<char>(36);
            while (v > 0)
            {
                int x = v % fromBase;
                int c = 48;
                if (x >= 10) { c = 87; }
                cs.Add((char)(x + c));
                v /= fromBase;
            }
            cs.Reverse();
            return new string(cs.ToArray());
        }

        /// <summary>
        /// 转换32进制
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static int ConvertToInt32(char c)
        {
            if (c >= 48 && c <= 57) { return c - 48; }
            if (c >= 97 && c <= 122) { return c - 87; }
            if (c >= 65 && c <= 90) { return c - 55; }
            return 0;
        }
        /// <summary>
        /// 转换10进制
        /// </summary>
        /// <param name="v"></param>
        /// <param name="fromBase"></param>
        /// <returns></returns>
        public static int ConvertToInt(string v, int fromBase)
        {
            char[] chars = v.ToCharArray();
            Array.Reverse(chars);
            int fb = 1;
            int value = 0;
            foreach (char aa in chars)
            {
                value += ConvertToInt32(aa) * fb;
                fb *= fromBase;
            }
            return value;
        }
    }
}
