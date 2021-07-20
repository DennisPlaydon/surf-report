using System.Threading.Tasks;
using SurfReport.Services;

namespace SurfReport
{
    public class Function
    {
        static async Task Main(string[] args)
        {
            await Handle();
        }
        
        public async Task FunctionHandler()
        {
            await Handle();
        }

        private static async Task Handle()
        {
            var reportAnalyser = new SurfReportAnalyser();
            var idealSurfReports = await reportAnalyser.Analyse();

            var pushNotificationService = new PushNotificationService();
            var body = pushNotificationService.FormatReportBody(idealSurfReports);
            await pushNotificationService.Push("Good Surf Incoming", "note", body);
        }
    }
}