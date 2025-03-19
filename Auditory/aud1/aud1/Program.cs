// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

string test1 = "ABC";
string test2 = "ABC";

Console.WriteLine(test1.Equals(test2));

//deklariranje nullable podatocni tipovi
int? nullableInt = null;
Console.WriteLine(nullableInt.HasValue);

//LINQ - slicno na java streams api 

//select
var numbers = new List<int> { 1, 6, 2, 9, 10,3, 8, 4 };
var squares = numbers.Select(n => n * n).ToList(); //mapiranje na podatoci 

foreach (var square in squares)
{
    Console.WriteLine(square);
}

//where - slicno kako filter
var names = new List<string>{"david", "stefan", "marko"};

var filtered = names.Where(n => n.Contains("rk") || n.StartsWith("da")).ToList();
foreach (var name in filtered)
{
    Console.WriteLine(name);
}

var sorted_numbers = numbers.OrderBy(name => name).ToList();
Console.WriteLine(string.Join(" ", sorted_numbers));