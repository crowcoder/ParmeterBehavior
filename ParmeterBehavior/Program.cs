using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ParmeterBehavior
{
    class Program
    {
        static string resultFormat = "RESULT: {0}\nVALUE: {1}";
        static void Main(string[] args)
        {
            SqlConnectionStringBuilder bldr = new SqlConnectionStringBuilder();
            bldr.DataSource = "(localdb)\\DEV";
            bldr.InitialCatalog = "DbParams";
            bldr.ApplicationName = "ParameterBehavior";
            bldr.IntegratedSecurity = true;

            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                try
                {
                    WriteScenario("Scenario 1: ParameterDirection.Input with valid Value, no server-side default");

                    cmd.CommandText = "usp_Input_No_Default";
                    SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = "Foo";
                    cmd.Parameters.Add(par);
                    var result = cmd.ExecuteScalar();
                    var parValue = cmd.Parameters[0].Value;
                    Console.WriteLine(resultFormat, result, parValue);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                finally { cmd.Parameters.Clear(); }

                try
                {
                    WriteScenario("Scenario 2: ParameterDirection.Input with null value, no server-side default");

                    cmd.CommandText = "usp_Input_No_Default";
                    SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = null;
                    cmd.Parameters.Add(par);
                    var result = cmd.ExecuteScalar();
                    var parValue = cmd.Parameters[0].Value;
                    Console.WriteLine(resultFormat, result, parValue);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                finally { cmd.Parameters.Clear(); }

                try
                {
                    WriteScenario("Scenario 3: ParameterDirection.Input with DBNull.Value, no server-side default");

                    cmd.CommandText = "usp_Input_No_Default";
                    SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = DBNull.Value;
                    cmd.Parameters.Add(par);
                    var result = cmd.ExecuteScalar();
                    var parValue = cmd.Parameters[0].Value;
                    Console.WriteLine(resultFormat, result, parValue);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                finally { cmd.Parameters.Clear(); }

                try
                {
                    WriteScenario("Scenario 4: Parameter omitted, no server-side default");

                    cmd.CommandText = "usp_Input_No_Default";
                    var result = cmd.ExecuteScalar();
                    var parValue = cmd.Parameters[0].Value;
                    Console.WriteLine(resultFormat, result, parValue);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try
                {
                    cmd.Parameters.Clear();
                    InputNull_HasDefault(cmd); var result = cmd.ExecuteScalar();
                    WriteResults(result, cmd);

                    WriteScenario("Scenario 5: ParameterDirection.Input null value, has server-side default");

                    cmd.CommandText = "usp_Input_Has_Default";
                    SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
                    par.Direction = ParameterDirection.Input;
                    par.Value = null;
                    cmd.Parameters.Add(par);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try
                {
                    cmd.Parameters.Clear();
                    InputDBNull_HasDefault(cmd); var result = cmd.ExecuteScalar();
                    WriteResults(result, cmd);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try
                {
                    cmd.Parameters.Clear();
                    NoParamAdded_HasDefault(cmd); var result = cmd.ExecuteScalar();
                    WriteResults(result, cmd);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

                try
                {
                    OutputNoDefault(cmd);
                    var result = cmd.ExecuteScalar();
                    WriteResults(result, cmd);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }

            Console.ReadKey();
        }

        private static void OutputNoDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);

            cmd.CommandText = "usp_Output_No_Default";

            SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
            par.Direction = ParameterDirection.Output;
            par.Size = 50;
            par.Value = "foobar";
            cmd.Parameters.Add(par);
        }

        private static void NoParamAdded_HasDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);
            cmd.CommandText = "usp_Input_Has_Default";
        }

        private static void InputDBNull_HasDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);

            cmd.CommandText = "usp_Input_Has_Default";

            SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
            par.Direction = ParameterDirection.Input;
            par.Value = DBNull.Value;
            cmd.Parameters.Add(par);
        }

        private static void InputNull_HasDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);

            cmd.CommandText = "usp_Input_Has_Default";

            SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
            par.Direction = ParameterDirection.Input;
            par.Value = null;
            cmd.Parameters.Add(par);
        }

        private static void InputNotNull_HasDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);

            cmd.CommandText = "usp_Input_No_Default";

            SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
            par.Direction = ParameterDirection.Input;
            par.Value = "Foo";
            cmd.Parameters.Add(par);
        }

        private static void InputDBNull_NoDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);

            cmd.CommandText = "usp_Input_No_Default";

            SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
            par.Direction = ParameterDirection.Input;
            par.Value = DBNull.Value;
            cmd.Parameters.Add(par);
        }

        private static void InputNull_NoDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);

            cmd.CommandText = "usp_Input_No_Default";

            SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
            par.Direction = ParameterDirection.Input;
            par.Value = null;
            cmd.Parameters.Add(par);
        }

        private static void InputNotNull_NoDefault(SqlCommand cmd)
        {
            WriteScenario(MethodBase.GetCurrentMethod().Name);

            cmd.CommandText = "usp_Input_No_Default";

            SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
            par.Direction = ParameterDirection.Input;
            par.Value = "Foo";
            cmd.Parameters.Add(par);
        }

        private static void WriteResults(object result, SqlCommand cmd)
        {
            Console.WriteLine(
                resultFormat,
                result,
                cmd.Parameters.Count > 0 ? cmd.Parameters[0].Value : null);
        }
        private static void WriteScenario(string methodname)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Executing: " + methodname);
            Console.ResetColor();
        }
    }
}
