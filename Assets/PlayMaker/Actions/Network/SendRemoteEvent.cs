// (c) Copyright HutongGames, LLC 2010-2012. All rights reserved.

#if !(UNITY_FLASH || UNITY_NACL || UNITY_METRO || UNITY_WP8 || UNITY_WIIU || UNITY_PSM || UNITY_WEBGL)

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Network)]
	[Tooltip("Send an Fsm Event on a remote machine. Uses Unity RPC functions.")]
	public class SendRemoteEvent : ComponentAction<NetworkView>
	{
		[RequiredField]
		[CheckForComponent(typeof(NetworkView))]
		[Tooltip("The game object that sends the event.")]
		public FsmOwnerDefault gameObject;

		[RequiredField]
		[Tooltip("The event you want to send.")]
		public FsmEvent remoteEvent;
		
		[Tooltip("Optional string data. Use 'Get Event Info' action to retrieve it.")]
		public FsmString stringData;

		[Tooltip("Option for who will receive an RPC.")]
		public RPCMode mode;
		
		public override void Reset()
		{
			gameObject = null;
			remoteEvent = null;
			mode = RPCMode.All;
			stringData = null;
			mode = RPCMode.All;
		}

		public override void OnEnter()
		{
			DoRemoteEvent();
			
			Finish();
		}

		void DoRemoteEvent()
		{
			var go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (!UpdateCache(go))
			{
				return;
			}
	
			if (!stringData.IsNone && stringData.Value != "")
			{
				networkView.RPC("SendRemoteFsmEventWithData", mode, remoteEvent.Name,stringData.Value);
			}
			else
			{
				networkView.RPC("SendRemoteFsmEvent", mode, remoteEvent.Name);
			}		
		}
	}
}

#endif
