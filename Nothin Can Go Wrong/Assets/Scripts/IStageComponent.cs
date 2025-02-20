using System.Collections;
using UnityEngine;

public interface IStageComponent
{
    public bool LongPressing { get; set; }
    public bool LongRight { get; set; }

    public IEnumerator LongPress();
    public IEnumerator Activate(bool right);
}
