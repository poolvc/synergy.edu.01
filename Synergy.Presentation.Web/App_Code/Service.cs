using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: If you change the class name "Service" here, you must also update the reference to "Service" in Web.config and in the associated .svc file.
public class Service : IService
{
    public string GetData(int value)
    {
        return string.Format("You entered: {0}", value*-1);
    }
    /// <summary>
    /// Return List of student.
    /// </summary>
    /// <returns></returns>
    public List<Student> GetStudentList()
    {

        List<Student> students = new List<Student>()
           {  new Student{StudentId=1,StudentName="S#001",Marks1=10,Marks2=20,EmailAddress="student1@st.com"},
               new Student{StudentId=2,StudentName="S#002",Marks1=9,Marks2=30, EmailAddress="student2@st.com"},
               new Student{StudentId=3,StudentName="S#003",Marks1=11,Marks2=20,EmailAddress="student3@st.com"},
               new Student{StudentId=4,StudentName="S#004",Marks1=20,Marks2=30,EmailAddress="student4@st.com"},
               new Student{StudentId=5,StudentName="S#005",Marks1=9,Marks2=40,EmailAddress="student5@st.com"},
               new Student{StudentId=6,StudentName="S#006",Marks1=30,Marks2=50,EmailAddress="student6@st.com"},
           };
        return students;

    }
}
