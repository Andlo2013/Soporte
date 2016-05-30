using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TicketsMVC.Clases
{
    public class clsUtilidades
    {
        private static byte[] keyCifrado = Encoding.ASCII.GetBytes("SI-Tickets");
        private static byte[] ivCifrado = Encoding.ASCII.GetBytes("395.42solinteg18");

        public static SqlParameter[] _ParamsSQL(string [] paramsNames,object [] paramsValues)
        {
            SqlParameter[] result = null;
            if(paramsNames!=null && paramsValues!=null && paramsNames.Length == paramsValues.Length)
            {
                result = new SqlParameter[paramsNames.Length];
                for(int i = 0; i < paramsNames.Length; i++)
                {
                    result[i] = new SqlParameter(paramsNames[i], paramsValues[i]);
                }
            }
            return result;
        }

        public static string _Encripta(string Cadena)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(Cadena);
            byte[] encripted;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes.Length))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateEncryptor(keyCifrado, ivCifrado), CryptoStreamMode.Write))
                {
                    objCryptoStream.Write(inputBytes, 0, inputBytes.Length);
                    objCryptoStream.FlushFinalBlock();
                    objCryptoStream.Close();
                }
                encripted = ms.ToArray();
            }
            return Convert.ToBase64String(encripted);
        }

        public static string _Desencripta(string Cadena)
        {
            byte[] inputBytes = Convert.FromBase64String(Cadena);
            byte[] resultBytes = new byte[inputBytes.Length];
            string textoLimpio = String.Empty;
            RijndaelManaged cripto = new RijndaelManaged();
            using (MemoryStream ms = new MemoryStream(inputBytes))
            {
                using (CryptoStream objCryptoStream = new CryptoStream(ms, cripto.CreateDecryptor(keyCifrado, ivCifrado), CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(objCryptoStream, true))
                    {
                        textoLimpio = sr.ReadToEnd();
                    }
                }
            }
            return textoLimpio;
        }

    }
}