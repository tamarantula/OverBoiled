using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject queueListParent;
    public GameObject queueItemPrefab;

    public GameObject[] queueObjects;

    public void UpdateUI(List<QueueItem> qil)
    {
        foreach(Transform ch in queueListParent.transform){
            Destroy(ch.gameObject);
        }
        int qid=0;
        foreach(QueueItem qi in qil){
            var newQIL = Instantiate(queueItemPrefab,queueListParent.transform);
            newQIL.GetComponent<QueueItemPopulator>().populate(qid,qi);
            qid+=1;

            if(qid>10){ break; }
        }
    }
}
