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
    [Serializable]
    public class JSONColor
    {
        public byte r;
        public byte g;
        public byte b;
        public byte a;
    }
    
    /// <summary>
    /// Hellession's utils UNITY VERSION
    /// Version: 10.0
    /// Latest update: 2022-11-01
    /// Latest changes: Deleted a bunch of Animation stuff that is now part of HLSNAnimator. Deleted a few useless methods.
    /// </summary>
    public class HLSNUtil
    {

        public static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        /// <summary>
        /// </summary>
        /// <returns>Random double from 0.0 to 1.0</returns>
        public static double GetRandomDouble()
        {
            byte[] result = new byte[8];
            rngCsp.GetBytes(result);
            return (double)BitConverter.ToUInt64(result, 0) / ulong.MaxValue;
        }

        public static string GetOrdinalEnding(int num)
        {
            if (num == 1)
                return "st";
            if (num == 2)
                return "nd";
            if (num == 3)
                return "rd";
            if (num.ToString().EndsWith("11") || num.ToString().EndsWith("12") || num.ToString().EndsWith("13"))
                return "th";
            if (num.ToString().EndsWith("1"))
                return "st";
            if (num.ToString().EndsWith("2"))
                return "nd";
            if (num.ToString().EndsWith("3"))
                return "rd";
            return "th";
        }


        public static double GetRandomDouble(double min, double max)
        {
            byte[] result = new byte[8];
            rngCsp.GetBytes(result);
            return (double)BitConverter.ToUInt64(result, 0) / ulong.MaxValue * (max - min) + min;
        }

        /// <summary>
        /// Unlike C# pseudo-random, this one may return the value equal to param max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int GetRandomInt(int min, int max)
        {
            byte[] result = new byte[8];
            rngCsp.GetBytes(result);
            return (int)Math.Round(((double)BitConverter.ToUInt64(result, 0) / ulong.MaxValue) * (max - min)) + min;
        }
        
        /// <summary>
        /// Gets the source that is playing the clip named in arg 'source' in the GameObject 'ob'.
        /// </summary>
        public static AudioSource getSoundSource(string source, GameObject ob)
        {
            AudioSource[] mouseOverSound = ob.GetComponents<AudioSource>();
            for (int i = 0; i < mouseOverSound.Length; i++)
            {
                if (mouseOverSound[i].clip.name.Equals(source))
                {
                    return mouseOverSound[i];
                }
            }
            return new AudioSource();
        }
        
        /// <summary>
        /// Turns the IEnumerable<T> into a nice string (should be an extension method imo)
        /// </summary>
        public static string ToNiceString<T>(IEnumerable<T> input)
        {
            string returnString = "{";
            bool shouldPutComma = false;
            foreach (T i in input)
            {
                if (shouldPutComma)
                {
                    returnString += ",";
                }
                returnString += i.ToString();
                shouldPutComma = true;
            }
            returnString += "}";
            return returnString;
        }

        /// <summary>
        /// Instantly hides all of the graphic objects in the parent AND children game objects
        /// </summary>
        /// <param name="parent"></param>
        public static void HideGraphics(GameObject parent)
        {
            Graphic[] listom = parent.GetComponentsInChildren<Graphic>();
            foreach (Graphic s in listom)
            {
                //Debug.Log("Hide: " + s.gameObject.name);
                s.color = s.color - new Color(0, 0, 0, 1f);
            }
        }

        /// <summary>
        /// Instantly shows all of the graphic objects in the parent AND children game objects
        /// </summary>
        /// <param name="parent"></param>
        public static void ShowGraphics(GameObject parent)
        {
            Graphic[] listom = parent.GetComponentsInChildren<Graphic>();
            foreach (Graphic s in listom)
            {
                //Debug.Log("Show: " + s.gameObject.name);
                s.color = s.color + new Color(0, 0, 0, 1f);
            }
        }

        /// <summary>
        /// Instantly hides all of the graphic objects in the parent AND children game objects
        /// </summary>
        /// <param name="parent"></param>
        public static void HideGraphics(GameObject[] parent)
        {
            foreach (GameObject i in parent)
            {
                Graphic[] listom = i.GetComponentsInChildren<Graphic>();
                foreach (Graphic s in listom)
                {
                    s.color = s.color - new Color(0, 0, 0, 1f);
                }
            }
        }

        /// <summary>
        /// Instantly shows all of the graphic objects in the parent AND children game objects
        /// </summary>
        /// <param name="parent"></param>
        public static void ShowGraphics(GameObject[] parent)
        {
            foreach (GameObject i in parent)
            {
                Graphic[] listom = i.GetComponentsInChildren<Graphic>();
                foreach (Graphic s in listom)
                {
                    s.color = s.color + new Color(0, 0, 0, 1f);
                }
            }
        }
        
        /// <summary>
        /// Hides all the graphic objects provided in the array (sets 0 alpha)
        /// </summary>
        public static void HideGraphics(Graphic[] parent)
        {
            foreach (Graphic i in parent)
            {
                i.color = i.color - new Color(0, 0, 0, 1f);
            }
        }

        /// <summary>
        /// Shows all the graphic objects provided in the array (sets 1 alpha)
        /// </summary>
        public static void ShowGraphics(Graphic[] parent)
        {
            foreach (Graphic i in parent)
            {

                i.color = i.color + new Color(0, 0, 0, 1f);
            }
        }
        
        /// <summary>
        /// Get a list of Graphic objects in the children of all of these game objects.
        /// </summary>
        public static Graphic[] GetGraphicList(GameObject[] parent)
        {
            List<Graphic> res = new List<Graphic>();
            foreach (GameObject i in parent)
            {
                Graphic[] ress = i.GetComponentsInChildren<Graphic>();
                foreach (Graphic s in ress)
                {
                    res.Add(s);
                }
            }
            return res.ToArray();
        }


        /// <summary>
        /// Gets itself and all of its children graphic objects
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Graphic[] GetGraphicList(GameObject parent)
        {
            List<Graphic> res = new List<Graphic>();
            Graphic[] ress = parent.GetComponentsInChildren<Graphic>();
            foreach (Graphic s in ress)
            {
                res.Add(s);
            }
            return res.ToArray();
        }

        public enum EasingMode
        {
            EASEIN2,
            EASEOUT2,
            EASEOUT3,
            EASEOUT4,
            EASEINOUT2,
            EASEIN3,
            LINEAR
        }

        /// <summary>
        /// Instantly hides the graphic
        /// </summary>
        /// <param name="parent"></param>
        public static void HideGraphics(Graphic parent)
        {

            parent.color = parent.color - new Color(0, 0, 0, 1f);
        }

        public static int GetLowestNumber(List<int> lowNums)
        {
            int lowest = int.MaxValue;
            foreach (int k in lowNums)
            {
                if (k < lowest)
                {
                    lowest = k;
                }
            }
            return lowest;
        }

        /// <summary>
        /// Instantly shows the graphic
        /// </summary>
        /// <param name="parent"></param>
        public static void ShowGraphics(Graphic parent)
        {
            parent.color = parent.color + new Color(0, 0, 0, 1f);
        }

        public static T FindObjectInChildrenNamed<T>(GameObject host, string parentGOname) where T : UnityEngine.Component
        {
            T[] op = host.GetComponentsInChildren<T>();
            foreach (T s in op)
            {
                if (s.gameObject.name == parentGOname)
                {
                    return s;
                }
            }
            Debug.Log("Failed to find object " + parentGOname + " in the game object named " + host.name);
            return null;
        }

        public static System.Random GetRandom()
        {
            return new System.Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        }
        /*public static GameObject FindGameObjectWithChild<T>(GameObject host, string name)
        {
            Component[] op = host.GetComponentsInChildren<Component>();
            foreach (Component s in op)
            {
                if (s == parentGOname)
                {
                    return s.gameObject;
                }
            }
            Debug.Log("Failed to find object with component " + parentGOname + " in the game object named " + host.name);
            return null;
        }*/

        public static TimeSpan averageOfTimeSpans(TimeSpan[] o)
        {
            long ticks = 0;
            foreach (TimeSpan kk in o)
            {
                ticks += kk.Ticks;
            }
            ticks = (long)(ticks / (double)o.Length);
            return TimeSpan.FromTicks(ticks);
        }

        public static double avg(double[] nums)
        {
            double total = 0.0;
            foreach (double s in nums)
            {
                total += s;
            }
            return total / nums.Length;
        }

        /// <summary>
        /// Takes a list of values and then returns the lowest number, if one of these is lower than lowest parameter
        /// </summary>
        /// <param name="lowest"></param>
        /// <param name="candids"></param>
        /// <returns></returns>
        public static double low(double lowest, double[] candids)
        {
            double result = lowest;
            foreach (double s in candids)
            {
                if (s < result)
                {
                    result = s;
                }
            }
            return result;
        }

        public static string RemoveAllSpaces(string k)
        {
            for (int i = 0; i < k.Length; i++)
            {
                if (k[i] == ' ') { k.Remove(i, 1); }
            }
            return k;
        }
        
        public static UnityEngine.Object FindObjectByName(string name, Type type)
        {
            UnityEngine.Object[] op = MonoBehaviour.FindObjectsOfType(type);
            for (int i = 0; i < op.Length; i++)
            {
                if (op[i].name.Equals(name))
                {
                    return op[i];
                }
            }
            return null;
        }

        public static V ForceDictExistance<T, V>(Dictionary<T, V> thing, T k, V def)
        {
            if (thing.ContainsKey(k))
            {
                return thing[k];
            }
            else
            {
                thing.Add(k, def);
                return def;
            }
        }

        /// <summary>
        /// Downgrades or increments the number and makes sure that it is not higher than max or lower than 0, by subtracting or adding the max
        /// </summary>
        /// <param name="toDowngrade"></param>
        /// <param name="max"></param>
        /// <param name="computerStyle">if true, will make sure it is not equal to or higher than max, but may have 0 as possible outcome</param>
        /// <returns></returns>
        public static int Downgrade(int toDowngrade, int max, bool computerStyle)
        {
            if (computerStyle)
            {
                while (toDowngrade < 0)
                {
                    toDowngrade += max;
                }
                while (toDowngrade >= max)
                {
                    toDowngrade -= max;
                }
                return toDowngrade;
            }
            else
            {
                while (toDowngrade <= 0)
                {
                    toDowngrade += max;
                }
                while (toDowngrade > max)
                {
                    toDowngrade -= max;
                }
                return toDowngrade;
            }
        }

        /// <summary>
        /// Returns the color transition between one to another  for the color float component
        /// </summary>
        /// <param name="startColor">starting color value</param>
        /// <param name="finishColor">finish color value</param>
        /// <param name="multiplier">progress 0-1</param>
        /// <returns></returns>
        public static float transitionToColor(float startColor, float finishColor, float multiplier)
        {

            return startColor + (finishColor - startColor) * multiplier;

        }

        public static Color transitionToColor(Color startColor, Color finishColor, float multiplier, bool includeA)
        {
            if (includeA)
            {
                return new Color(transitionToColor(startColor.r, finishColor.r, multiplier),
                    transitionToColor(startColor.g, finishColor.g, multiplier),
                    transitionToColor(startColor.b, finishColor.b, multiplier),
                    transitionToColor(startColor.a, finishColor.a, multiplier));
            }
            else
            {
                return new Color(transitionToColor(startColor.r, finishColor.r, multiplier),
                    transitionToColor(startColor.g, finishColor.g, multiplier),
                    transitionToColor(startColor.b, finishColor.b, multiplier),
                    startColor.a);
            }
        }

        /// <summary>
        /// Takes the input and makes it so there are numCount digits in total.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double CutToNumberCount(double input, int numCount)
        {//use the fact that the first number to violate being NOT LESS than 10^numCount is likely more digits
            //if numCount = 3, 698 < 1000, etc.
            //D("Cutting " + input + " down to " + numCount + " numbers.");
            int zerosStripped = 0;

            // say we need 3 numbers and have 50283. 50283 is not < 1000, then 5028.3 is not < 1000, but 502.83 is < 1000. But because we took away 2 zeros, we have to multiply the rounded number by 10^cutZeros

            while (!(input < Math.Pow(10.0, numCount)))
            {
                //D("Deleting zeros");
                zerosStripped++;
                input /= 10.0;
            }
            if (zerosStripped > 0)
            {
                input = Math.Round(input) * Math.Pow(10.0, zerosStripped);
            }
            //inverse problem, that is MUCH more likely to happen with QU: The number is actually something like 30.593. We need to bring it to 30.6.
            int zerosAdded = 0;
            while (!(input > Math.Pow(10.0, numCount - 1)))
            {
                //D("Running");
                zerosAdded++;
                input *= 10.0;
            }
            //D("I added " + zerosAdded + " so the number " + input + " after rounding has to be divided by " + Math.Pow(10.0, zerosAdded));
            if (zerosAdded > 0)
            {
                input = Math.Round(input) / Math.Pow(10.0, zerosAdded);
            }
            return input;
        }

        /// <summary>
        /// Useless... use Color = Color32 instead!!!!!
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Color colorFromByteRGB(int r, int g, int b)
        {
            return new Color(r / 255f, g / 255f, b / 255f);
        }

        public static double whatPercentageOf(int part, int total, bool roundToHundreths)
        {
            double percent = part / (double)total;
            percent *= 100.0;
            if (roundToHundreths)
            {
                return Math.Round(percent * 100.0) / 100.0;
            }
            else
            {
                return percent;
            }
        }

        /// <summary>
        /// Returns the d value rounded to look like percentage with hundreths
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double doubleToPercentage(double d)
        {
            return Math.Round(d * 10000.0) / 100.0;
        }

        public static float NumToHundreths(float d)
        {
            return NumToHundreths(new Decimal(d));
        }
        public static float NumToTenths(float d)
        {
            return NumToTenths(new Decimal(d));
        }

        public static float NumToHundreths(Decimal d)
        {
            return (float)Decimal.ToDouble(Decimal.Round(Decimal.Multiply(d, new Decimal(100f))) / new Decimal(100f));
        }

        public static float NumToTenths(Decimal d)
        {
            return (float)Decimal.ToDouble(Decimal.Round(Decimal.Multiply(d, new Decimal(10f))) / new Decimal(10f));
        }

        public static T[] removeFromArray<T>(int index, T[] list)
        {
            List<T> convertable = list.ToList();
            convertable.RemoveAt(index);
            return convertable.ToArray();
        }

        public static T[] addToArray<T>(T toAdd, T[] list)
        {
            List<T> convertable = list.ToList();
            convertable.Add(toAdd);
            return convertable.ToArray();
        }

        public static T[] addToArray<T>(T toAdd, T[] list, int insertionIndex)
        {
            List<T> convertable = list.ToList();
            convertable.Insert(insertionIndex, toAdd);
            return convertable.ToArray();
        }

        public static int indexOf<T>(T[] table, T element)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (element.Equals(table[i]))
                {
                    return i;
                }
            }
            return -1;
        }

        public static T[] copyArray<T>(T[] array)
        {
            List<T> convertable = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                convertable.Add(array[i]);
            }
            return convertable.ToArray();
        }

        public static List<T> copyList<T>(List<T> list)
        {
            List<T> convertable = new List<T>();
            for (int i = 0; i < list.Count; i++)
            {
                convertable.Add(list[i]);
            }
            return convertable;
        }

        public static string listToString<T>(List<T> any)
        {
            string result = "{ ";
            foreach (T k in any)
            {
                result = result + k.ToString() + ",";
            }
            result = result.Substring(0, result.Length - 1) + "}";
            return result;
        }

        public static string arrayToString<T>(T[] any)
        {
            string result = "{";
            foreach (T k in any)
            {
                result = result + k.ToString() + ",";
            }
            result = result.Substring(0, result.Length - 1) + "}";
            return result;
        }

        /// <summary>
        /// Use this... Find() if you can
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindObjectByName<T>(string name) where T : UnityEngine.Object
        {
            T[] op = MonoBehaviour.FindObjectsOfType<T>();
            for (int i = 0; i < op.Length; i++)
            {
                if (op[i].name.Equals(name))
                {
                    return op[i];
                }
            }
            Debug.Log("Failed to find the needed object!");
            return default(T);
        }

        public Color JSONColorToColor(JSONColor k)
        {
            return new Color(k.r / 255f, k.g / 255f, k.b / 255f);
        }

        public JSONColor ColorToJSONColor(Color k)
        {
            return new JSONColor()
            {
                r = (byte)Math.Round(k.r * 255),
                g = (byte)Math.Round(k.g * 255),
                b = (byte)Math.Round(k.b * 255),
                a = (byte)Math.Round(k.a * 255)
            };
        }

        public static bool IsNumberPrimePrimitive(int number)
        {
            for (int i = 2; i < number; i++)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(Dictionary<TKey, TValue> me, Dictionary<TKey, TValue> merge)
        {
            foreach (var item in merge)
            {
                me[item.Key] = item.Value;
            }
            return me;
        }

        public static List<T> Merge<T>(List<T> mergeInto, List<T> fromWhat)
        {
            foreach (T fromwhat in fromWhat)
            {
                mergeInto.Add(fromwhat);
            }
            return mergeInto;
        }

        public bool testColorEquality(Color one, Color two)
        {
            return ((one.r == two.r) && (one.g == two.g) && (one.b == two.b));
        }

        public const bool debugAllowed = true;

        public static void DebuggingOnlyLog(string n)
        {
            if (debugAllowed)
            {
                Debug.Log(n);
            }
        }

        public static void MLog(string n, string sourceName)
        {
            if (debugAllowed)
            {
                Debug.Log("[" + sourceName + "]" + n);
            }
        }

        public static void MLog(string n, UnityEngine.Object sourceObj)
        {
            if (debugAllowed)
            {
                Debug.Log("[" + sourceObj.name + "]" + n);
            }
        }

        public static Color multNoA(Color k, Color v)
        {
            return new Color(k.r * v.r, k.g * v.g, k.b * v.b);
        }

        public static Color multNoA(float k, Color v)
        {
            return new Color(k * v.r, k * v.g, k * v.b);
        }

        public static Color multNoA(Color k, float v)
        {
            return multNoA(v, k);
        }

        public static void D(object n)
        {
            if (debugAllowed)
            {
                Debug.Log(n);
            }
        }

        public static object GetObjectOfTypeFromString(Type type, string value)
        {
            if (type == typeof(string))
            {
                return value;
            }
            else if (type == typeof(char))
            {
                return value[0];
            }
            else if (type == typeof(int))
            {
                int k = 0;
                int.TryParse(value, out k);
                return k;
            }
            else if (type == typeof(float))
            {
                float k = 0.0f;
                float.TryParse(value, out k);
                return k;
            }
            else if (type == typeof(double))
            {
                double k = 0.0;
                double.TryParse(value, out k);
                return k;
            }
            return null;
        }
    }

    /// <summary>
    /// Special class that can be used to roll a random value and then
    /// get something out of it
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HLSNRandomable<T>
    {
        public Dictionary<T, float> chanceTable = new Dictionary<T, float>();
        System.Random rnd = HLSNUtil.GetRandom();



        public T GetRandom(bool deleteAfter)
        {
            T result = default(T);
            double rolled = rnd.NextDouble() * GetAllElementSum();
            foreach (KeyValuePair<T, float> i in chanceTable)
            {
                if (i.Value > rolled)
                {
                    result = i.Key;
                    break;
                }
                else
                {
                    rolled -= i.Value;
                }
            }

            if (deleteAfter)
            {
                chanceTable.Remove(result);
            }
            return result;
        }

        public void ChangeRNG(System.Random kk)
        {
            rnd = kk;
        }

        public float GetAllElementSum()
        {
            float result = 0.0f;
            foreach (KeyValuePair<T, float> i in chanceTable)
            {
                result += i.Value;
            }
            return result;
        }
    }

    /// <summary>
    /// Class for identifying which routines should be kept active.
    /// </summary>
    public class HLSNIdentityAsync
    {
        /// <summary>
        /// Insert a number, then pass it to a routine so that it initiates only these routines once they're in the list
        /// </summary>
        public List<ulong> identities = new List<ulong>();

        public static ulong GetRandomIdentity()
        {
            byte[] bytes = new byte[64];
            HLSNUtil.rngCsp.GetBytes(bytes);

            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}
