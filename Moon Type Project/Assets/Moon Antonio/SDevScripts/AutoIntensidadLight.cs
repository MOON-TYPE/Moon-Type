//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// AutoDestroy.cs (21/02/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Auto aumenta y disminuye la intensidad de la luz			\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio de namespace												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDevScripts
{
    /// <summary>
    /// <para>Auto aumenta y disminuye la intensidad de la luz</para>
    /// </summary>
    [AddComponentMenu("MoonAntonio/Dev/AutoIntensidadLight")]
    [RequireComponent(typeof(Light))]
    public class AutoIntensidadLight : MonoBehaviour 
	{
        #region Variables Publicas
        /// <summary>
        /// <para>Valor minimo de la intensidad.</para>
        /// </summary>
        [Range(0, 8)]
        public float minIntensidad = 0;                             // Valor minimo de la intensidad
        /// <summary>
        /// <para>Valor maximo de la intensidad.</para>
        /// </summary>
        [Range(0, 8)]
        public float maxIntensidad = 7;                             // Valor maximo de la intensidad
        /// <summary>
        /// <para>Velocidad de la intensidad.</para>
        /// </summary>
        public float vel = 1;                                       // Velocidad de la intensidad
        /// <summary>
        /// <para>Tiempo antes de empezar el ciclo.</para>
        /// </summary>
        public float timeDelay = 1;                                 // Tiempo antes de empezar el ciclo
        /// <summary>
        /// <para>Maximo valor de intensidad alcanzado.</para>
        /// </summary>
        public bool isMaximo = true;                                // Maximo valor de intensidad alcanzado
        /// <summary>
        /// <para>Ciclo loop activado.</para>
        /// </summary>
        public bool isLoop = true;                                  // Ciclo loop activado
        #endregion

        #region Variables Privadas
        /// <summary>
        /// <para>Light del gameobject.</para>
        /// </summary>
        private Light lightG;                                       // Light del gameobject
        /// <summary>
        /// <para>Intensidad actual de la luz.</para>
        /// </summary>
        private float actualIntensidad = 0;                         // Intensidad actual de la luz
        /// <summary>
        /// <para>Tiempo interno del reloj.</para>
        /// </summary>
        private float internalTime = 0;                             // Tiempo interno del reloj
        #endregion

        #region Init
        /// <summary>
        /// <para>Metodo init de AutoIntensidadLight.</para>
        /// </summary>
        private void Start()// Metodo init de AutoIntensidadLight
        {
            lightG = this.gameObject.GetComponent(typeof(Light)) as Light;
            actualIntensidad = lightG.intensity;
            internalTime = Time.time + timeDelay;
        }
        #endregion

        #region Actualizadores
        /// <summary>
        /// <para>Actualizador de AutoIntensidadLight.</para>
        /// </summary>
        private void Update()// Actualizador de AutoIntensidadLight
        {
            if (Time.time >= internalTime)
            {
                if (isMaximo == true)
                {
                    if (actualIntensidad < maxIntensidad)
                    {
                        actualIntensidad += vel * Time.deltaTime;
                    }
                    else
                    {
                        actualIntensidad = maxIntensidad;
                        if (isLoop == true)
                        {
                            internalTime = Time.time + timeDelay;
                            isMaximo = false;
                        }
                    }
                }
                else
                    if (actualIntensidad > minIntensidad)
                {
                    actualIntensidad -= vel * Time.deltaTime;
                }
                else
                {
                    actualIntensidad = minIntensidad;
                    if (isLoop == true)
                    {
                        internalTime = Time.time + timeDelay;
                        isMaximo = true;
                    }
                }
                lightG.intensity = actualIntensidad;
            }
        }
        #endregion
    }
}
