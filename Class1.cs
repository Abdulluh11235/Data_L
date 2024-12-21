using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_L
{
  public static class clsBook
    {
        private static  string conS = "server=.;database=librarySystem;user id=sa;password=sa123456 ;";
        private static SqlConnection con = new SqlConnection(conS);
        public static bool Find(int Id, ref string Title, ref string Author, ref string Genre,
              ref DateTime PublishD, ref int Copies, ref int pubId)
        {
            try
            {
                con.Open();
                string Query = "SELECT * FROM Books WHERE id=@Id";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {

                    Title = r["title"].ToString();
                    Author = r["author"].ToString();
                    Genre = r["Genre"].ToString();
                    PublishD = Convert.ToDateTime( r["publishdate"] );
                    Copies = (int) r["copies"];
                   
                    if (r["pubid"] != System.DBNull.Value)
                        pubId = Convert.ToInt32(r["pubid"]);
                    else
                        pubId = -1;

                    r.Close();

                    return true;
                }
                else

                {
                    r.Close();
                    return false;
                }

            }
            catch
            {
                // to do : log the exception in a file or something

                return false;
            }


            finally { con.Close(); }


        }

        public static int AddNewBook( string Title,  string Author,  string Genre,
               DateTime PublishD,  int Copies,  int pubId )
        {
            string nonQ = @"INSERT INTO Books (Title, Author, Genre, PublishDate, Copies, Pubid)
                            VALUES(@Title, @Author, @Genre, @PublishDate, @Copies, @Pubid)
                        SELECT SCOPE_IDENTITY()";



            int B_ID = -1;

            try
            {
                con.Open();

                SqlCommand cmdI = new SqlCommand(nonQ, con);

                cmdI.Parameters.AddWithValue("@Title", Title);
                cmdI.Parameters.AddWithValue("@Author", Author);
                cmdI.Parameters.AddWithValue("@Genre", Genre);
                cmdI.Parameters.AddWithValue("@PublishDate", PublishD);
                cmdI.Parameters.AddWithValue("@Copies", Copies);

                if (pubId != -1)
                    cmdI.Parameters.AddWithValue("@Pubid", pubId);
                else
                    cmdI.Parameters.AddWithValue("@Pubid", System.DBNull.Value);



                B_ID = Convert.ToInt32(cmdI.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }

            return B_ID;
        }

        public static bool UpdateBook(int Id,  string Title,  string Author,  string Genre,
               DateTime PublishD,  int Copies,  int pubId )
        {
            try
            {
                int rowsE = 0;
                con.Open();
                string nonQ = @"Update Books 
Set Title=@Title,Author=@Author,Genre=@Genre,
PublishDate=@PublishD,Copies=@Copies,pubId=@pubId";
                SqlCommand cmd = new SqlCommand(nonQ, con);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@Author", Author);
                cmd.Parameters.AddWithValue("@Genre", Genre);
                cmd.Parameters.AddWithValue("@PublishD", PublishD);
                cmd.Parameters.AddWithValue("@Copies", Copies);

                if (pubId != -1)
                    cmd.Parameters.AddWithValue("@pubId", pubId);
                else
                    cmd.Parameters.AddWithValue("@pubId", System.DBNull.Value);

                rowsE = cmd.ExecuteNonQuery();

                return rowsE > 0;
            }
            catch
            {
                return false;
            }


            finally { con.Close(); }

        }
        public static bool DeleteBook(int Id)
        {
            string nonQ = "DELETE FROM Books WHERE id=@Id";
            SqlCommand cmd = new SqlCommand(nonQ, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            int rowsE ;

            try
            {
                con.Open();
                rowsE = cmd.ExecuteNonQuery();
                return (rowsE != 0);
            }
            catch { return false; }
            finally { con.Close(); }

        }
        public static DataTable ListAllBooks()
        {
            string Q = "SELECT * FROM Books";
            SqlCommand cmd = new SqlCommand(Q, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                }

                return dt;
            }
            catch { return null; }
            finally { con.Close(); }

        }
        public static bool IsBookPresent(int id)
        {
            string q = "SELECT found=1 FROM Books WHERE id=@ID";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                con.Open();
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                return n == 1;
            }
            catch { return false; }
            finally { con.Close(); }
        }


    }
    public class clsEmployee
    {
        private static string conS = "server=.;database=librarySystem;user id=sa;password=sa123456 ;";
        private static SqlConnection con = new SqlConnection(conS);
        public static bool Find(int Id, ref string FirstName, ref string LastName, ref DateTime HireD,
              ref int Salary, ref int ShiftId)
        {
            try
            {
                con.Open();
                string Query = "SELECT * FROM Employes WHERE id=@Id";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {

                    FirstName = r["firstname"].ToString();
                    LastName = r["lastname"].ToString();
                    Salary = (int)r["salary"];
                    ShiftId =Convert.ToInt32( r["shiftid"] );

                    if (r["HireDate"] == DBNull.Value)
                        HireD = DateTime.MinValue;
                    else
                        HireD = Convert.ToDateTime(r["hiredate"]);

                    r.Close();

                    return true;
                }
                else

                {
                    r.Close();
                    return false;
                }

            }
            catch
            {
                // to do : log the exception in a file or something

                return false;
            }


            finally { con.Close(); }


        }

        public static int AddNewEmployee(string FirstName,  string LastName,  DateTime HireD,
               int Salary,  int ShiftId = -1)
        {
            string nonQ = @"INSERT INTO Employes (firstname, lastname, hiredate, salary, shiftid)
                            VALUES(@firstname, @lastname, @hiredate, @salary, @shiftid)
                        SELECT SCOPE_IDENTITY()";



            int E_ID = -1;

            try
            {
                con.Open();

                SqlCommand cmdI = new SqlCommand(nonQ, con);

                cmdI.Parameters.AddWithValue("@firstname", FirstName);
                cmdI.Parameters.AddWithValue("@lastname", LastName);
                cmdI.Parameters.AddWithValue("@shiftid", ShiftId);
                cmdI.Parameters.AddWithValue("@salary", Salary);

                if (HireD != null)
                    cmdI.Parameters.AddWithValue("@hiredate", HireD);
                else
                    cmdI.Parameters.AddWithValue("@hiredate", System.DBNull.Value);



                E_ID = Convert.ToInt32(cmdI.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }

            return E_ID;
        }

        public static bool UpdateEmployee(int Id, string FirstName, string LastName, DateTime HireD,
               int Salary, int ShiftId = -1)
        {
            try
            {
                int rowsE = 0;
                con.Open();
                string nonQ = @"Update Employes 
Set firstname=@firstname,lastname=@lastname,hiredate=@hireD,
salary=@salary,shiftid=@ShiftId";
                SqlCommand cmd = new SqlCommand(nonQ, con);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@firstname", FirstName);
                cmd.Parameters.AddWithValue("@lastname", LastName);
                cmd.Parameters.AddWithValue("@salary", Salary);
                    cmd.Parameters.AddWithValue("@ShiftId", ShiftId);

                if (HireD !=null)
                    cmd.Parameters.AddWithValue("@hiredate", HireD);
                else
                    cmd.Parameters.AddWithValue("@hiredate", System.DBNull.Value);

                rowsE = cmd.ExecuteNonQuery();

                return rowsE > 0;
            }
            catch
            {
                return false;
            }


            finally { con.Close(); }

        }
        public static bool DeleteEmployee(int Id)
        {
            string nonQ = "DELETE FROM employes WHERE id=@Id";
            SqlCommand cmd = new SqlCommand(nonQ, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            int rowsE = 0;

            try
            {
                con.Open();
                rowsE = cmd.ExecuteNonQuery();
                return (rowsE != 0);
            }
            catch { return false; }
            finally { con.Close(); }

        }
        public static DataTable ListAllEmployees()
        {
            string Q = "SELECT * FROM employes";
            SqlCommand cmd = new SqlCommand(Q, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                }

                return dt;
            }
            catch { return null; }
            finally { con.Close(); }

        }
        public static bool IsEmployeePresent(int id)
        {
            string q = "SELECT found=1 FROM employes WHERE id=@ID";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                con.Open();
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                return n == 1;
            }
            catch { return false; }
            finally { con.Close(); }
        }
    }
    public class clsLoan
    {
        private static string conS = "server=.;database=librarySystem;user id=sa;password=sa123456 ;";
        private static SqlConnection con = new SqlConnection(conS);
        public static bool Find(int Id, ref int MemberId, ref int BookId, ref DateTime LoanD,
              ref DateTime DueD)
        {
            try
            {
                con.Open();
                string Query = "SELECT * FROM Loans WHERE id=@Id";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {

                    MemberId = Convert.ToInt32( r["Memberid"] );
                    BookId = Convert.ToInt32( r["bookid"] );
                    LoanD = Convert.ToDateTime(r["loandate"]);
                    DueD = Convert.ToDateTime(r["duedate"] );




                    r.Close();

                    return true;
                }
                else

                {
                    r.Close();
                    return false;
                }

            }
            catch
            {
                // to do : log the exception in a file or something

                return false;
            }


            finally { con.Close(); }


        }

        public static int AddNewLoan(  int MemberId,  int BookId,  DateTime LoanD,
               DateTime DueD)
        {
            string nonQ = @"INSERT INTO Loans (MemberId, BookId, LoanD, DueD)
                            VALUES(@MemberId, @BookId, @LoanD, @DueD)
                        SELECT SCOPE_IDENTITY()";



            int L_ID = -1;

            try
            {
                con.Open();

                SqlCommand cmdI = new SqlCommand(nonQ, con);

                cmdI.Parameters.AddWithValue("@firstname", MemberId);
                cmdI.Parameters.AddWithValue("@lastname", BookId);
                cmdI.Parameters.AddWithValue("@hiredate", LoanD);
                cmdI.Parameters.AddWithValue("@salary", DueD);

           



                L_ID = Convert.ToInt32(cmdI.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }

            return L_ID;
        }

        public static bool UpdateLoan(int Id,  int MemberId,  int BookId,  DateTime LoanD,
               DateTime DueD)
        {
            try
            {
                int rowsE = 0;
                con.Open();
                string nonQ = @"Update loans 
Set MemberId=@MemberId,BookId=@BookId,LoanDate=@LoanD,
DueDate=@DueD";
                SqlCommand cmd = new SqlCommand(nonQ, con);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@MemberId", MemberId);
                cmd.Parameters.AddWithValue("@BookId", BookId);
                cmd.Parameters.AddWithValue("@LoanD",LoanD );
                cmd.Parameters.AddWithValue("@DueD", DueD);

                rowsE = cmd.ExecuteNonQuery();

                return rowsE > 0;
            }
            catch
            {
                return false;
            }


            finally { con.Close(); }

        }
        public static bool DeleteLoan(int Id)
        {
            string nonQ = "DELETE FROM Loans WHERE id=@Id";
            SqlCommand cmd = new SqlCommand(nonQ, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            int rowsE = 0;

            try
            {
                con.Open();
                rowsE = cmd.ExecuteNonQuery();
                return (rowsE != 0);
            }
            catch { return false; }
            finally { con.Close(); }

        }
        public static DataTable ListAllLoans()
        {
            string Q = "SELECT * FROM Loans";
            SqlCommand cmd = new SqlCommand(Q, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                }

                return dt;
            }
            catch { return null; }
            finally { con.Close(); }

        }
        public static bool IsLoanPresent(int id)
        {
            string q = "SELECT found=1 FROM Loans WHERE id=@ID";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                con.Open();
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                return n == 1;
            }
            catch { return false; }
            finally { con.Close(); }
        }
    }
    public class clsMember
    {
        private static string conS = "server=.;database=librarySystem;user id=sa;password=sa123456 ;";
        private static SqlConnection con = new SqlConnection(conS);
        public static bool Find(int Id, ref string FirstName, ref string LastName, ref DateTime JoinD,
               ref DateTime EndD)
        {
            try
            {
                con.Open();
                string Query = "SELECT * FROM Members WHERE id=@Id";
                SqlCommand cmd = new SqlCommand(Query, con);
                cmd.Parameters.AddWithValue("@Id", Id);
                SqlDataReader r = cmd.ExecuteReader();

                if (r.Read())
                {

                    FirstName = r["firstname"].ToString();
                    LastName = r["lastname"].ToString();
                    JoinD = Convert.ToDateTime(r["joindate"]);
                   EndD  = Convert.ToDateTime(r["MemberShipEndDate"]);



                    r.Close();

                    return true;
                }
                else

                {
                    r.Close();
                    return false;
                }

            }
            catch
            {
                // to do : log the exception in a file or something

                return false;
            }


            finally { con.Close(); }


        }

        public static int AddNewMember( string FirstName,  string LastName,  DateTime JoinD,
                DateTime EndD)
        {
            string nonQ = @"INSERT INTO Members (firstname, lastname, joindate, MemberShipEndDate)
                            VALUES(@firstname, @lastname, @joindate, @EndDate)
                        SELECT SCOPE_IDENTITY()";



            int M_ID = -1;

            try
            {
                con.Open();

                SqlCommand cmdI = new SqlCommand(nonQ, con);

                cmdI.Parameters.AddWithValue("@firstname", FirstName);
                cmdI.Parameters.AddWithValue("@lastname", LastName);
                cmdI.Parameters.AddWithValue("@joindate", JoinD);
                cmdI.Parameters.AddWithValue("@EndDate", EndD);

             



                M_ID = Convert.ToInt32(cmdI.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }

            return M_ID;
        }

        public static bool UpdateMember(int Id,  string FirstName,  string LastName,  DateTime JoinD,
                DateTime EndD)
        {
            try
            {
                int rowsE = 0;
                con.Open();
                string nonQ = @"Update Members 
Set firstname=@firstname,lastname=@lastname,joindate=@joinD,
MemberShipEndDate=@EndD";
                SqlCommand cmd = new SqlCommand(nonQ, con);

                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@firstname", FirstName);
                cmd.Parameters.AddWithValue("@lastname", LastName);
                cmd.Parameters.AddWithValue("@hiredate", JoinD);
                cmd.Parameters.AddWithValue("@EndD", EndD);

               
                rowsE = cmd.ExecuteNonQuery();

                return rowsE > 0;
            }
            catch
            {
                return false;
            }


            finally { con.Close(); }

        }
        public static bool DeleteMember(int Id)
        {
            string nonQ = "DELETE FROM Members WHERE id=@Id";
            SqlCommand cmd = new SqlCommand(nonQ, con);
            cmd.Parameters.AddWithValue("@Id", Id);
            int rowsE ;

            try
            {
                con.Open();
                rowsE = cmd.ExecuteNonQuery();
                return (rowsE != 0);
            }
            catch { return false; }
            finally { con.Close(); }

        }
        public static DataTable ListAllMembers()
        {
            string Q = "SELECT * FROM Members";
            SqlCommand cmd = new SqlCommand(Q, con);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                }

                return dt;
            }
            catch { return null; }
            finally { con.Close(); }

        }
        public static bool IsMemberPresent(int id)
        {
            string q = "SELECT found=1 FROM Members WHERE id=@ID";
            SqlCommand cmd = new SqlCommand(q, con);
            cmd.Parameters.AddWithValue("@ID", id);
            try
            {
                con.Open();
                int n = Convert.ToInt32(cmd.ExecuteScalar());
                return n == 1;
            }
            catch { return false; }
            finally { con.Close(); }
        }
    }

}
