using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Security.Cryptography;
using TMPro;

namespace Hellession
{
    /// <summary>
    /// Hellession's UNITY Animator
    /// Version: 1.2
    /// Latest update: 2022-12-26
    /// Latest changes: Added AnimationType.TMPRetext, as well as TMP as an animation target.
    /// Added AnchorMin and AnchorMax animation types.
    /// </summary>
    public class HLSNAnimator
    {
        /*
        * ISSUES:
        * - No support for animations that can be cutoff like I've had in the past
        * - Make it easier to create HLSNAnimationRequest files!
        * - Easier usage - don't require different functions for different animation types.
        */
        public enum TimeMode
        {
            /// <summary>
            /// The animations measure their progress by the frame count and not time.
            /// This animation time mode can cause inconsistent behavior across various devices with varying refresh rates.
            /// </summary>
            Frame,
            /// <summary>
            /// The animations measure their progress by the time elapsed, rather than frames.
            /// </summary>
            Time
        }
        
        public enum DelayMode
        {
            /// <summary>
            /// yield return null;
            /// Waits till next frame.
            /// </summary>
            NextFrame,
            /// <summary>
            /// yield return new WaitForEndOfFrame;
            /// Waits till next end of frame.
            /// </summary>
            NextEndOfFrame,
            /// <summary>
            /// yield return new WaitForFixedUpdate;
            /// Waits till next fixed update. (happens 50 times / sec)
            /// </summary>
            NextFixedUpdate,
            /// <summary>
            /// yield return new WaitForSeconds(1);
            /// Waits for 1 second.
            /// </summary>
            NextSecond,
            /// <summary>
            /// yield return new WaitForSeconds(x);
            /// Waits for the seconds you've specified in CustomDelaySeconds.
            /// Uses yield return new WaitForSecondsRealtime(x); if you set IsDelayRelatime to true.
            /// </summary>
            Custom
        }
        
        public DelayMode MyDelayMode { get; set; }
        public TimeMode MyTimeMode { get; set; }

        public float CustomDelaySeconds { get; set; }
        public bool IsDelayRealtime { get; set; }
        
        object GetDelayValue()
        {
            switch(MyDelayMode)
            {
                case DelayMode.NextFrame:
                    return null;
                    case DelayMode.NextEndOfFrame:
                    return new WaitForEndOfFrame();
                case DelayMode.NextFixedUpdate:
                    return new WaitForFixedUpdate();
                case DelayMode.NextSecond:
                    {
                        if(IsDelayRealtime)
                            return new WaitForSecondsRealtime(1);
                        return new WaitForSeconds(1);
                    }
                case DelayMode.Custom:
                    {
                        if (IsDelayRealtime)
                            return new WaitForSecondsRealtime(CustomDelaySeconds);
                        return new WaitForSeconds(CustomDelaySeconds);
                    }
            }
            return null;
        }

        public enum AnimationType
        {
            /// <summary>
            /// Requires Vector2s.
            /// Change localPosition of the objects from one position to the other.
            /// </summary>
            Move,
            /// <summary>
            /// Requires Vector2s.
            /// Change anchorMin of the objects from one position to the other.
            /// </summary>
            MoveAnchorMin,
            /// <summary>
            /// Requires Vector2s.
            /// Change anchorMax of the objects from one position to the other.
            /// </summary>
            MoveAnchorMax,
            /// <summary>
            /// Requires Vector2s.
            /// Change localScale of the objects from one scale to the other.
            /// </summary>
            Rescale,
            /// <summary>
            /// Requires Vector2s.
            /// Change the sizeDelta of the objects from one size to the other.
            /// </summary>
            Resize,
            /// <summary>
            /// Requires Colors.
            /// Change the color of the objects from one to the other, but do nothing to the alpha.
            /// </summary>
            ColorAlphaless,
            /// <summary>
            /// Requires Colors.
            /// Change the color of the objects from one to the other.
            /// </summary>
            Color,
            /// <summary>
            /// Change the alpha of the objects from 1 to 0.
            /// </summary>
            Fade,
            /// <summary>
            /// Change the alpha of the objects from 0 to 1.
            /// </summary>
            Show,
            /// <summary>
            /// Requires Colors.
            /// Change the alpha of the objects from x to y.
            /// </summary>
            ChangeA,
            /// <summary>
            /// Requires Vector3s.
            /// Change the rotation of the objects from one to the other using Quaternion.Slerp().
            /// </summary>
            Rotate,
            /// <summary>
            /// Requires Strings.
            /// Animate from one string to another on a TMP_Text object by adding or removing characters over time.
            /// </summary>
            TMPRetext
        }

