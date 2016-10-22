using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ParameterBehavior.Test
{
    [TestClass]
    public class ParameterDirectionInputWithDefaultTests
    {
        static SqlConnectionStringBuilder bldr;

        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            bldr = new SqlConnectionStringBuilder();
            bldr.DataSource = "(localdb)\\DEV";
            bldr.InitialCatalog = "DbParams";
            bldr.ApplicationName = "ParameterBehavior";
            bldr.IntegratedSecurity = true;
        }

        [TestMethod]
        public void ParameterDirection_Input_with_valid_Value()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.CommandText = "usp_Input_Has_Default";
                SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
                par.Direction = ParameterDirection.Input;
                par.Value = "Foo";
                cmd.Parameters.Add(par);
                var result = cmd.ExecuteScalar();
                var parValue = cmd.Parameters[0].Value;

                Assert.AreEqual("Foo", result, "Failed on assert of result");
                Assert.AreEqual(result, parValue, "Failed on assert of parValue");
            }
        }

        [TestMethod]
        public void ParameterDirection_Input_with_DBNull_Value()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.CommandText = "usp_Input_Has_Default";
                SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
                par.Direction = ParameterDirection.Input;
                par.Value = DBNull.Value;
                cmd.Parameters.Add(par);
                var result = cmd.ExecuteScalar();
                var parValue = cmd.Parameters[0].Value;

                Assert.AreEqual(DBNull.Value, result, "Failed on assert of result");
                Assert.AreEqual(DBNull.Value, parValue, "Failed on assert of parValue");
            }
        }

        [TestMethod]
        public void ParameterDirection_Input_with_Null_Value_()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.CommandText = "usp_Input_Has_Default";
                SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar);
                par.Direction = ParameterDirection.Input;
                par.Value = null;
                cmd.Parameters.Add(par);
                var result = cmd.ExecuteScalar();
                var parValue = cmd.Parameters[0].Value;
                Assert.AreEqual("I'm a default value", result);
                Assert.IsNull(parValue);
            }
        }

        [TestMethod]
        public void No_Parameter()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.CommandText = "usp_Input_Has_Default";
                var result = cmd.ExecuteScalar();
                Assert.AreEqual("I'm a default value", result);
            }
        }


    }
}
