namespace MusicApp.Contracts;


public class Error
{
    public string Message { get; set; }
    public string Field { get; set; }
}

public class Errors
{
    public List<Error> ErrorsMessages { get; set; }
}