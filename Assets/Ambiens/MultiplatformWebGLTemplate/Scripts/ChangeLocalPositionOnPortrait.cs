using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  ambiens.webgltemplate
{
    public class ChangeLocalPositionOnPortrait : MonoBehaviour
    {
        
        public Vector3 PortraitPosition;
        public Vector3 LandscapePosition;
        private ScreenOrientation lastOrientation;
        private RectTransform rectT;
        OrientationManager orientMan;
        void Start()
        {
            orientMan=FindObjectOfType<OrientationManager>();
            orientMan.OnScreenSizeChange+=this.OnChangeScreenSize;
            this.lastOrientation=Screen.orientation;
            rectT=this.GetComponent<RectTransform>();
            
            this.OnChangeScreenSize(Screen.width, Screen.height);
        }

        private void OnChangeScreenSize(float w, float h)
        {
            if(Screen.orientation == this.lastOrientation)
            {
                this.transform.localPosition=(w<h)?PortraitPosition:LandscapePosition;
            }
            else{
                this.transform.localPosition=(Screen.orientation== ScreenOrientation.Portrait)?PortraitPosition:LandscapePosition;
            }
            this.lastOrientation=Screen.orientation;

        }
        void OnDestroy()
        {
            if(orientMan!=null)
                orientMan.OnScreenSizeChange-=this.OnChangeScreenSize;
        }
    }
}
