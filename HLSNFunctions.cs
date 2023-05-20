using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hellession
{
    /// <summary>
    /// Hellession's easing functions
    /// Version: 10.0
    /// Latest update: unknown
    /// Latest changes: unknown
    /// </summary>
    public static class HLSNFunctions
    {
        /// <summary>
        /// Slows down at the end using a number to the power of 2
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseOut2(float x)
        {
            return (float)-Math.Pow(x, 2) + 2 * x;
        }

        /// <summary>
        /// Slows down at the start using a number to the power of 2
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseIn2(float x)
        {
            return (float)Math.Pow(x, 2);
        }

        /// <summary>
        /// Slows down at the start using a number to the power of 3
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseIn3(float x)
        {
            return (float)Math.Pow(x, 3);
        }

        /// <summary>
        /// Slows down at the end using a number to the power of 3
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseOut3(float x)
        {
            return (float)Math.Pow(x - 1, 3) + 1;
        }

        /// <summary>
        /// Slows down at the end using a number to the power of 4
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseOut4(float x)
        {
            return (float)-Math.Pow(x - 1, 4) + 1;
        }

        /// <summary>
        /// Slows down at the start and the end using a reciprocal function - small change
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseInOutDiv(float x)
        {
            return 1f / (-(x - 1.618f)) - 0.618f;
        }
        /// <summary>
        /// Slows down at the start and the end using a reciprocal function - slight change
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseInOutDiv2(float x)
        {
            return 0.5f / (-(x - 1.366f)) - 0.366f;
        }

        /// <summary>
        /// Slows down at the start and the end using a reciprocal function - ok change
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseInOutDiv3(float x)
        {
            return 0.1f / (-(x - 1.0916f)) - 0.0916f;
        }

        /// <summary>
        /// Slows down at the start and the end using a reciprocal function - strong change
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseInOutDiv4(float x)
        {
            return 0.1f / (-(x - 1.0099f)) - 0.0099f;
        }

        /// <summary>
        /// Ease in out I've found on the internet k
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static float EaseInOutQuadInt(float x)
        {
            x *= 2;
            if (x < 1) return 1f / 2f * x * x;
            x--;
            return -1f / 2f * (x * (x - 2) - 1);
        }

        public static float GetFunctionedNumber(float x, HLSNUtil.EasingMode easing = HLSNUtil.EasingMode.LINEAR)
        {
            if(easing==HLSNUtil.EasingMode.LINEAR)
            {
                return x;
            }
            else if (easing == HLSNUtil.EasingMode.EASEIN2)
            {
                return EaseIn2(x);
            }
            else if (easing == HLSNUtil.EasingMode.EASEIN3)
            {
                return EaseIn3(x);
            }
            else if (easing == HLSNUtil.EasingMode.EASEINOUT2)
            {
                return EaseInOutQuadInt(x);
            }
            else if (easing == HLSNUtil.EasingMode.EASEOUT2)
            {
                return EaseOut2(x);
            }
            else if (easing == HLSNUtil.EasingMode.EASEOUT3)
            {
                return EaseOut3(x);
            }
            else if (easing == HLSNUtil.EasingMode.EASEOUT4)
            {
                return EaseOut4(x);
            }

            return x;
        }
    }
}
