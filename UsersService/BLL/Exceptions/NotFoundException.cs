namespace UsersService.BLL.Exceptions;

public class NotFoundException : BusinessException
{
    public override string Message => "User not found";
}