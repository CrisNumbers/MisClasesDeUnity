using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Clase de Unity para obtener las traducciones en base a un archivo, por medio de código.
/// </summary>
public class TraducirTexto
{
    /// <summary>
    /// Clase para realizar PseudoExcepciones de la clase AnimarObjeto. No detendrá el programa, pero mostrará un mensaje de error en la consola.
    /// </summary>
    public static class TraducirTextoPseudoException
    {
        private static readonly string Titulo = "{TraducirTextoPseudoException}: ";
        private static readonly string Titulo_NoEsEsaLlave = "<NoEsEsaLlave>: ";
        private static readonly string Titulo_ArchivoNoEncontrado = "<ArchivoNoEncontrado>: ";
        private static readonly string Titulo_ArchivoNoGenerado = "<ArchivoNoEncontrado>: ";
        private static readonly string Titulo_LlaveRepetida = "<LlaveRepetida>: ";
        public static void NoEsEsaLlave(string llaveListaP) =>
            Debug.LogError(
                Titulo +
                Titulo_NoEsEsaLlave +
                string.Format(
                    "La llave de la lista no fue encontrada. " +
                    "La llave de la lista {0}.", llaveListaP)
                );
        public static void NoEsEsaLlave(string llaveListaP, string llaveSubListaP) =>
            Debug.LogError(
                Titulo +
                Titulo_NoEsEsaLlave +
                string.Format(
                    "La llave de la sublista no fue encontrada. " +
                    "La llave de la sublista es {0} y la llave de la lista es {1}.", llaveSubListaP, llaveListaP)
                );
        public static void NoEsEsaLlave(string llaveListaP, string llaveSubListaP, TraducirTextoIdioma idiomaP) =>
            Debug.LogError(
                Titulo +
                Titulo_NoEsEsaLlave +
                string.Format(
                    "El idioma no fue encontrado en la lista. " +
                    "El idioma es {0}, la llave de la sublista es {1} y la llave de la lista es {2}.", idiomaP.ToString(), llaveSubListaP, llaveListaP)
                );
        public static void ArchivoNoEncontrado(string archivoP) =>
            Debug.LogError(
                Titulo +
                Titulo_ArchivoNoEncontrado +
                string.Format(
                    "El archivo no fue encontrado en la carpeta ubicada. " +
                    "La dirección es {0}.", archivoP)
                );
        public static void LlaveRepetida(string llaveListaP) =>
           Debug.LogError(
               Titulo +
               Titulo_LlaveRepetida +
               string.Format(
                   "Se intentó agregar una llave repetida en la lista principal. " +
                   "La llave de la lista {0}.", llaveListaP)
               );
        public static void LlaveRepetida(string llaveListaP, string llaveSubListaP) =>
            Debug.LogError(
                Titulo +
                Titulo_NoEsEsaLlave +
                string.Format(
                    "Se intentó agregar una llave repetida en la sublista. " +
                    "La llave de la sublista es {0} y la llave de la lista es {1}.", llaveSubListaP, llaveListaP)
                );
        public static void LlaveRepetida(string llaveListaP, string llaveSubListaP, TraducirTextoIdioma idiomaP) =>
            Debug.LogError(
                Titulo +
                Titulo_NoEsEsaLlave +
                string.Format(
                    "Se intentó agregar un idioma repetido. " +
                    "El idioma es {0}, la llave de la sublista es {1} y la llave de la lista es {2}.", idiomaP.ToString(), llaveSubListaP, llaveListaP)
                );
        public static void ArchivoNoGenerado(string rutaP) =>
            Debug.LogError(
                Titulo +
                Titulo_ArchivoNoGenerado +
                string.Format(
                    "No se pudo generar el archivo plantilla de ejemplo. Es posible que la ruta no esté correcta. " +
                    "La ruta es {0}.", rutaP)
                );

    }
    /// <summary>
    /// Enum que tendrá todos los idiomas posibles.
    /// </summary>
    public enum TraducirTextoIdioma
    {
        Español, //Español
        English, //Ingles
        Français, //Frances
        Português, //Portugues
        Deutsche //Alemán
    };
    /// <summary>
    /// Crea una instancia TraducirTexto.
    /// </summary>
    public TraducirTexto()
    {

    }
    /// <summary>
    /// Version original de la clase.
    /// </summary>
    private static byte versionOriginal = 1;
    /// <summary>
    /// Version actual de la clase
    /// </summary>
    private byte version = 1;
    /// <summary>
    /// Lista con todas las traducciones.
    /// </summary>
    private Dictionary<string, Dictionary<string, Dictionary<TraducirTextoIdioma, string>>> lista = new Dictionary<string, Dictionary<string, Dictionary<TraducirTextoIdioma, string>>>();
    /// <summary>
    /// Llave actual de la lista. Se utiliza cuando se lee un archivo de texto.
    /// </summary>
    private string keyActual_lista = "";
    /// <summary>
    /// Llave actual de la sublista. Se utiliza cuando se lee un archivo de texto.
    /// </summary>
    private string keyActual_sublista = "";
    /// <summary>
    /// Llave actual del idioma. Se utiliza cuando se lee un archivo de texto.
    /// </summary>
    private TraducirTextoIdioma keyActual_idioma = TraducirTextoIdioma.Español;
    /// <summary>
    /// Agrega un elemento a la lista principal.
    /// </summary>
    /// <param name="llaveListaP">Llave única que tendrá la lista principal. En caso de existir</param>
    /// <returns>Retorna la llave agregada como parametro.</returns>
    public string AgregarLista(string llaveListaP)
    {
        if (SonLlavesEncontradas(llaveListaP))
            TraducirTextoPseudoException.LlaveRepetida(llaveListaP);
        else
            lista.Add(llaveListaP, new Dictionary<string, Dictionary<TraducirTextoIdioma, string>>());
        return llaveListaP;
    }
    /// <summary>
    /// Agrega un elemento a una sublista, en base a un elemento de la lista principal.
    /// </summary>
    /// <param name="llaveListaP">Llave única ya agregada a la lista principal.</param>
    /// <param name="llaveSubListaP">Llave única que tendrá la sublista.</param>
    /// <returns>Retorna la llave agregada como parametro.</returns>
    public string AgregarSubLista(string llaveListaP, string llaveSubListaP)
    {
        if (SonLlavesEncontradas(llaveListaP, llaveSubListaP))
            TraducirTextoPseudoException.LlaveRepetida(llaveListaP, llaveSubListaP);
        else
            lista[llaveListaP].Add(llaveSubListaP, new Dictionary<TraducirTextoIdioma, string>());
        return llaveSubListaP;
    }
    /// <summary>
    /// Agrega la traducción de un idioma, en base a un elemento de la sublista y elemento de la lista principal.
    /// </summary>
    /// <param name="llaveListaP">Llave única ya agregada a la lista principal.</param>
    /// <param name="llaveSubListaP">Llave única ya agregada a la sublista.</param>
    /// <param name="idiomaP">Llave única que tendrá el idioma.</param>
    /// <param name="traduccionP">Mensaje traducido.</param>
    /// <returns>Retorna la llave agregada como parametro.</returns>
    public string AgregarTraduccion(string llaveListaP, string llaveSubListaP, TraducirTextoIdioma idiomaP, string traduccionP)
    {
        if (SonLlavesEncontradas(llaveListaP, llaveSubListaP, idiomaP))
            TraducirTextoPseudoException.LlaveRepetida(llaveListaP, llaveSubListaP, idiomaP);
        else
            lista[llaveListaP][llaveSubListaP].Add(idiomaP, traduccionP);
        return traduccionP;
    }
    /// <summary>
    /// Transforma una secuencia string a un variable enum TraducirTextoIdioma.
    /// </summary>
    /// <param name="idiomaP">Cadena que menciona el idioma.</param>
    /// <returns></returns>
    private TraducirTextoIdioma SetIdioma(string idiomaP)
    {
        if (idiomaP == "Deutsche")
            return TraducirTextoIdioma.Deutsche;
        else if (idiomaP == "English")
            return TraducirTextoIdioma.English;
        else if (idiomaP == "Français")
            return TraducirTextoIdioma.Français;
        else if (idiomaP == "Português")
            return TraducirTextoIdioma.Português;
        else
            return TraducirTextoIdioma.Español;
    }
    /// <summary>
    /// *Metodo solo para lectura de archivos* Realiza las modificaciones necesarias al leer un archivo. 
    /// </summary>
    /// <param name="lineaP">Opcion completa, sin el prefijo ".".</param>
    private void OpcionesArchivo(string lineaP)
    {
        string[] linea = lineaP.Split(new char[] { ':' }, 2);
        if (linea[0].ToLower() == "version")
            version = byte.Parse(linea[1]);
    }
    /// <summary>
    /// Lee un archivo y crea todas las instancias de la lista.
    /// </summary>
    /// <param name="filepathP">Dirección en donde se encuentra el archivo.</param>
    /// <param name="isPersistentDataPath">En caso de ser verdadero, la ruta se leerá apartir de la carpeta persistent de Unity.</param>
    public void LeerArchivo(string filepathP, bool isPersistentDataPath)
    {
        //string urlP = isPersistentDataPath ? Application.persistentDataPath + "/" + filepathP : filepathP;
        string urlP = filepathP;
        try
        {
            StreamReader sr = new StreamReader(urlP);
            string linea = sr.ReadLine();
            byte estado = 0;

            while (estado != 2 && linea != null && linea != "")
            {
                if (estado == 0)
                    switch (linea[0])
                    {
                        case '{':
                            estado = 1;
                            break;
                        case '.':
                            OpcionesArchivo(linea.Substring(1));
                            break;
                        default:
                            break;
                    }
                else if (estado == 1)
                {
                    if (linea[0] == '}')
                        estado = 2;
                    else if (linea.Contains("\t\t\t\t"))
                        AgregarTraduccion(keyActual_lista, keyActual_sublista, keyActual_idioma, linea.Substring(4));
                    else if (linea.Contains("\t\t\t"))
                        keyActual_idioma = SetIdioma(linea.Substring(3));
                    else if (linea.Contains("\t\t"))
                        keyActual_sublista = AgregarSubLista(keyActual_lista, linea.Substring(2));
                    else if (linea.Contains("\t"))
                        keyActual_lista = AgregarLista(linea.Substring(1));
                }
                linea = sr.ReadLine();
            }

            sr.Close();
        }
        catch
        {
            TraducirTextoPseudoException.ArchivoNoEncontrado(filepathP);
        }
    }
    /// <summary>
    /// Verifica si una llave se encuentra en la lista.
    /// </summary>
    /// <param name="llaveListaP">Llave a buscar.</param>
    /// <returns>Retorna true si las llaves fueron encontradas en la lista.</returns>
    private bool SonLlavesEncontradas(string llaveListaP)
    {
        if (!lista.ContainsKey(llaveListaP))
        {
            //TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Verifica si una llave se encuentra en la lista.
    /// </summary>
    /// <param name="llaveListaP">Llave de la lista principal a buscar.</param>
    /// <param name="llaveSubListaP">Llave de la sublista a buscar.</param>
    /// <returns>Retorna true si las llaves fueron encontradas en la lista.</returns>
    private bool SonLlavesEncontradas(string llaveListaP, string llaveSubListaP)
    {
        if (!lista.ContainsKey(llaveListaP))
        {
            //TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP);
            return false;
        }
        else if (!lista[llaveListaP].ContainsKey(llaveSubListaP))
        {
            //TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP, llaveSubListaP);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Verifica si una llave se encuentra en la lista. 
    /// </summary>
    /// <param name="llaveListaP">Llave de la lista principal a buscar.</param>
    /// <param name="llaveSubListaP">Llave de la sublista a buscar.</param>
    /// <param name="idiomaP">Llave de los idiomas a buscar.</param>
    /// <returns>Retorna true si las llaves fueron encontradas en la lista.</returns>
    private bool SonLlavesEncontradas(string llaveListaP, string llaveSubListaP, TraducirTextoIdioma idiomaP)
    {
        if (!lista.ContainsKey(llaveListaP))
            return false;
        else if (!lista[llaveListaP].ContainsKey(llaveSubListaP))
            return false;
        else if (!lista[llaveListaP][llaveSubListaP].ContainsKey(idiomaP))
            return false;
        return true;
    }
    /// <summary>
    /// Verifica si una llave se encuentra en la lista. En caso de no encontrarlo, lanzará una PseudoExcepcion.
    /// </summary>
    /// <param name="llaveListaP">Llave de la lista principal a buscar.</param>
    /// <returns>Retorna true si las llaves fueron encontradas en la lista.</returns>
    private bool SonLlavesEncontradasException(string llaveListaP)
    {
        if (!lista.ContainsKey(llaveListaP))
        {
            TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Verifica si una llave se encuentra en la lista. En caso de no encontrarlo, lanzará una PseudoExcepcion.
    /// </summary>
    /// <param name="llaveListaP">Llave de la lista principal a buscar.</param>
    /// <param name="llaveSubListaP">Llave de la sublista a buscar.</param>
    /// <returns>Retorna true si las llaves fueron encontradas en la lista.</returns>
    private bool SonLlavesEncontradasException(string llaveListaP, string llaveSubListaP)
    {
        if (!lista.ContainsKey(llaveListaP))
        {
            TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP);
            return false;
        }
        else if (!lista[llaveListaP].ContainsKey(llaveSubListaP))
        {
            TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP, llaveSubListaP);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Verifica si una llave se encuentra en la lista. En caso de no encontrarlo, lanzará una PseudoExcepcion.
    /// </summary>
    /// <param name="llaveListaP">Llave de la lista principal a buscar.</param>
    /// <param name="llaveSubListaP">Llave de la sublista a buscar.</param>
    /// <param name="idiomaP">Llave de los idiomas a buscar.</param>
    /// <returns>Retorna true si las llaves fueron encontradas en la lista.</returns>
    private bool SonLlavesEncontradasException(string llaveListaP, string llaveSubListaP, TraducirTextoIdioma idiomaP)
    {
        if (!lista.ContainsKey(llaveListaP))
        {
            TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP);
            return false;
        }
        else if (!lista[llaveListaP].ContainsKey(llaveSubListaP))
        {
            TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP, llaveSubListaP);
            return false;
        }
        else if (!lista[llaveListaP][llaveSubListaP].ContainsKey(idiomaP))
        {
            TraducirTextoPseudoException.NoEsEsaLlave(llaveListaP, llaveSubListaP, idiomaP);
            return false;
        }
        return true;
    }
    /// <summary>
    /// Obtiene la traducción correcta, en base a las llaves ya agregadas.
    /// </summary>
    /// <param name="llaveListaP">Llave de la lista principal a buscar.</param>
    /// <param name="llaveSubListaP">Llave de la sublista a buscar.</param>
    /// <param name="idiomaP">Llave de los idiomas a buscar.</param>
    /// <returns>Retorna la traducción.</returns>
    public string ObtenerTraduccion(string llaveListaP, string llaveSubListaP, TraducirTextoIdioma idiomaP)
    {
        if (!SonLlavesEncontradasException(llaveListaP, llaveSubListaP, idiomaP))
            return "";

        return lista[llaveListaP][llaveSubListaP][idiomaP];
    }
    /// <summary>
    /// Imprime la lista completa en la consola de Unity.
    /// </summary>
    public void ImprimirLista()
    {
        Debug.Log("{TraducirTexto}: <Imprimiendo lista>: ");
        Dictionary<string, Dictionary<string, Dictionary<TraducirTextoIdioma, string>>>.KeyCollection listaPrincipal = lista.Keys;
        foreach (string llavePrincipal in listaPrincipal)
        {
            Dictionary<string, Dictionary<TraducirTextoIdioma, string>>.KeyCollection sublista = lista[llavePrincipal].Keys;
            foreach (string llaveSecundaria in sublista)
            {
                Dictionary<TraducirTextoIdioma, string>.KeyCollection idiomas = lista[llavePrincipal][llaveSecundaria].Keys;
                foreach (TraducirTextoIdioma llaveTerciaria in idiomas)
                {
                    Debug.Log("<Elemento>: " + llavePrincipal + " -> " + llaveSecundaria + " -> " + llaveTerciaria + " ==> " + lista[llavePrincipal][llaveSecundaria][llaveTerciaria]);
                }
            }
        }
        Debug.Log("{TraducirTexto}: <Terminado <Imprimiento lista>>");

    }
    /// <summary>
    /// Genera un archivo de texto que sirve como plantilla para la lectura de archivos.
    /// </summary>
    /// <param name="carpetaP">Dirección de la carpeta en donde se creará el archivo.</param>
    public static void GenerarPlantilla(string carpetaP)
    {
        string ruta = carpetaP + "plantillaTraducirTexto.txt";
        try
        {
            StreamWriter sw = new StreamWriter(ruta);
            sw.WriteLine("#-----------------------------------------");
            sw.WriteLine("# Plantilla de ejemplo version " + versionOriginal);
            sw.WriteLine("#-----------------------------------------");
            sw.WriteLine(".Version:" + versionOriginal);
            sw.WriteLine("# Todo se maneja con tabulaciones");
            sw.WriteLine("# Idiomas:");
            sw.WriteLine("# Español, English, Français, Português, Deutsche");
            sw.WriteLine("{");
            sw.WriteLine("\tLlave Elemento Principal");
            sw.WriteLine("\t\tSubElemento");
            sw.WriteLine("\t\t\tEspañol");
            sw.WriteLine("\t\t\t\tEsto es la traduccion en español.");
            sw.WriteLine("\t\t\tEnglish");
            sw.WriteLine("\t\t\t\tThis is the English translation.");
            sw.WriteLine("\t\tSubElemento2");
            sw.WriteLine("\t\t\tDeutsche");
            sw.WriteLine("\t\t\t\tDies ist die englische Übersetzung.");
            sw.WriteLine("}");
            sw.Close();
        }
        catch
        {
            TraducirTextoPseudoException.ArchivoNoGenerado(ruta);
        }
    }
}
