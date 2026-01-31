using UnityEngine;

namespace ZhengHua
{
    public abstract class StageManager : MonoBehaviour
    {
        public virtual void StageInit()
        {
            Debug.Log("StageInit", this.gameObject);
        }
    }
}