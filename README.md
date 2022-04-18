# KrysztalowaPointsCounter

This program is created for WRSS WEAIiIB for "Kryształowa Kreda" plebiscite. This program have no use outside WRSS organization. It counts points for persons using CSV data source exported from google forms.

It is written using MultiThreading. If you don't want to use multiple threads go to Program.cs and change the line of code calling CalculateWithMultiThreading() method to Calculate() 

## Installation
In the project file in Powershell type this command to build the project: 

```bash
dotnet build KrysztalowaKreda.csproj
```
After this go to the directory:

[ProjectFile]\bin\Debug\net6.0

And paste here a CSV file named wyniki.csv

After this run program: KrysztlowaKreda.Exe and then open Result.txt
