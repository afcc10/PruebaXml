using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace XmlProyecto
{
    public class Program
    {
        

        static void Main(string[] args)
        {
            Proceso obj = new Proceso();
            obj.ProcesoXml();
        }
        
    }

    public class Proceso
    {
        public int versionNew;
        public int version;

        string ruta = @"D:\Ejecutable";
        string rutaNuevo = @"D:\NuevaRuta";        

        public void ProcesoXml()
        {
            string patron = @"[^\w]";
            Regex regex = new Regex(patron);

            using (XmlReader reader = XmlReader.Create(rutaNuevo + "\\prueba.xml"))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name.ToString())
                        {
                            case "version":
                                versionNew = int.Parse(regex.Replace(reader.ReadString(), ""));
                                break;
                        }
                    }                    
                }
            }

            using (XmlReader readerEje = XmlReader.Create(ruta + "\\prueba.xml"))
            {
                while (readerEje.Read())
                {
                    if (readerEje.IsStartElement())
                    {
                        switch (readerEje.Name.ToString())
                        {
                            case "version":
                                version = int.Parse(regex.Replace(readerEje.ReadString(), ""));
                                break;
                        }
                    }                    
                }
            }

            if (versionNew > version && (versionNew != 0 && version != 0))
            {
                try
                {
                    File.Delete(ruta + "\\prueba.xml");
                    if (File.Exists(ruta + "\\prueba.xml"))
                    {
                        Console.WriteLine("El archivo sigue existiendo.");
                    }
                    else
                    {
                        File.Copy(rutaNuevo + "\\prueba.xml", ruta  +"\\prueba.xml");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error al borrar archivo: {0}", e.ToString());
                }
            }            
            Console.ReadKey();
        }
    }
}
