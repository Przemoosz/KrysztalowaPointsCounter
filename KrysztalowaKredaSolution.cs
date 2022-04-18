namespace KrysztalowaKreda;

public sealed class KrysztalowaKredaSolution
{
    // Implementing Singleton design pattern 
    private static KrysztalowaKredaSolution? _solution;

    private Dictionary<string, decimal> _itResults = new Dictionary<string, decimal>();
    private Dictionary<string, decimal> _inzResults = new Dictionary<string, decimal>();
    private Dictionary<string, decimal> _elektroResults = new Dictionary<string, decimal>();
    private Dictionary<string, decimal> _mtmResults = new Dictionary<string, decimal>();
    private Dictionary<string, decimal> _airResults = new Dictionary<string, decimal>();
    
    // Change Coefficient here
    private const decimal _IYearCoefficient = 1;
    private const decimal _IIYearCoefficient = 2;
    private const decimal _IIIYearCoefficient = 3;
    private const decimal _IVYearCoefficient = 4m;
    private const decimal _VYearCoefficient = 5m;
    
    // Not used in program
    private const decimal _GraduateCoefficient = 2.5m;
    private KrysztalowaKredaSolution()
    {
        
    }

    public void ResetDict()
    {
        _itResults = new Dictionary<string, decimal>();
        _inzResults = new Dictionary<string, decimal>();
        _mtmResults = new Dictionary<string, decimal>();
        _elektroResults = new Dictionary<string, decimal>();
        _airResults = new Dictionary<string, decimal>();
    }

    public static KrysztalowaKredaSolution GetInstance()
    {
        if (_solution is null)
        {
            _solution = new KrysztalowaKredaSolution();
        }
        return _solution;
    }

    public void Calculate()
    {
        IEnumerable<string[]> inz = new List<string[]>();
        IEnumerable<string[]> air = new List<string[]>();
        IEnumerable<string[]> elektro = new List<string[]>();
        IEnumerable<string[]> mtm = new List<string[]>();
        IEnumerable<string[]> it = new List<string[]>();
        DataAccess.ReadCsv(out it, out elektro, out inz, out air, out mtm);
        PointsCalculate(it, ref _itResults);
        PointsCalculate(inz, ref _inzResults);
        PointsCalculate(elektro, ref _elektroResults);
        PointsCalculate(air, ref _airResults);
        PointsCalculate(mtm, ref _mtmResults);
        // ShowResult(ref _itResults);
    }

    public void CalculateWithMultiThreading()
    {
        // Multithreading Calculate method version 
        
        IEnumerable<string[]> inz = new List<string[]>();
        IEnumerable<string[]> air = new List<string[]>();
        IEnumerable<string[]> elektro = new List<string[]>();
        IEnumerable<string[]> mtm = new List<string[]>();
        IEnumerable<string[]> it = new List<string[]>();
        DataAccess.ReadCsv(out it, out elektro, out inz, out air, out mtm);
        Thread[] threads = new Thread[5]
        {
            new Thread(() => PointsCalculate(it, ref _itResults)),
            new Thread(() => PointsCalculate(inz, ref _inzResults)),
            new Thread(() => PointsCalculate(air, ref _airResults)),
            new Thread(() => PointsCalculate(mtm, ref _mtmResults)),
            new Thread(() => PointsCalculate(elektro, ref _elektroResults)),
        };
        foreach (Thread th in threads)
        {
            th.Start();
        }

        foreach (Thread th in threads)
        {
            th.Join();
        }
        
        // ShowResult(ref _itResults);
    }
    

    public void ShowResult(ref Dictionary<string, decimal> dict)
    {
        foreach (KeyValuePair<string,decimal> kvp in dict)
        {
            Console.WriteLine($"{kvp.Key} -- {kvp.Value}");
        }
    }

    public void PointsCalculate(IEnumerable<string[]> dataSet, ref Dictionary<string, decimal> resultDict)
    {
        
        foreach (string[] data in dataSet)
        {
            decimal points = 0.0m;
            if (!YearEnum.TryParse(data[1].Split(" ")[0], out YearEnum year))
            {
                throw new Exception("Cant parse Year!");
            }

            if (!resultDict.ContainsKey(data[2]))
            {
                resultDict.Add(data[2],0.0m);
            }
            switch (year)
            {
                case YearEnum.I:
                    resultDict[data[2]] += _IYearCoefficient;
                    break;
                case YearEnum.II:
                    resultDict[data[2]] += _IIYearCoefficient;
                    break;
                case YearEnum.III:
                    resultDict[data[2]] += _IIIYearCoefficient;
                    break;
                case YearEnum.IV:
                    resultDict[data[2]] += _IVYearCoefficient;
                    break;
                case YearEnum.V:
                    resultDict[data[2]] += _VYearCoefficient;
                    break;
                case YearEnum.Absolwent:
                    resultDict[data[2]] += _GraduateCoefficient;
                    break;
            }

        }
    }

