using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using Community.CsharpSqlite.SQLiteClient;
using System.IO;

namespace EbayLeaveBulkFeedback
{
	public class SQLiteDatabase
	{
		string _dbConnection;

		/// <summary>
		///     Single Param Constructor for specifying the DB file.
		/// </summary>
		/// <param name="connectionString">The File containing the DB</param>
		public SQLiteDatabase(string inputFile)
		{
			//_dbConnection = string.Format("Data Source={0}", inputFile);  //;Version=3;
			_dbConnection = string.Format("URI=file:{0}", inputFile);  //;Version=3;
			//_dbConnection = connectionString;
		}

		/// <summary>
		///     Single Param Constructor for specifying advanced connection options.
		/// </summary>
		/// <param name="connectionOpts">A dictionary containing all desired options and their values</param>
		public SQLiteDatabase(Dictionary<string, string> connectionOpts)
		{
			string str = "";
			int count = 0;
			foreach (KeyValuePair<string, string> row in connectionOpts)
			{
				str += string.Format((count > 0 ? "; " : "") + "{0}={1}", row.Key, row.Value);
				count++;
			}
			_dbConnection = str;
		}

		/// <summary>
		///     Allows the programmer to run a query against the Database.
		/// </summary>
		/// <param name="sql">The SQL to run</param>
		/// <returns>A DataTable containing the result set.</returns>
		public DataTable GetDataTable(string sql, DataColumn[] dataColumns = null)
		{
			DataTable dt = new DataTable();
			try
			{
				SqliteConnection cnn = new SqliteConnection(_dbConnection);
				cnn.Open();
				SqliteCommand mycommand = new SqliteCommand(sql, cnn);
				SqliteDataReader reader = mycommand.ExecuteReader();

				if (dataColumns == null)
				{
					var schema = reader.GetSchemaTable();
					foreach (DataRow schCol in schema.Rows)
					{
						DataColumn dataColumn = new DataColumn(
							(string)schCol["ColumnName"],
							(Type)schCol["DataType"]);
						dt.Columns.Add(dataColumn);
					}
				}
				else
				{
					foreach (DataColumn dataColumn in dataColumns)
					{
						dt.Columns.Add(dataColumn);
					}
				}

				while (reader.NextResult())
				{
					DataRow row = dt.NewRow();
					for (int i = 0; i < dt.Columns.Count; i++)
					{
						row[i] = reader.GetValue(i);
					}
					dt.Rows.Add(row);
				}

				reader.Close();
				cnn.Close();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}
			return dt;
		}

		private object GetBytes(SqliteDataReader reader, int columnNumber)
		{
			const int CHUNK_SIZE = 2 * 1024;
			byte[] buffer = new byte[CHUNK_SIZE];
			long bytesRead;
			long fieldOffset = 0;
			using (MemoryStream stream = new MemoryStream())
			{
				while ((bytesRead = reader.GetBytes(columnNumber, fieldOffset, buffer, 0, buffer.Length)) > 0)
				{
					if (bytesRead > buffer.Length)
						bytesRead = buffer.Length;

					stream.Write(buffer, 0, (int)bytesRead);
					fieldOffset += bytesRead;
				}
				return stream.ToArray();
			}
		}

		/// <summary>
		///     Allows the programmer to run a query against the Database.
		/// </summary>
		/// <param name="sql">The SQL to run</param>
		/// <returns>A DataRow containing the result or null if not exactly 1 row was found</returns>
		public DataRow GetDataRow(string sql, DataColumn[] dataColumns = null)
		{
			var table = GetDataTable(sql, dataColumns);
			if (table.Rows.Count != 1)
				return null;

			return table.Rows[0];
		}
		
		/// <summary>
		///     Allows the programmer to interact with the database for purposes other than a query.
		/// </summary>
		/// <param name="sql">The SQL to be run.</param>
		/// <returns>An Integer containing the number of rows updated.</returns>
		public int ExecuteNonQuery(string sql)
		{
			SqliteConnection cnn = new SqliteConnection(_dbConnection);
			cnn.Open();
			SqliteCommand mycommand = new SqliteCommand(sql, cnn);
			int rowsUpdated = mycommand.ExecuteNonQuery();
			cnn.Close();
			return rowsUpdated;
		}

		/// <summary>
		///     Allows the programmer to interact with the database for purposes other than a query.
		/// </summary>
		/// <param name="command">The SQLCommand to be run.</param>
		/// <returns>An Integer containing the number of rows updated.</returns>
		public int ExecuteNonQuery(SqliteCommand command)
		{
			SqliteConnection cnn = new SqliteConnection(_dbConnection);
			cnn.Open();
			command.Connection = cnn;
			int rowsUpdated = command.ExecuteNonQuery();
			cnn.Close();
			return rowsUpdated;
		}

