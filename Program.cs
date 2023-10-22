using System;
using System.Collections.Generic;
using System.Text;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 3, 50, 75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            // Question 2:
            Console.WriteLine("Question 2");
            string parenthesis = "()[]{}";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 1, 5, 3, 6, 4 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string s1 = "88";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();

            //Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 2, 3, 1, 1, 3 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3, 2, 1 };
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            // Question 7:
            Console.WriteLine("Question 7:");
            string currentState = "++++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();

            //Question 8:
            Console.WriteLine("Question 8:");
            string longString = "leetcodeisacommunityforcoders";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            // Initialize a list to store the missing ranges
            List<IList<int>> missingRanges = new List<IList<int>>();

            // Handle the case where 'nums' is empty
            if (nums.Length == 0)
            {
                // Check if lower is equal to upper; if yes, there are no missing ranges
                if (lower == upper)
                {
                    return missingRanges;
                }

                // Otherwise, there is a single missing range from lower to upper
                missingRanges.Add(new List<int> { lower, upper });
                return missingRanges;
            }

            for (int i = 0; i < nums.Length; i++)
            {
                // Check if the current number is equal to or less than the lower bound
                if (nums[i] <= lower)
                {
                    // Increment the lower bound
                    lower = nums[i] + 1;
                }
                else
                {
                    // Calculate the missing range
                    if (nums[i] - 1 > lower)
                    {
                        missingRanges.Add(new List<int> { lower, nums[i] - 1 });
                    }

                    // Update the lower bound
                    lower = nums[i] + 1;
                }

                // If we have reached the last element of 'nums', check for a missing range between the last element and the upper bound
                if (i == nums.Length - 1 && nums[i] < upper)
                {
                    missingRanges.Add(new List<int> { nums[i] + 1, upper });
                }
            }

            return missingRanges;
        }
        public static bool IsValid(string s)
        {
            try
            {
                // Check for the case where the string is empty; it's considered valid
                if (string.IsNullOrEmpty(s))
                {
                    return true;
                }

                // Initialize a stack to keep track of open brackets
                Stack<char> stack = new Stack<char>();

                // Define a dictionary to map open and close brackets
                Dictionary<char, char> bracketPairs = new Dictionary<char, char>
                {
                    { '(', ')' },
                    { '{', '}' },
                    { '[', ']' }
                };

                // Iterate through each character in the string
                foreach (char c in s)
                {
                    // If the character is an open bracket, push it onto the stack
                    if (bracketPairs.ContainsKey(c))
                    {
                        stack.Push(c);
                    }
                    else
                    {
                        // If the character is a close bracket
                        if (stack.Count == 0)
                        {
                            // If there is no corresponding open bracket, return false
                            return false;
                        }

                        char openBracket = stack.Pop();

                        // Check if the close bracket matches the most recent open bracket
                        if (bracketPairs[openBracket] != c)
                        {
                            return false;
                        }
                    }
                }

                // After iterating through the string, if there are open brackets left in the stack, return false
                return stack.Count == 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static int MaxProfit(int[] prices)
        {
            try
            {
                // Check for the case where there are no prices or only one price; in such cases, no profit can be achieved
                if (prices == null || prices.Length < 2)
                {
                    return 0;
                }

                int minPrice = prices[0]; // Initialize the minimum price to the first day's price
                int maxProfit = 0; // Initialize the maximum profit to 0

                // Iterate through the prices array
                for (int i = 1; i < prices.Length; i++)
                {
                    // Check if the current price is lower than the previously seen minimum price
                    if (prices[i] < minPrice)
                    {
                        minPrice = prices[i];
                    }
                    else
                    {
                        // Calculate the profit if we sell at the current price
                        int currentProfit = prices[i] - minPrice;

                        // Update the maximum profit if the current profit is greater
                        if (currentProfit > maxProfit)
                        {
                            maxProfit = currentProfit;
                        }
                    }
                }

                return maxProfit;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool IsStrobogrammatic(string s)
                {
                    try
                    {
                        // Define a dictionary to map strobogrammatic numbers
                        Dictionary<char, char> strobogrammaticMap = new Dictionary<char, char>
                        {
                            { '0', '0' },
                            { '1', '1' },
                            { '6', '9' },
                            { '8', '8' },
                            { '9', '6' }
                        };

                        // Initialize two pointers, one at the beginning and one at the end of the string
                        int left = 0;
                        int right = s.Length - 1;

                        // Continue checking characters while the left pointer is less than or equal to the right pointer
                        while (left <= right)
                        {
                            char leftChar = s[left];
                            char rightChar = s[right];

                            // Check if both characters are in the strobogrammatic map and map to each other
                            if (strobogrammaticMap.ContainsKey(leftChar) && strobogrammaticMap[leftChar] == rightChar)
                            {
                                left++;
                                right--;
                            }
                            else
                            {
                                // If not, the string is not strobogrammatic
                                return false;
                            }
                        }

                        // If we've checked all characters and they are strobogrammatic, the string is strobogrammatic
                        return true;
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
        public static int NumIdenticalPairs(int[] nums)
        {
            try
            {
                // Create a dictionary to store the count of each number in the array
                Dictionary<int, int> countDict = new Dictionary<int, int>();

                int goodPairs = 0; // Initialize the count of good pairs

                // Iterate through the array and update the countDict
                foreach (int num in nums)
                {
                    if (countDict.ContainsKey(num))
                    {
                        // Increment the count of this number and add it to the goodPairs count
                        goodPairs += countDict[num];
                        countDict[num]++;
                    }
                    else
                    {
                        countDict[num] = 1;
                    }
                }

                return goodPairs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ThirdMax(int[] nums)
        {
            try
            {
                // Use a HashSet to store distinct numbers
                HashSet<int> distinctNums = new HashSet<int>();

                foreach (int num in nums)
                {
                    distinctNums.Add(num);

                    // If the HashSet size exceeds 3, remove the minimum element
                    if (distinctNums.Count > 3)
                    {
                        distinctNums.Remove(distinctNums.Min());
                    }
                }

                // If the distinctNums set has less than 3 elements, return the maximum number
                if (distinctNums.Count < 3)
                {
                    return distinctNums.Max();
                }

                // Otherwise, return the third maximum number
                return distinctNums.Min();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            try
            {
                List<string> nextStates = new List<string>();

                // Iterate through the string, checking for consecutive "++"
                for (int i = 0; i < currentState.Length - 1; i++)
                {
                    if (currentState[i] == '+' && currentState[i + 1] == '+')
                    {
                        // Create a new string with "++" replaced by "--"
                        StringBuilder nextState = new StringBuilder(currentState);
                        nextState[i] = '-';
                        nextState[i + 1] = '-';
                        nextStates.Add(nextState.ToString());
                    }
                }

                return nextStates;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string RemoveVowels(string s)
        {
            try
            {
                StringBuilder result = new StringBuilder();

                // Iterate through the characters in the string
                foreach (char c in s)
                {
                    // Check if the character is not a vowel
                    if (c != 'a' && c != 'e' && c != 'i' && c != 'o' && c != 'u')
                    {
                        result.Append(c); // Append the character to the result
                    }
                }

                return result.ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }
        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }

        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }

    


    }
}
