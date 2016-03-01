using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace CollegeNamespace
{
  public class Student
  {
    private int _id;
    private string _name;
    private DateTime _enrolldate;

    public Student(string Name, DateTime enrollDate, int Id = 0)
    {
      _id = Id;
      _name = Name;
      _enrolldate = enrollDate;
    }
    public override bool Equals(System.Object otherStudent)
    {
        if (!(otherStudent is Student))
        {
          return false;
        }
        else {
          Student newStudent = (Student) otherStudent;
          bool idEquality = this.GetId() == newStudent.GetId();
          bool nameEquality = this.GetName() == newStudent.GetName();
          bool dateequality = this.GetDate() == newStudent.GetDate();
          return (idEquality && nameEquality && dateequality);
        }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public DateTime GetDate()
    {
      return _enrolldate;
    }

    public static List<Student> GetAll()
    {
      List<Student> AllStudents = new List<Student>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentdate =rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, studentdate, studentId);
        AllStudents.Add(newStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllStudents;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students (name, enroll_date) OUTPUT INSERTED.id VALUES (@StudentName, @StudentDate)", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StudentName";
      nameParameter.Value = this.GetName();

      SqlParameter dateParameter = new SqlParameter();
      dateParameter.ParameterName = "@StudentDate";
      dateParameter.Value = this.GetDate();

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(dateParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    // public List<Course> GetCourses()
    // {
    //   SqlConnection conn = DB.Connection();
    //   SqlDataReader rdr = null;
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("SELECT course_id FROM student_course WHERE student_id = @StudentId;", conn);
    //
    //   SqlParameter studentIdParameter = new SqlParameter();
    //   studentIdParameter.ParameterName = "@StudentId";
    //   studentIdParameter.Value = this.GetId();
    //
    //   cmd.Parameters.Add(studentIdParameter);
    //
    //   rdr = cmd.ExecuteReader();
    //
    //   List<int> categoryIds = new List<int> {};
    //
    //   while (rdr.Read())
    //   {
    //     int categoryId = rdr.GetInt32(0);
    //     categoryIds.Add(categoryId);
    //   }
    //   if (rdr != null)
    //   {
    //     rdr.Close();
    //   }
    //
    //   List<Course> courses = new List<Course> {};
    //
    //   foreach (int categoryId in categoryIds)
    //   {
    //     SqlDataReader queryReader = null;
    //     SqlCommand categoryQuery = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
    //
    //     SqlParameter categoryIdParameter = new SqlParameter();
    //     categoryIdParameter.ParameterName = "@CourseId";
    //     categoryIdParameter.Value = categoryId;
    //     categoryQuery.Parameters.Add(categoryIdParameter);
    //
    //     queryReader = categoryQuery.ExecuteReader();
    //     while (queryReader.Read())
    //     {
    //       int thisCourseId = queryReader.GetInt32(0);
    //       string categoryName = queryReader.GetString(1);
    //       Course foundCourse = new Course(categoryName, thisCourseId);
    //       courses.Add(foundCourse);
    //     }
    //     if (queryReader != null)
    //     {
    //       queryReader.Close();
    //     }
    //   }
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    //   return courses;
    // }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM students;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId", conn);
      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = id.ToString();
      cmd.Parameters.Add(studentIdParameter);
      rdr = cmd.ExecuteReader();

      int foundStudentId = 0;
      string foundStudentName = null;
      DateTime founddate = new DateTime(1996-01-01);

      while(rdr.Read())
      {
        foundStudentId = rdr.GetInt32(0);
        foundStudentName = rdr.GetString(1);
        founddate = rdr.GetDateTime(2);
      }
      Student foundStudent = new Student(foundStudentName, founddate, foundStudentId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStudent;
    }

    // public void AddCourse(Course newCourse)
    // {
    //   SqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   SqlCommand cmd = new SqlCommand("INSERT INTO student_course (course_id, student_id) VALUES (@CourseId, @StudentId);", conn);
    //
    //   SqlParameter courseIdParameter = new SqlParameter();
    //   courseIdParameter.ParameterName = "@CourseId";
    //   courseIdParameter.Value = newCourse.GetId();
    //   cmd.Parameters.Add(courseIdParameter);
    //
    //   SqlParameter studentIdParameter = new SqlParameter();
    //   studentIdParameter.ParameterName = "@StudentId";
    //   studentIdParameter.Value = this.GetId();
    //   cmd.Parameters.Add(studentIdParameter);
    //
    //   cmd.ExecuteNonQuery();
    //
    //   if (conn != null)
    //   {
    //     conn.Close();
    //   }
    // }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id = @StudentId; DELETE FROM student_course WHERE student_id = @StudentId;", conn);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = this.GetId();

      cmd.Parameters.Add(studentIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
