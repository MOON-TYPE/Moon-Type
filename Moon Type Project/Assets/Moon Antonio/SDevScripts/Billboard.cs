//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Billboard.cs (02/03/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Rota el objeto para que siempre este mirando a camara		\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio de namespace											\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDevScripts
{
	/// <summary>
	/// <para>Rota el objeto para que siempre este mirando a camara</para>
	/// </summary>
	[AddComponentMenu("MoonAntonio/Dev/Billboard")]
	public class Billboard : MonoBehaviour 
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Objetivo de rotacion.</para>
		/// </summary>
		public Transform objetivo;					// Objetivo de rotacion
		/// <summary>
		/// <para>Lerp de rotacion.</para>
		/// </summary>
		public bool lerpRotacion = false;			// Lerp de rotacion
		/// <summary>
		/// <para>Velocidad de rotacion.</para>
		/// </summary>
		public float velRotacion = 6;				// Velocidad de rotacion
		/// <summary>
		/// <para>Permitir rotacion en X.</para>
		/// </summary>
		public bool rotX = true;					// Permitir rotacion en X
		/// <summary>
		/// <para>Permitir rotacion en Y.</para>
		/// </summary>
		public bool rotY = true;					// Permitir rotacion en Y
		/// <summary>
		/// <para>Permitir rotacion en Z.</para>
		/// </summary>
		public bool rotZ = true;					// Permitir rotacion en Z
		#endregion

		#region Variables Privadas
		/// <summary>
		/// <para>Objeto que se rota.</para>
		/// </summary>
		private Transform obj;                      // Objeto que se rota
		#endregion

		#region Init
		/// <summary>
		/// <para>Init de Billboard.</para>
		/// </summary>
		private void Start()// Init de Billboard
		{
			// Asignacion
			if (objetivo == null)
				objetivo = Camera.main.transform;
			obj = this.transform;
		}
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de Billboard.</para>
		/// </summary>
		private void Update()// Actualizador de Billboard
		{
			if (lerpRotacion)
			{
				Vector3 direccion = objetivo.position - obj.position;

				if (direccion.magnitude < 0.1f) return;

				if (!rotX && !rotY && rotZ) return;

				obj.rotation = Quaternion.Slerp(obj.rotation, Quaternion.LookRotation(direccion), velRotacion * Time.deltaTime);

				if (rotX && rotY && rotZ)
					return;
				else if (rotX && rotY && !rotZ)
					obj.eulerAngles = new Vector3(obj.eulerAngles.x, obj.eulerAngles.y, 0f);
				else if (rotX && !rotY && rotZ)
					obj.eulerAngles = new Vector3(obj.eulerAngles.x, 0f, obj.eulerAngles.z);
				else if (rotX && !rotY && !rotZ)
					obj.eulerAngles = new Vector3(obj.eulerAngles.x, 0f, 0f);
				else if (!rotX && rotY && rotZ)
					obj.eulerAngles = new Vector3(0f, obj.eulerAngles.y, obj.eulerAngles.z);
				else if (!rotX && rotY && !rotZ)
					obj.eulerAngles = new Vector3(0f, obj.eulerAngles.y, 0f);
				else if (!rotX && !rotY && rotZ)
					obj.eulerAngles = new Vector3(0f, 0f, obj.eulerAngles.z);
			}
			else
			{
				obj.LookAt(objetivo.position);
			}	
		}
		#endregion
	}
}
