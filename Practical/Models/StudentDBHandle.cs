using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Practical.Models
{
    public class StudentDBHandle
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["Stud"].ToString();
            con = new SqlConnection(constring);
        }

        public bool AddStudent(Students s)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Stud_name", s.Stud_name);
            cmd.Parameters.AddWithValue("@Email", s.Email);
            cmd.Parameters.AddWithValue("@Phone", s.Phone);
            cmd.Parameters.AddWithValue("@Dept_id", s.Dept_id);
            cmd.Parameters.AddWithValue("@Course_id", s.Course_id);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<Students> GetStudent()
        {
            connection();
            List<Students> studentlist = new List<Students>();

            SqlCommand cmd = new SqlCommand("GetStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);


            foreach (DataRow dr in dt.Rows)
            {
                //getting department
                DataTable dt_dept = new DataTable();
                SqlDataAdapter dept_da = new SqlDataAdapter("select Dept_name from Tbl_Dept where Dept_id = " + dr["Dept_id"] + "", con);
                dept_da.Fill(dt_dept);
                string dept = dt_dept.Rows[0][0].ToString();

                //getting course
                SqlDataAdapter course_da = new SqlDataAdapter("select Course_name from Tbl_Course where Course_id = " + dr["Course_id"] + "", con);
                DataTable course_dept = new DataTable();
                course_da.Fill(course_dept);
                string course = course_dept.Rows[0][0].ToString();

                studentlist.Add(
                    new Students
                    {
                        Stud_ID = Convert.ToInt32(dr["Stud_ID"]),
                        Stud_name = Convert.ToString(dr["Stud_name"]),
                        Email = Convert.ToString(dr["Email"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Dept_id = Convert.ToString(dept),
                        Course_id = Convert.ToString(course)
                    });
            }
            con.Close();
            return studentlist;
        }

        public bool UpdateDetails(Students student)
        {
            connection();
            SqlCommand cmd = new SqlCommand("UpdateStudentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Stud_ID", student.Stud_ID);
            cmd.Parameters.AddWithValue("@Stud_name", student.Stud_name);
            cmd.Parameters.AddWithValue("@Email", student.Email);
            cmd.Parameters.AddWithValue("@Phone", student.Phone);
            cmd.Parameters.AddWithValue("@Dept_id", student.Dept_id);
            cmd.Parameters.AddWithValue("@Course_id", student.Course_id);


            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool DeleteStudent(int Stud_ID)
        {
            connection();
            SqlCommand cmd = new SqlCommand("DeleteStudent", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Stud_ID", Stud_ID);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<SelectListItem> getDropdownDept()
        {
            DataTable table = new DataTable();
            List<SelectListItem> list = new List<SelectListItem>();
            connection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Dept", con);
            da.Fill(table);
            con.Close();

            foreach (DataRow dr in table.Rows)
            {
                list.Add(new SelectListItem { Text = Convert.ToString(dr.ItemArray[1]), Value = Convert.ToString(dr.ItemArray[0]) });
            }
            return list;
        }

        public List<SelectListItem> getDropdownCourse(int id)
        {
            DataTable table = new DataTable();
            List<SelectListItem> list = new List<SelectListItem>();
            connection();
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("select * from Tbl_Course where Dept_id = " + id + "", con);
            da.Fill(table);
            con.Close();

            foreach (DataRow dr in table.Rows)
            {
                list.Add(new SelectListItem { Text = Convert.ToString(dr.ItemArray[1]), Value = Convert.ToString(dr.ItemArray[0]) });
            }
            return list;
        }

        public List<Students> GetStudentByDepartment(string departmentId)
        {
            connection();
            List<Students> studentlist = new List<Students>();

            SqlCommand cmd = new SqlCommand("GetStudentDetailsByDepartment", con); // Assuming you have a stored procedure for this
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeptID", departmentId); // Add department ID as a parameter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                // Same code as in GetStudent() to retrieve department and course
                //getting department
                DataTable dt_dept = new DataTable();
                SqlDataAdapter dept_da = new SqlDataAdapter("select Dept_name from Tbl_Dept where Dept_id = " + dr["Dept_id"] + "", con);
                dept_da.Fill(dt_dept);
                string dept = dt_dept.Rows[0][0].ToString();

                //getting course
                SqlDataAdapter course_da = new SqlDataAdapter("select Course_name from Tbl_Course where Course_id = " + dr["Course_id"] + "", con);
                DataTable course_dept = new DataTable();
                course_da.Fill(course_dept);
                string course = course_dept.Rows[0][0].ToString();

                studentlist.Add(
                    new Students
                    {
                        Stud_ID = Convert.ToInt32(dr["Stud_ID"]),
                        Stud_name = Convert.ToString(dr["Stud_name"]),
                        Email = Convert.ToString(dr["Email"]),
                        Phone = Convert.ToString(dr["Phone"]),
                        Dept_id = Convert.ToString(dept),
                        Course_id = Convert.ToString(course)
                    });
            }
            con.Close();
            return studentlist;
        }

    }
}