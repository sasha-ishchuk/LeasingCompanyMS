namespace LeasingCompanyMS.Model.Repositories;

public interface IRepository<Type, in IdType, in FilterType> {
    Type? GetById(IdType id);
    List<Type> GetAll();

    List<Type> Get(FilterType filter);

    void Add(Type t);
}