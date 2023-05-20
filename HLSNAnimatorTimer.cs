using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Security.Cryptography;

namespace Hellession
{
    /// <summary>
    /// Part of Hellession's UNITY Animator.
    /// All versioning details found in HLSNAnimator.cs
    /// </summary>
    public class HLSNAnimatiorTimer
    {
        public HLSNAnimatiorTimer(HLSNAnimationRequest associatedRequest, HLSNAnimator.TimeMode timeMode)
        {
            TimeMode = timeMode;
            AssociatedRequest = associatedRequest;
            StartTime = DateTime.Now;
            FramesElapsed = 0;
        }
        
        public HLSNAnimator.TimeMode TimeMode { get; }
        public DateTime StartTime { get; }
        public int FramesElapsed { get; set; }
        public HLSNAnimationRequest AssociatedRequest { get; }
        
        public bool ShouldAnimationContinue()
        {
            switch(TimeMode)
            {
                case HLSNAnimator.TimeMode.Time:
                    return DateTime.Now - StartTime < AssociatedRequest.Duration;
                case HLSNAnimator.TimeMode.Frame:
                    return FramesElapsed < AssociatedRequest.Frames;
            }
            return false;
        }
        
        /// <summary>
        /// Tells you how much of the animation should be done at this point with a number ranging 0-1.
        /// </summary>
        public float GetProgress()
        {
            switch (TimeMode)
            {
                case HLSNAnimator.TimeMode.Time:
                    return Convert.ToSingle((DateTime.Now - StartTime).TotalSeconds / AssociatedRequest.Duration.TotalSeconds);
                case HLSNAnimator.TimeMode.Frame:
                    return FramesElapsed / (float)AssociatedRequest.Frames;
            }
            return 0;
        }
    }
}