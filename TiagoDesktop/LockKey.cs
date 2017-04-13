using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace TiagoDesktop
{
    public class LockKey
    {
        /*
            //CODIGO ABAIXO
            //http://stackoverflow.com/questions/11413576/how-to-implement-triple-des-in-c-sharp-complete-example
            //Lê o código
            TripleDESCryptoServiceProvider TDES = new TripleDESCryptoServiceProvider();

        TDES.GenerateIV();
            TDES.GenerateKey();
            byte[] teste = TDES.IV;

           /* for (int i = 0; i < 24; i++)
            {
                chaveCriptografada += teste[i].ToString();
            }
            MessageBox.Show("chave gerada: " + chaveCriptografada);*/

        /*

        string serial = usb.getSerialNumberFromDriveLetter("E");
        MessageBox.Show(serial);*/

        public static string Criptografa (string aCriptografar)
        {
            //Salva informação do Pendrive
            Properties.Settings.Default.Hardlook = aCriptografar;
            Properties.Settings.Default.Save();

            byte[] arrayDeChave;
            byte[] arrayACriptografar = UTF8Encoding.UTF8.GetBytes(aCriptografar);

            //Chave
            string chave = Properties.Settings.Default.ChaveSeguranca;

            //Criando HASH
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            arrayDeChave = hashMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(chave));
            //Limpa o hashMD5
            hashMD5.Clear();

            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider()
            {
                //Seta a chave secreta para o algoritimo tripleDES
                Key = arrayDeChave,

                //Modo de operação (Existem 4 outros modos)
                //Escolheremos o ECB (Eletronic code Book)
                Mode = CipherMode.ECB,

                //Modo Padding (Se algum byte extra for adicionado)
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tDES.CreateEncryptor();

            //Transforma uma região especifica de bytes do array para o arrayResultado
            byte[] arrayResultado = cTransform.TransformFinalBlock(arrayACriptografar, 0, arrayACriptografar.Length);
            //Limpa o TDES
            tDES.Clear();

            //Retorna os dados encriptografados em uma string irreconhecivel
            return Convert.ToBase64String(arrayResultado, 0, arrayResultado.Length);
        }

        public static string Descriptografa(string aDescriptografar)
        {
            byte[] arrayDeChave;
            byte[] arrayADescriptografar = Convert.FromBase64String(aDescriptografar);

            //Chave
            //string chave = "l}=O4}80AR5X4";
            string chave = Properties.Settings.Default.ChaveSeguranca;

            //Criando HASH
            MD5CryptoServiceProvider hashMD5 = new MD5CryptoServiceProvider();
            arrayDeChave = hashMD5.ComputeHash(UTF8Encoding.UTF8.GetBytes(chave));
            //Limpa o hashMD5
            hashMD5.Clear();

            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider()
            {
                //Seta a chave secreta para o algoritimo tripleDES
                Key = arrayDeChave,

                //Modo de operação (Existem 4 outros modos)
                //Escolheremos o ECB (Eletronic code Book)
                Mode = CipherMode.ECB,

                //Modo Padding (Se algum byte extra for adicionado)
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tDES.CreateDecryptor();

            //Transforma uma região especifica de bytes do array para o arrayResultado
            byte[] arrayResultado = cTransform.TransformFinalBlock(arrayADescriptografar, 0, arrayADescriptografar.Length);
            //Limpa o TDES
            tDES.Clear();

            //Retorna os dados encriptografados em uma string irreconhecivel
            return UTF8Encoding.UTF8.GetString(arrayResultado);
        }
    }
}
