namespace Zetacean.BETEAP.Students.Services
{
    public interface ICommonService<T, TInsert, TUpdate>
    {
        public IList<string> Errors { get; }
        Task<IEnumerable<T>> Get();
        Task<T> GetById(int id);
        Task<T> Add(TInsert insertDto);
        Task<T> Update(int id, TUpdate updateDto);
        Task<T> Delete(int id);
        bool Validate(TInsert dto);
        bool Validate(int id, TUpdate dto);
    }
}
