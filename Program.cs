using System;
using System.Data;
using Microsoft.Data.Sqlite;

namespace metricell_assessment
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder();

            //Creating a DB in the project directory
            connectionStringBuilder.DataSource = "./EmployeeDB.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Creating the table (drop if already exists first):
                
                var delTableCmd = connection.CreateCommand();
                delTableCmd.CommandText = "DROP TABLE IF EXISTS Employees";
                delTableCmd.ExecuteNonQuery();

                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = "CREATE TABLE Employees(Name VARCHAR(50), Value INT)";
                createTableCmd.ExecuteNonQuery();

                //Inserting data into the table
                using (var transaction = connection.BeginTransaction())
                {
                    var insertCmd = connection.CreateCommand();
                    insertCmd.CommandText = "INSERT INTO Employees VALUES('Abul', 1357),('Adolfo', 1224),('Alexander', 2296),('Amber', 1145),('Amy', 4359),('Andy', 1966),('Anna', 4040),('Antony', 449),('Ashley', 8151),('Borja', 9428),('Cecilia', 2136),('Christopher', 9035),('Dan', 1475),('Dario', 284),('David', 948),('Elike', 1860),('Ella', 4549),('Ellie', 5736),('Elliot', 1020),('Emily', 7658),('Faye', 7399),('Fern', 1422),('Francisco', 5028),('Frank', 3281),('Gary', 9190),('Germaine', 6437),('Greg', 5929),('Harvey', 8471),('Helen', 963),('Huzairi', 9491),('Izmi', 8324),('James', 6994),('Jarek', 6581),('Jim', 202),('John', 261),('Jose', 1605),('Josef', 3714),('Karthik', 4828),('Katrin', 5393),('Lee', 269),('Luke', 5926),('Madiha', 2329),('Marc', 3651),('Marina', 6903),('Mark', 3368),('Marzena', 7515),('Mohamed', 1080),('Nichole', 1221),('Nikita', 8520),('Oliver', 2868),('Patryk', 1418),('Paul', 4332),('Ralph', 1581),('Raymond', 7393),('Roman', 4056),('Ryan', 252),('Sara', 2618),('Sean', 691),('Seb', 5395),('Sergey', 8282),('Shaheen', 3721),('Sharni', 7737),('Sinu', 3349),('Stephen', 8105),('Tim', 8386),('Tina', 5133),('Tom', 7553),('Tony', 4432),('Tracy', 1771),('Tristan', 2030),('Victor', 1046),('Yury', 1854)";
                    insertCmd.ExecuteNonQuery();

                    transaction.Commit();
                }

                //Reading the inserted data:
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "SELECT * FROM Employees";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var value = reader.GetInt32(1);
                        
                        Console.WriteLine("Showing all Name and Value from Employee Table");
                        Console.WriteLine(name);
                        Console.WriteLine(value);
                        Console.WriteLine();

                    }
                }
                
                
                // Showing employess with name beggining with E
                var nameLikeE = connection.CreateCommand();
                nameLikeE.CommandText = "SELECT Name, Value from Employees WHERE Name LIKE 'E%'";

                using (var reader = nameLikeE.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var value = reader.GetInt32(1);
                        
                        Console.WriteLine("Showing names that begin with an E");
                        Console.WriteLine(name);
                        Console.WriteLine(value);
                        Console.WriteLine();
                    }
                }

                // Show employess with name beggining with G
                var nameLikeG = connection.CreateCommand();
                nameLikeG.CommandText = "SELECT Name, Value from Employees WHERE Name LIKE 'G%'";

                using (var reader = nameLikeG.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var value = reader.GetInt32(1);
                        
                        Console.WriteLine("Showing names that begin with a G");
                        Console.WriteLine(name);
                        Console.WriteLine(value);
                        Console.WriteLine();
                    }
                }
                
                
                // Incrementing the field Value by 1 where the field Name begins with ‘E’ and by 10 where Name begins with ‘G’ and all others by 100
                var updateCmd = connection.CreateCommand();
                updateCmd.CommandText = "UPDATE Employees SET Value = CASE WHEN Name LIKE 'E%' THEN Value + 1 WHEN Name LIKE 'G%' THEN Value + 10 ELSE Value + 100 END";


                using (var reader = updateCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var value = reader.GetInt32(1);
                        
                        Console.WriteLine("Adding 1 to names that start with E, 10 to names that start with a G and 100 to anything else");
                        Console.WriteLine(name);
                        Console.WriteLine(value);
                        Console.WriteLine();
                    /*/
                    This blovk of code is not being run by the console, I am not sure why, I have tried to fix it but no luck, must be an easy fix but im not aware of it. */
                    }
                }


                // Listing the sum of all Values for all Names that begin with A, B or C but only presenting the data where the summed values are greater than or equal to 11171
                var showSum = connection.CreateCommand();
                showSum.CommandText = "SELECT SUM(Value) FROM Employees WHERE Name LIKE 'A%' or Name LIKE 'B%' or Name LIKE 'C%'";

                using (var reader = showSum.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var name = reader.GetString(0);
                        var value = reader.GetInt32(1);
                        
                        Console.WriteLine("Showing sum of values for Employees where Name starts with A, B or C and value is greater or equal to 11171");
                        Console.WriteLine(name);
                        Console.WriteLine(value);
                        Console.WriteLine();

                 /*/
                Unhandled exception. System.ArgumentOutOfRangeException: Exception of type 'System.ArgumentOutOfRangeException' was thrown. (Parameter 'ordinal')
                Actual value was 1.
                at Microsoft.Data.Sqlite.SqliteDataRecord.GetSqliteType(Int32 ordinal)
                at Microsoft.Data.Sqlite.SqliteValueReader.IsDBNull(Int32 ordinal)
                at Microsoft.Data.Sqlite.SqliteDataRecord.IsDBNull(Int32 ordinal)
                at Microsoft.Data.Sqlite.SqliteValueReader.GetInt64(Int32 ordinal)
                at Microsoft.Data.Sqlite.SqliteValueReader.GetInt32(Int32 ordinal)
                at Microsoft.Data.Sqlite.SqliteDataReader.GetInt32(Int32 ordinal)

                The error code below shows when I run the program on the console this error happens for the var showsum 'line 126'. I am not sure how to fix it. would love to know what went wrong.
                */        
                    }
                }

               


            }
        }
    }
}