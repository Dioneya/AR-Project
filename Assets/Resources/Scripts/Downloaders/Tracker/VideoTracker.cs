/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Vuforia;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
/// 
public class VideoTracker : Tracker
{
    protected override void Start()
    {
        Player = GetComponent<VideoDownload>();
        base.Start();
    }
    override protected void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            EnableComponents();
            Player.StartPlay();
        }

        TargetFound();
    }

    override protected void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            DisableComponents();
            Player.StopPlay();
        }

        TargetLost();
    }
}
