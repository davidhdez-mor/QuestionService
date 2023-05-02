using Ardalis.Specification;
using Domain.Entities;

namespace Application.Specification;

public class PagedQuestionSpecification : Specification<Question>
{
    public PagedQuestionSpecification(int pageSize, int pageNumber, string description, byte? order ,
        string tags)
    {
        Query.Skip((pageNumber - 1)* pageSize)
            .Take(pageSize);
        
        if(!string.IsNullOrEmpty(description))
            Query.Search(x => x.Description, "%" + description + "%");

        if (!string.IsNullOrEmpty(order.ToString()))
            Query.Search(x => x.Order.ToString(), "%" + order + "%");

        if (!string.IsNullOrEmpty(tags))
            Query.Search(x => x.Tags, "%" + tags + "%");
        
    }
}