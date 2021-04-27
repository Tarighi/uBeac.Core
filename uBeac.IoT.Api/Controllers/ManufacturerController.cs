using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using uBeac.Core.Web;
using uBeac.IoT.Api.DTO;
using uBeac.IoT.Models;
using uBeac.IoT.Services;

namespace uBeac.IoT.Api.Controllers
{
    public class ManufacturerController : BaseController
    {
        private readonly IManufacturerService _mfService;
        private readonly IProductService _prService;
        private readonly IFirmwareService _fmService;
        private readonly IMapper _mapper;
        public ManufacturerController(IMapper mapper,
            IManufacturerService mfService,
            IProductService prService,
            IFirmwareService fmService)
        {
            _mfService = mfService;
            _prService = prService;
            _fmService = fmService;
            _mapper = mapper;
        }

        [Post]
        public async Task<IResultSet<Guid>> Add([Body] ManufacturerAddRequestDTO dto, CancellationToken cancellationToken = default)
        {
            var model = _mapper.Map<Manufacturer>(dto);
            await _mfService.Insert(model, cancellationToken);
            return model.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Update([Body] ManufacturerUpdateRequestDTO dto, CancellationToken cancellationToken = default)
        {
            var model = _mapper.Map<Manufacturer>(dto);
            var isUpdated = (await _mfService.Replace(model, cancellationToken)) != null;
            return isUpdated.ToResultSet();
        }

        [PostById]
        public async Task<IResultSet<bool>> Remove([Body] Guid id, CancellationToken cancellationToken = default)
        {
            // todo: check if it has child products
            var isDeleted = await _mfService.Delete(id, cancellationToken);
            return isDeleted.ToResultSet(); ;
        }

        [Get]
        public async Task<IListResultSet<ManufacturerViewDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            var manufacturersTask = _mfService.GetAll(cancellationToken);
            var productsTask = _prService.GetAll(cancellationToken);
            var firmwaresTask = _fmService.GetAll(cancellationToken);

            await Task.WhenAll(manufacturersTask, productsTask, firmwaresTask);

            var manufacturersVm = _mapper.Map<List<ManufacturerViewDTO>>(manufacturersTask.Result.ToList());
            var productsVm = _mapper.Map<List<ProductViewDTO>>(productsTask.Result.ToList());
            var firmwaresVm = _mapper.Map<List<FirmwareViewDTO>>(firmwaresTask.Result.ToList());

            // todo: this should be moved to service or repository?
            manufacturersVm.ForEach(mft =>
            {
                mft.Products = productsVm.Where(x => x.ManufacturerId == mft.Id).ToList();
                mft.Products.ForEach(prd => prd.Firmwares = firmwaresVm.Where(x => x.ProductId == prd.Id).ToList());
            });

            return manufacturersVm.ToListResultSet();
        }
    }
}
