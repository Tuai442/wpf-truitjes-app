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
    public class TruitjeRepositoryADO : ITruitjeRepository
    {
        private string connectionString;

        public TruitjeRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;

        }

        private int ZoekClubId(string seizoen,string competitie,string ploegnaam)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            string query = "SELECT clubid FROM Club where ploegnaam=@ploegnaam and competitie=@competitie and seizoen=@seizoen";
            using(SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@ploegnaam",ploegnaam);
                    cmd.Parameters.AddWithValue("@competitie", competitie);
                    cmd.Parameters.AddWithValue("@seizoen",seizoen);
                    int clubid=(int)cmd.ExecuteScalar();
                    return clubid;
                }
                catch(Exception ex)
                {
                    throw new TruitjeRepositoryException("ZoekClubId", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void VoegTruitjeToe(Truitje truitje)
        {
            SqlConnection conn=new SqlConnection(connectionString);
            string sql = "INSERT INTO Truitje(prijs,versie,uit,clubid,maat) output INSERTED.truitjeid VALUES(@prijs,@versie,@uit,@clubid,@maat) ";
            using(SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    int clubid=ZoekClubId(truitje.Seizoen,truitje.Club.Competitie,truitje.Club.PloegNaam);
                    conn.Open();
                    cmd.CommandText=sql;
                    cmd.Parameters.AddWithValue("@prijs", truitje.Prijs);
                    cmd.Parameters.AddWithValue("@versie", truitje.ClubSet.Versie);
                    cmd.Parameters.AddWithValue("@uit", truitje.ClubSet.Uit);
                    cmd.Parameters.AddWithValue("@clubid", clubid);
                    string maat=Enum.GetName(typeof(MaatTruitje), truitje.Maat);
                    cmd.Parameters.AddWithValue("@maat", maat);
                    int truitjeId=(int)cmd.ExecuteScalar();
                    truitje.TruitjeId=truitjeId;
                }
                catch(Exception ex)
                {
                    throw new TruitjeRepositoryException("VoegTruitjeToe", ex);
                }
                finally { conn.Close(); }
            }
        }
    }
}
