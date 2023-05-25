using CodeForPresentation_2;
using System.Diagnostics;
using System.Text;

internal class Program
{
    public static string GenerateRandomString(int length)
    {
        var builder = new StringBuilder();
        var random = new Random();
        for (int i = 0; i < length; i++)
        {
            var symbol = (char)random.Next(97,123);
            builder.Append(symbol);
        }
        return builder.ToString();
    }
    public static bool DoRandomTest(int length1, int length2)
    {
        var tasker = new Tasker();
        var time = new Stopwatch();
        var source = GenerateRandomString(length1);
        var pattern = GenerateRandomString(length2);
        var sourceArr = source.ToArray();
        var patternArr = pattern.ToArray();
        time.Start();
        var result1 = Tasker.IndexOf(sourceArr, patternArr);
        time.Stop();
        Console.WriteLine(time.ElapsedMilliseconds);
        time.Reset();
        time.Start();
        var result2 = source.IndexOf(pattern);
        time.Stop();
        Console.WriteLine(time.ElapsedMilliseconds);
        if (result1 != result2)
        { 
            Console.WriteLine(source);
            Console.WriteLine(pattern);
            Console.WriteLine(result1);
            Console.WriteLine(result2);
        }
        return result1 == result2;
    }
    private static void Main(string[] args)
    {
        bool isCorrect = true;
        for (int i = 0; i < 10000; i++)
        {
            var random1 = new Random().Next(1,2500000);
            var random2 = new Random().Next(1,random1);
            if(!DoRandomTest(random1,random2))
            {
                Console.WriteLine(false);
                isCorrect = false;
            }
        }
        if (isCorrect) Console.WriteLine(true);
    }
}