using MediatR;
using RealEstate.Properties.Application.DTOs;

namespace RealEstate.Properties.Application.Commands.OwnersCommand
{
    public class CreateOwnerCommand : IRequest<OwnerDto>
    {
        public OwnerDto Owner { get; }
        public CreateOwnerCommand(OwnerDto owner) => Owner = owner;
    }
}
