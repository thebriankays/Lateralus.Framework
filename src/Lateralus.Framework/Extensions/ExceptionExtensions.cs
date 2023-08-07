namespace Lateralus.Framework;

public static class ExceptionExtensions
{
    public static string ToString(this Exception exception, bool includeInnerException)
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (!includeInnerException)
            return exception.ToString();

        var sb = new StringBuilder();
        var currentException = exception;
        while (currentException != null)
        {
            sb.Append(currentException).AppendLine();
            currentException = currentException.InnerException;
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns the first Exception of type T that is in the stack trace.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static T GetByType<T>(this Exception ex) where T : Exception
    {
        if (ex is T)
        {
            return ex as T;
        }
        else if (ex.InnerException != null)
        {
            return ex.InnerException.GetByType<T>();
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Creates a simple summary of the exception and inner exceptions.
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public static string GetSummary(this Exception ex)
    {
        string summary = String.Format("{0}: {1}", ex.GetType().Name, ex.Message);
        summary += Environment.NewLine;
        summary += ex.StackTrace;

        if (ex.InnerException != null)
            summary += Environment.NewLine + ex.InnerException.GetSummary();

        return summary;
    }
}
