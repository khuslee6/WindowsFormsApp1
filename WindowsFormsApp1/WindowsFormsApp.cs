using System;
using System.Data.SQLite;
using System.IO;

public static class WindowsFormsApp
{
    private static string databaseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LibraryManagementSystem.db");
    private static string connectionString = $"Data Source={databaseFilePath};Version=3;";

    public static void InitializeDatabase()
    {
        try
        {
            Console.WriteLine($"Database Path: {databaseFilePath}");

            if (!File.Exists(databaseFilePath))
            {
                Console.WriteLine("Database file does not exist. Creating a new one...");
                SQLiteConnection.CreateFile(databaseFilePath);

                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string createUsersTableQuery = @"
                        CREATE TABLE IF NOT EXISTS users (
                            id INTEGER PRIMARY KEY AUTOINCREMENT,
                            name TEXT NOT NULL,
                            email TEXT UNIQUE NOT NULL,
                            password TEXT NOT NULL,
                            user_type TEXT NOT NULL
                        );";

                    using (SQLiteCommand command = new SQLiteCommand(createUsersTableQuery, connection))
                    {

                        command.ExecuteNonQuery();
                        Console.WriteLine("Users table created successfully.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Database file already exists.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
        }
    }
}
