using System.Data.SqlClient;

namespace ExpenseTrackerADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server= US-5HSQ8S3; database=ExpenseTracker; Integrated Security=true");
            string ans = "";
            do
            {
                Console.WriteLine("Welcome to Expense Tracker App");
                Console.WriteLine();
                Console.WriteLine("Choose the Operation you would like to perform:");
                Console.WriteLine("1.Add Transaction");
                Console.WriteLine("2.View Expenses");
                Console.WriteLine("3.View Income");
                Console.WriteLine("4.Check Available Balance");
                Console.WriteLine();
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand($"insert into Transactions values(@title, @descriptions, @amount, @dates)", con);

                            Console.WriteLine("Enter Title:");
                            string title = Console.ReadLine();
                            Console.WriteLine("Enter Description");
                            string descriptions = Console.ReadLine();
                            Console.WriteLine("Enter Amount (Negative for Expense and Positive for Income)");
                            int amount = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter the Date (mm/dd/yyyy): ");
                            DateTime dates = DateTime.Parse(Console.ReadLine());
                            cmd.Parameters.AddWithValue("@title", title);
                            cmd.Parameters.AddWithValue("@descriptions", descriptions);
                            cmd.Parameters.AddWithValue("@amount", amount);
                            cmd.Parameters.AddWithValue("@dates", dates);
                            cmd.ExecuteNonQuery();
                            Console.WriteLine("Record saved successfully");
                            con.Close();
                            break;
                        }
                    case 2:
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Select * from Transactions where amount<0", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Console.WriteLine($"{dr[0]} | {dr[1]} | {dr[2]} | {dr[3]} | {dr[4]}");
                            }
                            con.Close();
                            break;
                        }
                    case 3:
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Select * from Transactions where amount>=0", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Console.WriteLine($"{dr[0]} | {dr[1]} | {dr[2]} | {dr[3]} | {dr[4]}");
                            }
                            con.Close();
                            break;
                        }
                    case 4:
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("Select Sum(Amount) as Available_Balance from Transactions", con);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Console.WriteLine(dr["Available_Balance"]);
                            }
                            con.Close();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Wrong Choice Entered");
                            break;
                        }
                }
                Console.WriteLine("Do you wish to continue? [y/n]");
                ans = Console.ReadLine();
            } while (ans.ToLower() == "y");
        }
    }
}