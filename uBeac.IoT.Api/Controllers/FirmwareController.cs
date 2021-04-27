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
    public class FirmwareController : BaseController
    {
        private readonly IFirmwareService _service;
        private readonly IMapper _mapper;
        public FirmwareController(IMapper mapper, IFirmwareService service) 
        {
            _service = service;
            _mapper = mapper;
        }

        [Post]
        public async Task<IResultSet<Guid>> Add([Body] FirmwareAddRequest model, CancellationToken cancellationToken = default)
        {
            // todo: check if product exists
            var firmware = _mapper.Map<Firmware>(model);
            await _service.Insert(firmware, cancellationToken);
            return firmware.Id.ToResultSet();
        }

        [Post]
        public async Task<IResultSet<bool>> Update([Body] FirmwareUpdateRequest model, CancellationToken cancellationToken = default)
        {
            // todo: check if product exists
            var firmware = _mapper.Map<Firmware>(model);
            var isUpdated = (await _service.Replace(firmware, cancellationToken)) != null;
            return isUpdated.ToResultSet();
        }

        [PostById]
        public async Task<IResultSet<bool>> Remove([Body] Guid id, CancellationToken cancellationToken = default)
        {
            var isDeleted = await _service.Delete(id, cancellationToken);
            return isDeleted.ToResultSet(); ;
        }
    }
}
