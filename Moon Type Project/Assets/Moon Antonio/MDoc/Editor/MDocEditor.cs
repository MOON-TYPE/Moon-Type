//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// MDocEditor.cs (20/03/2017)													\\
// Autor: Antonio Mateo (Moon Antonio) 									        \\
// Descripcion:		Herramienta para crear una documentacion simple.			\\
// Fecha Mod:		20/03/2017													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
#endregion

namespace MoonAntonio.MDoc
{
	/// <summary>
	/// <para>Herramienta para crear una documentacion simple</para>
	/// </summary>
	public class MDocEditor : EditorWindow
	{
		#region Ajustes
		public string nombreDoc = "MDoc";
		public string versionDoc = "1.0.0";
		#endregion

		#region Variables Privadas
		private GUISkin estilo;
		private GUIStyle wordWrapped;
		private GUIStyle headerText;
		private GUIStyle seccionHeader;
		private GUIStyle tituloNegrita;
		private GUIStyle tituloWindow;
		private GUIStyle btnBack;
		private int smallSpace = 8;
		private int mediumSpace = 12;
		private int largeSpace = 20;
		GUILayoutOption[] btnSize = new GUILayoutOption[] { GUILayout.Width(200), GUILayout.Height(35) };
		GUILayoutOption[] docSize = new GUILayoutOption[] { GUILayout.Width(300), GUILayout.Height(330) };
		private static MenuActual menuActual;
		private static string tituloMenu = "Menu Principal";
		private Vector2 scrollDocs = Vector2.zero;
		#endregion

		#region Menu
		/// <summary>
		/// <para>Inicia MDoc.</para>
		/// </summary>
		[MenuItem("Moon Antonio/MDoc", false, 0)]
		public static void InitMDoc()// Inicia MDoc
		{
			Texture icono = AssetDatabase.LoadAssetAtPath<Texture>("Assets/Moon Antonio/MDoc/Icon/mdoc.icon.png");
			GUIContent tituloContenido = new GUIContent(" MDoc", icono);

			EditorWindow window = GetWindow<MDocEditor>(true,tituloContenido.text,true);
			window.titleContent = tituloContenido;
			window.maxSize = new Vector2(500, 500);
			window.minSize = new Vector2(500, 500);
			window.Show();
		}
		#endregion

		#region Iniciadores
		/// <summary>
		/// <para>Cuando esta activo.</para>
		/// </summary>
		private void OnEnable()// Cuando esta activo
		{
			// Asignamos el GUISkin
			estilo = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/Moon Antonio/MDoc/Utils/MDocSkin.guiskin");

			// Asignamos el estilo del GUISkin
			if (estilo != null)
			{
				wordWrapped = estilo.GetStyle("NormalWordWrapped");
				headerText = estilo.GetStyle("HeaderText");
				seccionHeader = estilo.GetStyle("SeccionHeader");
				tituloNegrita = estilo.GetStyle("TituloNegrita");
				tituloWindow = estilo.GetStyle("TituloWindow");
				btnBack = estilo.GetStyle("BtnBack");
			}
		}
		#endregion

		#region GUI
		/// <summary>
		/// <para>Interfaz de MDoc.</para>
		/// </summary>
		private void OnGUI()// Interfaz de MDoc
		{
			GUI.skin = estilo;

			EditorGUILayout.Space();

			GUILayout.BeginVertical("Box");

			EditorGUILayout.LabelField(nombreDoc, tituloWindow);

			GUILayout.Space(3);

			EditorGUILayout.LabelField(" Version " + versionDoc);

			GUILayout.Space(12);

			EditorGUILayout.BeginHorizontal();
			GUILayout.Space(smallSpace);

			if (menuActual != MenuActual.MenuPrincipal)
			{
				EditorGUILayout.BeginVertical();
				GUILayout.Space(smallSpace);
				if (GUILayout.Button("", btnBack, GUILayout.Width(80), GUILayout.Height(40)))
					BackMenuPrincipal();
				EditorGUILayout.EndVertical();
			}
			else
			{
				GUILayout.Space(80);
			}
			GUILayout.Space(15);
			EditorGUILayout.BeginVertical();
			GUILayout.Space(14);
			EditorGUILayout.LabelField(tituloMenu, headerText);
			EditorGUILayout.EndVertical();
			GUILayout.FlexibleSpace();
			GUILayout.Space(80);
			EditorGUILayout.EndHorizontal();

			GUILayout.Space(10);

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			ActualizarMenu();
			GUILayout.FlexibleSpace();

			EditorGUILayout.EndHorizontal();

			GUILayout.FlexibleSpace();

			GUILayout.Space(largeSpace);
			EditorGUILayout.LabelField("Moon Antonio");
			EditorGUILayout.EndVertical();
		}
		#endregion

