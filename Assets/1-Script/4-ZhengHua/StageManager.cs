using UnityEngine;

namespace ZhengHua
{
    public abstract class StageManager : MonoBehaviour
    {
        public virtual void StageInit()
        {
            Debug.Log("StageInit", this.gameObject);
        }
        
        [SerializeField] private MaskSelectCanvas _maskSelectCanvas;

        /// <summary>
        /// 當前是否為左綠面具
        /// </summary>
        public bool IsLeftGreenOnCurrent => _maskSelectCanvas.current_L_Image != null && _maskSelectCanvas.current_L_Image.name == "MaskA";
        /// <summary>
        /// 當前是否為右綠面具
        /// </summary>
        public bool IsRightGreenOnCurrent => _maskSelectCanvas.current_R_Image != null && _maskSelectCanvas.current_R_Image.name == "MaskF";
        /// <summary>
        /// 當前是否為左藍面具
        /// </summary>
        public bool IsLeftBlueOnCurrent => _maskSelectCanvas.current_L_Image != null && _maskSelectCanvas.current_L_Image.name == "MaskB";
        /// <summary>
        /// 當前是否為右藍面具
        /// </summary>
        public bool IsRightBlueOnCurrent => _maskSelectCanvas.current_R_Image != null && _maskSelectCanvas.current_R_Image.name == "MaskE";
        /// <summary>
        /// 當前是否為左紅面具
        /// </summary>
        public bool IsLeftRedOnCurrent => _maskSelectCanvas.current_L_Image != null && _maskSelectCanvas.current_L_Image.name == "MaskC";
        /// <summary>
        /// 當前是否為右紅面具
        /// </summary>
        public bool IsRightRedOnCurrent => _maskSelectCanvas.current_R_Image != null && _maskSelectCanvas.current_R_Image.name == "MaskD";
    }
}