    private void OrderDict()
    {
        _airResults = _airResults.OrderByDescending(a => a.Value).ToDictionary(x => x.Key, x => x.Value);
        _elektroResults = _elektroResults.OrderByDescending(a => a.Value).ToDictionary(x => x.Key, x => x.Value);
        _mtmResults = _mtmResults.OrderByDescending(a => a.Value).ToDictionary(x => x.Key, x => x.Value);
        _inzResults = _inzResults.OrderByDescending(a => a.Value).ToDictionary(x => x.Key, x => x.Value);
        _itResults = _itResults.OrderByDescending(a => a.Value).ToDictionary(x => x.Key, x => x.Value);

    }
    public void ExportDataResult()
    {
        OrderDict();
        string path = Directory.GetCurrentDirectory() + "\\Results.txt";
        Dictionary<string, decimal> firstPlace = new Dictionary<string, decimal>(5);
        Dictionary<string, decimal> secondPlace = new Dictionary<string, decimal>(5);
        Dictionary<string, decimal> thirdPlace = new Dictionary<string, decimal>(5);

        
        using (var fileStream = File.Create(path))
        {
            using (StreamWriter streamWriter = new StreamWriter(fileStream))
            {
                bool first = true , second = false, third = false;
                // var orderedDict = _airResults.OrderBy(a => a.Value).ToDictionary(x => x.Key, x => x.Value);
                streamWriter.WriteLine("===AIR===");
                foreach (KeyValuePair<string,decimal> kvp in _airResults)
                {

                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                    if (first)
                    {
                        firstPlace.Add("AIR - " + kvp.Key,kvp.Value);
                        first = false;
                        second = true;
                    }
                    else if (second)
                    {
                        secondPlace.Add("AIR - " + kvp.Key,kvp.Value);
                        second = false;
                        third = true;
                    }
                    else if (third)
                    {
                        thirdPlace.Add("AIR - " + kvp.Key,kvp.Value);
                        third = false;
                    }
                }

                first = true;
                streamWriter.WriteLine();
                streamWriter.WriteLine("===MTM===");
                foreach (KeyValuePair<string,decimal> kvp in _mtmResults)
                {
                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                    if (first)
                    {
                        firstPlace.Add("MTM - " + kvp.Key,kvp.Value);
                        first = false;
                        second = true;
                    }
                    else if (second)
                    {
                        secondPlace.Add("MTM - " + kvp.Key,kvp.Value);
                        second = false;
                        third = true;
                    }
                    else if (third)
                    {
                        thirdPlace.Add("MTM - " + kvp.Key,kvp.Value);
                        third = false;
                    }
                }

                first = true;
                streamWriter.WriteLine();
                streamWriter.WriteLine("===ELEKTRO===");
                foreach (KeyValuePair<string,decimal> kvp in _elektroResults)
                {
                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                    if (first)
                    {
                        firstPlace.Add("ET - " + kvp.Key,kvp.Value);
                        first = false;
                        second = true;
                    }
                    else if (second)
                    {
                        secondPlace.Add("ET - " + kvp.Key,kvp.Value);
                        second = false;
                        third = true;
                    }
                    else if (third)
                    {
                        thirdPlace.Add("ET - " + kvp.Key,kvp.Value);
                        third = false;
                    }
                }

                first = true;
                streamWriter.WriteLine();
                streamWriter.WriteLine("===INZYNIERA BIOMEDYCZNA===");
                foreach (KeyValuePair<string,decimal> kvp in _inzResults)
                {
                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                    if (first)
                    {
                        firstPlace.Add("IB - " + kvp.Key,kvp.Value);
                        first = false;
                        second = true;
                    }
                    else if (second)
                    {
                        secondPlace.Add("IB - " + kvp.Key,kvp.Value);
                        second = false;
                        third = true;
                    }
                    else if (third)
                    {
                        thirdPlace.Add("IB - " + kvp.Key,kvp.Value);
                        third = false;
                    }
                }

                first = true;
                streamWriter.WriteLine();
                streamWriter.WriteLine("===IT===");
                foreach (KeyValuePair<string,decimal> kvp in _itResults)
                {
                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                    if (first)
                    {
                        firstPlace.Add("IT - " + kvp.Key,kvp.Value);
                        first = false;
                        second = true;
                    }
                    else if (second)
                    {
                        secondPlace.Add("IT - " + kvp.Key,kvp.Value);
                        second = false;
                        third = true;
                    }
                    else if (third)
                    {
                        thirdPlace.Add("IT - " + kvp.Key,kvp.Value);
                        third = false;
                    }
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine("=== I MIEJSCE ===");
                foreach (KeyValuePair<string, decimal> kvp in firstPlace)
                {
                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine("=== II MIEJSCE ===");
                foreach (KeyValuePair<string, decimal> kvp in secondPlace)
                {
                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                }
                streamWriter.WriteLine();
                streamWriter.WriteLine("=== III MIEJSCE ===");
                foreach (KeyValuePair<string, decimal> kvp in thirdPlace)
                {
                    streamWriter.WriteLine($"{kvp.Key} -- {kvp.Value}");
                }

            }
        }
    }
}