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

    }
  }
}
