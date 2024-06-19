using System.Runtime.CompilerServices;

namespace Yomikaze.Domain;

public static class PocoLoadingExtensions
{
    public static TRelated Load<TRelated>(
        this Action<object, string>? loader,
        object entity,
        ref TRelated navigationField,
        [CallerMemberName] string navigationName = null!)
        where TRelated : class
    {
        loader?.Invoke(entity, navigationName);

        return navigationField;
    }

    public static TRelated? LoadNullable<TRelated>(
        this Action<object, string>? loader,
        object entity,
        ref TRelated? navigationField,
        [CallerMemberName] string navigationName = null!)
        where TRelated : class
    {
        loader?.Invoke(entity, navigationName);

        return navigationField;
    }
}