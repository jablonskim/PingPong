using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PingPong;
using System.IO;
using System.Collections.Generic;

namespace CorrectnessTests
{
    [TestClass]
    public class CorrectnessTest
    {
        [TestMethod]
        public void Test6()
        {
            const string input = @"../../TestFiles/test6.txt";
            const string output = @"../../TestFiles/TESTING_res6.txt";
            const string expectedOutput = @"../../TestFiles/res6.txt";

            CheckResultEquivalence(input, output, expectedOutput);
        }

        [TestMethod]
        public void Test10()
        {
            const string input = @"../../TestFiles/test10.txt";
            const string output = @"../../TestFiles/TESTING_res10.txt";
            const string expectedOutput = @"../../TestFiles/res10.txt";

            CheckResultEquivalence(input, output, expectedOutput);
        }

        [TestMethod]
        public void Test12()
        {
            const string input = @"../../TestFiles/test12.txt";
            const string output = @"../../TestFiles/TESTING_res12.txt";
            const string expectedOutput = @"../../TestFiles/res12.txt";

            CheckResultEquivalence(input, output, expectedOutput);
        }

        [TestMethod]
        public void Test15()
        {
            const string input = @"../../TestFiles/test15.txt";
            const string output = @"../../TestFiles/TESTING_res15.txt";
            const string expectedOutput = @"../../TestFiles/res15.txt";

            CheckResultEquivalence(input, output, expectedOutput);
        }

        private static void CheckResultEquivalence(string input, string output, string expectedOutput)
        {
            PingPongSolver pingpong = new PingPongSolver(input, output);
            pingpong.Run();

            List<int> expected = new List<int>();
            List<int> actual = new List<int>();
            try
            {
                using (StreamReader sr = new StreamReader(expectedOutput))
                {
                    String line = sr.ReadToEnd();
                    string[] splits = line.Split(',');
                    for (int i = 0; i < splits.Length - 1; i++)
                    {
                        expected.Add(int.Parse(splits[i]));
                    }
                }
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            try
            {
                using (StreamReader sr = new StreamReader(output))
                {
                    String line = sr.ReadToEnd();
                    string[] splits = line.Split(',');
                    for (int i = 0; i < splits.Length - 1; i++)
                    {
                        actual.Add(int.Parse(splits[i]));
                    }
                }
            }
            catch (Exception e)
            {
                Assert.Fail();
            }

            CollectionAssert.AreEquivalent(expected, actual);
            File.Delete(output);
        }
    }
}
