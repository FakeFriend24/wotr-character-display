using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CharacterDisplayWotR
{
    public class ChangeSideUIBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
    {
        RectTransform.Edge currentEdge = RectTransform.Edge.Right;
        RectTransform rt;


        public void OnPointerEnter(PointerEventData eventData)
        {
#if DEBUG
            Log.Write("Start Repositioning");
#endif
            if (currentEdge == RectTransform.Edge.Left)
                currentEdge = RectTransform.Edge.Right;
            else
                currentEdge = RectTransform.Edge.Left;
            UpdatePos();
#if DEBUG
            Log.Write("Finished Repositioning");
#endif
        }

        public void OnPointerExit(PointerEventData eventData)
        {
         //   throw new NotImplementedException();
        }

        private void UpdatePos()
        {
            if(!rt)
                rt = GetComponent<RectTransform>();
            rt.SetInsetAndSizeFromParentEdge(currentEdge, Math.Min(Math.Abs(rt.offsetMax.x),Math.Abs( rt.offsetMin.x)), rt.rect.width);
        }
    }
}
