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
public class AssetTracker : Tracker
{
    public GameObject model;

    public bool isAnimatedBundle = false;
    public static Vector3 position = new Vector3(0,0,0);
    override protected void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            EnableComponents();
            if (model != null)
            {
                model.SetActive(true);

                if (isAnimatedBundle) {
                    SetPosition(model, position);
                }
            }
        }

        TargetFound();
    }

    override protected void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            DisableComponents();
            if (model != null)
                model.SetActive(false);
        }

        TargetLost();
    }

    public static void SetPosition(GameObject model, Vector3 vector) {
        model.transform.localPosition = vector;
    }
}
