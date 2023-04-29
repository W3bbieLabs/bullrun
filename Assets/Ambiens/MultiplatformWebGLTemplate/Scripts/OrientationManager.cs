using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace  ambiens.webgltemplate
{
    
    public class OrientationManager : MonoBehaviour
    {
        public List<GameObject> ActiveOnlyIfiPortrait;
        public List<GameObject> ActiveOnlyIfLandscape;

        public Action<float,float> OnScreenSizeChange;

        Vector2 lastScreenSize;

        ScreenOrientation lastOrientation;

        void Awake() {
            this.lastScreenSize = new Vector2(Screen.width,Screen.height);
            this.OnScreenSizeChange+=this.ManagePortraitLandScape;
            this.lastOrientation=Screen.orientation;
            this.ManagePortraitLandScape(this.lastScreenSize.x,this.lastScreenSize.y);
        }
    
        void Update() {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height); 

            if (this.lastScreenSize != screenSize || Screen.orientation != this.lastOrientation) {
                this.lastScreenSize = screenSize;
                this.lastOrientation=Screen.orientation;
                OnScreenSizeChange(Screen.width, Screen.height);
            }
        }
        void ManagePortraitLandScape(float w, float h)
        {
            if(Screen.orientation == this.lastOrientation)
            {//Se non sta cambiando l'orientamento
                foreach (var go in ActiveOnlyIfiPortrait) go.SetActive(w<h);
                foreach (var go in ActiveOnlyIfLandscape) go.SetActive(w>h);
            }
            else{
                foreach (var go in ActiveOnlyIfiPortrait) go.SetActive(Screen.orientation == ScreenOrientation.Portrait);
                foreach (var go in ActiveOnlyIfLandscape) go.SetActive(Screen.orientation == ScreenOrientation.LandscapeLeft);
            }
            
        }
    }
}