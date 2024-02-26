using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp41.Models;

namespace ConsoleApp41.Data {
    internal class EventDao {

        private const string connectionString = "Server=DELL-UMMAN\\SQLEXPRESS;Database=Organization;Trusted_Connection=true;";

        public int Insert(Event newEvent, params int[] speakersId) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("INSERT INTO Events (Name, Description, Address, StartDate, StartTime, EndTime) VALUES (@Name, @Description, @Address, @StartDate, @StartTime, @EndTime)", connection);
            command.Parameters.AddWithValue("@Name", newEvent.Name);
            command.Parameters.AddWithValue("@Description", newEvent.Description);
            command.Parameters.AddWithValue("@Address", newEvent.Address);
            command.Parameters.AddWithValue("@StartDate", newEvent.StartDate.Date);
            command.Parameters.AddWithValue("@StartTime", newEvent.StartTime.TimeOfDay);
            command.Parameters.AddWithValue("@EndTime", newEvent.EndTime.TimeOfDay);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0) {
                foreach (int speakerId in speakersId) {
                    using var command2 = new SqlCommand("INSERT INTO EventSpeakers (EventId, SpeakerId) VALUES (@EventId, @SpeakerId)", connection);
                    command2.Parameters.AddWithValue("@EventId", newEvent.Id);
                    command2.Parameters.AddWithValue("@SpeakerId", speakerId);
                    command2.ExecuteNonQuery();
                }
            }
            return rowsAffected;
        }

        public Event GetById(int id) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Events WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read()) {
                return new Event {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Address = reader.GetString(3),
                    StartDate = reader.GetDateTime(4),
                    StartTime = reader.GetDateTime(5),
                    EndTime = reader.GetDateTime(6)
                };
            }
            return new Event();
        }

        public List<Event> GetAll() {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT * FROM Events", connection);
            using var reader = command.ExecuteReader();
            List<Event> events = new();
            while (reader.Read()) {
                events.Add(new Event {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    Address = reader.GetString(3),
                    StartDate = reader.GetDateTime(4),
                    StartTime = reader.GetDateTime(5),
                    EndTime = reader.GetDateTime(6)
                });
            }
            return events;
        }

        public int AddSpeaker(int eventId, int speakerId) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("INSERT INTO EventSpeakers (EventId, SpeakerId) VALUES (@EventId, @SpeakerId)", connection);
            command.Parameters.AddWithValue("@EventId", eventId);
            command.Parameters.AddWithValue("@SpeakerId", speakerId);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }

        public int RemoveSpeaker(int eventId, int speakerId) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("DELETE FROM EventSpeakers WHERE EventId = @EventId AND SpeakerId = @SpeakerId", connection);
            command.Parameters.AddWithValue("@EventId", eventId);
            command.Parameters.AddWithValue("@SpeakerId", speakerId);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }

        public int Delete(int id) {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            using var command = new SqlCommand("DELETE FROM Events WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected;
        }
    }
}
