namespace KrysztalowaKreda;

public static class DataAccess
{
    public static void ReadCsv(out IEnumerable<string[]> it, out IEnumerable<string[]> elektro, out IEnumerable<string[]> inz,out IEnumerable<string[]> air,out IEnumerable<string[]> mtm)
    {
        string path = Directory.GetCurrentDirectory() + "\\wyniki.csv";
        // Console.WriteLine(path);
        var file = File.ReadLines(path);
        bool firstLine = true;
        int lineCount = 1;
        List<string[]> itFetchedData = new List<string[]>();
        List<string[]> elektroFetchedData = new List<string[]>();
        List<string[]> inzFetchedData = new List<string[]>();
        List<string[]> airFetchedData = new List<string[]>();
        List<string[]> mtmFetchedData = new List<string[]>();
        foreach (var line in file)
        {
            
            // Skip first line
            if (firstLine)
            {
                firstLine = false;
                continue;
            }

            lineCount++;
            if (string.IsNullOrEmpty(line) || line == "")
            {
                continue;
            }

            string[] splittedString = line.Split(",");
            string[] personalData = new string[3];
            personalData[0] = splittedString[1].Trim();
            personalData[1] = splittedString[2].Trim();
            // Console.WriteLine(personalData[0]);
            
            if (!FieldEnum.TryParse(personalData[0].Split(" ")[0], out FieldEnum fieldEnum))
            {
                Console.WriteLine($"Skipping line {lineCount} -- Cant parse string {personalData[0].Split(" ")[0].Trim()} to FieldEnum data type");
                continue;
                // throw new Exception("Something went wrong with parse to FieldEnum! Check input data");
            }

            switch (fieldEnum)
            {
                case FieldEnum.Automatyka:
                    personalData[2] = splittedString[3].Trim();
                    airFetchedData.Add(personalData);
                    break;
                case FieldEnum.Elektrotechnika:
                    personalData[2] = splittedString[4].Trim();
                    elektroFetchedData.Add(personalData);
                    break;
                case FieldEnum.Informatyka:
                    personalData[2] = splittedString[5].Trim();
                    itFetchedData.Add(personalData);
                    break;
                case FieldEnum.Inżynieria:
                    personalData[2] = splittedString[6].Trim();
                    inzFetchedData.Add(personalData);
                    break;
                case FieldEnum.Mikroelektronika:
                    personalData[2] = splittedString[7].Trim();
                    mtmFetchedData.Add(personalData);
                    break;
            }
            // Console.WriteLine($"{personalData[0]} -- {personalData[1]} -- {personalData[2]}");
        }

        it = itFetchedData;
        air = airFetchedData;
        mtm = mtmFetchedData;
        inz = inzFetchedData;
        elektro = elektroFetchedData;
    }
}