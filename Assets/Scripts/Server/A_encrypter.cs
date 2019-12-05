using UnityEngine;
using System.Security.Cryptography;
using System;
using System.Text;

public class A_encrypter
{
    #region Public Server Key
    private const string MODULUS = "ok/NHfH0xGxGnnBMI64v0k5Em1YyOMi8DhdcfXzA3uVOc4eOSGGnNFgZDzC2SeVIaKid7Z57PwEjKyLKY3FJd81bckeKWJFWh6t8zP8oTuMOEsd7Uc0DwZABblCnQ7AaQ7y+fq2D0ge4KssFff+2UsjH8djtVUUkylCuXCSXhhEM6sn4j58QkuVlqyGJ9HDimapKJc3x9uGuLYxZ9l6KggGA8Er8Kss1EKGDJqdGp8GXX0RZOcfdgRIOmPsjjKiKTxnjDBlxhkZ2OcA8CkXIWJRadysBoV4pq0UsnSau0nS5ZCkSFgHgZnpIYYU1RecuTymClgkUzoOEn8X92F6Dew==";
    private const string EXPONENT = "AQAB";
    private const int SIZE = 2048;
    #endregion

    /*Desencripta un mensaje con una llave privada*/
    public static string RSAdecrypt(string encrypt_msg, RSACryptoServiceProvider RSA)
    {
        int minsize = 0;
        int maxsize = 344;
        string encode_msg = "";
        while (encrypt_msg.Length > minsize + maxsize)
        {
            encode_msg += Encoding.UTF8.GetString(RSA.Decrypt(Convert.FromBase64String(encrypt_msg.Substring(minsize, maxsize)), true));
            minsize += maxsize;
        }
        encode_msg += Encoding.UTF8.GetString(RSA.Decrypt(Convert.FromBase64String(encrypt_msg.Substring(minsize, encrypt_msg.Length - minsize)), true));
        return encode_msg;
    }

    /*Encripta un mensaje con una llave pública*/
    public static string RSAencrypt(string string_msg, RSACryptoServiceProvider RSA)
    {
        int minsize = 0;
        int maxsize = 200;
        string encode_msg = "";
        while (string_msg.Length > minsize + maxsize)
        {
            encode_msg += Convert.ToBase64String(RSA.Encrypt(Encoding.UTF8.GetBytes(string_msg.Substring(minsize, maxsize)), true));
            minsize += maxsize;
        }
        encode_msg += Convert.ToBase64String(RSA.Encrypt(Encoding.UTF8.GetBytes(string_msg.Substring(minsize, string_msg.Length-minsize)), true));

        return encode_msg;
    }

    /*Genera la llave privada del cliente*/
    public static RSAParameters RSAgenerateKeyPrint(int size = SIZE)
    {
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(size);
        RSAParameters RSAKeyInfo = RSA.ExportParameters(true);
        Debug.Log(Convert.ToBase64String(RSAKeyInfo.Modulus));
        Debug.Log(Convert.ToBase64String(RSAKeyInfo.Exponent));
        Debug.Log(Convert.ToBase64String(RSAKeyInfo.D));
        Debug.Log(Convert.ToBase64String(RSAKeyInfo.P));
        Debug.Log(Convert.ToBase64String(RSAKeyInfo.Q));
        Debug.Log(Convert.ToBase64String(RSAKeyInfo.InverseQ));
        return RSAKeyInfo;
    }

    /*Genera una llave privada RSA*/
    public static RSACryptoServiceProvider RSAgenerateKey(int size = SIZE)
    {
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(size);
        return RSA;
    }

    /*Genera un RSA con llave publica*/
    public static RSACryptoServiceProvider RSApublicConstruct(int size = SIZE, string modulus = MODULUS, string exponent = EXPONENT)
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(size);
        RSAParameters rsaParameters = new RSAParameters();
        rsaParameters.Modulus = Convert.FromBase64String(modulus);
        rsaParameters.Exponent = Convert.FromBase64String(exponent);
        rsa.ImportParameters(rsaParameters);
        return rsa;
    }
}

