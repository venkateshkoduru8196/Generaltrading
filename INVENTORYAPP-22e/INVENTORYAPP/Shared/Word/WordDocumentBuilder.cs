using Xceed.Words.NET;

namespace INVENTORYAPP.Shared.Word;

public static class WordDocumentBuilder
{
    //--------------------------------------------------------
    // Create Document
    //--------------------------------------------------------

    public static DocX Create()
    {
        return DocX.Create(
            new MemoryStream());
    }
}