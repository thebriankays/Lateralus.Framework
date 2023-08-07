namespace Lateralus.Framework;

public abstract class ObjectGraphVisitor
{
    [RequiresUnreferencedCode("ObjectGraphVisitor uses reflection")]
    public void Visit(object? obj)
    {
        if (obj is null)
            return;

        var hashSet = new HashSet<object>();
        Visit(hashSet, obj);
    }

    [RequiresUnreferencedCode("ObjectGraphVisitor uses reflection")]
    private void Visit(HashSet<object> visitedObjects, object? obj)
    {
        if (obj is null)
            return;

        if (!visitedObjects.Add(obj))
            return;

        VisitValue(obj);

        if (obj is string) // string is IEnumerable, and we don't want to visit it
            return;

        if (obj is IEnumerable enumerable)
        {
            foreach (var item in enumerable)
            {
                Visit(visitedObjects, item);
            }
        }
        else
        {
            var type = obj.GetType();
            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!prop.CanRead)
                    continue;

                var propValue = prop.GetValue(obj);
                VisitProperty(obj, prop, propValue);
                Visit(visitedObjects, propValue);
            }
        }
    }

    protected virtual void VisitProperty(object parentInstance, PropertyInfo property, object? propertyValue)
    {
    }

    protected virtual void VisitValue(object value)
    {
    }
}
