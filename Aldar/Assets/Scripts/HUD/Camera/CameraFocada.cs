using UnityEngine;
using System.Collections;

public class CameraFocada : MonoBehaviour
{
	public Transform Foco { get; set; }

	/// <summary>
	/// A camera deverá estar dentro do objto que contiver esse script.
	/// A distância da camera para esse objeto será pre-definida.
	/// </summary>
	void Start ()
	{
		try
		{
			Foco = GameObject.FindGameObjectWithTag(EnumTags.Player.ToString()).transform;
		}
		catch
		{
			this.enabled = false;
			Debug.Log("Camera: Objeto Player não encontrado!");
		}
	}
	
	void Update ()
	{
		gameObject.transform.position += (Foco.position - gameObject.transform.position) * Time.deltaTime * 2;
	}
}
