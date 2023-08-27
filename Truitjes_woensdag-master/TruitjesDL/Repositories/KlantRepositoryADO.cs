using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Interfaces;
using TruitjesBL.Model;
using TruitjesDL.Exceptions;

namespace TruitjesDL.Repositories
{
    public class KlantRepositoryADO: IKlantRepository
    {
        private string connectionString;

        public KlantRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public bool BestaatKlant(Klant klant)
        {
            throw new NotImplementedException();
        }

        public bool BestaatKlant(int value)
        {
            throw new NotImplementedException();
        }

        public Klant GeefKlant(int klantid)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT t1.*,t2.bestellingId,t2.prijs,t2.betaald,t2.tijdstip,"
                +"t3.aantal,t3.truitjeId,t4.prijs prijstruitje,t4.versie,t4.uit,t4.maat,t5.ploegnaam,"
                +"t5.competitie,t5.seizoen "
                +" FROM Klant t1 "
                +" left join Bestelling t2 on t1.klantId = t2.klantId "
                +" left join BestellingDetail t3 on t2.bestellingId = t3.bestellingId "
                +" left join Truitje t4 on t3.truitjeId = t4.truitjeId "
                +" left join Club t5 on t4.clubId = t5.clubId "
                +" where t1.klantid = @klantid "
                +" order by t2.bestellingId";
            using(SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@klantid", klantid);
                    SqlDataReader reader = cmd.ExecuteReader();
                    bool leesklant = true;
                    Klant klant = null;
                    int bestellingIdOld = -1;
                    int bestellingId=0;
                    bool betaald=false;
                    bool firstRowBestelling = true;
                    double prijs=0.0;
                    DateTime tijdstip = DateTime.Now;
                    double prijsTruitje;
                    bool uit;
                    string competitie;
                    string ploegnaam;
                    string versie;
                    int truitjeId;
                    int aantal;
                    string seizoen;
                    MaatTruitje maatTruitje;
                    List<Bestelling> bestellingList = new List<Bestelling>();
                    Dictionary<Truitje,int> truitjes=new Dictionary<Truitje,int>();
                    while (reader.Read())
                    {
                        if (leesklant)
                        {
                            string naamKlant = (string)reader["naam"];
                            string adresKlant = (string)reader["adres"];
                            klant=new Klant(klantid,naamKlant,adresKlant);
                            leesklant = false;
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("bestellingid"))) //heeft bestellingen
                        {
                            bestellingId = (int)reader["bestellingid"];
                            if (bestellingId != bestellingIdOld)
                            {
                                //nieuwe bestelling of eerste
                                if (bestellingIdOld>0)
                                {
                                    Bestelling bestelling = new Bestelling(truitjes,bestellingId,tijdstip,betaald,prijs,klant); //TODO invullen
                                    bestellingList.Add(bestelling);
                                    truitjes = new Dictionary<Truitje, int>();
                                }
                                firstRowBestelling=true;
                                bestellingIdOld=bestellingId;
                            }
                            if (firstRowBestelling)
                            {
                                betaald = (bool)reader["betaald"];
                                prijs = (double)reader["prijs"];
                                tijdstip = (DateTime)reader["tijdstip"];
                                firstRowBestelling = false;
                            }
                            //er moeten altijd truitjes in een bestelling zitten
                            aantal = (int)reader["aantal"];
                            truitjeId = (int)reader["truitjeid"];
                            competitie = (string)reader["competitie"];
                            ploegnaam = (string)reader["ploegnaam"];
                            versie = (string)reader["versie"];
                            prijsTruitje = (double)reader["prijstruitje"];
                            maatTruitje = (MaatTruitje)Enum.Parse(typeof(MaatTruitje),(string)reader["maat"]);
                            seizoen = (string)reader["seizoen"];
                            uit = (bool)reader["uit"];
                            Truitje truitje = new Truitje(truitjeId,prijsTruitje,seizoen,maatTruitje,new Club(competitie,ploegnaam),new ClubSet(uit,versie));
                            truitjes.Add(truitje,aantal);
                        }
                    }
                    reader.Close();
                    if (bestellingId > 0)
                    {
                        Bestelling b= new Bestelling(truitjes, bestellingId, tijdstip, betaald, prijs, klant);
                        bestellingList.Add(b);
                    }
                    return klant;
                }
                catch(Exception ex)
                {
                    throw new KlantRepositoryException("GeefKlant", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IEnumerable<Klant> GeefKlanten(string naam, string adres)
        {
            throw new NotImplementedException();
        }

        public void UpdateKlant(Klant klant)
        {
            throw new NotImplementedException();
        }

        public void VerwijderKlant(Klant klant)
        {
            throw new NotImplementedException();
        }

        public void VoegKlantToe(Klant klant)
        {
            throw new NotImplementedException();
        }
    }
}
