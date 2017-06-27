//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// AutoDestroy.cs (21/02/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Autodestruye un objeto										\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio de namespace											\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDevScripts
{
    /// <summary>
    /// <para>Autodestruye un objeto</para>
    /// </summary>
    [AddComponentMenu("MoonAntonio/Dev/AutoDestroy")]
    public class AutoDestroy : MonoBehaviour 
	{
        #region Variables Publicas
        /// <summary>
        /// <para>Tiempo hasta destruir.</para>
        /// </summary>
        public float timeDestroy = 0.5f;                                                    // Tiempo hasta destruir
        #endregion

        #region Init
        /// <summary>
        /// <para>Metodo init de AutoDestroy.</para>
        /// </summary>
        private void Start()// Metodo init de AutoDestroy
        {
            // Solo destruir el objeto si el tiempo es mayor que cero.
            if (timeDestroy > 0)
                Destroy(this.gameObject, timeDestroy);
        }
        #endregion

        #region Metodos
        /// <summary>
        /// <para>Fija el tiempo para destruir el objeto.</para>
        /// </summary>
        /// <param name="tiempo">Tiempo para destruir el objeto.</param>
        public void SetTimeToDestroy(float tiempo)// Fija el tiempo para destruir el objeto
        {
            // Solo destruir el objeto si el tiempo es mayor que cero.
            if (tiempo > 0)
            {
                timeDestroy = tiempo;
                Destroy(this.gameObject, tiempo);
            }
        }

        /// <summary>
        /// <para>Destruye inmediatamente este objeto.</para>
        /// </summary>
        public void DestroyInmediate()// Destruye inmediatamente este objeto
        {
            Destroy(this.gameObject);
        }
        #endregion
    }
}
