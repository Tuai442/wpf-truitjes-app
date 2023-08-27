// See https://aka.ms/new-console-template for more information
using TruitjesBL.Model;
using TruitjesDL;
using TruitjesDL.Repositories;

Console.WriteLine("Hello, World!");
string connectionString = @"Data Source=NB21-6CDPYD3\SQLEXPRESS;Initial Catalog=Truitjes_woensdag;Integrated Security=True";
//ClubRepositoryADO repo = new ClubRepositoryADO("2022-2023", connectionString);
//var res=repo.GeefCompetities();
//foreach(var item in res)
//{
//   Console.WriteLine(item);
//    var clubs = repo.GeefClubs(item);
//    foreach (var club in clubs)
//        Console.WriteLine("   "+club);
//}
TruitjeRepositoryADO tRepo=new TruitjeRepositoryADO(connectionString);
//int id=tRepo.ZoekClubId("2022-2023", "pro league", "KAA Gent");
//Console.WriteLine(id);
Truitje t = new Truitje(85.0, "2022-2023", MaatTruitje.S, new Club("pro league", "KAA Gent"), new ClubSet(true, "22 europees"));
//tRepo.VoegTruitjeToe(t);
//Console.WriteLine(t.TruitjeId);
KlantRepositoryADO kRepo=new KlantRepositoryADO(connectionString);
var k=kRepo.GeefKlant(1);
Console.WriteLine(k);

