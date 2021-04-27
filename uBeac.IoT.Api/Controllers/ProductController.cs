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
        public async Task<IResultSet<Guid>> Add([Body] ProductAddRequest model, CancellationToken cancellationToken = default)
        {
            // todo: check if manufacturer exists
            var product = _mapper.Map<Product>(model);
            await _service.Insert(product, cancellationToken);
            return product.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Update([Body] ProductUpdateRequest model, CancellationToken cancellationToken = default)
        {
            // todo: check if manufacturer exists
            var product = _mapper.Map<Product>(model);
            var isUpdated = (await _service.Replace(product, cancellationToken)) != null;
            return isUpdated.ToResultSet();
        }

        [PostById]
        public async Task<IResultSet<bool>> Remove([Body] Guid id, CancellationToken cancellationToken = default)
        {
            // todo: check if it has child firmware
            var isDeleted = await _service.Delete(id, cancellationToken);
            return isDeleted.ToResultSet();
        }

    }
}
