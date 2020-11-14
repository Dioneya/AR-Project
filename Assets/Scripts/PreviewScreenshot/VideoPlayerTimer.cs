using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System;

public class VideoPlayerTimer : MonoBehaviour
{

    [SerializeField] private Slider _timerDisplayed;
    [SerializeField] private Slider _timerHidden;
    [SerializeField] private TextMeshProUGUI _currentTime;
    [SerializeField] private PlayPauseButton _playPauseButton;
    // Start is called before the first frame update
    private VideoPlayer _videoPlayer;

    System.TimeSpan VideoUrlLength;
    private string DurationVideo_Minutes;
    private string DurationVideo_Seconds;
    private float durationTime;

    System.TimeSpan CurrentVideoUrlLength;
    private string CurrentMinutes;
    private string CurrentSeconds;
    void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();

        _videoPlayer.Prepare();

        _videoPlayer.prepareCompleted += (val) =>{
            TotalTimeOfVideo(); 
            MaxTimerValues(); 
            _videoPlayer.Play(); 
        };
    }

    public IEnumerator ChangeTimer() {
        yield return new WaitForSeconds(1);
        _currentTime.text = GetCurrentVideoTime();

        _timerDisplayed.value = (float)_videoPlayer.time;

        if (!IsEndedVideo())
        {
            StartCoroutine(ChangeTimer());
        }
        else {
            _playPauseButton.SetPlayButton();
            _videoPlayer.Stop();
        }
    }
    private bool IsEndedVideo() {
        return _timerDisplayed.value == _timerDisplayed.maxValue;
    }
    private void MaxTimerValues() {
        _timerDisplayed.maxValue = (float)VideoUrlLength.TotalSeconds;
        _timerHidden.maxValue = (float)VideoUrlLength.TotalSeconds;
        _timerDisplayed.value = 0;
        _timerHidden.value = 0;
    }
    public void ChangeSlider() {
        _videoPlayer.time = _timerHidden.value;
        _timerDisplayed.value = _timerHidden.value;
        _videoPlayer.Pause();
        StopCoroutine(ChangeTimer());
        _videoPlayer.Play();
        StartCoroutine(ChangeTimer());
    }

    private string GetCurrentVideoTime() {
        double time = Mathf.FloorToInt((float)_videoPlayer.time);
        CurrentVideoUrlLength = System.TimeSpan.FromSeconds(time);
        CurrentMinutes = (CurrentVideoUrlLength.Minutes).ToString("00");
        CurrentSeconds = (CurrentVideoUrlLength.Seconds).ToString("00");

        return CurrentMinutes + ":" + CurrentSeconds + "/" + DurationVideo_Minutes + ":" + DurationVideo_Seconds;
    }

    private void TotalTimeOfVideo()
    {
        durationTime = _videoPlayer.frameCount / _videoPlayer.frameRate;
        VideoUrlLength = System.TimeSpan.FromSeconds(durationTime);
        DurationVideo_Minutes = (VideoUrlLength.Minutes).ToString("00");
        DurationVideo_Seconds = (VideoUrlLength.Seconds).ToString("00");
        _currentTime.text = "00:00"+"/" + DurationVideo_Minutes+":"+ DurationVideo_Seconds;
    }
}
