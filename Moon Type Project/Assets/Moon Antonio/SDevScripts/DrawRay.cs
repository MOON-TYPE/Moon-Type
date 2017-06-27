//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DrawRay.cs (02/03/2017)														\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Dibuja un ray hasta el objetivo dado						\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio de namespace											\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDevScripts
{
	/// <summary>
	/// <para>Dibuja un ray hasta el objetivo dado</para>
	/// </summary>
	[ExecuteInEditMode]
	[AddComponentMenu("MoonAntonio/Dev/DrawRay")]
	public class DrawRay : MonoBehaviour 
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Color del rayo.</para>
		/// </summary>
		public Color rayColor = Color.green;			// Color del rayo
		/// <summary>
		/// <para>Objetivo del rayo.</para>
		/// </summary>
		public Transform obj;							// Objetivo del rayo
		/// <summary>
		/// <para>LookAt del objeto al objetivo.</para>
		/// </summary>
		public bool isLookAt = false;					// LookAt del objeto al objetivo
		/// <summary>
		/// <para>Longitud del ray si no tiene objetivo.</para>
		/// </summary>
		public int longitud = 10;						// Longitud del ray si no tiene objetivo
		#endregion

		#region Variables Privadas
		/// <summary>
		/// <para>Vector utilizado para configurar la direccion</para>
		/// </summary>
		private Vector3 forward = Vector3.zero;         // Vector utilizado para configurar la direccion
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de DrawRay.</para>
		/// </summary>
		private void Update()// Actualizador de DrawRay
		{
			// Si hay un objetivo
			if (obj)
			{
				// Si hay lookat 
				if (isLookAt) transform.LookAt(obj);

				forward = obj.position - transform.position;
			}
			else
			{
				forward = transform.TransformDirection(Vector3.forward) * longitud;
			}
				
			// Dibujar ray
			Debug.DrawRay(transform.position, forward, rayColor);
		}
		#endregion
	}
}
