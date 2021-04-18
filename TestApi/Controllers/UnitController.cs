//using automapper;
//using system;
//using system.linq;
//using system.threading;
//using system.threading.tasks;
//using testapi.dto;
//using testapi.models;
//using testapi.services;
//using ubeac.core.web;

//namespace testapi.controllers
//{
//    public class unitcontroller : basecontroller
//    {
//        private readonly iunitservice _service;
//        private readonly imapper _mapper;

//        public unitcontroller(iunitservice service, imapper mapper)
//        {
//            _service = service;
//            _mapper = mapper;
//        }

//        [post]
//        public async task<iresultset<guid>> add([body] unitaddrequestdto dto, cancellationtoken cancellationtoken = default)
//        {
//            var model = _mapper.map<unit>(dto);
//            await _service.insert(model, cancellationtoken);
//            return model.id.toresultset();
//        }

//        [post]
//        public async task<iresultset<bool>> update([body] unitupdaterequestdto dto, cancellationtoken cancellationtoken = default)
//        {
//            var model = _mapper.map<unit>(dto);
//            var updated = await _service.replace(model, cancellationtoken);
//            return (updated != null).toresultset();
//        }

//        [post]
//        public async task<iresultset<bool>> updateparent([body] unitupdateparentrequestdto dto, cancellationtoken cancellationtoken = default)
//        {
//            var model = _mapper.map<unit>(dto);
//            // todo: implement here
//            //var isupdated = await _service.update(model, cancellationtoken);
//            return false.toresultset();
//        }

//        [postbyid]
//        public async task<iresultset<bool>> remove([body] guid id, cancellationtoken cancellationtoken = default)
//        {
//            var isdeleted = await _service.delete(id, cancellationtoken);
//            return isdeleted.toresultset(); ;
//        }

//        [getbyid]
//        public async task<iresultset<unit>> getbyid(guid id, cancellationtoken cancellationtoken = default)
//        {
//            var unit = await _service.getbyid(id, cancellationtoken);
//            return unit.toresultset();
//        }

//        [get]
//        public async task<ilistresultset<unit>> getall(cancellationtoken cancellationtoken = default)
//        {
//            // todo: deep iteration to find children in repository
//            var units = await _service.getall(cancellationtoken);
//            return units.tolist().tolistresultset();
//        }

//    }
//}