        /*
        *  ---------- GRAPHICS HERE
        */

        public Coroutine AnimateGraphics(HLSNAnimationRequest request, GameObject[] graphicsIn, bool getAllChildrenAndSelf)
        {
            List<Graphic> totalGraphics = new List<Graphic>();
            foreach(var kv in graphicsIn)
            {
                if(getAllChildrenAndSelf)
                    totalGraphics.AddRange(kv.GetComponentsInChildren<Graphic>());
                else
                    totalGraphics.AddRange(kv.GetComponents<Graphic>());
            }
            return AnimateGraphics(request, totalGraphics.ToArray());
        }

        public Coroutine AnimateGraphics(HLSNAnimationRequest request, GameObject graphicsIn, bool getAllChildrenAndSelf)
        {
            if(getAllChildrenAndSelf)
                return AnimateGraphics(request, graphicsIn.GetComponentsInChildren<Graphic>());
            else
                return AnimateGraphics(request, graphicsIn.GetComponents<Graphic>());
        }
        
        public Coroutine AnimateGraphics(HLSNAnimationRequest request, Graphic targetGraphic)
        {
            return AnimateGraphics(request, new Graphic[] { targetGraphic });
        }

        public Coroutine AnimateGraphics(HLSNAnimationRequest request, List<Graphic> targetGraphics)
        {
            return AnimateGraphics(request, targetGraphics.ToArray());
        }
        
        public Coroutine AnimateGraphics(HLSNAnimationRequest request, Graphic[] targetGraphics)
        {
            TimeMode currTimeMode = MyTimeMode;
            if(request.ForceTimeMode.HasValue)
                currTimeMode = request.ForceTimeMode.Value;
            return request.AttachToBehaviour.StartCoroutine(PlayGraphicAnimation(request, targetGraphics, currTimeMode));
        }
        
        /// <summary>
        /// Main method that SHOULD ALWAYS BE RUN IN A COROUTINE, processes animations that require the targetting of Graphic objects.
        /// </summary>
        IEnumerator PlayGraphicAnimation(HLSNAnimationRequest request, Graphic[] targetGraphics, TimeMode timeMode)
        {
            InitializeGraphicAnimation(request, targetGraphics);
            HLSNAnimatiorTimer timer = new HLSNAnimatiorTimer(request, timeMode);
            while(timer.ShouldAnimationContinue())
            {
                float progress = timer.GetProgress();
                switch (request.Type)
                {
                    case AnimationType.Show:
                        foreach (var kv in targetGraphics)
                            ShowAtFrame(request, progress, kv);
                        break;
                    case AnimationType.Fade:
                        foreach (var kv in targetGraphics)
                            FadeAtFrame(request, progress, kv);
                        break;
                    case AnimationType.ColorAlphaless:
                        foreach (var kv in targetGraphics)
                            ColorAlphalessAtFrame(request, progress, kv);
                        break;
                    case AnimationType.Color:
                        foreach (var kv in targetGraphics)
                            ColorAtFrame(request, progress, kv);
                        break;
                    case AnimationType.ChangeA:
                        foreach (var kv in targetGraphics)
                            ChangeAAtFrame(request, progress, kv);
                        break;
                }
                timer.FramesElapsed++;
                yield return GetDelayValue();
            }
            FinalizeGraphicAnimation(request, targetGraphics);
        }
        
        void InitializeGraphicAnimation(HLSNAnimationRequest request, Graphic[] targetGraphics)
        {
            switch(request.Type)
            {
                case AnimationType.Move:
                case AnimationType.Rescale:
                case AnimationType.Resize:
                case AnimationType.MoveAnchorMax:
                case AnimationType.MoveAnchorMin:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateObjects() with this AnimationType!");
                case AnimationType.TMPRetext:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateTMPs() with this AnimationType!");
                case AnimationType.Rotate:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use Animate3DObjects() with this AnimationType!");
                case AnimationType.Show:
                    foreach (var kv in targetGraphics)
                        ShowAtFrame(request, 0, kv);
                    break;
                case AnimationType.Fade:
                    foreach (var kv in targetGraphics)
                        FadeAtFrame(request, 0, kv);
                    break;
                case AnimationType.ColorAlphaless:
                    foreach (var kv in targetGraphics)
                        ColorAlphalessAtFrame(request, 0, kv);
                    break;
                case AnimationType.Color:
                    foreach (var kv in targetGraphics)
                        ColorAtFrame(request, 0, kv);
                    break;
                case AnimationType.ChangeA:
                    foreach(var kv in targetGraphics)
                        ChangeAAtFrame(request, 0, kv);
                    break;
            }
        }

