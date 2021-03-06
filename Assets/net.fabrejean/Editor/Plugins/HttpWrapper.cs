﻿using System;
using UnityEngine;
using System.Collections;

namespace Net.FabreJean.UnityEditor
{
	public class HttpWrapper {


		public WWW GET(string url,Action<WWW> action)
		{
			CancelFlag = false;
			WWW www = new WWW(url);
			EditorCoroutine.start(ProcessRequest(www,action));
			return www;
		}

		bool CancelFlag;
		public void Cancel()
		{
			CancelFlag = true;
		}

		IEnumerator ProcessRequest(WWW www,Action<WWW> action)
		{
		
			while (!www.isDone)
			{
				if (CancelFlag)
				{
					yield break;
				}
				yield return null;
			}
				
			action(www);
		}
	}
}
