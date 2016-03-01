// using Xunit;
// using System.Collections.Generic;
// using System;
// using System.Data;
// using System.Data.SqlClient;
//
// namespace CollegeNameSpace
// {
//   public class CourseTest : IDisposable
//   {
//     public CourseTest()
//     {
//       DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=college_test;Integrated Security=SSPI;";
//     }
//     [Fact]
//     public void Test_AddStudent_AddsStudentToCourse()
//     {
//       //Arrange
//       Course testCourse = new Course("Math", 101);
//       testCourse.Save();
//
//       Student firstStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
//       testStudent.Save();
//
//       Student firstStudent = new Student("Magic James", new DateTime(2016, 01, 01));
//       testStudent2.Save();
//
//       //Act
//       testCourse.AddStudent(testStudent);
//       testCourse.AddStudent(testStudent2);
//
//       List<Student> result = testCourse.GetStudents();
//       List<Student> testList = new List<Student>{testStudent, testStudent2};
//
//       //Assert
//       Assert.Equal(testList, result);
//     }
//
//         [Fact]
//     public void Test_GetStudents_ReturnsAllCourseStudents()
//     {
//       //Arrange
//       Course testCourse = new Course("Science", 101);
//       testCourse.Save();
//
//       Student firstStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
//       testStudent1.Save();
//
//       Student firstStudent = new Student("Magic James", new DateTime(2016, 01, 01));
//       testStudent2.Save();
//
//       //Act
//       testCourse.AddStudent(testStudent1);
//       List<Student> savedStudents = testCourse.GetStudents();
//       List<Student> testList = new List<Student> {testStudent1};
//
//       //Assert
//       Assert.Equal(testList, savedStudents);
//     }
//     [Fact]
//     public void Test_CategoriesEmptyAtFirst()
//     {
//       //Arrange, Act
//       int result = Course.GetAll().Count;
//
//       //Assert
//       Assert.Equal(0, result);
//     }
//
//     [Fact]
//     public void Test_Equal_ReturnsTrueForSameName()
//     {
//       //Arrange, Act
//       Course testCourse = new Course("Science", 101);
//       Course secondCourse = new Course("Math", 101);
//
//       //Assert
//       Assert.Equal(firstCourse, secondCourse);
//     }
//
//     [Fact]
//     public void Test_Save_SavesCourseToDatabase()
//     {
//       //Arrange
//       Course testCourse = new Course("Science", 101);
//       testCourse.Save();
//
//       //Act
//       List<Course> result = Course.GetAll();
//       List<Course> testList = new List<Course>{testCourse};
//
//       //Assert
//       Assert.Equal(testList, result);
//     }
//
//     [Fact]
//     public void Test_Save_AssignsIdToCourseObject()
//     {
//       //Arrange
//       Course testCourse = new Course("Science", 101);
//       testCourse.Save();
//
//       //Act
//       Course savedCourse = Course.GetAll()[0];
//       int result = savedCourse.GetId();
//       int testId = testCourse.GetId();
//
//       //Assert
//       Assert.Equal(testId, result);
//     }
//
//     [Fact]
//     public void Test_Find_FindsCourseInDatabase()
//     {
//       //Arrange
//       Course testCourse = new Course("Science", 101);
//       testCourse.Save();
//
//       //Act
//       Course foundCourse = Course.Find(testCourse.GetId());
//
//       //Assert
//       Assert.Equal(testCourse, foundCourse);
//     }
//
//     [Fact]
//     public void Test_Update_UpdatesCourseInDatabase()
//     {
//       //Arrange
//       string name = "Home stuff";
//       Course testCourse = new Course("Science", 101);
//       testCourse.Save();
//       string newName = "Work stuff";
//
//       //Act
//       testCourse.Update(newName);
//
//       string result = testCourse.GetName();
//
//       //Assert
//       Assert.Equal(newName, result);
//     }
//     [Fact]
//     public void Test_AddCourse_AddsCourseToStudent()
//     {
//       //Arrange
//       Student firstStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
//       firstStudent.Save();
//
//       Student secondStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
//       secondStudent.Save();
//
//       //Act
//       firstStudent.AddCourse(testCourse);
//
//       List<Course> result = firstStudent.GetCategories();
//       List<Course> testList = new List<Course>{testCourse};
//
//       //Assert
//       Assert.Equal(testList, result);
//     }
//     [Fact]
//     public void Test_GetCategories_ReturnsAllStudentCategories()
//     {
//       //Arrange
//       Student testStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
//       testStudent.Save();
//
//       Course testCourse1 = new Course("Science", 101);
//       testCourse1.Save();
//
//       Course testCourse2 = new Course("Math", 101);
//       testCourse2.Save();
//
//       //Act
//       testStudent.AddCourse(testCourse1);
//       List<Course> result = testStudent.GetCategories();
//       List<Course> testList = new List<Course> {testCourse1};
//
//       //Assert
//       Assert.Equal(testList, result);
//     }
//     [Fact]
//         public void Test_Delete_DeletesCourseAssociationsFromDatabase()
//         {
//           //Arrange
//           Student testStudent = new Student("Magic Johnson", new DateTime(2016, 01, 01));
//           testStudent.Save();
//
//           string testName = "Home stuff";
//           Course testCourse = new Course(testName, 101);
//           testCourse.Save();
//
//           //Act
//           testCourse.AddStudent(testStudent);
//           testCourse.Delete();
//
//           List<Course> resultStudentCategories = testStudent.GetCategories();
//           List<Course> testStudentCategories = new List<Course> {};
//
//           //Assert
//           Assert.Equal(testStudentCategories, resultStudentCategories);
//         }
//     [Fact]
//     public void Test_Delete_DeletesCourseFromDatabase()
//     {
//       //Arrange
//       string name1 = "Soccer";
//       Course testCourse1 = new Course(name1, 101);
//       testCourse1.Save();
//
//       string name2 = "Dancing";
//       Course testCourse2 = new Course(name2, 101);
//       testCourse2.Save();
//
//       //Act
//       testCourse1.Delete();
//       List<Course> resultCategories = Course.GetAll();
//       List<Course> testCourseList = new List<Course> {testCourse2};
//
//       //Assert
//       Assert.Equal(testCourseList, resultCategories);
//     }
//
//     [Fact]
//     public void Dispose()
//     {
//       Student.DeleteAll();
//       Course.DeleteAll();
//     }
//   }
// }
