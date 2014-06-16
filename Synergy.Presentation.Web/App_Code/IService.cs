using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;


[ServiceContract]
public interface IService
{

    [OperationContract]
    [WebGet(UriTemplate = "NewUri/?value={value}", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
    string GetData(int value);
    [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
    [OperationContract]
    List<Student> GetStudentList();


}
[DataContract]
public class Student
{
    [DataMember]
    public int StudentId { get; set; }
    [DataMember]
    public string StudentName { get; set; }
    [DataMember]
    public int Marks1 { get; set; }
    [DataMember]
    public int Marks2 { get; set; }
    [DataMember]
    public string EmailAddress { get; set; }

}


