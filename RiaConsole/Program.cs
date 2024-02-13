class ATMCombinationCalculator
{
    static void Main(string[] args)
    {
        int[] denominations = new int[] { 10, 50, 100 };
        int[] amounts = new int[] { 30, 50, 60, 80, 140, 230, 370, 610, 980 };

        foreach (var amount in amounts)
        {
            Console.WriteLine($"Combinations to pay out {amount} EUR:");
            FindCombinations(denominations, amount);
            Console.WriteLine();
        }
    }

    static void FindCombinations(int[] denominations, int amount)
    {
        List<int> combination = new List<int>();
        FindCombinationsRecursive(denominations, amount, 0, combination);
    }

    static void FindCombinationsRecursive(int[] denominations, int amount, int index, List<int> combination)
    {
        if (amount == 0)
        {
            PrintCombination(combination);
            return;
        }

        if (amount < 0 || index == denominations.Length)
        {
            return;
        }

        // Choose the denomination at current index
        combination.Add(denominations[index]);
        FindCombinationsRecursive(denominations, amount - denominations[index], index, combination);

        // Do not choose the denomination at current index
        combination.RemoveAt(combination.Count - 1);
        FindCombinationsRecursive(denominations, amount, index + 1, combination);
    }

    static void PrintCombination(List<int> combination)
    {
        Dictionary<int, int> denominationCount = new Dictionary<int, int>();
        foreach (var val in combination)
        {
            if (denominationCount.ContainsKey(val))
            {
                denominationCount[val]++;
            }
            else
            {
                denominationCount[val] = 1;
            }
        }

        foreach (var kvp in denominationCount)
        {
            Console.Write($"{kvp.Value} x {kvp.Key} EUR ");
        }
        Console.WriteLine();
    }
}
