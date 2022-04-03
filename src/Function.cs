using System.Threading.Tasks;
using SurfReport.Services;

namespace SurfReport
{
    public class Function
    {
        static async Task Main()
        {
            var metServiceApi = new MetServiceApi();
            await Handle(metServiceApi);
        }
        
        public async Task FunctionHandler()
        {
            var metServiceApi = new MetServiceApi();
            await Handle(metServiceApi);
        }

        private static async Task Handle(MetServiceApi metServiceApi)
        {
            var surfReporter = new SurfReporter(metServiceApi);
            var idealSurfReports = await surfReporter.Fetch();

            var pushNotificationService = new PushNotificationService();
            var body = pushNotificationService.FormatReportBody(idealSurfReports);
            await pushNotificationService.Push("Good Surf Incoming", "note", body);
        }
    }
}