using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Web;
using uBeac.IoT.Api.DTO;
using uBeac.IoT.Models;
using uBeac.IoT.Services;

namespace uBeac.IoT.Api.Controllers
{
    public class ProductController : BaseController
    {

        private readonly IProductService _service;
        private readonly IMapper _mapper;

        public ProductController(IMapper mapper, IProductService service)
        {
            _service = service;
            _mapper = mapper;
        }

        [Post]
        public async Task<IResultSet<Guid>> Add([Body] ProductAddRequestDTO dto, CancellationToken cancellationToken = default)
        {
            // todo: check if manufacturer exists
            var model = _mapper.Map<Product>(dto);
            await _service.Insert(model, cancellationToken);
            return model.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Update([Body] ProductUpdateRequestDTO dto, CancellationToken cancellationToken = default)
        {
            // todo: check if manufacturer exists
            var model = _mapper.Map<Product>(dto);
            var isUpdated = (await _service.Replace(model, cancellationToken)) != null;
            return isUpdated.ToResultSet();
        }

        [PostById]
        public async Task<IResultSet<bool>> Remove([Body] Guid id, CancellationToken cancellationToken = default)
        {
            // todo: check if it has child firmware
            var isDeleted = await _service.Delete(id, cancellationToken);
            return isDeleted.ToResultSet(); ;
        }

    }
}
