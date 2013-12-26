using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class CsvParserTests
{
    readonly string test_string;
    readonly string[][] expected_output;

    public CsvParserTests ()
    {
        test_string = "field1,field2,field3\r\n" + 
                      "\"aaa\r\n\",\"bb,b\",\"ccc\"\r\n" + 
                      "\"in \"\"quotes\"\"\",2,3\r\n" + 
                      "1,2,\r\n" + 
                      "zzz,yyy,xxx\r\n" + 
                      "1,,3\r\n" + 
                      ",,";

        expected_output = new string[][] { 
            new [] { "field1", "field2", "field3" },
            new [] { "aaa\r\n", "bb,b", "ccc" },
            new [] { "in \"quotes\"", "2", "3" },
            new [] { "1", "2", "" },
            new [] { "zzz", "yyy", "xxx" },
            new [] { "1", "", "3" },
            new [] { "", "", "" }
        };
    }

    public void should_contain_seven_lines ()
    {
        Parse (test_string).Count.ShouldEqual (expected_output.Length);
    }

    public void each_line_should_contain_three_columns ()
    {
        var expectedFieldCount = 3;
        var output = Parse (test_string);

        foreach (var line in output) {
            line.Count.ShouldEqual (expectedFieldCount);
        }
    }

    public void each_line_should_contain_expected_field ()
    {
        var output = Parse (test_string);

        for (int i = 0; i < expected_output.Length; i++) {
            for (int j = 0; j < expected_output[i].Length; j++) {
                output [i] [j].ShouldEqual (expected_output [i] [j]);
            }
        }
    }

    public static List<List<string>> Parse (string input)
    {
        var escapedPattern = @"(^"")|(""$)";
        var escapedRegex = new Regex (escapedPattern);

        var output = new List<List<string>> ();

        foreach (string line in Regex.Split (input, @"\r\n(?=(?:[^""]*""[^""]*"")*[^""]*$)")) {

            var fields = new List<string> ();

            foreach (string field in Regex.Split (line, @"\s*,\s*(?=(?:[^""]*""[^""]*"")*[^""]*$)")) {
                fields.Add (escapedRegex.Replace (field, ""));
            }

            output.Add (fields);
        }

        return output;
    }
}
