namespace Door2Door_API.ExceptionTypes;

public class RouteBuildingException : Exception
{
    public RouteBuildingException(string message) : base(message) { }

    public RouteBuildingException(string message, Exception inner) : base(message, inner) {}

}