        void FinalizeGraphicAnimation(HLSNAnimationRequest request, Graphic[] targetGraphics)
        {
            switch (request.Type)
            {
                case AnimationType.Show:
                    foreach (var kv in targetGraphics)
                        ShowAtFrame(request, 1, kv);
                    break;
                case AnimationType.Fade:
                    foreach (var kv in targetGraphics)
                        FadeAtFrame(request, 1, kv);
                    break;
                case AnimationType.ColorAlphaless:
                    foreach (var kv in targetGraphics)
                        ColorAlphalessAtFrame(request, 1, kv);
                    break;
                case AnimationType.Color:
                    foreach (var kv in targetGraphics)
                        ColorAtFrame(request, 1, kv);
                    break;
                case AnimationType.ChangeA:
                    foreach (var kv in targetGraphics)
                        ChangeAAtFrame(request, 1, kv);
                    break;
            }
        }
        
        public void ChangeAAtFrame(HLSNAnimationRequest request, float progress, Graphic graphic)
        {
            graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, request.ColorA.a + (request.ColorB.a - request.ColorA.a) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }
        public void ShowAtFrame(HLSNAnimationRequest request, float progress, Graphic graphic)
        {
            graphic.color += new Color(0, 0, 0, HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }
        public void FadeAtFrame(HLSNAnimationRequest request, float progress, Graphic graphic)
        {
            graphic.color -= new Color(0, 0, 0, HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }
        public void ColorAtFrame(HLSNAnimationRequest request, float progress, Graphic graphic)
        {
            graphic.color = new Color(
                request.ColorA.r + (request.ColorB.r - request.ColorA.r) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode), 
                request.ColorA.g + (request.ColorB.g - request.ColorA.g) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode), 
                request.ColorA.b + (request.ColorB.b - request.ColorA.b) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode), 
                request.ColorA.a + (request.ColorB.a - request.ColorA.a) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }
        public void ColorAlphalessAtFrame(HLSNAnimationRequest request, float progress, Graphic graphic)
        {
            graphic.color = new Color(
                request.ColorA.r + (request.ColorB.r - request.ColorA.r) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                request.ColorA.g + (request.ColorB.g - request.ColorA.g) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                request.ColorA.b + (request.ColorB.b - request.ColorA.b) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                graphic.color.a);
        }

        /*
        *  ---------- OBJECTS HERE
        */
        
        /// <summary>
        /// I don't think it makes sense to support multiple targets here. Like seriously in which case would that make sense?
        /// </summary>
        public Coroutine AnimateObjects(HLSNAnimationRequest request, RectTransform target)
        {
            TimeMode currTimeMode = MyTimeMode;
            if (request.ForceTimeMode.HasValue)
                currTimeMode = request.ForceTimeMode.Value;
            return request.AttachToBehaviour.StartCoroutine(PlayObjectAnimation(request, target, currTimeMode));
        }

        /// <summary>
        /// Main method that SHOULD ALWAYS BE RUN IN A COROUTINE, processes animations that require the targetting of Graphic objects.
        /// </summary>
        IEnumerator PlayObjectAnimation(HLSNAnimationRequest request, RectTransform target, TimeMode timeMode)
        {
            InitializeObjectAnimation(request, target);
            HLSNAnimatiorTimer timer = new HLSNAnimatiorTimer(request, timeMode);
            while (timer.ShouldAnimationContinue())
            {
                float progress = timer.GetProgress();
                switch (request.Type)
                {
                    case AnimationType.Move:
                        MoveAtFrame(request, progress, target);
                        break;
                    case AnimationType.Rescale:
                        RescaleAtFrame(request, progress, target);
                        break;
                    case AnimationType.Resize:
                        ResizeAtFrame(request, progress, target);
                        break;
                }
                timer.FramesElapsed++;
                yield return GetDelayValue();
            }
            FinalizeObjectAnimation(request, target);
        }

