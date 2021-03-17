using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Xml.Linq;
using static System.Convert;
namespace Packt.Shared
{
    public class Protector
    {
        // salt size must be at least 8 bytes, we will use 16 bytes
        // unicode means UTF-16 encoding
        private static readonly byte[] salt = Encoding.Unicode.GetBytes("7BANANAS");
        private static readonly int iterations = 2000;

    }
}
