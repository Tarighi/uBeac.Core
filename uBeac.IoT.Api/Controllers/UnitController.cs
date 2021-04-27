//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using uBeac.Core.Web;
//using uBeac.IoT.Api.DTO;
//using uBeac.IoT.Models;
//using uBeac.IoT.Services;

//namespace uBeac.IoT.Api.Controllers
//{
//    public class UnitController : BaseController
//    {
//        private readonly IUnitService _service;
//        private readonly IMapper _mapper;

//        public UnitController(IMapper mapper, IUnitService service) 
//        {
//            _service = service;
//            _mapper = mapper;
//        }

//        [Post]
//        public async Task<IResultSet<Guid>> Add([Body] UnitAddRequestDTO dto, CancellationToken cancellationToken = default)
//        {
//            var model = _mapper.Map<Unit>(dto);
//            await _service.Insert(model, cancellationToken);
//            return model.Id.ToResultSet();
//        }

//        [Post]
//        public async Task<IResultSet<bool>> Update([Body] UnitUpdateRequestDTO dto, CancellationToken cancellationToken = default)
//        {
//            var model = _mapper.Map<Unit>(dto);
//            var isUpdated = (await _service.Replace(model, cancellationToken)) != null;
//            return isUpdated.ToResultSet();
//        }

//        [Post]
//        public async Task<IResultSet<bool>> UpdateParent([Body] UnitUpdateParentRequestDTO dto, CancellationToken cancellationToken = default)
//        {
//            var model = _mapper.Map<Unit>(dto);
//            var isUpdated = (await _service.Replace(model, cancellationToken)) != null;
//            return isUpdated.ToResultSet();
//        }

//        [PostById]
//        public async Task<IResultSet<bool>> Remove([Body] Guid id, CancellationToken cancellationToken = default)
//        {
//            var isDeleted = await _service.Delete(id, cancellationToken);
//            return isDeleted.ToResultSet(); ;
//        }

//        [GetById]
//        public async Task<IResultSet<Unit>> GetById(Guid id, CancellationToken cancellationToken = default)
//        {
//            var unit = await _service.GetById(id, cancellationToken);
//            return unit.ToResultSet();
//        }

//        [Get]
//        public async Task<IListResultSet<UnitViewDTO>> GetAll(CancellationToken cancellationToken = default)
//        {
//            // todo: deep iteration to find children in repository
//            var units = await _service.GetAll(cancellationToken);
//            var unitsVm = _mapper.Map<List<UnitViewDTO>>(units);
//            return unitsVm.ToListResultSet();
//        }

//    }
//}
