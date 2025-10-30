using WebLibrary.API.Contracts.Contracts.Authors.Responses;

namespace WebLibrary.API.Contracts.Contracts.Books.Responses;

public class GetBookResponse
{
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public int PublishedYear { get; set; }

    public List<GetAuthorResponse> Authors { get; set; } = [];
}