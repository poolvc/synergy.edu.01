<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script src="../jsc/jquery-1.6.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        $().ready(function() {
            $("#btnStudent").click(function() {
                $.getJSON("../ws/Service.svc/GetStudentList", function(data) {
                   BuildTable(data);

                    });
                });
              

            $("#myButt").click(function() {
                var num = $("#name").val();
                $.getJSON("../ws/Service.svc/NewUri", { value: num }, function(data) {
                    alert(data);
                });
            });
        });
        function BuildTable(data) {
            var table = '<table><thead><tr><th>StudentID</th><th>StudentName</th><th>Marks1</th></thead><tbody>';

            $.each(data, function(i, student) {
                var row = '<tr>';
                row += '<td>' + student.StudentId + '</td>';
                row += '<td>' + student.StudentName + '</td>';
                row += '<td>' + student.Marks1 + '</td>';

                row += '</tr>';

                table += row;
            });

            table += '</tbody></table>';

            $('#Container').html(table);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <input type="button" value="Get values from server" id="myButt" />
        <input type="button" value="GetStudentList" id="btnStudent" />
        <p>
            <input type="text" id="name" value="1" /><br />
        </p>
    </div>
    <div id="Container">
    </div>
    </form>
</body>
</html>
