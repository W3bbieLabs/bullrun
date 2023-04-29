using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace ambiens.webgltemplate
{
    
    class WebGLBuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder { get { return 0; } }
        static string logoWarning="<color=yellow>Multiplatform WebGL Template</color> I wasn't able to set the logo of the template but no problem. If you want to automatically set the logo, just put a texture as the default icon of the app in Player Settings.";
        public void OnPreprocessBuild(BuildReport report)
        {
            Debug.Log("<color=green>Multiplatform WebGL Template</color> Starting build... If you get some 'Index Out of Bounds' exaptions please open Player Settings -> Resolution and Presentation -> check if the template is selected.");

            try{
                var textures=PlayerSettings.GetIconsForTargetGroup( BuildTargetGroup.Unknown);

                if(textures.Length>0)
                {                   
                    var iconPath=AssetDatabase.GetAssetPath(textures[0]);
                    AssetDatabase.CopyAsset(iconPath, "Assets/WebGLTemplates/Ambiens/Template/logo.png" );
                }   
                else 
                    Debug.Log(logoWarning);

            }
            catch{
                Debug.Log(logoWarning);
            }
        }
    }
}

