using System.Collections.Generic;
using TruitjesBL.Exceptions;
using TruitjesBL.Model;
using Xunit;

namespace UnitTestDomein
{
    public class UnitTestBestelling
    {
        private List<Bestelling> _bestellingen = new List<Bestelling>();
        private List<Truitje> _truitjes = new List<Truitje>();
        private Klant _klant;

        public UnitTestBestelling()
        {
            Truitje t1 = new Truitje(1, 45.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            Truitje t2 = new Truitje(2, 65.0, "2022-2023", MaatTruitje.M, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            Truitje t3 = new Truitje(3, 85.0, "2022-2023", MaatTruitje.XL, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            Bestelling b1 = new Bestelling();
            b1.BestellingId = 1;
            Bestelling b2 = new Bestelling();
            b2.BestellingId = 2;
            Bestelling b3 = new Bestelling();
            b3.BestellingId = 3;
            Bestelling b4 = new Bestelling();
            b4.BestellingId = 4;
            Bestelling b5 = new Bestelling();
            b5.BestellingId = 5;
            Bestelling b6 = new Bestelling();
            b6.BestellingId = 6;
            b1.VoegTruitjeToe(t1, 5);
            b2.VoegTruitjeToe(t1, 5);
            b3.VoegTruitjeToe(t1, 5);
            b4.VoegTruitjeToe(t1, 5);
            b4.VoegTruitjeToe(t3, 2);
            b5.VoegTruitjeToe(t1, 5);
            b5.VoegTruitjeToe(t2, 1);
            b6.VoegTruitjeToe(t1, 5);
            b6.VoegTruitjeToe(t2, 1);
            _klant = new Klant(1, "jos", "antwerpen");
            _truitjes.Add(t1);
            _truitjes.Add(t2);
            _truitjes.Add(t3);
            _bestellingen.Add(b1);
            _bestellingen.Add(b2);
            _bestellingen.Add(b3);
            _bestellingen.Add(b4);
            _bestellingen.Add(b5);
            _bestellingen.Add(b6);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(20)]
        public void BestellingId_Valid(int id)
        {
            //arrange
            Bestelling b = new Bestelling();
            //act
            b.BestellingId = id;
            //assert
            Assert.Equal(id, b.BestellingId);
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void BestellingId_InValid(int id)
        {
            Bestelling b = new Bestelling();
            Assert.Throws<BestellingException>(() => b.BestellingId = id);
        }
        [Fact]
        public void VoegTruitjeToe_InvalidTruitje_Exception()
        {
            Bestelling best = new Bestelling();
            Assert.Throws<BestellingException>(() => best.VoegTruitjeToe(null, 5));
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void VoegTruitjeToe_InvalidAantal_Exception(int aantal)
        {
            Bestelling best = new Bestelling();
            Truitje t = new Truitje(10,85.0,"2022-2023",MaatTruitje.S,new Club("premier league","Arsenal"),new ClubSet(false,"1"));
            Assert.Throws<BestellingException>(() => best.VoegTruitjeToe(t, aantal));
        }
        [Fact]
        public void VoegTruitjeToe_NieuwTruitje_Contains()
        {
            Bestelling best = new Bestelling();
            Truitje t = new Truitje(10, 85.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(t, 5);
            Assert.Contains(t,best.GeefTruitjes().Keys);
        }
        [Fact]
        public void VoegTruitjeToe_NieuwTruitje_ValidAantal()
        {
            Bestelling best = new Bestelling();
            //Truitje t = new Truitje(10, 85.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(_truitjes[1], 5);
            Assert.Equal(5, best.GeefTruitjes()[_truitjes[1]]);
            best.VoegTruitjeToe(_truitjes[1], 3);
            Assert.Equal(8, best.GeefTruitjes()[_truitjes[1]]);
        }
        [Fact]
        public void KostPrijs_ZonderKorting_ZonderKlant()
        {
            Bestelling best = new Bestelling();
            Truitje t1 = new Truitje(10, 85.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(t1, 5);
            Truitje t2 = new Truitje(11, 75.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(t2, 3);
            Assert.Equal(5*85+3*75, best.KostPrijs());
        }
        [Fact]
        public void KostPrijs_ZonderKorting_MetKlant()
        {
            Bestelling best = new Bestelling();
            best.BestellingId = 100;
            Truitje t1 = new Truitje(10, 85.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(t1, 5);
            Truitje t2 = new Truitje(11, 75.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(t2, 3);

            Klant k = new Klant(1, "jos", "gent henleykaai");
            best.ZetKlant(k);
            Assert.Equal(5 * 85 + 3 * 75, best.KostPrijs());
        }
        [Fact]
        public void KostPrijs_MetKorting()
        {
            Bestelling best = new Bestelling();
            best.BestellingId = 100;
            Truitje t1 = new Truitje(10, 85.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(t1, 5);
            Truitje t2 = new Truitje(11, 75.0, "2022-2023", MaatTruitje.S, new Club("premier league", "Arsenal"), new ClubSet(false, "1"));
            best.VoegTruitjeToe(t2, 3);
            Klant k = new Klant(1, "jos", "gent henleykaai");
            k.VoegBestellingToe(_bestellingen[0]);
            k.VoegBestellingToe(_bestellingen[1]);
            k.VoegBestellingToe(_bestellingen[2]);
            k.VoegBestellingToe(_bestellingen[3]);
            k.VoegBestellingToe(_bestellingen[4]);
            k.VoegBestellingToe(_bestellingen[5]);
            best.ZetKlant(k);
            Assert.Equal((5.0 * 85.0 + 3.0 * 75.0)*0.9, best.KostPrijs());
        }
        [Fact] 
        public void ZetKlant_NullValue_Exception()
        {
            Bestelling best = new Bestelling();
            Assert.Throws<BestellingException>(()=>best.ZetKlant(null));
        }
        [Fact]
        public void ZetKlant_ZelfdeKlant_Exception()
        {
            Bestelling best = new Bestelling();
            best.BestellingId = 10;
            best.ZetKlant(_klant);
            Klant klant = new Klant(1, "jos", "antwerpen");
            Assert.Throws<BestellingException>(() => best.ZetKlant(klant));
        }
        [Fact]
        public void ZetKlant_NieuweKLant_GeenBestaandeKlant()
        {
            Bestelling best=new Bestelling();
            best.BestellingId = 10;
            best.ZetKlant(_klant);
            Assert.Equal(_klant, best.Klant);
            Assert.Contains(best, _klant.Bestellingen());
        }
        [Fact]
        public void ZetKlant_NieuweKLant_BestaandeKlant()
        {
            Bestelling best = new Bestelling();
            best.BestellingId = 10;
            best.ZetKlant(_klant);
            Assert.Equal(_klant, best.Klant);
            Klant klant = new Klant(10, "jos", "antwerpen");
            best.ZetKlant(klant);
            Assert.Equal(klant, best.Klant);
            Assert.Contains(best, klant.Bestellingen());
            Assert.DoesNotContain(best, _klant.Bestellingen());
        }
        [Fact]
        public void HeeftZelfdeProperties()
        {
            Bestelling b = new Bestelling();
            b.BestellingId = 5;
            b.VoegTruitjeToe(_truitjes[0], 5);
            b.VoegTruitjeToe(_truitjes[1], 1);
            //Bestelling b=_bestellingen[5];
            //b.VerwijderTruitje(_truitjes[0], 2);
            Assert.True(_bestellingen[4].HeeftZelfdeProperties(b));
        }
    }
}