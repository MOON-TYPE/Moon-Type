//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// AutoMove.cs (21/02/2017)					    								\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Mueve un objeto     										\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio de namespace											\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDevScripts
{
    /// <summary>
    /// <para>Mueve un objeto </para>
    /// </summary>
    [AddComponentMenu("MoonAntonio/Dev/AutoMove")]
    public class AutoMove : MonoBehaviour 
	{
        #region Variables Publicas
        /// <summary>
        /// <para>El objeto de destino donde queremos ir.</para>
        /// </summary>
        public Transform destTransform;                                         //  El objeto de destino donde queremos ir
        /// <summary>
        /// <para>Punto de destino si es necesario (Si no tiene desTransform)</para>
        /// </summary>
        public Vector3 destPoint = Vector3.zero;                                // Punto de destino si es necesario (Si no tiene desTransform)
        /// <summary>
        /// <para>Tiempo que tardara del punto A(Actual) al punto B(Destino).</para>
        /// </summary>
        public float tiempoTranscurrido = 2;                                    // Tiempo que tardara del punto A(Actual) al punto B(Destino)
        /// <summary>
        /// <para>Es un loop.</para>
        /// </summary>
        public bool isLoop = false;                                             // Es un loop
        /// <summary>
        /// <para>Movimiento en local.</para>
        /// </summary>
        public bool isLocal = false;                                            // Movimiento en local
        /// <summary>
        /// <para>Esta activo.</para>
        /// </summary>
        public bool isActive = false;                                           // Esta activo
        #endregion

        #region Variables Privadas
        /// <summary>
        /// <para>Temporal, vamos o regresamos.</para>
        /// </summary>
        private bool tempIncremento = true;                                     // Temporal, vamos o regresamos
        /// <summary>
        /// <para>Posicion original del transform.</para>
        /// </summary>
        private Vector3 posOriginal;                                            // Posicion original del transform
        /// <summary>
        /// <para>Transform actual del objeto.</para>
        /// </summary>
        private Transform actualTransform;                                      // Transform actual del objeto
        /// <summary>
        /// <para>Ratio para la funcion lerp.</para>
        /// </summary>
        private float ratio = 0;                                                // Ratio para la funcion lerp
        /// <summary>
        /// <para>Temporal para la funcion lerp.</para>
        /// </summary>
        private float i = 0;                                                    // Temporal para la funcion lerp
        /// <summary>
        /// <para>Movimiento previo.</para>
        /// </summary>
        private float moveTimePrev = 2;                                         // Movimiento previo
        #endregion

        #region Init
        /// <summary>
        /// <para>Inicializa el sistema.</para>
        /// </summary>
        private void Init()// Inicializa el sistema
        {
            if (isLocal == true)
            {
                posOriginal = actualTransform.localPosition;
            }
            else
            {
                posOriginal = actualTransform.position;
            }

            ratio = 1.0f / tiempoTranscurrido;
            i = 0;
        }

        /// <summary>
        /// <para>Init de AutoMove.</para>
        /// </summary>
        private void Start()// Init de AutoMove
        {
            actualTransform = transform;
            Init();
            moveTimePrev = tiempoTranscurrido;
        }
        #endregion

        #region Actualizadores
        /// <summary>
        /// <para>Actualizador de AutoMove.</para>
        /// </summary>
        private void Update()// Actualizador de AutoMove
        {
            if (!isActive)
                return;

            if (tiempoTranscurrido <= 0)
            {
                tiempoTranscurrido = 0.01f;
            }

            if (moveTimePrev != tiempoTranscurrido)
            {
                ratio = 1.0f / tiempoTranscurrido;
                moveTimePrev = tiempoTranscurrido;
            }

            // Actualizar el movimiento del objeto
            LerpPosicion();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Mueve inmediatamente el objeto a su destino.</para>
        /// </summary>
        public void MoveInmediate()// Mueve inmediatamente el objeto a su destino
        {
            if (i < 1.0f)
                return;
            tempIncremento = true;
            i = 0;
        }

        /// <summary>
        /// <para>Devuelve inmediatamente el objeto a su posicion inicial.</para>
        /// </summary>
        public void ReturnInmediate()// Devuelve inmediatamente el objeto a su posicion inicial
        {
            if (i < 1.0f)
                return;
            tempIncremento = false;
            i = 0;
        }

        /// <summary>
        /// <para>Crea la interpolacion entre posicion actual y destino, mueve.</para>
        /// </summary>
        private void LerpPosicion()// Crea la interpolacion entre posicion actual y destino, mueve
        {
            if (i < 1.0f)
            {
                i += Time.deltaTime * ratio;
                if (tempIncremento == true)
                {
                    if (destTransform == true)
                    {
                        if (isLocal == true)
                        {
                            actualTransform.localPosition = Vector3.Lerp(posOriginal, destTransform.localPosition, i);
                        }
                        else
                        {
                            actualTransform.position = Vector3.Lerp(posOriginal, destTransform.position, i);
                        }
                    }
                    else
                    {
                        if (isLocal == true)
                        {
                            actualTransform.localPosition = Vector3.Lerp(posOriginal, destPoint, i);
                        }
                        else
                        {
                            actualTransform.position = Vector3.Lerp(posOriginal, destPoint, i);
                        }
                    }
                }
                else
                {
                    if (destTransform == true)
                    {
                        if (isLocal == true)
                        {
                            actualTransform.localPosition = Vector3.Lerp(destTransform.localPosition, posOriginal, i);
                        }
                        else
                        {
                            actualTransform.position = Vector3.Lerp(destTransform.position, posOriginal, i);
                        }
                    }
                    else
                    {
                        if (isLocal == true)
                        {
                            actualTransform.localPosition = Vector3.Lerp(destPoint, posOriginal, i);
                        }
                        else
                        {
                            actualTransform.position = Vector3.Lerp(destPoint, posOriginal, i);
                        }
                    }
                }
            }
            else if (isLoop == true)
            {
                tempIncremento = !tempIncremento;
                i = 0;
            }
        }
        #endregion
    }
}
