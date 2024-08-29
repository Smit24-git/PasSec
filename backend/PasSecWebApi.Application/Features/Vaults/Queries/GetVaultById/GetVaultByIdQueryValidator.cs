using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasSecWebApi.Application.Features.Vaults.Queries.GetVaultById
{
    public class GetVaultByIdQueryValidator : AbstractValidator<GetVaultByIdQuery>
    {
        public GetVaultByIdQueryValidator()
        {
            RuleFor(x => x.VaultId)
                .NotNull()
                .NotEmpty();
        }
    }
}
