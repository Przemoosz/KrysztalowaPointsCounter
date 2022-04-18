using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KrysztalowaKreda;

namespace KredaTests;

[TestClass]
public class PointCalculateTest
{
    
    [TestMethod]
    public void Testing_Single_Data()
    {
        // Testing Dict result after adding one data
        
        // Arrange Section
        KrysztalowaKredaSolution solution = KrysztalowaKredaSolution.GetInstance();
        List<string[]> testInputData = new List<string[]>(1) {new string[3]{"Informatyka", "III rok", "Aleksandra Krzyżanowska"} };
        Dictionary<string, decimal> resultDictionary = new Dictionary<string, decimal>(1);
        
        // Act Section
        solution.PointsCalculate(testInputData, ref resultDictionary);
        
        // Assert Section
        Assert.AreEqual(3,resultDictionary["Aleksandra Krzyżanowska"]);
    }

    [TestMethod]
    public void Testing_Multiple_Data()
    {
        // Testing Dict result after adding data with year from I to V
        
        // Arrange Section
        KrysztalowaKredaSolution solution = KrysztalowaKredaSolution.GetInstance();
        List<string[]> testInputData = new List<string[]>(1)
        {
            new string[3]{"Informatyka", "I rok", "Aleksandra Krzyżanowska"},
            new string[3]{"Informatyka", "II rok", "Aleksandra Krzyżanowska"},
            new string[3]{"Informatyka", "III rok", "Aleksandra Krzyżanowska"},
            new string[3]{"Informatyka", "IV rok", "Aleksandra Krzyżanowska"},
            new string[3]{"Informatyka", "V rok", "Aleksandra Krzyżanowska"},
        };
        Dictionary<string, decimal> resultDictionary = new Dictionary<string, decimal>(1);
        
        // Act Section
        solution.PointsCalculate(testInputData, ref resultDictionary);
        
        // Assert Section
        Assert.AreEqual(16,resultDictionary["Aleksandra Krzyżanowska"]);
    }
    [TestMethod]
    public void Testing_Different_Keys()
    {
        // Testing Dict result after adding data with year from I to V, each data have different key
        
        // Arrange Section
        KrysztalowaKredaSolution solution = KrysztalowaKredaSolution.GetInstance();
        List<string[]> testInputData = new List<string[]>(1)
        {
            new string[3]{"Informatyka", "I rok", "A"},
            new string[3]{"Informatyka", "II rok", "B"},
            new string[3]{"Informatyka", "III rok", "C"},
            new string[3]{"Informatyka", "IV rok", "D"},
            new string[3]{"Informatyka", "V rok", "E"},
        };
        Dictionary<string, decimal> resultDictionary = new Dictionary<string, decimal>(1);
        
        // Act Section
        solution.PointsCalculate(testInputData, ref resultDictionary);
        
        // Assert Section
        Assert.AreEqual(1,resultDictionary["A"]);
        Assert.AreEqual(2,resultDictionary["B"]);
        Assert.AreEqual(3,resultDictionary["C"]);
        Assert.AreEqual(4.5m,resultDictionary["D"]);
        Assert.AreEqual(5.5m,resultDictionary["E"]);
    }
}