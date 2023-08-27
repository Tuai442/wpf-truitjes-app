using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruitjesBL.Interfaces;
using TruitjesDL.Exceptions;

namespace TruitjesDL.Repositories
{
    public class ClubRepositoryADO : IClubRepository
    {
        private string _huidigSeizoen;
        private string _connectionString;
        public ClubRepositoryADO(string huidigSeizoen, string connectionString)
        {
            _huidigSeizoen = huidigSeizoen;
            _connectionString = connectionString;
        }

        public IReadOnlyList<string> GeefClubs(string competitie)
        {
            List<string> clubs = new List<string>();
            SqlConnection conn = new SqlConnection(_connectionString);
            string query = "SELECT ploegnaam FROM Club "
                +"where seizoen = @seizoen and competitie = @competitie";
            using(SqlCommand cmd = conn.CreateCommand())
            {
                try
                {
                    conn.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@seizoen",_huidigSeizoen);
                    cmd.Parameters.AddWithValue("@competitie",competitie);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string club = (string)reader["ploegnaam"];
                        clubs.Add(club);
                    }
                    reader.Close();
                    return clubs;
                }
                catch(Exception ex)
                {
                    throw new ClubRepositoryException("GeefClubs", ex);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public IReadOnlyList<string> GeefCompetities()
        {
            List<string> competities = new List<string>();
            SqlConnection connection=new SqlConnection(_connectionString);
            string query = "select distinct competitie from club where seizoen=@seizoen";
            using(SqlCommand cmd = connection.CreateCommand())
            {                
                try
                {
                    connection.Open();
                    cmd.CommandText = query;
                    cmd.Parameters.Add(new SqlParameter("@seizoen", SqlDbType.VarChar));
                    cmd.Parameters["@seizoen"].Value = _huidigSeizoen;
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        string competitie = (string)reader["competitie"];
                        competities.Add(competitie);
                    }
                    reader.Close();
                    return competities;
                }
                catch (Exception ex)
                {
                    throw new ClubRepositoryException("GeefCompetities",ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
