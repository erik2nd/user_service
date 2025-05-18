namespace UsersService.BLL.Exceptions;

public class ForbiddenException : BusinessException
{
    public override string Message => "User is not admin or not active";
}
