using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebCalendar.Services.Export.Contracts
{
    public interface IExportService
    {
        Task<byte[]> ExportCalendar(Guid id);
        Task<byte[]> ExportEvent(Guid id);
    }
}
