using Microsoft.AspNetCore.Identity;

namespace FitnessAI.Infrastructure.Identity;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    private const string PASSWORD = "Password";
    private const string EMAIL = "Email";

    public override IdentityError PasswordMismatch()
    {
        return new IdentityError
        {
            Code = PASSWORD,
            Description = "Hasło i Potwierdzone hasło są różne."
        };
    }

    public override IdentityError PasswordRequiresDigit()
    {
        return new IdentityError
        {
            Code = PASSWORD,
            Description = "Hasło musi zawierać przynajmniej 1 cyfrę."
        };
    }

    public override IdentityError PasswordRequiresLower()
    {
        return new IdentityError
        {
            Code = PASSWORD,
            Description = "Hasło musi zawierać przynajmniej 1 małą literę."
        };
    }

    public override IdentityError PasswordRequiresNonAlphanumeric()
    {
        return new IdentityError
        {
            Code = PASSWORD,
            Description = "Hasło musi zawierać przynajmniej 1 znak specjalny (np. $, #, & itp.)."
        };
    }

    public override IdentityError PasswordRequiresUpper()
    {
        return new IdentityError
        {
            Code = PASSWORD,
            Description = "Hasło musi zawierać przynajmniej 1 dużą literę."
        };
    }

    public override IdentityError PasswordTooShort(int length)
    {
        return new IdentityError
        {
            Code = PASSWORD,
            Description = "Hasło musi mieć co najmniej 8 znaków i nie więcej niż 100 znaków."
        };
    }

    public override IdentityError DuplicateUserName(string userName)
    {
        return new IdentityError
        {
            Code = EMAIL,
            Description = "Wybrany email jest już zajęty."
        };
    }
}