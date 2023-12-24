using System.Diagnostics;
using System.Text;

if (args.Length != 1)
{
    Console.WriteLine("Expecting path to input file...");
    return;
}

var sw = new Stopwatch();
sw.Start();

// var result = Part1();
var result = Part2();

sw.Stop();
Console.WriteLine($"Result: {result}; Elapsed: {sw.Elapsed.TotalMilliseconds}ms");
return;

int Part2()
{
    var numbers = new[] { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
    var sum = 0;

    foreach (var line in File.ReadAllLines(args[0]))
    {
        var d1 = '\0';
        var d2 = '\0';

        for (var i = 0; i < line.Length; i++)
        {
            var span = line.AsSpan(i, line.Length - i);
            if (char.IsDigit(span[0]))
            {
                d1 = span[0];
                goto FROM_END;
            }

            for (var j = 0; j < numbers.Length; j++)
            {
                if (span.StartsWith(numbers[j]))
                {
                    d1 = (char)(j + 1 + '0');
                    goto FROM_END;
                }
            }
        }

        FROM_END:
        for (var i = 0; i < line.Length; i++)
        {
            var span = line.AsSpan(0, line.Length - i);
            if (char.IsDigit(span[^1]))
            {
                d2 = span[^1];
                goto END;
            }

            for (var j = 0; j < numbers.Length; j++)
            {
                if (span.EndsWith(numbers[j]))
                {
                    d2 = (char)(j + 1 + '0');
                    goto END;
                }
            }
        }

        END:
        sum += (d1 - '0') * 10 + (d2 - '0');
    }

    return sum;
}

int Part1()
{
    var sum = 0;
    var sb = new StringBuilder();

    foreach (ReadOnlySpan<char> line in File.ReadAllLines(args[0]))
    {
        for (var i = 0; i < line.Length; i++)
        {
            var c = line[i];
            if (char.IsDigit(c))
            {
                sb.Append(c);
                break;
            }
        }

        for (var i = line.Length - 1; i > -1; i--)
        {
            var c = line[i];
            if (char.IsDigit(c))
            {
                sb.Append(c);
                break;
            }
        }

        sum += int.Parse(sb.ToString());
        sb.Length = 0;
    }

    return sum;
}