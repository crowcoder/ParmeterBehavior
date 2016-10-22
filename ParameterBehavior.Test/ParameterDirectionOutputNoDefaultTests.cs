using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ParameterBehavior.Test
{
    [TestClass]
    public class ParameterDirectionOutputNoDefaultTests
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
        public void Direction_Output_WithValue()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.CommandText = "usp_Output_No_Default";
                SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar, 250);
                par.Direction = ParameterDirection.Output;
                par.Value = "Foo";
                cmd.Parameters.Add(par);
                var result = cmd.ExecuteScalar();
                Assert.AreEqual(DBNull.Value, result);
                Assert.AreEqual("changed by procedure", cmd.Parameters["@param1"].Value);
            }
        }

        [TestMethod]
        public void Direction_Output_DBNullValue()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.CommandText = "usp_Output_No_Default";
                SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar, 250);
                par.Direction = ParameterDirection.Output;
                par.Value = DBNull.Value;
                cmd.Parameters.Add(par);
                var result = cmd.ExecuteScalar();
                Assert.AreEqual(DBNull.Value, result);
                Assert.AreEqual("changed by procedure", cmd.Parameters["@param1"].Value);
            }
        }

        [TestMethod]
        public void Direction_Output_WithoutValue()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.CommandText = "usp_Output_No_Default";
                SqlParameter par = new SqlParameter("@param1", SqlDbType.VarChar, 250);
                par.Direction = ParameterDirection.Output;
                par.Value = null;
                cmd.Parameters.Add(par);
                var result = cmd.ExecuteScalar();
                Assert.AreEqual(DBNull.Value, result);
                Assert.AreEqual("changed by procedure", cmd.Parameters["@param1"].Value);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        public void No_Parameter()
        {
            using (SqlConnection con = new SqlConnection(bldr.ConnectionString))
            using (SqlCommand cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();

                cmd.CommandText = "usp_Output_No_Default";
                var result = cmd.ExecuteScalar();
                //Assert.AreEqual(DBNull.Value, result);
                //Assert.AreEqual("changed by procedure", cmd.Parameters["@param1"].Value);
            }
        }
    }
}
