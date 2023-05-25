using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace CodeForPresentation_2
{
    public class Tasker
    {
        public static int IndexOf(char[] source, char[] pattern) 
        {
            if (source == null || pattern == null) throw new NullReferenceException();
            if (source.Length < pattern.Length) return -1;
            if (pattern.Length == 0) return 0;
            var charTable = MakeCharTable(pattern); 
            var offsetTable = MakeSuffixTable(pattern);
            for (int i = pattern.Length - 1, j; i < source.Length;) 
            { 
                for (j = pattern.Length - 1; pattern[j] == source[i]; i--, j--) 
                { 
                    if (j == 0) 
                    {
                        return i; 
                    } 
                } 
                i += Math.Max(offsetTable[pattern.Length - 1 - j], charTable[source[i]]); 
            } 
            return -1;
        }
        private static int[] MakeCharTable(char[] pattern) 
        { 
            int ALPHABET_SIZE = Char.MaxValue + 1;
            int[] table = new int[ALPHABET_SIZE]; 
            for (int i = 0; i < table.Length; ++i) 
            { 
                table[i] = pattern.Length; 
            }
            for (int i = 0; i < pattern.Length - 1; ++i) 
            {
                table[pattern[i]] = pattern.Length - 1 - i; 
            } 
            return table; 
        }  
        private static int[] MakeSuffixTable(char[] pattern) 
        { 
            var table = new int[pattern.Length]; 
            var lastPrefixPosition = pattern.Length;
            for (int i = pattern.Length; i > 0; --i) 
            { 
                if (IsPrefix(pattern, i))
                { 
                    lastPrefixPosition = i;
                } 
                table[pattern.Length - i] = lastPrefixPosition - i + pattern.Length;
            } for (int i = 0; i < pattern.Length - 1; ++i) 
            { 
                var slen = SuffixLength(pattern, i); table[slen] = pattern.Length - 1 - i + slen; 
            } 
            return table;
        }
        private static bool IsPrefix(char[] pattern, int index) 
        { 
            for (int i = index, j = 0; i < pattern.Length; i++, j++)
            { 
                if (pattern[i] != pattern[j]) 
                { 
                    return false; 
                } 
            } 
            return true;
        } 
        private static int SuffixLength(char[] needle, int index) 
        { 
            var length = 0; 
            for (int i = index, j = needle.Length - 1; i >= 0 && needle[i] == needle[j]; i--, j--) 
            { 
                length += 1; 
            }
            return length; 
        } 
    }
}


