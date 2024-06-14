using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace FocusPro.Models
{
    public class DalEmployee
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ToString());
        public List<StatesDO> GetStates()
            {
            List<StatesDO> liStatesDO = new List<StatesDO>();
            string query = "Select * from States";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr =cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    StatesDO sd = new StatesDO();
                    sd.StateId = Convert.ToInt32(dr[0].ToString());
                    sd.StateName = dr[1].ToString();
                    liStatesDO.Add(sd);
                }
            }
            conn.Close();
            return liStatesDO;
        }
        public List<CitiesDO> GetCitiesWithStateName(string stateName)
        {
            List<CitiesDO> liCitiesDO = new List<CitiesDO>();
            string query = "select CT.CityId,CT.CityName from Cities CT inner join States ST on CT.StateId=ST.StateId Where ST.StateName='" + stateName + "'";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr=cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    CitiesDO cd = new CitiesDO();
                    cd.CityId = Convert.ToInt32(dr[0].ToString());
                    cd.CityName = dr[1].ToString();
                    liCitiesDO.Add(cd);
                }
            }
            conn.Close();
            return liCitiesDO;
        }
        public List<EmployeeDO> GetEmployeeDetails()
        {
            List<EmployeeDO> liemployeeDOs = new List<EmployeeDO>();
            string query = "select * from Employee";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr=cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    EmployeeDO employeeDO = new EmployeeDO();
                    employeeDO.EmployeeId = Convert.ToInt32(dr[0].ToString());
                    employeeDO.EmployeeName = dr[1].ToString();
                    employeeDO.Gender = dr[2].ToString();
                    employeeDO.Hobbies = dr[3].ToString();
                    employeeDO.StateName = dr[4].ToString();
                    employeeDO.CityName = dr[5].ToString();
                    liemployeeDOs.Add(employeeDO);
                }
            }
            conn.Close();
            return liemployeeDOs;

        }
        public EmployeeDO GetEmployeeDetailsWithId(int empId)
        {
            string query = "Select * from Employee where EmployeeId=" + empId + "";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr=cmd.ExecuteReader();
            EmployeeDO employeeDO = new EmployeeDO();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    employeeDO.EmployeeId = Convert.ToInt32(dr[0].ToString());
                    employeeDO.EmployeeName = dr[1].ToString();
                    employeeDO.Gender = dr[2].ToString();
                    employeeDO.Hobbies = dr[3].ToString();
                    employeeDO.StateName = dr[4].ToString();
                    employeeDO.CityName = dr[5].ToString();
                    
                }
            }
            conn.Close();
            return employeeDO;
        }
        public string InsertOrUpdateEmployee(EmployeeDO emp,string type)
        {
            string query = "";
            if(type=="insert")
            {
                query = "Insert into Employee(EmployeeName, Gender, Hobbies, StateName, CityName) " +
                    "values('" + emp.EmployeeName + "','" + emp.Gender + "','" + emp.Hobbies + "','" + emp.StateName + "','" + emp.CityName + "')";
            }
            else if(type == "update")
            {
                query = "Update Employee Set EmployeeName='" + emp.EmployeeName + "',Gender='" + emp.Gender + "',Hobbies='" + emp.Hobbies + "',StateName='" + emp.StateName + "',CityName='" + emp.CityName + "' where EmployeeId='" + emp.EmployeeId + "'";
            }
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            int i= cmd.ExecuteNonQuery();
            conn.Close();
            string result = "";
            if(type=="insert" && i>0)
            {
                result = "insert success";
            }
            else if(type=="update"&& i>0)
            {
                result = "update success";
            }
            else
            {
                return result = "Falied";
            }
            return result;
        }
        public void DeleteEmployeeWithId(int employeeId)
        {
            string query="delete Employee Where EmployeeId="+employeeId+"";
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public bool Login(int employeeId,string employeeName)
        {
            string query = "select count(*) from employee where EmployeeId=@EmployeeId and EmployeeName=@EmployeeName";
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
            cmd.Parameters.AddWithValue("@EmployeeName", employeeName);
            int count=Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            return count > 0;
        }
        public bool ExistsEmployee(string empName)
        {
            conn.Open();
            string query = "select count(*) from Employee Where EmployeeName=@EmployeeName";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@EmployeeName", empName);
            int count = (int)cmd.ExecuteScalar();
            conn.Close();
            return count > 0;
         }
         
    }
 }