using System;
using System.Text;

namespace EncryptionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");
            int length = Encoding.UTF8.GetByteCount("我是薛景文");
            Console.WriteLine("length:"+ length);
            Console.WriteLine("salt:"+ salt.Length);
        }
    }
}
