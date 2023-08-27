// See https://aka.ms/new-console-template for more information
using TruitjesBL.Model;

Console.WriteLine("Hello, World!");
Klant klant = new Klant(10,"naam","adres1");
try
{
    klant.VoegBestellingToe(null);
}
catch (Exception ex) { Console.WriteLine(ex.ToString()); }
Console.WriteLine("end");

