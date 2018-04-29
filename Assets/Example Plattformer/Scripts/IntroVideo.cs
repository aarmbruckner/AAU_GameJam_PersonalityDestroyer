using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{

    public VideoPlayer myVideoPlayer;

    // Use this for initialization
    void Start()
    {
        //check if endpoint is reached 
        myVideoPlayer.loopPointReached += EndReached;
       
    }

    // Update is called once per frame
    void Update()
    {

        
        //if (!myVideoplayer.isPlaying)
        //{
        //}
    }

    private void EndLevel()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        EndLevel();
    }


    //private IEnumerator WaitForSeconds(float seconds) {
    //    yield return new WaitForSeconds(seconds);
    //}


}