		/// <summary>
		///     Allows the programmer to retrieve single items from the DB.
		/// </summary>
		/// <param name="sql">The query to run.</param>
		/// <returns>A string.</returns>
		public string ExecuteScalar(string sql)
		{
			SqliteConnection cnn = new SqliteConnection(_dbConnection);
			cnn.Open();
			SqliteCommand mycommand = new SqliteCommand(sql, cnn);
			object value = mycommand.ExecuteScalar();
			cnn.Close();
			if (value != null)
			{
				return value.ToString();
			}
			return "";
		}

		/// <summary>
		///     Allows the programmer to easily update rows in the DB.
		/// </summary>
		/// <param name="tableName">The table to update.</param>
		/// <param name="data">A dictionary containing Column names and their new values.</param>
		/// <param name="where">The where clause for the update statement.</param>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public int Update(string tableName, Dictionary<string, object> data, string where)
		{
			string vals = "";
			//bool returnCode = true;
			if (data.Count >= 1)
			{
				int count = 0;
				foreach (KeyValuePair<string, object> val in data)
				{
					if (val.Value == null)
						vals += string.Format((count > 0 ? "," : "") + " {0} = null", val.Key);
					else if (val.Value.ToString() == "current_timestamp")
						vals += string.Format((count > 0 ? "," : "") + " {0} = current_timestamp", val.Key);
					else
						vals += string.Format((count > 0 ? "," : "") + " {0} = '{1}'", val.Key, ConvertToDbValue(val.Value).ToString());
					count++;
				}
			}
			try
			{
				return this.ExecuteNonQuery(string.Format("UPDATE {0} SET {1} WHERE {2};", tableName, vals, where));
			}
			catch (Exception ex)
			{
				//returnCode = false;
				return -1;
			}
			//return returnCode;
		}

		/// <summary>
		///     Allows the programmer to easily delete rows from the DB.
		/// </summary>
		/// <param name="tableName">The table from which to delete.</param>
		/// <param name="where">The where clause for the delete.</param>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public bool Delete(string tableName, string where = null)
		{
			bool returnCode = true;
			try
			{
				this.ExecuteNonQuery(string.Format("DELETE FROM {0}" + (string.IsNullOrEmpty(where) ? "" : " WHERE {1}") + ";", tableName, where));
			}
			catch (Exception ex)
			{
				//MessageBox.Show(fail.Message);
				returnCode = false;
			}
			return returnCode;
		}

		/// <summary>
		///     Allows the programmer to easily insert into the DB
		/// </summary>
		/// <param name="tableName">The table into which we insert the data.</param>
		/// <param name="data">A dictionary containing the column names and data for the insert.</param>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public int Insert(string tableName, Dictionary<string, object> data)
		{
			string columns = "";
			string values = "";
			//bool returnCode = true;
			int count = 0;
			foreach (KeyValuePair<string, object> val in data)
			{
				columns += string.Format((count > 0 ? "," : "") + " {0}", val.Key.ToString());
				values += string.Format((count > 0 ? "," : "") + " @PARM_{0}", val.Key.ToString());
				count++;
			}

			try
			{
				SqliteCommand command = new SqliteCommand(string.Format("INSERT INTO {0}({1}) VALUES({2});", tableName, columns, values));
				foreach (KeyValuePair<string, object> val in data)
				{
					if (val.Value as string == "current_timestamp")
					{
						command.Parameters.Add(string.Format("@PARM_{0}", val.Key), DateTime.UtcNow);
					}
					else if (val.Value is byte[])
					{
						command.Parameters.Add(string.Format("@PARM_{0}", val.Key), DbType.Binary).Value = val.Value;
					}
					else
					{
						command.Parameters.Add(string.Format("@PARM_{0}", val.Key), ConvertToDbValue(val.Value));
					}
				}
				return this.ExecuteNonQuery(command);
			}
			catch (Exception ex)
			{
				//MessageBox.Show(fail.Message);
				//returnCode = false;
				return -1;
			}
			//return returnCode;
		}

		private object ConvertToDbValue(object val)
		{
			if (val is ICollection)
			{
				JavaScriptSerializer serializer = new JavaScriptSerializer();
				return serializer.Serialize(val);
			}
			else
			{
				return val;
			}
		}

		/// <summary>
		///     Allows the programmer to easily delete all data from the DB.
		/// </summary>
		/// <returns>A boolean true or false to signify success or failure.</returns>
		public bool ClearDB()
		{
			DataTable tables;
			try
			{
				tables = this.GetDataTable("SELECT NAME FROM SQLITE_MASTER WHERE type='table' ORDER BY NAME;");
				foreach (DataRow table in tables.Rows)
				{
					this.Delete(table["NAME"].ToString());
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
