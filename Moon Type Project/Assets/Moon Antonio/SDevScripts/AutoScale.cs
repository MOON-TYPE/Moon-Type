//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// AutoScale.cs (02/03/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Auto escalado del objeto									\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio de namespace											\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDevScripts
{
	/// <summary>
	/// <para>Auto escalado del objeto</para>
	/// </summary>
	[AddComponentMenu("MoonAntonio/Dev/AutoScale")]
	public class AutoScale : MonoBehaviour 
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Objetivo de la escala.</para>
		/// </summary>
		public Vector3 objetivo = new Vector3(2.0f, 2.0f, 2.0f);				// Objetivo de la escala
		/// <summary>
		/// <para>Tiempo en escalar el objeto.</para>
		/// </summary>
		public float tiempo = 2;												// Tiempo en escalar el objeto
		/// <summary>
		/// <para>Delay para empezar la escala.</para>
		/// </summary>
		public float delay = 1;													// Delay para empezar la escala
		/// <summary>
		/// <para>Es un loop.</para>
		/// </summary>
		public bool isLoop = true;												// Es un loop
		#endregion

		#region Variables Privadas
		/// <summary>
		/// <para>Sera incrementado.</para>
		/// </summary>
		private bool isIncremento = true;										// Sera incrementado
		/// <summary>
		/// <para>Escala original del objeto.</para>
		/// </summary>
		private Vector3 origScale;												// Escala original del objeto
		/// <summary>
		/// <para>Transform del objeto.</para>
		/// </summary>
		private Transform obj;													// Transform del objeto
		/// <summary>
		/// <para>Tiempo interno del escalado.</para>
		/// </summary>
		private float tiempoInterno = 0;										// Tiempo interno del escalado
		/// <summary>
		/// <para>Ratio de accion.</para>
		/// </summary>
		private float ratio = 0;												// Ratio de accion
		/// <summary>
		/// <para>Temporal para el tiempo.</para>
		/// </summary>
		private float tempI = 0;												// Temporal para el tiempo
		#endregion

		#region Init
		/// <summary>
		/// <para>Init de AutoScale.</para>
		/// </summary>
		private void Start()// Init de AutoScale
		{
			// Asignacion
			obj = transform;
			origScale = obj.localScale;
			ratio = 1.0f / tiempo;
			tiempoInterno = Time.time + delay;
		}
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de AutoScale.</para>
		/// </summary>
		private void Update()// Actualizador de AutoScale
		{
			// Comprobacion de tiempo interno
			if (Time.time >= tiempoInterno)
			{
				LerpScale();
			}			
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Lerp Scale.</para>
		/// </summary>
		private void LerpScale()// Lerp Scale
		{
			// Lerp
			if (tempI < 1.0f)
			{
				tempI += Time.deltaTime * ratio;
				if (isIncremento) obj.localScale = Vector3.Lerp(origScale, objetivo, tempI);
				else obj.localScale = Vector3.Lerp(objetivo, origScale, tempI);
			}
			else if (isLoop)
			{
				isIncremento = !isIncremento;
				tempI = 0;
				tiempoInterno = Time.time + delay;
			}
		}
		#endregion
	}
}
