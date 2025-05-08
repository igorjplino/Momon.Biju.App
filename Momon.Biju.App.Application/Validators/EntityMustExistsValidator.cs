using FluentValidation;
using Momon.Biju.App.Domain.Entities;
using Momon.Biju.App.Domain.Interfaces.Repositories;

namespace Momon.Biju.App.Application.Validators;

public class EntityMustExistsValidator<T> : AbstractValidator<Guid> where T : BaseEntity
{
    public EntityMustExistsValidator(IBaseRepository<T> baseRepository)
    {
        RuleFor(x => x)
            .MustAsync(async (id, ct) =>
            {
                T? entity = await baseRepository.GetAsync(id);

                return entity is not null;
            });
    }
}