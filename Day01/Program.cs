using System.Diagnostics;
using System.Text;

if (args.Length != 1)
{
    Console.WriteLine("Expecting path to input file...");
    return;
}

var sw = new Stopwatch();
sw.Start();

// Part1();
Part2();

sw.Stop();
Console.WriteLine($"Elapsed: {sw.Elapsed.TotalMilliseconds}ms");
return;

void Part2()
{
    var mappings = new Dictionary<string, char>
    {
        { "one", '1' },
        { "eno", '1' },

        { "two", '2' },
        { "owt", '2' },

        { "three", '3' },
        { "eerht", '3' },

        { "four", '4' },
        { "ruof", '4' },

        { "five", '5' },
        { "evif", '5' },

        { "six", '6' },
        { "xis", '6' },

        { "seven", '7' },
        { "neves", '7' },

        { "eight", '8' },
        { "thgie", '8' },

        { "nine", '9' },
        { "enin", '9' },
    };

    var longestKeyLength = mappings.Max(pair => pair.Key.Length);
    var sb = new StringBuilder(longestKeyLength);
    var d1 = '\0';
    var d2 = '\0';
    var sum = 0;

    foreach (ReadOnlySpan<char> line in File.ReadAllLines(args[0]))
    {
        for (var i = 0; i < line.Length; i++)
        {
            sb.Length = 0;
            for (var j = i; j < line.Length; j++)
            {
                if (sb.Length == longestKeyLength)
                {
                    break;
                }

                var c = line[j];
                if (sb.Length == 0 && char.IsDigit(c))
                {
                    d1 = c;
                    goto FROM_END;
                }

                sb.Append(c);
                if (mappings.TryGetValue(sb.ToString(), out var value))
                {
                    d1 = value;
                    goto FROM_END;
                }
            }
        }

        FROM_END:
        for (var i = line.Length - 1; i > -1; i--)
        {
            sb.Length = 0;
            for (var j = i; j > -1; j--)
            {
                if (sb.Length == longestKeyLength)
                {
                    break;
                }

                var c = line[j];
                if (sb.Length == 0 && char.IsDigit(c))
                {
                    d2 = c;
                    goto END;
                }

                sb.Append(c);
                if (mappings.TryGetValue(sb.ToString(), out var value))
                {
                    d2 = value;
                    goto END;
                }
            }
        }

        END:
        sum += (d1 - '0') * 10 + (d2 - '0');
    }

    Console.WriteLine(sum);
}

void Part1()
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

    Console.WriteLine(sum);
}