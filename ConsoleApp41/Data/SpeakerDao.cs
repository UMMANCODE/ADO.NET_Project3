using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp41.Models;

namespace ConsoleApp41.Data {
    internal class SpeakerDao {

        private const string connectionString = "Server=DELL-UMMAN\\SQLEXPRESS;Database=Organization;Trusted_Connection=true;";
        public int Insert(Speaker newEvent) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("INSERT INTO Speakers (FullName, Position, Company, ImageUrl) VALUES (@FullName, @Position, @Company, @ImageUrl)", connection);
            command.Parameters.AddWithValue("@FullName", newEvent.FullName);
            command.Parameters.AddWithValue("@Position", newEvent.Position);
            command.Parameters.AddWithValue("@Company", newEvent.Company);
            command.Parameters.AddWithValue("@ImageUrl", newEvent.ImageUrl);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }

        public Speaker GetById(int id) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Speakers WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read()) {
                return new Speaker {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Position = reader.GetString(2),
                    Company = reader.GetString(3),
                    ImageUrl = reader.GetString(4)
                };
            }
            return new Speaker();
        }

        public List<Speaker> GetAll() {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Speakers WHERE Id = @Id", connection);
            using var reader = command.ExecuteReader();
            List<Speaker> speakers = new();
            while (reader.Read()) {
                speakers.Add(new Speaker {
                    Id = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Position = reader.GetString(2),
                    Company = reader.GetString(3),
                    ImageUrl = reader.GetString(4)
                });
            }
            return speakers;
        }

        public int Update(Speaker newSpeaker) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("UPDATE Speakers SET FullName = @FullName, Position = @Position, Company = @Company, ImageUrl = @ImageUrl WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", newSpeaker.Id);
            command.Parameters.AddWithValue("@FullName", newSpeaker.FullName);
            command.Parameters.AddWithValue("@Position", newSpeaker.Position);
            command.Parameters.AddWithValue("@Company", newSpeaker.Company);
            command.Parameters.AddWithValue("@ImageUrl", newSpeaker.ImageUrl);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }

        public int Delete(int id) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("DELETE FROM Speakers WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
