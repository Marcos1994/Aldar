using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour
{
	public Transform Foco { get; set; }

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
