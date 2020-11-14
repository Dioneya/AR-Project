using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recorder
{
    [RequireComponent(typeof(ScreenRecorder))]
    public class RecordManager : MonoBehaviour
    {
        public ScreenRecorder recorder;
        [HideInInspector] public bool isSavedVideo = false; 
		private void Start()
		{
            recorder = GetComponent<ScreenRecorder>();
		}		

        public void StartRecord()
        {
            recorder.PrepareRecorder();
            StartCoroutine(DelayCallRecord());
        }
        private IEnumerator DelayCallRecord()
        {
            yield return new WaitForSeconds(1);
            recorder.StartRecording();
        }


        public void StopRecord()
        {
            recorder.StopRecording();
            StartCoroutine(DelaySaveVideo());
        }
        private IEnumerator DelaySaveVideo()
        {
            yield return new WaitForSeconds(1);
            recorder.SaveVideoToGallery();
            isSavedVideo = true;
        }
    }
}

