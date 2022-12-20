using MediatR;
using MyDoctor.Application.Response;
using MyDoctor.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.Application.Queries.MedicalRoomQueries
{
    public class GetMedicalRoomsQuery : IRequest<List<MedicalRoomResponse>>{}
}
