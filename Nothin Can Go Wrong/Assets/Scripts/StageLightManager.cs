using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StageLightManager : MonoBehaviour, IStageComponent
{
    public bool LongPressing { get; set; }
    public bool LongRight { get; set; }

    [SerializeField] Light2D light2D;

    private void Awake()
    {
        light2D.color = Color.yellow;
        light2D.intensity = 0;
    }

    void Update()
    {
       
    }

    public IEnumerator LongPress()
    {
        LongPressing = true;
        light2D.color = Color.white;
        light2D.intensity = 9;
        yield return new WaitUntil(() => LongPressing == false);
        light2D.color = Color.yellow;
        light2D.intensity = 0;
        
       
    }

    public IEnumerator Activate(bool right)
    {
        light2D.intensity = 9;
        yield return new WaitForSeconds(0.2f);
        light2D.intensity = 0;
    }

  
}
