(function () {
    
    //UNITY STUFF
    var buildUrl = "Build";
    var loaderUrl = buildUrl + "/{{{ LOADER_FILENAME }}}";
    var config = {
        dataUrl: buildUrl + "/{{{ DATA_FILENAME }}}",
        frameworkUrl: buildUrl + "/{{{ FRAMEWORK_FILENAME }}}",
        codeUrl: buildUrl + "/{{{ CODE_FILENAME }}}",
#if MEMORY_FILENAME
        memoryUrl: buildUrl + "/{{{ MEMORY_FILENAME }}}",
#endif
#if SYMBOLS_FILENAME
        symbolsUrl: buildUrl + "/{{{ SYMBOLS_FILENAME }}}",
#endif
        streamingAssetsUrl: "StreamingAssets",
        companyName: "{{{ COMPANY_NAME }}}",
        productName: "{{{ PRODUCT_NAME }}}",
        productVersion: "{{{ PRODUCT_VERSION }}}",
    };



    function iOS() {
        return [
            'iPad Simulator',
            'iPhone Simulator',
            'iPod Simulator',
            'iPad',
            'iPhone',
            'iPod'
        ].includes(navigator.platform)
        // iPad on iOS 13 detection
        || (navigator.userAgent.includes("Mac") && "ontouchend" in document)
    }
    function isFullscreen(){
        return document.fullscreenElement ||
        document.webkitFullscreenElement ||
        document.mozFullScreenElement ||
        document.msFullscreenElement;
    }
    var main_container = document.querySelector("#main-container");
    var container = document.querySelector("#unity-container");
    var canvas = document.querySelector("#unity-canvas");
    var loader= document.querySelector("#loader");
    var loaderFill= document.querySelector("#fill");
    var toggle_fullscreen=document.querySelector("#toggle_fullscreen");

    function onProgress(progress) {
        loaderFill.style.width = progress * 100 + "%";
    }

    function onComplete(unityInstance) {
        loader.remove();
    }
    var resizeTimeOut;
    function onWindowResize() {
        var width = window.innerWidth
        || document.documentElement.clientWidth
        || document.body.clientWidth;

        var height = window.innerHeight
        || document.documentElement.clientHeight
        || document.body.clientHeight;

        canvas.height=height;
        canvas.width=width;
    }
    function onWindowResizeWithDelay(){
        clearTimeout(resizeTimeOut);
        resizeTimeOut = setTimeout(onWindowResize, 200);
    }


    var script = document.createElement("script");
    script.src = loaderUrl;
    script.onload = () => {
        createUnityInstance(canvas, config, onProgress)
            .then(onComplete)
            .catch((message) => {
                alert(message);
        });
    };
    document.body.appendChild(script);

    window.addEventListener('resize', onWindowResizeWithDelay);
    onWindowResizeWithDelay();


    if(iOS()){
        toggle_fullscreen.style.display="none";
    }
    else{
        toggle_fullscreen.addEventListener('click', function(){

           // if already full screen; exit
            // else go fullscreen
            if ( isFullscreen() ) {
              
                if (document.exitFullscreen) {
                    document.exitFullscreen();
                } 
                else if (document.mozCancelFullScreen) {
                    document.mozCancelFullScreen();
                } 
                else if (document.webkitExitFullscreen) {
                    document.webkitExitFullscreen();
                } 
                else if (document.msExitFullscreen) {
                    document.msExitFullscreen();
                }
                
            } else {
                
                if (main_container.requestFullscreen) {
                    main_container.requestFullscreen();
                } else if (main_container.mozRequestFullScreen) {
                    main_container.mozRequestFullScreen();
                } else if (main_container.webkitRequestFullscreen) {
                    main_container.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
                } else if (container.msRequestFullscreen) {
                    main_container.msRequestFullscreen();
                }
            }
           
        });

        document.onfullscreenchange = function ( event ) {
            if ( isFullscreen() ) {
                if (toggle_fullscreen.classList.contains("fullscreenON")) {
                    toggle_fullscreen.classList.remove("fullscreenON");
                }
                toggle_fullscreen.classList.add("fullscreenOFF");

               
            } else {
                
                if (toggle_fullscreen.classList.contains("fullscreenOFF")) {
                    toggle_fullscreen.classList.remove("fullscreenOFF");
                }
                toggle_fullscreen.classList.add("fullscreenON");

              
            }
            setTimeout(() => {
                canvas.width=1000;
                onWindowResizeWithDelay();
            }, 400);
        };

    }

})();