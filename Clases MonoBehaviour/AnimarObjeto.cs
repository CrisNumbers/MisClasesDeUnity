using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Clase de Unity para agregar animaciones a los objetos del juego, por medio de código.
/// </summary>
public class AnimarObjeto : MonoBehaviour
{
    /// <summary>
    /// Clase para realizar PseudoExcepciones de la clase AnimarObjeto. No detendrá el programa, pero mostrará un mensaje de error en la consola.
    /// </summary>
    public static class AnimarObjetoPseudoException
    {
        private static readonly string Titulo = "{AnimarObjetoPseudoException}: ";
        private static readonly string Titulo_IndiceNegativo = "<IndiceNegativo>: ";
        private static readonly string Titulo_FueraDeIndice = "<FueraDeIndice>: ";
        private static readonly string Titulo_NoHayElementos = "<NoHayElementos>: ";
        public static void IndiceNegativo(int indiceP) =>
            Debug.LogError(
                Titulo +
                Titulo_IndiceNegativo +
                string.Format(
                    "El indice proporcionado es negativo, por lo que no puede acceder a la lista. " +
                    "El indice es {0}.", indiceP)
                );
        public static void FueraDeIndice(int indiceP, int listaCountP) => 
            Debug.LogError(
                Titulo + 
                Titulo_FueraDeIndice + 
                string.Format(
                    "El indice proporcionado no corresponde a algun elemento de la lista. " +
                    "El indice es {0} y la lista contiene {1} elementos.",indiceP, listaCountP)
                );
        public static void NoHayElementos() =>
            Debug.LogError(
                Titulo +
                Titulo_NoHayElementos +
                "La lista no contiene ningún elemento, y se está intentando acceder a ella por medio de un indice."
                );
    }
    /// <summary>
    /// Clase para contener las variables base para una animación.
    /// </summary>
    [System.Serializable]
    public sealed class AnimarObjetoSettings
    {
        /// <summary>
        /// Crea una instancia de AnimarObjetoSettings, especificando obligatoriamente la duración de la animación y la duración del retraso.
        /// </summary>
        /// <param name="tiempoP">Duración que tendrá la animación.</param>
        /// <param name="delayP">Duración que tendrá el retraso.</param>
        public AnimarObjetoSettings(float tiempoP, float delayP)
        {
            delay = delayP;
            tiempo = tiempoP;
            vectorTraslacion = Vector3.zero;
            vectorRotacion = Vector3.zero;
            vectorEscalacion = Vector3.zero;
            ReiniciarValoresBase();
        }
        /// <summary>
        /// Reiniciar los valores principales: activado, finalizado, tiempoContador y delayContador.
        /// </summary>
        public void ReiniciarValoresBase()
        {
            activado = false;
            finalizado = false;
            tiempoContador = 0;
            delayContador = 0;
        }

