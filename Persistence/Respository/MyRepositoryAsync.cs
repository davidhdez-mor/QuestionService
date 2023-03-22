using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Persistence.Context;

namespace Domain.Respository
{
    public class MyRepositoryAsync<T>: RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly QuestionContext _context;
        public MyRepositoryAsync(QuestionContext context): base(context) 
        {
            _context = context;
        }
    }
}
