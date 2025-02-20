using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StageActorManager : MonoBehaviour, IStageComponent
{
    public bool LongPressing { get; set; }
    public bool LongRight { get; set; }


    [SerializeField] GameObject[] actorsGO;
    List<Actor> actors;


    private void Awake()
    {
        actors = new List<Actor>();
        foreach (GameObject actor in actorsGO) {
            actors.Add(actor.GetComponent<Actor>());
        }
    }

    private void Start()
    {
       
    }

    void Update()
    {

    }

    public IEnumerator LongPress()
    {
        LongPressing = true;
        yield return new WaitUntil(() => LongPressing == false);


    }

    public IEnumerator Activate(bool right)
    {
        foreach (Actor member in actors) {
            member.Scatter(); 
        }


        yield return new WaitForSeconds(0.2f);
    }


}
