using UnityEngine;

namespace ZhengHua
{
    public abstract class StageManager : MonoBehaviour
    {
        [SerializeField] private MaskManager _maskManager;

        public virtual void StageInit()
        {
            Debug.Log("StageInit", this.gameObject);
        }

        // ---------------- 當前面具判斷 ----------------
        public bool IsLeftGreenOnCurrent => _maskManager.GetEquippedLeft() == MaskType.LeftGreen;
        public bool IsRightGreenOnCurrent => _maskManager.GetEquippedRight() == MaskType.RightGreen;
        public bool IsLeftBlueOnCurrent => _maskManager.GetEquippedLeft() == MaskType.LeftBlue;
        public bool IsRightBlueOnCurrent => _maskManager.GetEquippedRight() == MaskType.RightBlue;
        public bool IsLeftRedOnCurrent => _maskManager.GetEquippedLeft() == MaskType.LeftRed;
        public bool IsRightRedOnCurrent => _maskManager.GetEquippedRight() == MaskType.RightRed;
    }
}
