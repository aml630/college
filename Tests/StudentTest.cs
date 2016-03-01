using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CollegeNameSpace
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=college_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_EmptyAtFirst()
    {
      //Arrange, Act
      int result = Student.GetAll().Count;

      //Assert
      Assert.Equal(0, result);

      Student.DeleteAll();
      Course.DeleteAll();
    }

    [Fact]
    public void Test_EqualOverrideTrueForSameDescription()
    {
      //Arrange, Act
      Student firstStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
      Student secondStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));

      //Assert
      Assert.Equal(firstStudent, secondStudent);

      Student.DeleteAll();
      Course.DeleteAll();
    }

    [Fact]
    public void Test_Save()
    {
      //Arrange
      Student testStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
      testStudent.Save();

      //Act
      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      //Assert
      Assert.Equal(testList, result);

      Student.DeleteAll();
      Course.DeleteAll();
    }



    [Fact]
    public void Test_SaveAssignsIdToObject()
    {
      //Arrange
      Student testStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
      testStudent.Save();

      //Act
      Student savedStudent = Student.GetAll()[0];

      int result = savedStudent.GetId();
      int testId = testStudent.GetId();

      //Assert
      Assert.Equal(testId, result);

      Student.DeleteAll();
      Course.DeleteAll();
    }

    [Fact]
    public void Test_FindFindsStudentInDatabase()
    {
      //Arrange
      Student testStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
      testStudent.Save();

      //Act
      Student result = Student.Find(testStudent.GetId());

      //Assert
      Assert.Equal(testStudent, result);

      Student.DeleteAll();
      Course.DeleteAll();
    }

    [Fact]
    public void Test_AddCourse_AddsCourseToStudent()
    {
      //Arrange
      Student testStudent =new Student("Magic Johnson", new DateTime(2016, 01, 01));
      testStudent.Save();

      Course testCourse = new Course("Math", 101);
      testCourse.Save();

      testStudent.AddCourse(testCourse);

      //Act
      List<Course> result = testStudent.GetCourses();

      List<Course> testList = new List<Course>{testCourse};

      //Assert
      Assert.Equal(testList, result);

      Student.DeleteAll();
      Course.DeleteAll();
    }

  [Fact]
  public void Test_GetCourses_ReturnsAllStudentCourses()
  {
    //Arrange
    Student testStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
    testStudent.Save();

    Course testCourse1 = new Course("Math", 101);
    testCourse1.Save();

    Course testCourse2 = new Course("Gym", 101);
    testCourse2.Save();

    //Act
    testStudent.AddCourse(testCourse1);
    List<Course> result = testStudent.GetCourses();
    List<Course> testList = new List<Course> {testCourse1};

    //Assert
    Assert.Equal(testList, result);

    Student.DeleteAll();
    Course.DeleteAll();
  }

  public void Test_Delete_DeletesStudentAssociationsFromDatabase()
  {
    //Arrange
    Course testCourse = new Course("Math", 101);
    testCourse.Save();

    Student testStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
    testStudent.Save();

    //Act
    testStudent.AddCourse(testCourse);
    testStudent.Delete();

    List<Student> resultCourseStudents = testCourse.GetStudents();
    List<Student> testCourseStudents = new List<Student> {};

    //Assert
    Assert.Equal(testCourseStudents, resultCourseStudents);

    Student.DeleteAll();
    Course.DeleteAll();
  }


    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
  }
}
