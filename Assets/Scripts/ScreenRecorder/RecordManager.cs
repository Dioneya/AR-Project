using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
            try
            {
                recorder.PrepareRecorder();
                StartCoroutine(DelayCallRecord());
                //GetComponent<Image>().color = Color.red;
                Debug.Log("Запись началась");
            }
            catch(Exception e)
            {
                Recorder.ScreenRecorder.ShowToast(e.Message);
            }
            
        }
        private IEnumerator DelayCallRecord()
        {
            yield return new WaitForSeconds(1);
            recorder.StartRecording();
        }


        public void StopRecord()
        {
            try
            {
                recorder.StopRecording();
                StartCoroutine(DelaySaveVideo());
                //GetComponent<Image>().color = Color.white;
                Debug.Log("Запись окончилась");
                Recorder.ScreenRecorder.ShowToast("Видео сохранилось в галлерее.");
            }
            catch(Exception e)
            {
                Recorder.ScreenRecorder.ShowToast(e.Message);
            }
            
        }
        private IEnumerator DelaySaveVideo()
        {
            yield return new WaitForSeconds(1);
            recorder.SaveVideoToGallery();
            isSavedVideo = true;
        }
    }
}

