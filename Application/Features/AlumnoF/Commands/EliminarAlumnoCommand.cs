using Application.Exceptions;
using Application.Interfaces;
using Application.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AlumnoF.Commands
{
    public class EliminarAlumnoCommand : IRequest<ApiResponse<string>>
    {
        public int IdAlumno { get; set; }
    }

    public class EliminarAlumnoCommandHandler : IRequestHandler<EliminarAlumnoCommand, ApiResponse<string>>
    {
        private readonly IRepositorio<Alumno> _repositorio;

        public EliminarAlumnoCommandHandler(IRepositorio<Alumno> repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ApiResponse<string>> Handle(EliminarAlumnoCommand request, CancellationToken cancellationToken)
        {
            Alumno? alumno = await _repositorio.ObtenerPorId(request.IdAlumno);

            if (alumno == null)
            {
                throw new ApiException("El alumno no fue encontrado.");
            }

            await _repositorio.Eliminar(alumno);

            return new ApiResponse<string>("¡Alumno eliminado correctamente!", "Eliminado");
        }
    }
}
