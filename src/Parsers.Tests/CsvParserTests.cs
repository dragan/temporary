using Should;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

public class CsvParserTests
{
    string test_string;

    public CsvParserTests ()
    {
        test_string = "field1,field2,field3\r\n" + 
                      "\"aaa\r\n\",\"bb,b\",\"ccc\"\r\n" + 
                      "\"in \"\"quotes\"\"\",2,3\r\n" + 
                      "1,2,\r\n" + 
                      "zzz,yyy,xxx\r\n" + 
                      "1,,3\r\n" + 
                      ",,";
    }

    public void should_contain_seven_lines ()
    {
        Parse (test_string).Count.ShouldEqual (7);
    }

    public static List<string> Parse (string input)
    {
        var output = new List<string> ();

        foreach (string line in Regex.Split (input, @"\r\n(?=(?:[^""]*""[^""]*"")*[^""]*$)")) {
            output.Add (line);
        }

        return output;
    }
}
