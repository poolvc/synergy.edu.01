using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Compression;
using System.IO;

/// <summary>
/// Summary description for ViewStateZip
/// </summary>
public class ViewStateZip
{
	public ViewStateZip()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static byte[] Compress(byte[] bytes)
    {
        if (bytes != null)
        {
            MemoryStream memory = new MemoryStream();
            DeflateStream compStream = new DeflateStream(memory, CompressionMode.Compress);
            compStream.Write(bytes, 0, bytes.Length);
            compStream.Close();
            return memory.ToArray();
        }
        else throw new ArgumentNullException("bytes");
    }

    public static byte[] Decompress(byte[] bytes) 
    {  
       if (bytes != null)  
       {  
           DeflateStream compStream = new DeflateStream(new MemoryStream(bytes),CompressionMode.Decompress);  
           MemoryStream memory = new MemoryStream();  
           int theByte = compStream.ReadByte();  
 
            while (theByte != -1) 
            {  
                memory.WriteByte((byte)theByte);  
                theByte = compStream.ReadByte(); 
            }  
            compStream.Close();  
            return memory.ToArray();  
          }  
        else throw new ArgumentNullException("bytes");  
      }
}
