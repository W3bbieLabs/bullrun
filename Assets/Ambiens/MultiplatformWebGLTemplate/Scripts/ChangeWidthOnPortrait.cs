using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  ambiens.webgltemplate
{
    
    public class ChangeWidthOnPortrait : MonoBehaviour
    {
        public Vector2 PortraitSizeDelta;
        public Vector2 LandscapeSizeDelta;
        private ScreenOrientation lastOrientation;
        private RectTransform rectT;
        void Start()
        {
            FindObjectOfType<OrientationManager>().OnScreenSizeChange+=this.OnChangeScreenSize;
            this.lastOrientation=Screen.orientation;
            rectT=this.GetComponent<RectTransform>();
            
            this.OnChangeScreenSize(Screen.width, Screen.height);
        }

        private void OnChangeScreenSize(float w, float h)
        {
            if(Screen.orientation == this.lastOrientation)
            {
                if(w<h){
                    rectT.sizeDelta=PortraitSizeDelta;
                }
                else{
                    rectT.sizeDelta=LandscapeSizeDelta;
                }
            }
            else{
                rectT.sizeDelta=(Screen.orientation== ScreenOrientation.Portrait)?PortraitSizeDelta:LandscapeSizeDelta;
            }
        }
    }
}