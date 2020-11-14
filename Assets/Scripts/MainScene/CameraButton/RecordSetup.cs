using Recorder;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class RecordSetup : MonoBehaviour
{
    [SerializeField] private Sprite _Pause;
    [SerializeField] private Sprite _Record;
    [SerializeField] private Image _CameraButtonImage;
    [SerializeField] private Recorder.RecordManager _RecordManager;

    [SerializeField] private VideoPlayer _videoPlayer;
    [SerializeField] private PlayPauseButton _playPauseButton;
    private RawImage _rawImage;
    private bool _isRecordIcon = true;
    [HideInInspector] public bool isRecording = false;

    private void Start()
    {
        _rawImage = _videoPlayer.GetComponent<RawImage>();
    }
    private void ChangeToPauseIcon() {
        _CameraButtonImage.sprite = _Pause;
    }

    private void ChangeToRecordIcon()
    {
        _CameraButtonImage.sprite = _Record;
    }

    private void ToggleIcon() {
        if (_isRecordIcon)
            ChangeToPauseIcon();
        else
            ChangeToRecordIcon();
        _isRecordIcon = !_isRecordIcon;
    }

    public void ToggleRecord() {
        if (isRecording && _CameraButtonImage.sprite == _Pause) {
            StartCoroutine(ShowPreviewVideo());
            ChangeToRecordIcon();
            _RecordManager.StopRecord();
        }
        if (transform.parent.gameObject.activeSelf)
        {
            if (!isRecording)
                _RecordManager.StartRecord();
            ToggleIcon();
            isRecording = !isRecording;
        }
    }

    public IEnumerator ShowPreviewVideo() {
        while (ScreenRecorder.fileName == "")
            yield return new WaitForSeconds(.25f); // Ожидаем сохранения видео

        _videoPlayer.targetTexture.Release();
        string videoFilePath = Application.persistentDataPath + ScreenRecorder.GALLERY_PATH + "/" + ScreenRecorder.fileName;
        if (File.Exists(videoFilePath))
        {
            _videoPlayer.url = videoFilePath;
            _videoPlayer.Prepare();
            if (_videoPlayer.isPrepared == true) {
                _playPauseButton.PausePlay();
            }
        }
    }
}
