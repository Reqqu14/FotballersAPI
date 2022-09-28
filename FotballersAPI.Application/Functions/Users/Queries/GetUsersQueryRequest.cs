using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FotballersAPI.Application.Functions.Users.Queries
{
    public class GetUsersQueryRequest : IRequest<GetUsersQueryResponse>
    {
    }
}
