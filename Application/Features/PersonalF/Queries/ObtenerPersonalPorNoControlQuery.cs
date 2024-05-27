using Application.DTOs;
using Application.Interfaces;
using Application.Responses;
using MediatR;

namespace Application.Features.PersonalF.Queries
{
    public class ObtenerPersonalPorNoControlQuery : IRequest<ApiResponse<UpdatePersonalDTO>>
    {
        public string NumeroControl { get; set; } = null!;
    }

    public class ObtenerPersonalPorNoControlQueryHandler : IRequestHandler<ObtenerPersonalPorNoControlQuery, ApiResponse<UpdatePersonalDTO>>
    {
        private readonly IRepositorioPersonal _repositorioPersonal;

        public ObtenerPersonalPorNoControlQueryHandler(IRepositorioPersonal repositorioPersonal)
        {
            _repositorioPersonal = repositorioPersonal;
        }

        public async Task<ApiResponse<UpdatePersonalDTO>> Handle(ObtenerPersonalPorNoControlQuery request, CancellationToken cancellationToken)
        {
            var dto = await _repositorioPersonal.ObtenerPersonalPorNoControl(request.NumeroControl);

            return new ApiResponse<UpdatePersonalDTO>(dto);
        }
    }
}
