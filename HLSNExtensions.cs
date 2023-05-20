using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Hellession
{
    /// <summary>
    /// This class contains extensions for improving classes
    /// </summary>
    public static class HellessionExtensions
    {
        /// <summary>
        /// Returns all FIRST indices of each occurrence of occurrence in this string.
        /// </summary>
        public static IEnumerable<int> AllIndicesOf(this string str, string occurrence)
        {
            List<int> returnVal = new List<int>();
            string deprecableStr = str;
            while (deprecableStr.IndexOf(occurrence) != -1)
            {
                string totalSubstringOfOccurr = deprecableStr.Substring(deprecableStr.IndexOf(occurrence));
                returnVal.Add(str.IndexOf(totalSubstringOfOccurr));
                deprecableStr = str.Remove(0, str.IndexOf(totalSubstringOfOccurr) + 1);
            }
            return returnVal;
        }


        /// <summary>
        /// Removes all items in deductibles from sourceList, if they exist in sourceList.
        /// </summary>
        /// <remarks>ALL occurrences of items in deductibles are removed, NOT just the first.</remarks>
        public static List<T> RemoveEnumerable<T>(this List<T> sourceList, IEnumerable<T> deductibles)
        {
            //HOW DOES THIS GET MODIFIED???????? WHAT ARE YOU TALKING ABOUT YOU STUPID IDIOTIC SHIT.
            // THERE IS NOTHING HERE THAT EVEN MODIFIES THIS. WHAT CRACK POT ARE YOU SMOKING???
            //foreach(var d in deductibles)
            //{
            for (int i = deductibles.Count() - 1; i >= 0; i--)
            {
                //I completely fail to understand why this has to be done. This is utterly and definitely retarded.
                //sourceList is NOT deductibles. They have no relation. Yes removing from sourceList somehow causes a "Collection was modified error". Super dumb.
                var d = deductibles.ElementAt(i);
                //this feels super mega crusty
                while (sourceList.Remove(d)) { }
            }
            return sourceList;
        }

        /// <summary>
        /// Attempts looking for and giving you the value of key, but if it doesn't exist it returns ifNoneExists
        /// </summary>
        public static V TryGetting<K, V>(this Dictionary<K, V> source, K key, V ifNoneExists = default)
        {
            return source.ContainsKey(key) ? source[key] : ifNoneExists;
        }

        /// <summary>
        /// Attempts to get the key of value
        /// </summary>
        public static K KeyOf<K, V>(this Dictionary<K, V> source, V value, K ifNotFound = default)
        {
            K result = ifNotFound;
            foreach (var kv in source)
                if (EqualityComparer<V>.Default.Equals(kv.Value, value))
                {
                    result = kv.Key;
                    break;
                }
            return result;
        }

        public static string CapitalizeEveryWord(this string str)
        {
            var breakItDown = str.Split(' ', '\n');
            List<string> newWords = new List<string>();
            foreach (var word in breakItDown)
            {
                if (word.Length > 2)
                {
                    //MASSIVE AIDS RIGHT HERE LOL
                    var character = word[0].ToString().ToUpper()[0];
                    newWords.Add(character + word.Substring(1));
                }
            }
            return string.Join(" ", newWords);
        }

        public static string TurnToChangeString(this double num)
        {
            if (num > 0)
                return "+" + num;
            return num.ToString();
        }

        public static string TurnToChangeString(this int num)
        {
            if (num > 0)
                return "+" + num;
            return num.ToString();
        }

        public static string TurnToChangeString(this float num)
        {
            if (num > 0)
                return "+" + num;
            return num.ToString();
        }

        /// <summary>
        /// Returns all FIRST indices of each occurrence of occurrence in this string.
        /// </summary>
        public static IEnumerable<int> AllIndicesOf(this string str, char occurrence)
        {
            List<int> returnVal = new List<int>();
            string deprecableStr = str;
            while (deprecableStr.IndexOf(occurrence) != -1)
            {
                string totalSubstringOfOccurr = deprecableStr.Substring(deprecableStr.IndexOf(occurrence));
                returnVal.Add(str.IndexOf(totalSubstringOfOccurr));
                deprecableStr = str.Remove(0, str.IndexOf(totalSubstringOfOccurr) + 1);
            }
            return returnVal;
        }

        /// <summary>
        /// Tells whether the enumerable is empty
        /// </summary>
        /// <returns>True only if the enumerable is empty</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> array)
        {
            if (array.Count() == 0)
                return true;
            return false;
        }

        /// <summary>
        /// This is basically GetRange() but with a condom. It may return a list with less than 'count' items.
        /// </summary>
        public static List<T> GetRangeProtected<T>(this List<T> list, int index, int count)
        {
            if (list.Count == 0)
                return list;
            //Simply check if the arguments end up requesting the end of the table and would normally exceed the total amount of items and reduce the count.
            if (index + count + 1 > list.Count)
                count = list.Count - index;

            return list.GetRange(index, count);
        }


        /// <summary>
        /// Randomizes the order of all items on the enumerable.
        /// </summary>
        /// <param name="list">The enumerable to randomize</param>
        /// <returns>A new enumerable of items that has the order randomized.</returns>
        /// <remarks>The enumerable passed as an argument is not affect at all. The return value is a shallow copy.</remarks>
        public static IEnumerable<T> RandomizeArray<T>(this IEnumerable<T> list)
        {
            return from g in list orderby HLSNUtil.GetRandomDouble() select g;
        }

        /// <summary>
        /// Picks a random item out of this array.
        /// </summary>
        public static T RandomItem<T>(this IEnumerable<T> list)
        {
            if (list == null)
                throw new ArgumentOutOfRangeException("The enumerable is null. (How were you able to call this extension method?)");
            if (list.Count() == 0)
                throw new ArgumentOutOfRangeException("Cannot pick a random item out of this enumerable, as there are no items in it at all.");
            if (list.Count() == 1)
                return list.ElementAt(0);
            return list.ElementAt(HLSNUtil.GetRandomInt(0, list.Count() - 1));
        }
    }
}
