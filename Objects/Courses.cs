using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace CollegeNameSpace
{
  public class Course
  {
    private int _id;
    private string _title;
    private int _number;

    public Course(string Title, int Number, int Id = 0)
    {
      _id = Id;
      _title = Title;
      _number = Number;
    }

    public override bool Equals(System.Object otherCourse)
    {
        if (!(otherCourse is Course))
        {
          return false;
        }
        else
        {
          Course newCourse = (Course) otherCourse;
          bool idEquality = this.GetId() == newCourse.GetId();
          bool titleEquality = this.GetTitle() == newCourse.GetTitle();
          bool numberEquality = this.GetNumber() == newCourse.GetNumber();
          return (idEquality && titleEquality);
        }
    }

    public int GetId()
    {
      return _id;
    }
    public string GetTitle()
    {
      return _title;
    }
    public void SetTitle(string newTitle)
    {
      _title = newTitle;
    }
    public int GetNumber()
    {
      return _number;
    }

    public void AddStudent(Student newStudent)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO student_course (course_id, student_id) VALUES (@CourseId, @StudentId)", conn);

      SqlParameter CourseIdParameter = new SqlParameter();
      CourseIdParameter.ParameterName = "@CourseId";
      CourseIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CourseIdParameter);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = newStudent.GetId();
      cmd.Parameters.Add(studentIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int CourseId = rdr.GetInt32(0);
        string CourseTitle= rdr.GetString(1);
        int CourseNumber = rdr.GetInt32(2);
        Course newCourse = new Course(CourseTitle, CourseNumber, CourseId);
        allCourses.Add(newCourse);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allCourses;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (title, number) OUTPUT INSERTED.id VALUES (@CourseTitle, @CourseNumber );", conn);

      SqlParameter titleParameter = new SqlParameter();
      titleParameter.ParameterName = "@CourseTitle";
      titleParameter.Value = this.GetTitle();
      cmd.Parameters.Add(titleParameter);

      SqlParameter numberParameter = new SqlParameter();
      numberParameter.ParameterName = "@CourseNumber";
      numberParameter.Value = this.GetNumber();
      cmd.Parameters.Add(numberParameter);

      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
      cmd.ExecuteNonQuery();
    }

    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);

      SqlParameter CourseIdParameter = new SqlParameter();
      CourseIdParameter.ParameterName = "@CourseId";
      CourseIdParameter.Value = id.ToString();
      cmd.Parameters.Add(CourseIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCourseId = 0;
      string foundCourseTitle = null;
      int foundCourseNumber = 0;

      while(rdr.Read())
      {
        foundCourseId = rdr.GetInt32(0);
        foundCourseTitle = rdr.GetString(1);
        foundCourseNumber = rdr.GetInt32(2);
      }
      Course foundCourse = new Course(foundCourseTitle, foundCourseNumber, foundCourseId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCourse;
    }

    public List<Student> GetStudents()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      List<Student> students = new List<Student>{};

      SqlCommand cmd = new SqlCommand("SELECT students. * from courses join student_course on (courses.id = student_course.course_id) join students on (student_course.student_id = students.id) where courses.id = @CourseId;", conn);
      SqlParameter CourseIdParameter = new SqlParameter();

      CourseIdParameter.ParameterName = "@CourseId";
      CourseIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CourseIdParameter);

      rdr = cmd.ExecuteReader();

      List<int> studentIds = new List<int> {};
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        DateTime studentEnrollDate = rdr.GetDateTime(2);
        Student newStudent = new Student(studentName, studentEnrollDate, studentId);
        students.Add(newStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }


      return students;
    }

    public void Update(string newTitle)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE courses SET title = @NewTitle OUTPUT INSERTED.title WHERE id = @CourseId;", conn);

      SqlParameter newTitleParameter = new SqlParameter();
      newTitleParameter.ParameterName = "@NewTitle";
      newTitleParameter.Value = newTitle;
      cmd.Parameters.Add(newTitleParameter);


      SqlParameter CourseIdParameter = new SqlParameter();
      CourseIdParameter.ParameterName = "@CourseId";
      CourseIdParameter.Value = this.GetId();
      cmd.Parameters.Add(CourseIdParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._title = rdr.GetString(0);
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
    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses WHERE id = @CourseId; DELETE FROM student_course WHERE Course_id = @CourseId;", conn);
      SqlParameter CourseIdParameter = new SqlParameter();
      CourseIdParameter.ParameterName = "@CourseId";
      CourseIdParameter.Value = this.GetId();

      cmd.Parameters.Add(CourseIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
