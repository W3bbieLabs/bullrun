using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  ambiens.webgltemplate
{
        
    public class ChangeRotationOnPortrait : MonoBehaviour
    {
        public Vector3 PortraitRotation;
        public Vector3 LandscapeRotation;
        private ScreenOrientation lastOrientation;
        
        OrientationManager orientMan;
        void Start()
        {
            orientMan=FindObjectOfType<OrientationManager>();
            orientMan.OnScreenSizeChange+=this.OnChangeScreenSize;
            this.lastOrientation=Screen.orientation;
            
            this.OnChangeScreenSize(Screen.width, Screen.height);
        }

        private void OnChangeScreenSize(float w, float h)
        {
            if(Screen.orientation == this.lastOrientation)
            {
                this.transform.localEulerAngles=(w<h)?PortraitRotation:LandscapeRotation;
            }
            else{
                this.transform.localEulerAngles=(Screen.orientation== ScreenOrientation.Portrait)?PortraitRotation:LandscapeRotation;
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