		#region API
		/// <summary>
		/// <para>Inicia un menu.</para>
		/// </summary>
		public void InicioMenu()// Inicia un menu
		{
			EditorGUILayout.BeginVertical();
			GUILayout.Space(25);
		}

		/// <summary>
		/// <para>Fin de un menu.</para>
		/// </summary>
		public void FinMenu()// Fin de un menu
		{
			EditorGUILayout.EndVertical();
		}

		/// <summary>
		/// <para>Crea un boton.</para>
		/// </summary>
		/// <param name="Texto">Texto del boton.</param>
		/// <param name="menu">Menu asignado del boton.</param>
		public void Boton(string texto, MenuActual menu)// Crea un boton
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(texto, btnSize))
			{
				menuActual = menu;
				tituloMenu = texto;
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}

		/// <summary>
		/// <para>Crea un boton.</para>
		/// </summary>
		/// <param name="Texto">Texto del boton.</param>
		/// <param name="menu">Menu asignado del boton.</param>
		/// <param name="espacio">Tiene espacio hasta el siguiente boton o va seguido.</param>
		public void Boton(string texto, MenuActual menu,bool espacio)// Crea un boton
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(texto, btnSize))
			{
				menuActual = menu;
				tituloMenu = texto;
			}
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			if(espacio) GUILayout.FlexibleSpace();
		}

		/// <summary>
		/// <para>Crea un boton para ir a una url.</para>
		/// </summary>
		/// <param name="texto">Texto del boton.</param>
		/// <param name="url">Url completa.</param>
		public void Boton(string texto, string url)// Crea un boton para ir a una url
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			if (GUILayout.Button(texto, btnSize))
				Application.OpenURL(url);
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}

		/// <summary>
		/// <para>Crea un titulo</para>
		/// </summary>
		/// <param name="texto">Texto del titulo.</param>
		public void Titulo(string texto)// Crea un titulo
		{
			EditorGUILayout.LabelField(texto, seccionHeader);
		}

		/// <summary>
		/// <para>Crea un titulo</para>
		/// </summary>
		/// <param name="texto">Texto del titulo.</param>
		/// <param name="negrita">En negrita o normal.</param>
		public void Titulo(string texto,bool negrita)// Crea un titulo
		{
			if (negrita)
			{
				EditorGUILayout.LabelField(texto, tituloNegrita);
			}
			else
			{
				EditorGUILayout.LabelField(texto, seccionHeader);
			}
			
		}

		/// <summary>
		/// <para>Crea un texto.</para>
		/// </summary>
		/// <param name="texto">El texto, salto de linea \n.</param>
		public void Texto(string texto)// Crea un texto
		{
			EditorGUILayout.LabelField(texto, wordWrapped);
		}

		/// <summary>
		/// <para>Separador vertical.</para>
		/// </summary>
		public void Separador()// Separador
		{
			GUILayout.Space(3);
			EditorGUILayout.LabelField("<------------------------------------>", tituloNegrita);
		}

		/// <summary>
		/// <para>Crea un espacio con un valor dado.</para>
		/// </summary>
		/// <param name="value">Valor del espacio.</param>
		public void Espacio(float value)// Crea un espacio con un valor dado
		{
			GUILayout.Space(value);
		}

		/// <summary>
		/// <para>Crea un espacio de un tipo fijado.</para>
		/// </summary>
		/// <param name="tipo">Tipo de espacio.[Min]8 |[Med]12 |[Max]20 .</param>
		public void Espacio(TipoEspacio tipo)// Crea un espacio de un tipo fijado
		{
			switch (tipo)
			{
				case TipoEspacio.Min:
					GUILayout.Space(smallSpace);
					break;

				case TipoEspacio.Med:
					GUILayout.Space(mediumSpace);
					break;

				case TipoEspacio.Max:
					GUILayout.Space(largeSpace);
					break;

				default:
					GUILayout.Space(5f);
					break;
			}
		}

		/// <summary>
		/// <para>Crea una imagen.</para>
		/// </summary>
		/// <param name="texture">Textura.</param>
		/// <param name="width">Anchura de la imagen.</param>
		/// <param name="height">Altura de la imagen.</param>
		public void Imagen(Texture2D texture,float width,float height)// Crea una imagen
		{
			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Space(15);
			GUILayout.Label(texture, GUILayout.Width(width), GUILayout.Height(height));
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();
		}

		/// <summary>
		/// <para>Inicia un scroll.</para>
		/// </summary>
		public void InicioScroll()// Inicia un scroll
		{
			scrollDocs = EditorGUILayout.BeginScrollView(scrollDocs, false, false, docSize);
		}

		/// <summary>
		/// <para>Finaliza un scroll.</para>
		/// </summary>
		public void FinScroll()// Finaliza un scroll
		{
			EditorGUILayout.EndScrollView();
		}
		#endregion

		#region Funcional
		/// <summary>
		/// <para>Actualiza los menus.</para>
		/// </summary>
		private void ActualizarMenu()// Actualiza los menus
		{
			switch (menuActual)
			{
				case MenuActual.MenuPrincipal:
					MenuPrincipal();
					break;
				case MenuActual.Descripcion:
					MenuDescripcion();
					break;

				case MenuActual.API:
					MenuAPI();
					break;

				case MenuActual.Changelog:
					MenuChangeLog();
					break;

				default:
					MenuPrincipal();
					break;
			}
		}
		#endregion

		#region Secciones Documentacion
		#region MenuPrincipal
		/// <summary>
		/// <para>Menu principal de la documentacion.</para>
		/// </summary>
		private void MenuPrincipal()// Menu principal de la documentacion
		{
			InicioMenu();

			Boton("Descripcion", MenuActual.Descripcion);
			Boton("API", MenuActual.API,true);
			Boton("ChangeLog", MenuActual.Changelog);
			Boton("GitHub", "https://github.com/MOON-TYPE/MDoc");

			FinMenu();
		}
		#endregion

		#region Descripcion
		/// <summary>
		/// <para>Menu descriptivo de MDoc.</para>
		/// </summary>
		private void MenuDescripcion()// Menu descriptivo de MDoc
		{
			InicioMenu();

			InicioScroll();

			Titulo("Descripcion");
			Separador();
			Texto("MDoc es una herramienta para crear desde script una rapida y sencilla documentacion en poco tiempo.");

			FinScroll();

			FinMenu();
		}
		#endregion

		#region API
		/// <summary>
		/// <para>Menu de la API de MDoc.</para>
		/// </summary>
		private void MenuAPI()// Menu de la API de MDoc
		{
			InicioMenu();

			InicioScroll();

			Titulo("void InicioMenu()", true);
			Texto("Inicio de un menu");

			Espacio(TipoEspacio.Min);

			Titulo("void FinMenu()", true);
			Texto("Fin de un menu");

			Espacio(TipoEspacio.Min);

			Titulo("void Boton(string nomBtn, MenuActual menu)", true);
			Texto("Crear un boton en la documentacion");

			Espacio(TipoEspacio.Min);

			Titulo("void Boton(string nomBtn,MenuActual menu,bool espacio)", true);
			Texto("Crear un boton en la documentacion");

			Espacio(TipoEspacio.Min);

			Titulo("void Boton(string texto, string url)", true);
			Texto("Crear un boton en la documentacion");

			Espacio(TipoEspacio.Min);

			Titulo("void Titulo(string texto)", true);
			Texto("Crear un titulo en la documentacion");

			Espacio(TipoEspacio.Min);

			Titulo("void Titulo(string texto,bool negrita)", true);
			Texto("Crear un titulo en la documentacion");

			Espacio(TipoEspacio.Min);

			Titulo("void Texto(string texto)", true);
			Texto("Crear un texto");

			Espacio(TipoEspacio.Min);

			Titulo("void Separador()", true);
			Texto("Crea un separador");

			Espacio(TipoEspacio.Min);

			Titulo("void Espacio(TipoEspacio tipo)", true);
			Texto("Crea un espacio de un tipo fijado");

			Espacio(TipoEspacio.Min);

			Titulo("void Imagen(Texture2D texture,float width,float height)", true);
			Texto("Crea una imagen");

			Espacio(TipoEspacio.Min);

			Titulo("void InicioScroll()", true);
			Texto("Inicia un scroll");

			Espacio(TipoEspacio.Min);

			Titulo("FinScroll()", true);
			Texto("Finaliza un scroll");

			FinScroll();

			FinMenu();
		}
		#endregion

		#region ChangeLog
		/// <summary>
		/// <para>Menu de los cambios realizados.</para>
		/// </summary>
		private void MenuChangeLog()// Menu de los cambios realizados
		{
			InicioMenu();

			Titulo("1.0.0");
			Texto("Version Inicial.");

			FinMenu();
		}
		#endregion
		#endregion

		#region Metodos
		/// <summary>
		/// <para>Volver al menu principal.</para>
		/// </summary>
		private void BackMenuPrincipal()// Volver al menu principal
		{
			menuActual = MenuActual.MenuPrincipal;
			tituloMenu = "Menu Principal";
		}
		#endregion
	}

	/// <summary>
	/// <para>Metodo provisional para ver los menus.</para>
	/// </summary>
	public enum MenuActual
	{
		MenuPrincipal,
		Descripcion,
		API,
		Changelog
	}

	/// <summary>
	/// <para>Tipo de espacio.</para>
	/// </summary>
	public enum TipoEspacio
	{
		Min,
		Med,
		Max
	}
}
