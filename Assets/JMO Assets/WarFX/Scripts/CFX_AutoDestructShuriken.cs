using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;
	[SerializeField] private float delay;
	
	void OnEnable()
	{
		StartCoroutine(CheckIfAlive());
	}
	
	IEnumerator CheckIfAlive ()
	{
		while(true)
		{
			yield return new WaitForSeconds(delay);
			if (OnlyDeactivate)
			{
				gameObject.SetActive(false);
			}
			else
			{
				Debug.Log("DestroyParticle");
				Destroy(gameObject);
			}
			break;
		}
	}
}
