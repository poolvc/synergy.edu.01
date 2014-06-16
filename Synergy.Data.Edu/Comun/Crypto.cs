using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Synergy.Data.Edu
{

    // Nested Types
    public enum TamanosLLaves
    {
        llave = 8,
        vector = 0x18
    }


    //class Encripta
    //{

    //    private byte[] DeCadenaAVector(string texto, TamanosLLaves tamanofijo)
    //    {
    //        int index = 0;
    //        string str = texto.Trim();
    //        int length = texto.Length;
    //        switch (tamanofijo)
    //        {
    //            case TamanosLLaves.llave:
    //                texto = (texto + new string('0', 8).PadLeft(8));
    //                break;

    //            case TamanosLLaves.vector:
    //                texto = (texto + new string('0', 0x18).PadLeft(0x18));
    //                break;
    //        }
    //        byte[] buffer2 = new byte[(int)tamanofijo];
    //        int num3 = ((int)tamanofijo) - 1;
    //        for (index = 0; index <= num3; index++)
    //        {
    //            buffer2[index] = Convert.ToByte("&H" + Convert.Hex(Strings.Asc(Strings.Mid(str, index + 1, 1))));
    //        }
    //        return buffer2;
    //    }



    //    public string Desencripta(string CadenaCifrada, string vectorcad, string llavecad)
    //    {
    //        byte[] cypherData = Convert.FromBase64String(CadenaCifrada);
    //        byte[] key = this.DeCadenaAVector(vectorcad, TamanosLLaves.vector);
    //        byte[] iV = this.DeCadenaAVector(llavecad, TamanosLLaves.llave);
    //        byte[] bytes = this.Desencriptar(cypherData, iV, key);
    //        return Encoding.Unicode.GetString(bytes);
    //    }


    //    public string Encripta(string Cadena, string vectorcad, string llavecad)
    //    {
    //        byte[] bytes = Encoding.Unicode.GetBytes(Cadena);
    //        byte[] key = this.DeCadenaAVector(vectorcad, TamanosLLaves.vector);
    //        byte[] iV = this.DeCadenaAVector(llavecad, TamanosLLaves.llave);
    //        return Convert.ToBase64String(this.Encriptar(bytes, iV, key));
    //    }

    //    private byte[] Desencriptar(byte[] cypherData, byte[] IV, byte[] Key)
    //    {
    //        MemoryStream stream2 = new MemoryStream();
    //        TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
    //        provider.Key = Key;
    //        provider.IV = IV;
    //        CryptoStream stream = new CryptoStream(stream2, provider.CreateDecryptor(), CryptoStreamMode.Write);
    //        stream.Write(cypherData, 0, cypherData.Length);
    //        stream.Close();
    //        return stream2.ToArray();
    //    }

 

    //}
}
