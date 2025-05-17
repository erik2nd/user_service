namespace UsersService.BLL.Exceptions;

public class BusinessException : Exception
{
    public override string Message => "Business exception occurred";
}