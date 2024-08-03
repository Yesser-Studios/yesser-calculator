namespace YesserCalculator.Extension.Exceptions;

public class UserCancelledException : Exception
{
    public UserCancelledException() : base("User cancelled operation.") { }

    public UserCancelledException(string msg) : base(msg) {}
}