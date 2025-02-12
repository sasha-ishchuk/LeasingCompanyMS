namespace LeasingCompanyMS.Model.Repositories;

public interface IRepository<Type, in IdType, in FilterType> {
    void Add(Type t);
    
    Type? GetById(IdType id);
    List<Type> GetAll();

    List<Type> Get(FilterType filter);

    bool UpdateById(IdType id, Type t);
}