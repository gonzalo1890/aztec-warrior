using UnityEngine;
using UnityEngine;
using System.Collections;

public enum OffMeshLinkMoveMethod {
	Teleport,
	NormalSpeed,
	Parabola
}

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class OffmeshConfig : MonoBehaviour {

	public float JumpVelocity = 0.5f;
	public float JumpHeight = 0.5f;
	//public bool saltando = false;
	public OffMeshLinkMoveMethod method = OffMeshLinkMoveMethod.Parabola;
	IEnumerator Start () {
		UnityEngine.AI.NavMeshAgent agent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
		agent.autoTraverseOffMeshLink = false;
		while (true) {
			if (agent.isOnOffMeshLink) {
				if (method == OffMeshLinkMoveMethod.NormalSpeed)
					yield return StartCoroutine (NormalSpeed (agent));
				else if (method == OffMeshLinkMoveMethod.Parabola)
					yield return StartCoroutine (Parabola (agent, JumpHeight, JumpVelocity));
				agent.CompleteOffMeshLink ();
			}
			yield return null;
		}
	}
	IEnumerator NormalSpeed (UnityEngine.AI.NavMeshAgent agent) {
		UnityEngine.AI.OffMeshLinkData data = agent.currentOffMeshLinkData;
		Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
		while (agent.transform.position != endPos) {
			agent.transform.position = Vector3.MoveTowards (agent.transform.position, endPos, agent.speed*Time.deltaTime);
			yield return null;
		}
	}
	IEnumerator Parabola (UnityEngine.AI.NavMeshAgent agent, float height, float duration) {
		UnityEngine.AI.OffMeshLinkData data = agent.currentOffMeshLinkData;
		Vector3 startPos = agent.transform.position;
		Vector3 endPos = data.endPos + Vector3.up*agent.baseOffset;
		agent.updateRotation = false;

		//if(!saltando)
		//{
		//	agent.GetComponent<Animator>().SetBool("Jump", true);
		//	saltando = true;
		//}
		float normalizedTime = 0.0f;
		while (normalizedTime < 1.0f) {
			float yOffset = height * 4.0f*(normalizedTime - normalizedTime*normalizedTime);
			agent.transform.position = Vector3.Lerp (startPos, endPos, normalizedTime) + yOffset * Vector3.up;

			Vector3 _direction = (endPos - agent.transform.position).normalized;
			_direction.y = 0;
			float RotationSpeed = 30f;
			Quaternion _lookRotation = Quaternion.LookRotation(_direction);
			agent.transform.rotation = Quaternion.Slerp(agent.transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);

			normalizedTime += Time.deltaTime / duration;
			yield return null;
		}
		agent.updateRotation = true;
	}
}