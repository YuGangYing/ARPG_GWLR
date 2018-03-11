﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MMO
{
	public class PerformManager : SingleMonoBehaviour<PerformManager>
	{

		public GameObject hitUITextPrefab;
		public List<GameObject> hitPrefabs;

		//TODO 表示する場合が確認する
		public void ShowHitInfo(HitInfo hitInfo,Dictionary<int,GameObject> unitDic){
			ShowHitEffects (hitInfo);
			ShowHitUIInfos (hitInfo,unitDic);
		}

		void ShowHitEffects(HitInfo hitInfo){
			for (int i = 0; i < hitInfo.hitObjectIds.Length; i++) {
				ShowHitEffect (hitInfo.hitObjectIds [i],hitInfo.hitPositions [i]);
			}
		}

		void ShowHitEffect(int objectId,IntVector3 pos){
			GameObject prefab = this.hitPrefabs [objectId];
			GameObject go = Instantiater.Spawn (false, prefab, IntVector3.ToVector3 (pos), Quaternion.identity);
			go.transform.position += new Vector3 (0, 1, 0);//TODO
			Destroy (go, 10);
		}

		void ShowHitUIInfos(HitInfo hitInfo,Dictionary<int,GameObject> unitDic){
			for (int j = 0; j < hitInfo.hitIds.Length; j++) {
				if (unitDic.ContainsKey (hitInfo.hitIds[j])) {
					GameObject go = unitDic [hitInfo.hitIds [j]];
					ShowHitUIInfo (go.GetComponent<MMOUnit> (), hitInfo.damages [j]);
				}
			}
		}

		void ShowHitUIInfo (MMOUnit mmoUnit,int damage)
		{
			GameObject uiGo = Instantiater.Spawn (false, this.hitUITextPrefab, mmoUnit.GetHeadPos () + new Vector3(Random.Range(-0.5f,0.5f),0,Random.Range(-0.5f,0.5f)), Quaternion.identity);
			uiGo.GetComponent<TextMeshPro> ().text = damage.ToString ();
			uiGo.SetActive (true);
		}

		public void ShowDeathEffect(){
			ImageEffectManager.Instance.ShowGray ();
		}

		public void HideDeathEffect(){
			ImageEffectManager.Instance.HideGray ();
		}

	}
}
