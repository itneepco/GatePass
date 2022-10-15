using GatePass.Core.PassAggregate;

namespace GatePass.UI.Services
{
    public interface IGeneratePDFService
    {
        public byte[] GenerateSinglePassPdf(SinglePass singlePass);
        public byte[] GenerateMultiPassPdf(MultiplePass multiplePass);
    }
}