        void FinalizeObjectAnimation(HLSNAnimationRequest request, RectTransform target)
        {
            switch (request.Type)
            {
                case AnimationType.Move:
                    MoveAtFrame(request, 1, target);
                    break;
                case AnimationType.Rescale:
                    RescaleAtFrame(request, 1, target);
                    break;
                case AnimationType.Resize:
                    ResizeAtFrame(request, 1, target);
                    break;
            }
        }

        void InitializeObjectAnimation(HLSNAnimationRequest request, RectTransform target)
        {
            switch (request.Type)
            {
                case AnimationType.Move:
                    MoveAtFrame(request, 0, target);
                    break;
                case AnimationType.Rescale:
                    RescaleAtFrame(request, 0, target);
                    break;
                case AnimationType.Resize:
                    ResizeAtFrame(request, 0, target);
                    break;
                case AnimationType.Show:
                case AnimationType.Fade:
                case AnimationType.ColorAlphaless:
                case AnimationType.Color:
                case AnimationType.ChangeA:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateGraphics() with this AnimationType!");
                case AnimationType.TMPRetext:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateTMPs() with this AnimationType!");
                case AnimationType.Rotate:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use Animate3DObjects() with this AnimationType!");
            }
        }
        
        public void MoveAtFrame(HLSNAnimationRequest request, float progress, RectTransform target)
        {
            target.localPosition = new Vector2(
                request.Vector2A.x + (request.Vector2B.x - request.Vector2A.x) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                request.Vector2A.y + (request.Vector2B.y - request.Vector2A.y) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }

        public void MoveAnchorMinAtFrame(HLSNAnimationRequest request, float progress, RectTransform target)
        {
            target.anchorMin = new Vector2(
                request.Vector2A.x + (request.Vector2B.x - request.Vector2A.x) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                request.Vector2A.y + (request.Vector2B.y - request.Vector2A.y) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }

        public void MoveAnchorMaxAtFrame(HLSNAnimationRequest request, float progress, RectTransform target)
        {
            target.anchorMax = new Vector2(
                request.Vector2A.x + (request.Vector2B.x - request.Vector2A.x) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                request.Vector2A.y + (request.Vector2B.y - request.Vector2A.y) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }
        public void RescaleAtFrame(HLSNAnimationRequest request, float progress, RectTransform target)
        {
            target.localScale = new Vector2(
                request.Vector2A.x + (request.Vector2B.x - request.Vector2A.x) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                request.Vector2A.y + (request.Vector2B.y - request.Vector2A.y) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }
        public void ResizeAtFrame(HLSNAnimationRequest request, float progress, RectTransform target)
        {
            target.sizeDelta = new Vector2(
                request.Vector2A.x + (request.Vector2B.x - request.Vector2A.x) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode),
                request.Vector2A.y + (request.Vector2B.y - request.Vector2A.y) * HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }

        /*
        *  ---------- 3D OBJECTS HERE
        */

        public Coroutine Animate3DObjects(HLSNAnimationRequest request, Transform target)
        {
            TimeMode currTimeMode = MyTimeMode;
            if (request.ForceTimeMode.HasValue)
                currTimeMode = request.ForceTimeMode.Value;
            return request.AttachToBehaviour.StartCoroutine(Play3DObjectAnimation(request, target, currTimeMode));
        }

        /// <summary>
        /// Main method that SHOULD ALWAYS BE RUN IN A COROUTINE, processes animations that require the targetting of Graphic objects.
        /// </summary>
        IEnumerator Play3DObjectAnimation(HLSNAnimationRequest request, Transform target, TimeMode timeMode)
        {
            Initialize3DObjectAnimation(request, target);
            HLSNAnimatiorTimer timer = new HLSNAnimatiorTimer(request, timeMode);
            while (timer.ShouldAnimationContinue())
            {
                float progress = timer.GetProgress();
                switch (request.Type)
                {
                    case AnimationType.Rotate:
                        RotateAtFrame(request, progress, target);
                        break;
                }
                timer.FramesElapsed++;
                yield return GetDelayValue();
            }
            Finalize3DObjectAnimation(request, target);
        }

        void Finalize3DObjectAnimation(HLSNAnimationRequest request, Transform target)
        {
            switch (request.Type)
            {
                case AnimationType.Rotate:
                    RotateAtFrame(request, 1, target);
                    break;
            }
        }

        void Initialize3DObjectAnimation(HLSNAnimationRequest request, Transform target)
        {
            switch (request.Type)
            {
                case AnimationType.Rotate:
                    RotateAtFrame(request, 0, target);
                    break;
                case AnimationType.Move:
                case AnimationType.Rescale:
                case AnimationType.Resize:
                case AnimationType.MoveAnchorMax:
                case AnimationType.MoveAnchorMin:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateObjects() with this AnimationType!");
                case AnimationType.Show:
                case AnimationType.Fade:
                case AnimationType.ColorAlphaless:
                case AnimationType.Color:
                case AnimationType.ChangeA:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateGraphics() with this AnimationType!");
                case AnimationType.TMPRetext:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateTMPs() with this AnimationType!");
            }
        }

        public void RotateAtFrame(HLSNAnimationRequest request, float progress, Transform target)
        {
            target.localRotation = Quaternion.Slerp(Quaternion.Euler(request.Vector3A), Quaternion.Euler(request.Vector3B), HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode));
        }

        /*
        *  ---------- TMP OBJECTS HERE
        */

        public Coroutine AnimateTMPs(HLSNAnimationRequest request, TMP_Text target)
        {
            TimeMode currTimeMode = MyTimeMode;
            if (request.ForceTimeMode.HasValue)
                currTimeMode = request.ForceTimeMode.Value;
            return request.AttachToBehaviour.StartCoroutine(PlayTMPAnimation(request, target, currTimeMode));
        }

        /// <summary>
        /// Main method that SHOULD ALWAYS BE RUN IN A COROUTINE, processes animations that require the targetting of Graphic objects.
        /// </summary>
        IEnumerator PlayTMPAnimation(HLSNAnimationRequest request, TMP_Text target, TimeMode timeMode)
        {
            InitializeTMPAnimation(request, target);
            HLSNAnimatiorTimer timer = new HLSNAnimatiorTimer(request, timeMode);
            while (timer.ShouldAnimationContinue())
            {
                float progress = timer.GetProgress();
                switch (request.Type)
                {
                    case AnimationType.TMPRetext:
                        TMPRetextFrame(request, progress, target);
                        break;
                }
                timer.FramesElapsed++;
                yield return GetDelayValue();
            }
            FinalizeTMPAnimation(request, target);
        }

        void FinalizeTMPAnimation(HLSNAnimationRequest request, TMP_Text target)
        {
            switch (request.Type)
            {
                case AnimationType.TMPRetext:
                    TMPRetextFrame(request, 1, target);
                    break;
            }
        }

        void InitializeTMPAnimation(HLSNAnimationRequest request, TMP_Text target)
        {
            switch (request.Type)
            {
                case AnimationType.Rotate:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use Animate3DObjects() with this AnimationType!");
                case AnimationType.Move:
                case AnimationType.Rescale:
                case AnimationType.Resize:
                case AnimationType.MoveAnchorMax:
                case AnimationType.MoveAnchorMin:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateObjects() with this AnimationType!");
                case AnimationType.Show:
                case AnimationType.Fade:
                case AnimationType.ColorAlphaless:
                case AnimationType.Color:
                case AnimationType.ChangeA:
                    throw new ArgumentException($"Invalid AnimationType: {request.Type}. Please use AnimateGraphics() with this AnimationType!");
                case AnimationType.TMPRetext:
                    TMPRetextFrame(request, 0, target);
                    break;
            }
        }

        public void TMPRetextFrame(HLSNAnimationRequest request, float progress, TMP_Text target)
        {
            //check which string has more or less characters
            bool addingChars = true;
            int charDifference = request.StringB.Length - request.StringA.Length;
            if (charDifference < 0)
            {
                addingChars = false;
                charDifference = -charDifference;
            }
            string substringToAdd = request.StringB.Substring(request.StringA.Length);

            if(addingChars)
                target.text = $"{request.StringA}{substringToAdd.Substring(0, (int)Math.Round(HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode) * charDifference))}";
            else
                target.text = request.StringA.Substring(0, request.StringA.Length - (int)Math.Round(HLSNFunctions.GetFunctionedNumber(progress, request.EasingMode) * charDifference));
        }
    }
}