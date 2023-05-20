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
    public class HLSNAnimationRequest
    {
        public HLSNAnimator.AnimationType Type { get; set; }
        
        public MonoBehaviour AttachToBehaviour { get; set; }

        public HLSNUtil.EasingMode EasingMode { get; set; } = HLSNUtil.EasingMode.LINEAR;

        public Vector2 Vector2A { get; set; }
        public Vector2 Vector2B { get; set; }

        public Vector3 Vector3A { get; set; }
        public Vector3 Vector3B { get; set; }
        
        public string StringA { get; set; }
        public string StringB { get; set; }

        
        public Color ColorA { get; set; }
        public Color ColorB { get; set; }
        
        
        public TimeSpan Duration { get; set; }
        public int Frames { get; set; }
        public HLSNAnimator.TimeMode? ForceTimeMode { get; set; }

        public static HLSNAnimationRequest Move(Vector2 from, Vector2 to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                Vector2A = from,
                Vector2B = to,
                Type = HLSNAnimator.AnimationType.Move,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest Rescale(Vector2 from, Vector2 to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                Vector2A = from,
                Vector2B = to,
                Type = HLSNAnimator.AnimationType.Rescale,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest Resize(Vector2 from, Vector2 to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                Vector2A = from,
                Vector2B = to,
                Type = HLSNAnimator.AnimationType.Resize,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest TMPRetext(string from, string to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                StringA = from,
                StringB = to,
                Type = HLSNAnimator.AnimationType.TMPRetext,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest Rotate(Vector3 from, Vector3 to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                Vector3A = from,
                Vector3B = to,
                Type = HLSNAnimator.AnimationType.Rotate,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest ColorAlphaless(Color from, Color to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                ColorA = from,
                ColorB = to,
                Type = HLSNAnimator.AnimationType.ColorAlphaless,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest Color(Color from, Color to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                ColorA = from,
                ColorB = to,
                Type = HLSNAnimator.AnimationType.Color,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest Fade(MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Fade,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest Show(MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                Type = HLSNAnimator.AnimationType.Show,
                AttachToBehaviour = mbh
            };
        }
        public static HLSNAnimationRequest ChangeA(Color from, Color to, MonoBehaviour mbh = null)
        {
            return new HLSNAnimationRequest
            {
                ColorA = from,
                ColorB = to,
                Type = HLSNAnimator.AnimationType.ChangeA,
                AttachToBehaviour = mbh
            };
        }
    }
}