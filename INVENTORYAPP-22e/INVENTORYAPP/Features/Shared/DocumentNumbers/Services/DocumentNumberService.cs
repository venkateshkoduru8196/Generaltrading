using INVENTORYAPP.Features.Shared.DocumentNumbers.Interfaces;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Shared.DocumentNumbers.Services;

public class DocumentNumberService : IDocumentNumberService
{
    private readonly IDocumentSequenceRepository _repository;

    public DocumentNumberService(
        IDocumentSequenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> GenerateAsync(
      int companyId,
      string moduleCode)
    {
        var sequence = await _repository.GetByModuleAsync(
            companyId,
            moduleCode);

        if (sequence == null)
            throw new Exception(
                $"Document sequence not found for company '{companyId}' and module '{moduleCode}'.");

        sequence.CurrentNumber++;

        await _repository.SaveChangesAsync();

        return FormatDocumentNumber(sequence);
    }

    private static string FormatDocumentNumber(DocumentSequence sequence)
    {
        var number = sequence.CurrentNumber
            .ToString()
            .PadLeft(sequence.Digits, '0');

        if (string.IsNullOrWhiteSpace(sequence.Separator))
        {
            return $"{sequence.Prefix}{number}";
        }

        return $"{sequence.Prefix}{sequence.Separator}{number}";
    }
}