//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// AutoRotate.cs (02/03/2017)													\\
// Autor: Antonio Mateo (Moon Pincho) 									        \\
// Descripcion:		Rota el objeto en la direccion dada							\\
// Fecha Mod:		21/03/2017													\\
// Ultima Mod:		Cambio de namespace											\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio.MDevScripts
{
	/// <summary>
	/// <para>Rota el objeto en la direccion dada</para>
	/// </summary>
	[AddComponentMenu("MoonAntonio/Dev/AutoRotate")]
	public class AutoRotate : MonoBehaviour 
	{
		#region Variables Publicas
		/// <summary>
		/// <para>Rotacion en cada eje.</para>
		/// </summary>
		public Vector3 rotacion = new Vector3(0, 0, 0);					// Rotacion en cada ej
		/// <summary>
		/// <para>Velocidad de rotacion en cada eje.</para>
		/// </summary>
		public Vector3 velocidad = new Vector3(0, 0, 0);				// Velocidad de rotacion en cada eje
		/// <summary>
		/// <para>Rotacion en local.</para>
		/// </summary>
		public bool isLocal = false;									// Rotacion en local
		/// <summary>
		/// <para>Velocidad aleatoria.</para>
		/// </summary>
		public bool isVelRandom = false;								// Velocidad aleatoria
		/// <summary>
		/// <para>Ciclo de rotacion.</para>
		/// </summary>
		public bool isLoop = false;										// Ciclo de rotacion
		#endregion

		#region Variables Privadas
		/// <summary>
		/// <para>Temporal del angulo global.</para>
		/// </summary>
		private Vector3 tempAngulo = Vector3.zero;						// Temporal del angulo global
		/// <summary>
		/// <para>Temporal del angulo local.</para>
		/// </summary>
		private Vector3 tempAnguloLocal = Vector3.zero;					// Temporal del angulo local
		/// <summary>
		/// <para>Se ha completado la rotacion.</para>
		/// </summary>
		private bool isCompletado = false;								// Se ha completado la rotacion
		/// <summary>
		/// <para>Esta subiendo la rotacion.</para>
		/// </summary>
		private bool isSubiendo = true;									// Esta subiendo la rotacion
		/// <summary>
		/// <para>Transform del objeto.</para>
		/// </summary>
		private Transform obj;											// Transform del objeto
		/// <summary>
		/// <para>Original Euler Angles en global.</para>
		/// </summary>
		private Vector3 OrigEulerAngles = Vector3.zero;					// Original Euler Angles en global
		/// <summary>
		/// <para>Original Euler Angles en local.</para>
		/// </summary>
		private Vector3 OrigLocalEulerAngles = Vector3.zero;            // Original Euler Angles en local
		#endregion

		#region Init
		/// <summary>
		/// <para>Init de AutoRotate.</para>
		/// </summary>
		private void Start()// Init de AutoRotate
		{
			// Asignaciones
			obj = this.transform;
			OrigEulerAngles = obj.eulerAngles;
			OrigLocalEulerAngles = obj.localEulerAngles;

			// Resetear Angulos
			ResetAngulos();

			// Si la velocidad es aleatoria, fijarla.
			if (isVelRandom) velocidad = new Vector3(Random.Range(0, velocidad.x), Random.Range(0, velocidad.y), Random.Range(0, velocidad.z));
		}
		#endregion

		#region Actualizadores
		/// <summary>
		/// <para>Actualizador de AutoRotate.</para>
		/// </summary>
		private void Update()// Actualizador de AutoRotate
		{
			// Si se ha completado la rotacion y esta en loop
			if (isCompletado && isLoop)
			{
				isSubiendo = !isSubiendo;
				isCompletado = false;
			}

			// Asignar rotacion
			if (isSubiendo)
			{
				RotacionUp();
			}
			else
			{
				RotacionDown();
			}	
		}
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Resetea los angulos del objeto.</para>
		/// </summary>
		private void ResetAngulos()// Resetea los angulos del objeto
		{
			// Rotacion local
			if (isLocal)
			{
				if (velocidad.x == 0)
					rotacion.x = OrigLocalEulerAngles.x;
				if (velocidad.y == 0)
					rotacion.y = OrigLocalEulerAngles.y;
				if (velocidad.z == 0)
					rotacion.z = OrigLocalEulerAngles.z;
			}
			else // Rotacion Global
			{
				if (velocidad.x == 0)
					rotacion.x = OrigEulerAngles.x;
				if (velocidad.y == 0)
					rotacion.y = OrigEulerAngles.y;
				if (velocidad.z == 0)
					rotacion.z = OrigEulerAngles.z;
			}
		}

		/// <summary>
		/// <para>Rotacion inmediata del objeto.</para>
		/// </summary>
		public void RotateInmediate()// Rotacion inmediata del objeto
		{
			isSubiendo = true;
			isCompletado = false;
			ResetAngulos();
			RotacionUp();
		}

		/// <summary>
		/// <para>Rotacion Up del objeto.</para>
		/// </summary>
		private void RotacionUp()// Rotacion Up del objeto
		{
			if (velocidad.x > 0)
			{
				if (isLocal)
					tempAnguloLocal.x = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, rotacion.x, velocidad.x * Time.deltaTime);
				else
					tempAngulo.x = Mathf.MoveTowardsAngle(transform.eulerAngles.x, rotacion.x, velocidad.x * Time.deltaTime);
			}
			if (velocidad.y > 0)
			{
				if (isLocal)
					tempAnguloLocal.y = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, rotacion.y, velocidad.y * Time.deltaTime);
				else
					tempAngulo.y = Mathf.MoveTowardsAngle(transform.eulerAngles.y, rotacion.y, velocidad.y * Time.deltaTime);
			}
			if (velocidad.z > 0)
			{
				if (isLocal)
					tempAnguloLocal.z = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, rotacion.z, velocidad.z * Time.deltaTime);
				else
					tempAngulo.z = Mathf.MoveTowardsAngle(transform.eulerAngles.z, rotacion.z, velocidad.z * Time.deltaTime);
			}

			if (isLocal)
				transform.localEulerAngles = tempAnguloLocal;
			else
				transform.eulerAngles = tempAngulo;


			if (isLocal && Vector3.Distance(rotacion, transform.localEulerAngles) < 0.1f)
				isCompletado = true;
			else if (!isLocal && Vector3.Distance(rotacion, transform.eulerAngles) < 0.1f)
				isCompletado = true;
		}

		/// <summary>
		/// <para>Rotacion Down del objeto.</para>
		/// </summary>
		private void RotacionDown()// Rotacion Down del objeto
		{
			if (velocidad.x > 0)
			{
				if (isLocal)
					tempAnguloLocal.x = Mathf.MoveTowardsAngle(transform.localEulerAngles.x, OrigLocalEulerAngles.x, velocidad.x * Time.deltaTime);
				else
					tempAngulo.x = Mathf.MoveTowardsAngle(transform.eulerAngles.x, OrigEulerAngles.x, velocidad.x * Time.deltaTime);
			}
			if (velocidad.y > 0)
			{
				if (isLocal)
					tempAnguloLocal.y = Mathf.MoveTowardsAngle(transform.localEulerAngles.y, OrigLocalEulerAngles.y, velocidad.y * Time.deltaTime);
				else
					tempAngulo.y = Mathf.MoveTowardsAngle(transform.eulerAngles.y, OrigEulerAngles.y, velocidad.y * Time.deltaTime);
			}
			if (velocidad.z > 0)
			{
				if (isLocal)
					tempAnguloLocal.z = Mathf.MoveTowardsAngle(transform.localEulerAngles.z, OrigLocalEulerAngles.z, velocidad.z * Time.deltaTime);
				else
					tempAngulo.z = Mathf.MoveTowardsAngle(transform.eulerAngles.z, OrigEulerAngles.z, velocidad.z * Time.deltaTime);
			}

			if (isLocal)
				transform.localEulerAngles = tempAnguloLocal;
			else
				transform.eulerAngles = tempAngulo;


			if (isLocal && Vector3.Distance(OrigLocalEulerAngles, transform.localEulerAngles) < 0.1f)
				isCompletado = true;
			else if (!isLocal && Vector3.Distance(OrigEulerAngles, transform.eulerAngles) < 0.1f)
				isCompletado = true;
		}
		#endregion
	}
}
