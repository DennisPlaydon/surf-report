using System.Threading.Tasks;
using SurfReport.Services;

namespace SurfReport
{
    public class Function
    {
        public async Task FunctionHandler()
        {
            var reportAnalyser = new SurfReportAnalyser();
            var idealSurfReports = await reportAnalyser.Analyse();

            var pushNotificationService = new PushNotificationService();
            var body = pushNotificationService.FormatReportBody(idealSurfReports);
            await pushNotificationService.Push("Good Surf Incoming", "note", body);
        }
    }
}