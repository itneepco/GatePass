using GatePass.Core.PassAggregate;

namespace GatePass.UI.Services
{
    public interface ISinglePassPDFService
    {
        public Task<byte[]> GenerateSinglePassPdf(SinglePass singlePass);
    }
}
