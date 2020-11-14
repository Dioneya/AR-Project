using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayPauseButton : MonoBehaviour
{
    [SerializeField] private Sprite _playIcon;
    [SerializeField] private Sprite _pauseIcon;
    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private Image _ImagePlayPauseButton;

    private VideoPlayerTimer _videoPlayerTimer;
    bool is_Play = false;
    // Start is called before the first frame update
    void Start()
    {
        _videoPlayerTimer = _videoPlayer.GetComponent<VideoPlayerTimer>();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            PausePlay();
        });
    }

    public void PausePlay() {
        if (_videoPlayer.isPlaying == true)
        {
            SetPlayButton();
            _videoPlayer.Pause();
            StopCoroutine(_videoPlayerTimer.ChangeTimer());
        }
        else {
            SetPauseButton();
            _videoPlayer.Play();
            StartCoroutine(_videoPlayerTimer.ChangeTimer());
        }
    }
    public void SetPlayButton() {
        _ImagePlayPauseButton.sprite = _playIcon;
    }
    public void SetPauseButton() {
        _ImagePlayPauseButton.sprite = _pauseIcon;
    }
}
