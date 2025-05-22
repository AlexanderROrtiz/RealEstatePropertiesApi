using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.OwnersCommand
{
    public class UpdateOwnerCommand : IRequest<OwnerDto>
    {
        public OwnerDto Owner { get; }
        public UpdateOwnerCommand(OwnerDto owner) => Owner = owner;
    }
}
