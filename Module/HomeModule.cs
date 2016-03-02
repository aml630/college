using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace CollegeNameSpace
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Student> allStudents = Student.GetAll();
        return View["index.cshtml", allStudents];
      };

      Post["/"] =_=> {
        Student newStudent = new Student(Request.Form["student"], Request.Form["studentDate"]);
        newStudent.Save();
        List<Student> newStudentList = new List<Student>{};
        newStudentList.Add(newStudent);
        List<Student> allStudents = Student.GetAll();
        return View["index.cshtml", allStudents];
      };

      Get["/student_courses/{id}"]= parameters =>{
        Dictionary<string, object> model = new Dictionary<string, object>();

        Student selectedStudent = Student.Find(parameters.id);
        List<Course> studentCourses = selectedStudent.GetCourses();
        model.Add("student", selectedStudent);
        model.Add("studentCourses", studentCourses);
        return View["course_list.cshtml", model];
      };

      Post["/student_courses/{id}"] = parameters => {

        Dictionary<string, object> model = new Dictionary<string, object>();

        Course newCourse = new Course(Request.Form["course"], Request.Form["courseNumber"]);
        newCourse.Save();

        List<Course> studentCourses = new List<Course>{};
        studentCourses.Add(newCourse);
        Student selectedStudent = Student.Find(parameters.id);
        selectedStudent.AddCourse(newCourse);
        List<Course> studentCoursesFinal = selectedStudent.GetCourses();
        model.Add("student", selectedStudent);
        model.Add("studentCourses", studentCoursesFinal);
        return View["course_list.cshtml", model];
      };

      Get["/course_students/{id}"]= parameters =>{
        Dictionary<string, object> model = new Dictionary<string, object>();

        Course selectedCourse = Course.Find(parameters.id);
        List<Student> CoursesStudents = selectedCourse.GetStudents();
        model.Add("course", selectedCourse);
        model.Add("CoursesStudents", CoursesStudents);
        return View["course_student_list.cshtml", model];
      };


      Get["/course_list"]= _ =>{
        List<Course> allCourses = Course.GetAll();
        return View["list_of_courses.cshtml", allCourses];
      };

      Post["/course_add"]= _ =>{
        List<Course> allCourses = Course.GetAll();

        Course newCourse = new Course(Request.Form["course"], Request.Form["courseNumber"]);
        newCourse.Save();

        allCourses.Add(newCourse);

        return View["list_of_courses.cshtml", allCourses];
      };

      // Get["/course_students_see/{id}"]= parameters =>{
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   Course selectedCourse = Course.Find(parameters.id);
      //   List<Student> CoursesStudents = selectedCourse.GetStudents();
      //   model.Add("course", selectedCourse);
      //   model.Add("CoursesStudents", CoursesStudents);
      //   return View["course_student_list_see.cshtml", model];
      // };



    }
  }
}
