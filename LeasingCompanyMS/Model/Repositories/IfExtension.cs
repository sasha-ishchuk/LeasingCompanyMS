namespace LeasingCompanyMS.Model.Repositories;

public static class IfExtension {
    public static IEnumerable<TSource> If<TSource>(
        this IEnumerable<TSource> source,
        bool condition,
        Func<IEnumerable<TSource>, IEnumerable<TSource>> branch
    ) {
        return condition ? branch(source) : source;
    }
}