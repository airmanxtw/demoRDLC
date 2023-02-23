using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
namespace demoRDLC.Controllers;
[ApiController]
[Route("[controller]")]
public class RDLC : ControllerBase
{
    [HttpGet]
    public FileResult Get()
    {
        var bytes = System.IO.File.ReadAllBytes("./Report.rdlc");
        Stream reportDefinition = new MemoryStream(bytes);
        List<Model.data> dataSource = new List<Model.data>();
        dataSource.Add(new Model.data { EmployeeID = 40 });

        LocalReport report = new LocalReport();
        report.LoadReportDefinition(reportDefinition);
        report.DataSources.Add(new ReportDataSource("DataSet1", dataSource));
        var pdf = report.Render("PDF");
        
        return File(pdf," application/pdf ");
    }
}