using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Lector_De_Lotes
{
    class Program
    {
        static void Main(string[] args)
        {
            List<LOTE> lotes = new List<LOTE>();
            string line;
            int numero_lineas = 0;
            int coordenada = 0;
            int i = 0;
            string coord = "";

            System.IO.StreamReader file = new System.IO.StreamReader(@"C:\Users\alexs\Desktop\lotes\Lector_De_Lotes\Datos.txt");
            while ((line = file.ReadLine()) != null)
            {
                LOTE lote = new LOTE();
                COLINDANCIAS colindancia = new COLINDANCIAS();

                int Mposition;
                int Sposition;
                int Uposition;
                int Lcposition;
                int Mtsposition;

                if (line.Contains("LOTE NO."))
                {
                    string Lote;
                    string Manzana;
                    string Superficie;
                    string Uso;

                    Mposition = line.IndexOf("MANZANA");
                    Sposition = line.IndexOf("SUPERFICIE");
                    Uposition = line.IndexOf("USO");

                    Lote = line.Substring(9, Mposition - 10);
                    Manzana = line.Substring(Mposition + 8, Sposition - (Mposition + 8));
                    Superficie = line.Substring(Sposition + 12, Uposition - (Sposition + 12));
                    Uso = line.Substring(Uposition + 5);

                    lote.NoLote = Lote.Trim();
                    lote.Manzana = Manzana.Trim();
                    lote.Superficie = Superficie.Trim();
                    lote.Uso = Uso.Trim();
                    lote.Colindancias = new List<COLINDANCIAS>();

                    coordenada++;

                    lotes.Add(lote);
                }
                else
                {
                    if (line.StartsWith(" AL"))
                    {
                        if (line.Contains("Lc ="))
                        {
                            Lcposition = line.IndexOf("Lc");
                            Mtsposition = line.IndexOf("MTS.");

                            string lc = line.Substring(Lcposition, (Mtsposition + 4) - (Lcposition));
                            coord = line.Substring(3, 22);
                            string vecino = line.Substring(Mtsposition + 8);

                            colindancia.Superficie = lc.Trim();
                            colindancia.Coordenada = coord.Trim();
                            colindancia.Vecino = vecino.Trim();
                        }
                        else
                        {
                            int Supposition = line.IndexOf("");
                            Mtsposition = line.IndexOf("MTS.");

                            string sup = line.Substring(25, (Mtsposition + 4) - 25);
                            coord = line.Substring(3, 22);
                            string vecino = line.Substring(Mtsposition + 8);

                            colindancia.Superficie = sup.Trim();
                            colindancia.Coordenada = coord.Trim();
                            colindancia.Vecino = vecino.Trim();
                        }

                        lotes[coordenada - 1].Colindancias.Add(colindancia);
                    }
                    else if (line.StartsWith("                         Lc"))
                    {
                        Lcposition = line.IndexOf("Lc");
                        Mtsposition = line.IndexOf("MTS.");

                        string lc = line.Substring(Lcposition, (Mtsposition + 4) - (Lcposition));
                        string vecino = line.Substring(Mtsposition + 8);

                        colindancia.Superficie = lc.Trim();
                        colindancia.Coordenada = coord.Trim();
                        colindancia.Vecino = vecino.Trim();

                        lotes[coordenada - 1].Colindancias.Add(colindancia);
                    }
                    else if (line.Contains("MTS.") && int.TryParse(line.Substring(25,1), out i))
                    {
                        Mtsposition = line.IndexOf("MTS.");

                        string sup = line.Substring(24, (Mtsposition + 4) - 24);
                        string vecino = line.Substring(Mtsposition + 8);

                        colindancia.Coordenada = coord.Trim();
                        colindancia.Superficie = sup.Trim();
                        colindancia.Vecino = vecino.Trim();

                        lotes[coordenada - 1].Colindancias.Add(colindancia);
                    }
                    
                }
                lote = null;
                colindancia = null;
            }

            foreach (var l in lotes)
            {
                System.Console.WriteLine("Numero de lote: " + l.NoLote + ", Manzana: " + l.Manzana + ", Superficie: " + l.Superficie);
                if (l.Colindancias.Count != 0)
                {
                    foreach (var col in l.Colindancias)
                    {
                        System.Console.WriteLine("Coordenada: " + col.Coordenada + ", Superficie: " + col.Superficie + ", Vecino: " + col.Vecino);
                    }
                    
                }
                System.Console.WriteLine("\n");
            }

            numero_lineas++;

        
        file.Close();
            System.Console.ReadLine();
            
        } 
    }
}
