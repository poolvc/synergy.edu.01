using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Synergy.Presentation.Web;
using Synergy.Infraestructure.CrossCutting;

public class Encripta
{
    /// <summary>
    /// Encripta una candena de coneccion
    /// </summary>
    /// <returns>Devuelve un string</returns>
    public string Encriptar(string pstrClave)
    {
        int intLenh, intI, intCaracter, intSemilla, intSigno;
        string strCifrado = string.Empty;

        //Encriptado tipo Spring

        intLenh = pstrClave.Trim().Length;
        intSigno = 1;

        //... Encriptado:
        //... Determino la longitud de la cadena

        for (intI = 0; intI < intLenh; intI++)
        {
            intCaracter =  Int32.Parse(Encoding.ASCII.GetBytes(pstrClave.Substring(intI,1))[0].ToString());   
	        intSemilla = (intI + 1) + 3 * intSigno;
	        intCaracter  = 	intCaracter + intSemilla;
            strCifrado = strCifrado + Convert.ToChar(intCaracter);
            intSigno = intSigno * -1;
        }

        return strCifrado;
    }

    /// <summary>
    /// Desencripta una candena de coneccion
    /// </summary>
    /// <returns>Devuelve un string</returns>
    public string Desencriptar(string pstrClaveEncriptada)
    {
        int intLenh, intI, intCaracter, intSemilla, intSigno;
        string strCifrado = string.Empty;

        //Desencriptado tipo Spring

        intLenh = pstrClaveEncriptada.Trim().Length;
        intSigno = 1;

        //... Desencriptado:
        //... Determino la longitud de la cadena

        for (intI = 0; intI < intLenh; intI++)
        {
            intCaracter = Int32.Parse(Encoding.ASCII.GetBytes(pstrClaveEncriptada.Substring(intI, 1))[0].ToString());
            intSemilla = (intI + 1) + 3 * intSigno;
            intCaracter = intCaracter - intSemilla;
            strCifrado = strCifrado + Convert.ToChar(intCaracter);
            intSigno = intSigno * -1;
        }
        return strCifrado;
    }

    public string Desencriptar_db(string pstrClaveEncriptada)
    {
        string strTabla, strOuput;
        int intK, intM, intPos, intLenh;

        if (pstrClaveEncriptada.Trim().Length == 0)
            return string.Empty;
            
        intLenh = pstrClaveEncriptada.Trim().Length;
        strOuput = string.Empty;
        strTabla = "5736082914";

        if (intLenh == 36)
        {
            intLenh = (int)(Math.Sqrt(Double.Parse(pstrClaveEncriptada.Substring(30, 3)) - 200));
            for (intK = 0; intK < intLenh; intK++)
            {
                intPos = strTabla.IndexOf(intK.ToString("00").Substring(1, 1));
                intM = (int)Double.Parse(pstrClaveEncriptada.Substring((intPos - 2) * 3 + 1, 3));

                intM = intM - intK * intK;
                strOuput = strOuput + Convert.ToChar(intM);
            }
        }
        else if (intLenh == 66)
        {
            intLenh = (int)(Math.Sqrt(Double.Parse(pstrClaveEncriptada.Substring(60, 3)) - 200));
            for (intK = 0; intK < intLenh; intK++)
            {
                if (intK > 10)
                {
                    intM = intK - 10;
                }
                else
                {
                    intM = intK;
                }
                intPos = strTabla.IndexOf(intM.ToString("00").Substring(2, 1));
                if (intK > 10)
                {
                    intPos = intPos + 10;
                }
                intM = (int)Double.Parse(pstrClaveEncriptada.Substring((intPos - 1) * 3 + 1, 3));

                intM = intM - intK * intK;
                strOuput = strOuput + Convert.ToChar(intM);
            }
        }
        return strOuput;
    }

    public string Encriptar(string cadena, string vector, string llave)
    {
        EncriptadorDLL.Encriptador objEncriptacion = new EncriptadorDLL.Encriptador();
        return objEncriptacion.Encripta(cadena, vector, llave);
    }

    private static string Desencriptar(string cadenaEncriptada, string vector, string llave)
    {
        EncriptadorDLL.Encriptador objEncriptacion = new EncriptadorDLL.Encriptador();
        return objEncriptacion.Desencripta(cadenaEncriptada, vector, llave);
    }

    public string DesencriptarCadena(string pstrCadena)
    {
        string strCadena = string.Empty;
        strCadena = Desencriptar(pstrCadena, AppSettings.CadenaVector, AppSettings.CadenaClave);
        return strCadena;
    }

    public string EncriptarCadena(string pstrCadena)
    {
        string strCadena = string.Empty;
        strCadena = Encriptar(pstrCadena, AppSettings.CadenaVector, AppSettings.CadenaClave);
        return strCadena;
    }

    public bool EsValidoDesencriptarCadena(string pstrCadena)
    {
        string strCadena = string.Empty;
        try
        {
            strCadena = Desencriptar(pstrCadena, AppSettings.CadenaVector, AppSettings.CadenaClave);
        }
        catch
        {
            return false;
        }
        return true;
    }

    public bool EsValidoEncriptarCadena(string pstrCadena)
    {
        string strCadena = string.Empty;
        try
        {
            strCadena = Encriptar(pstrCadena, AppSettings.CadenaVector, AppSettings.CadenaClave);
        }
        catch
        {
            return false;
        }
        return true;
    }
}