        /// <summary>
        /// Delay inicial en segundos antes de que inicie la animación.
        /// </summary>
        [Tooltip("Delay inicial en segundos antes de que inicie la animación.")]
        [Min(0)]
        public float delay;
        /// <summary>
        /// Si la animación está en curso, mostrará el tiempo del delay actual.
        /// </summary>
        [Tooltip("Si la animación está en curso, mostrará el tiempo del delay actual.")]
        [Min(0)]
        public float delayContador;
        /// <summary>
        /// Tiempo que durará la animación.
        /// </summary>
        [Tooltip("Tiempo que durará la animación.")]
        [Min(0)]
        public float tiempo;
        /// <summary>
        /// Si la animación está en curso, mostrará el tiempo actual.
        /// </summary>
        [Tooltip("Si la animación está en curso, mostrará el tiempo de la animación actual.")]
        [Min(0)]
        public float tiempoContador;
        /// <summary>
        /// Verifica sí está activada o desactivada la animación
        /// </summary>
        [Tooltip("Verifica sí está activada o desactivada la animación")]
        public bool activado;
        /// <summary>
        /// Verifica sí la animación fue finalizada con éxito.
        /// </summary>
        [Tooltip("Verifica sí la animación fue finalizada con éxito.")]
        public bool finalizado;
        /// <summary>
        /// Vector de dirección que se moverá el objeto.
        /// </summary>
        [Tooltip("Vector de dirección que se moverá el objeto.")]
        public Vector3 vectorTraslacion;
        /// <summary>
        /// Vector de dirección que se escalará el objeto.
        /// </summary>
        [Tooltip("Vector de dirección que se escalará el objeto.")]
        public Vector3 vectorEscalacion;
        /// <summary>
        /// Vector de dirección que se escalará el objeto.
        /// </summary>
        [Tooltip("Vector de dirección que se rotará el objeto.")]
        public Vector3 vectorRotacion;
    }
    /// <summary>
    /// Lista de animaciones del GameObject.
    /// </summary>
    public List<AnimarObjetoSettings> animaciones = new List<AnimarObjetoSettings>();
    /// <summary>
    /// Retorna la cantidad de animaciones del GameObject.
    /// </summary>
    public int CantidadAnimaciones => animaciones.Count;
    /// <summary>
    /// Especifica el tiempo y delay de una animación del objeto.
    /// </summary>
    /// <param name="indiceP">Indice en base 0 de la lista de animaciones del objeto. Si el indice no es menor al tamaño de la lista, se creará una nueva animación.</param>
    /// <param name="tiempoP">Tiempo que durará la animación.</param>
    /// <param name="delayP">Tiempo que durará el retraso para la animación.</param>
    /// <returns>La misma clase para hacer fluidez.</returns>
    public AnimarObjeto AgregarTiempo(int indiceP, float tiempoP, float delayP)
    {
        if (indiceP < 0)
        {
            AnimarObjetoPseudoException.IndiceNegativo(indiceP);
            return this;
        }

        if (indiceP < animaciones.Count)
        {
            animaciones[indiceP].tiempo = tiempoP;
            animaciones[indiceP].delay = delayP;
            animaciones[indiceP].ReiniciarValoresBase();
        }
        else
            animaciones.Add(new AnimarObjetoSettings(tiempoP,delayP)) ;
        return this;
    }
    /// <summary>
    /// Especifica el punto final que tendrá el objeto.
    /// </summary>
    /// <param name="indiceP">Indice en base 0 de la lista de animaciones del objeto. Si el indice no es menor al tamaño de la lista, se creará una nueva animación.</param>
    /// <param name="puntoTraslacionP">Punto final que tendrá el objeto.</param>
    /// <returns>La misma clase para hacer fluidez.</returns>
    public AnimarObjeto AgregarTraslacionPorPunto(int indiceP, Vector3 puntoTraslacionP)
    {
        if (indiceP < 0)
        {
            AnimarObjetoPseudoException.IndiceNegativo(indiceP);
            return this;
        }

        Vector3 velocidad = puntoTraslacionP - this.transform.localPosition;
        

        if (indiceP < animaciones.Count)
        {
            velocidad /= animaciones[indiceP].tiempo;

            animaciones[indiceP].vectorTraslacion = velocidad;
            animaciones[indiceP].ReiniciarValoresBase();
        }
        else
        {
            velocidad /= 5;
            animaciones.Add(new AnimarObjetoSettings(5,0) { vectorTraslacion = velocidad});
        }
        return this;
    }
    /// <summary>
    /// Especifica la dirección en la que se trasladará el objeto cada segundo.
    /// </summary>
    /// <param name="indiceP">Indice en base 0 de la lista de animaciones del objeto. Si el indice no es menor al tamaño de la lista, se creará una nueva animación.</param>
    /// <param name="direccionTraslacionP">Dirección en la que se trasladará el objeto cada segundo.</param>
    /// <returns>La misma clase para hacer fluidez.</returns>
    public AnimarObjeto AgregarTraslacionPorDireccion(int indiceP, Vector3 direccionTraslacionP)
    {
        if (indiceP < 0)
        {
            AnimarObjetoPseudoException.IndiceNegativo(indiceP);
            return this;
        }

        if (indiceP < animaciones.Count)
        {
            animaciones[indiceP].vectorTraslacion = direccionTraslacionP;
            animaciones[indiceP].ReiniciarValoresBase();
        }
        else
            animaciones.Add(new AnimarObjetoSettings(5,0) { vectorTraslacion = direccionTraslacionP });
        return this;
    }
    /// <summary>
    /// Especifica la escala final que tendrá el objeto.
    /// </summary>
    /// <param name="indiceP">Indice en base 0 de la lista de animaciones del objeto. Si el indice no es menor al tamaño de la lista, se creará una nueva animación.</param>
    /// <param name="puntoEscalacionP">Escala final que tendrá el objeto.</param>
    /// <returns>La misma clase para hacer fluidez.</returns>
    public AnimarObjeto AgregarEscalacionPorPunto(int indiceP, Vector3 puntoEscalacionP)
    {
        if (indiceP < 0)
        {
            AnimarObjetoPseudoException.IndiceNegativo(indiceP);
            return this;
        }

        Vector3 velocidad = puntoEscalacionP - this.transform.localScale;
       

        if (indiceP < animaciones.Count)
        {
            velocidad /= animaciones[indiceP].tiempo;
            animaciones[indiceP].vectorEscalacion = velocidad;
            animaciones[indiceP].ReiniciarValoresBase();
        }
        else
        {
            velocidad /= 5;
            animaciones.Add(new AnimarObjetoSettings(5, 0) { vectorEscalacion = velocidad });
        }
        return this;
    }
    /// <summary>
    /// Especifica la dirección en la que se escalará el objeto cada segundo.
    /// </summary>
    /// <param name="indiceP">Indice en base 0 de la lista de animaciones del objeto. Si el indice no es menor al tamaño de la lista, se creará una nueva animación.</param>
    /// <param name="direccionEscalacionP">Dirección en la que se escalará el objeto cada segundo.</param>
    /// <returns>La misma clase para hacer fluidez.</returns>
    public AnimarObjeto AgregarEscalacionPorDireccion(int indiceP, Vector3 direccionEscalacionP)
    {
        if (indiceP < 0)
        {
            AnimarObjetoPseudoException.IndiceNegativo(indiceP);
            return this;
        }

        if (indiceP < animaciones.Count)
        {
            animaciones[indiceP].vectorEscalacion = direccionEscalacionP;
            animaciones[indiceP].ReiniciarValoresBase();
        }
        else
            animaciones.Add(new AnimarObjetoSettings(5, 0) { vectorEscalacion = direccionEscalacionP });
        return this;
    }
    /// <summary>
    /// Especifica la dirección en la que se rotará el objeto cada segundo.
    /// </summary>
    /// <param name="indiceP">Indice en base 0 de la lista de animaciones del objeto. Si el indice no es menor al tamaño de la lista, se creará una nueva animación.</param>
    /// <param name="direccionRotacionP">Dirección en la que se rotará el objeto cada segundo.</param>
    /// <returns>La misma clase para hacer fluidez.</returns>
    public AnimarObjeto AgregarRotacionPorDireccion(int indiceP, Vector3 direccionRotacionP)
    {
        if (indiceP < 0)
        {
            AnimarObjetoPseudoException.IndiceNegativo(indiceP);
            return this;
        }

        if (indiceP < animaciones.Count)
        {
            animaciones[indiceP].vectorRotacion = direccionRotacionP;
            animaciones[indiceP].ReiniciarValoresBase();
        }
        else
            animaciones.Add(new AnimarObjetoSettings(5, 0) { vectorRotacion = direccionRotacionP });
        return this;
    }   
    /// <summary>
    /// Reproduce la primera animación agregada, incluyendo el delay del mismo.
    /// </summary>
    public void ReproducirPrimero()
    {
        if (animaciones.Count > 0)
            animaciones[0].activado = true;
        else
            AnimarObjetoPseudoException.NoHayElementos();
    }
    /// <summary>
    /// Reproduce la ultima animación agregada, incluyendo el delay del mismo.
    /// </summary>
    public void ReproducirUltimo()
    {
        if (animaciones.Count > 0) 
            animaciones[animaciones.Count - 1].activado = true;
        else
            AnimarObjetoPseudoException.NoHayElementos();
    }
    /// <summary>
    /// Reproduce una animación de la lista, incluyendo el delay. 
    /// </summary>
    /// <param name="indice">Posición de la lista de animaciones en base 0.</param>
    public void Reproducir(int indice)
    {
        if (indice < animaciones.Count && indice >= 0)
            animaciones[indice].activado = true;
        else
            AnimarObjetoPseudoException.FueraDeIndice(indice, animaciones.Count);
    }
    /// <summary>
    /// Reproduce todas las animaciones de la lista, incluyendo el delay.
    /// </summary>
    public void ReproducirTodo()
    {
        foreach (AnimarObjetoSettings ds in animaciones)
            ds.activado = true;
    }
    /// <summary>
    /// Detiene la primera animación agregada, en la posición actual.
    /// </summary>
    public void DetenerPrimero()
    {
        if (animaciones.Count > 0)
            animaciones[0].activado = false;
        else
            AnimarObjetoPseudoException.NoHayElementos();
    }
    /// <summary>
    /// Detiene la ultima animación agregada, en la posición actual.
    /// </summary>
    public void DetenerUltimo()
    {
        if (animaciones.Count > 0)
            animaciones[animaciones.Count - 1].activado = false;
        else
            AnimarObjetoPseudoException.NoHayElementos();
    }
    /// <summary>
    /// Detiene una animación de la lista, en la posición actual.
    /// </summary>
    /// <param name="indice">Posición de la lista de animaciones en base 0.</param>
    public void Detener(int indice)
    {
        if (indice < animaciones.Count && indice >= 0)
            animaciones[indice].activado = false;
        else
            AnimarObjetoPseudoException.FueraDeIndice(indice, animaciones.Count);
    }
    /// <summary>
    /// Detiene todas las animaciones de la lista. El objeto permanecerá en su posición actual.
    /// </summary>
    public void DetenerTodo()
    {
        foreach (AnimarObjetoSettings ds in animaciones)
            ds.activado = false;
    }
    /// <summary>
    /// Verifica sí la primera animación agregada fue terminada con éxito.
    /// </summary>
    /// <returns>Retorna la variable finalizado de la primera animación agregada.</returns>
    public bool EsPrimeraAnimacionTerminada()
    {
        if (animaciones.Count > 0)
            return animaciones[0].finalizado;
        else
            AnimarObjetoPseudoException.NoHayElementos();
        return false;
    }
    /// <summary>
    /// Verifica sí la última animación agregada fue terminada con éxito.
    /// </summary>
    /// <returns>Retorna la variable finalizado de la última animación agregada.</returns>
    public bool EsUltimaAnimacionTerminada()
    {
        if (animaciones.Count > 0)
            return animaciones[animaciones.Count - 1].finalizado;
        else
            AnimarObjetoPseudoException.NoHayElementos();
        return false;
    }
    /// <summary>
    /// Verifica sí una animación de la lista fue terminada con éxito.
    /// </summary>
    /// <param name="indice">Index de la lista.</param>
    /// <returns>Retorna la variable finalizado de algun elemento de la lista.</returns>
    public bool EsAnimacionTerminada(int indice)
    {
        if (indice < animaciones.Count && indice >= 0)
            return animaciones[indice].finalizado;
        else
            AnimarObjetoPseudoException.FueraDeIndice(indice, animaciones.Count);
        return false;
    }
    /// <summary>
    /// Verifica sí todas las animaciones fueron terminadas con éxito.
    /// </summary>
    /// <returns>Retorna sí todas las animaciones fueron terminadas, en base a la variable finalizado.</returns>
    public bool EsAnimacionesTerminadas()
    {
        foreach (AnimarObjetoSettings ds in animaciones)
        {
            if (!ds.finalizado)
                return false;
        }
        return true;
    }
    /// <summary>
    /// Sistema en la que se reproducirá las animaciones.
    /// </summary>
    private void Sistema()
    {
        foreach (AnimarObjetoSettings ds in animaciones)
        {
            if (ds.activado && !ds.finalizado)
            {
                if (ds.delayContador < ds.delay)
                {
                    ds.delayContador += Time.deltaTime;
                }
                else
                {
                    ds.delayContador = ds.delay;
                    if (ds.tiempoContador < ds.tiempo)
                    {
                        ds.tiempoContador += Time.deltaTime;
                        transform.localPosition = transform.localPosition + (ds.vectorTraslacion * Time.deltaTime);
                        transform.localScale = transform.localScale + (ds.vectorEscalacion * Time.deltaTime);
                        transform.Rotate(Vector3.right, ds.vectorRotacion.x * Time.deltaTime, Space.Self);
                        transform.Rotate(Vector3.up, ds.vectorRotacion.y * Time.deltaTime, Space.Self);
                        transform.Rotate(Vector3.forward, ds.vectorRotacion.z * Time.deltaTime, Space.Self);
                    }
                    else
                    {
                        ds.tiempoContador = ds.tiempo;
                        ds.finalizado = true;
                    }
                }
            }
        }
    }
    /// <summary>
    /// Metodo Update para actualizar las animaciones.
    /// </summary>
    private void Update()
    {
        Sistema();
    }

}
