/* Welcome to the "Crypting.cs"- file
 * Here is everything you need to encrypt/ decrypt,
 * create hashes and protect stuff.
 *
 * The idea behind this is to create a settingsfile 
 * that is encrypted and readable.
 * 
 * bellaPatricia
 * 2013 - May - 08
 */

using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Sc2Hack.Classes.BackEnds
{
    class Crypting
    {
        public static String CreateXor(String strLine)
        {
            var strResult = String.Empty;

            for (var i = 0; i < strLine.Length; i++)
            {
                int val = strLine[i];

                val ^= 42;

                char c = (char) val;
                strResult += c;
            }

            return strResult;
        }

        public static String CreateXor(String strLine, Int32 iSign)
        {
            var strResult = String.Empty;

            for (var i = 0; i < strLine.Length; i++)
            {
                int val = strLine[i];

                val ^= iSign;

                char c = (char)val;
                strResult += c;
            }

            return strResult;
        }

        public static String CreateSha1(String strLine)
        {
            String strResult = String.Empty;

            SHA1 sha = new SHA1CryptoServiceProvider();

            var buffer = Encoding.ASCII.GetBytes(strLine);

            buffer = sha.ComputeHash(buffer);


            for (var i = 0; i < buffer.Length; i++)
            {
                strResult += (char)buffer[i];
            }

            return strResult;
        }

        public static String CreateMd5(ref FileStream fsSourcefile)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            var bHash = md5.ComputeHash(fsSourcefile);
            fsSourcefile.Close();

            var sb = new StringBuilder();
            for (int i = 0; i < bHash.Length; i++)
            {
                sb.Append(bHash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
