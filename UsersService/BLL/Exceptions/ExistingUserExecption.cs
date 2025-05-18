namespace UsersService.BLL.Exceptions;

public class ExistingUserExecption : BusinessException
{
    public override string Message => "User with this login already exists";
}