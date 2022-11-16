using ISpan.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Exec3_MaintainUsers
{
	internal class Program
	{
		static void Main(string[] args)
		{
			//Insert();
			//Update();
			//Delete();
			Select();

		}
		public static void Insert()
		{
			string sql = @"  INSERT INTO Users(Name,Account,Password,DateOfBirth,Height)
								VALUES
								(@Name, @Account,@Password,@DateOfBirth,@Height)";
			var dbHelper = new SqlDbHelper("default");
			try
			{
				var parameters = new SqlParameterBuilder()
					.AddNVarchar("Name", 50, "Eric")
					.AddNVarchar("Account", 50, "Eric")
					.AddNVarchar("Password", 50, "Eric123")
					.AddDateTime("DateOfBirth", new DateTime(1998, 9, 17))
					.AddInt("Height", 175)
					.Build();


				dbHelper.ExecuteNonQuery(sql, parameters);

				Console.WriteLine("記錄已新增");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"操作失敗, 原因 :{ex.Message}");
			}


		}
		public static void Update()
		{
			string sql = @" UPDATE Users 
							SET 
							Name=@Name, 
							Account=@Account, 
							Password=@Password, 
							DateOfBirth=@DateOfBirth, 
							Height=@Height
							WHERE Id=@Id";

			var dbHelper = new SqlDbHelper("default");
			try
			{
				var parameters = new SqlParameterBuilder()
					.AddNVarchar("Name", 50, "Franklyn456")
					.AddNVarchar("Account", 50, "Franklyn456")
					.AddNVarchar("Password", 50, "Franklyn456")
					.AddDateTime("DateOfBirth", new DateTime(1998, 10, 6))
					.AddInt("Height", 165)
					.AddInt("id", 2)
					.Build();

				dbHelper.ExecuteNonQuery(sql, parameters);

				Console.WriteLine("記錄已更新");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"操作失敗, 原因 :{ex.Message}");
			}
		}
		public static void Delete()
		{
			string sql = @" DELETE FROM Users WHERE Id=@Id";

			var dbHelper = new SqlDbHelper("default");
			try
			{
				var parameters = new SqlParameterBuilder()
					.AddInt("id", 2)
					.Build();

				dbHelper.ExecuteNonQuery(sql, parameters);

				Console.WriteLine("記錄已刪除");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"操作失敗, 原因 :{ex.Message}");
			}

		}

		public static void Select()
		{
			var dbHelper = new SqlDbHelper("default");
			string sql = "SELECT Id, Name FROM Users WHERE Id> @Id  ORDER BY Id ASC";
			try
			{
				var parameters = new SqlParameterBuilder().AddInt("id", 0).Build();
				DataTable news = dbHelper.Select(sql, parameters);
				foreach (DataRow row in news.Rows)
				{
					int id = row.Field<int>("id");
					string Name = row.Field<string>("Name");
					Console.WriteLine($"Id={id}, Name={Name}");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"連線失敗, 原因 :{ex.Message}");
			}
		}
	}

}
