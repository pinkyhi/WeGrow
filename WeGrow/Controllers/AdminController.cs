﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using WeGrow.Core.Resources;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Interfaces;
using WeGrow.Models.Entities;
using WeGrow.Models.Requests;

namespace WeGrow.Controllers
{
    [Route("admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly IBlobService blobService;

        public AdminController(IRepository repository, IMapper mapper, IBlobService blobService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.blobService = blobService;
        }

        #region Modules

        [Route("modules")]
        [HttpGet]
        public async Task<IActionResult> GetModules()
        {
            var items = await repository.GetRangeAsync<Module>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<ModuleEntity>(x));
            return Ok(itemsEntities);
        }
        [Route("modules")]
        [HttpDelete]
        public async Task<IActionResult> DeleteModule(ModuleEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<Module>(true, x => x.Id == deleteItem.Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("modules")]
        [HttpPost]
        public async Task<IActionResult> AddModule([FromBody] ModuleEntity addItem)
        {
            Module newItem = mapper.Map<Module>(addItem);
            var exemplar = await repository.AddAsync(newItem);
            return Ok(mapper.Map<ModuleEntity>(exemplar));
        }
        
        [Route("modules")]
        [HttpPatch]
        public async Task<IActionResult> EditModule([FromBody] ModuleEntity[] oldAndEditedItem)
        {
            var exemplar = await repository.GetAsync<Module>(true, x => x.Id == oldAndEditedItem[0].Id);
            mapper.Map(oldAndEditedItem[1], exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }

        [Route("modules")]
        [HttpPut]
        public async Task<IActionResult> UploadModuleImage([FromBody] ChangeImageRequest request)
        {
            var exemplar = await repository.GetAsync<Module>(true, x => x.Id == request.ItemId);
            var newFileName = $"module{request.ItemId}{request.File.FileName.Substring(request.File.FileName.LastIndexOf('.'))}";
            try
            {
                var uploadResult = await blobService.UploadFileBlobAsync(ConstNames.Blob.Modules, newFileName, request.File.FileContent, request.File.ContentType);
                var newBlobLink = blobService.GetBlobLinkAsync(ConstNames.Blob.Modules, newFileName);
                exemplar.BlobName = newFileName;
                exemplar.BlobLink = newBlobLink.ToString();
                await repository.UpdateAsync(exemplar);
            }
            catch
            {
                BadRequest();
            }
            return Ok(exemplar.BlobLink);
        }
        #endregion

        #region Receipts

        [Route("receipts")]
        [HttpGet]
        public async Task<IActionResult> GetReceipts()
        {
            var items = await repository.GetRangeAsync<Receipt>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<ReceiptEntity>(x));
            return Ok(itemsEntities);
        }
        [Route("receipts")]
        [HttpDelete]
        public async Task<IActionResult> DeleteReceipt(ReceiptEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<Receipt>(true, x => x.Module_Id == deleteItem.Module_Id && x.Order_Id == deleteItem.Order_Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("receipts")]
        [HttpPost]
        public async Task<IActionResult> AddReceipt([FromBody] ReceiptEntity addItem)
        {
            var newItem = mapper.Map<Receipt>(addItem);
            var exemplar = await repository.AddAsync(newItem);
            return Ok(mapper.Map<ReceiptEntity>(exemplar));
        }

        [Route("receipts")]
        [HttpPatch]
        public async Task<IActionResult> EditReceipt([FromBody] ReceiptEntity[] oldAndEditedItem)
        {
            var exemplar = await repository.GetAsync<Receipt>(true, x => x.Module_Id == oldAndEditedItem[0].Module_Id && x.Order_Id == oldAndEditedItem[0].Order_Id);
            mapper.Map(oldAndEditedItem[1], exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }
        #endregion

        #region Orders
        [Route("orders")]
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var items = await repository.GetRangeAsync<Order>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<OrderEntity>(x));
            return Ok(itemsEntities);
        }
        [Route("orders")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(OrderEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<Order>(true, x => x.Id == deleteItem.Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("orders")]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderEntity addItem)
        {
            var newItem = mapper.Map<Order>(addItem);
            var exemplar = await repository.AddAsync(newItem);
            return Ok(mapper.Map<OrderEntity>(exemplar));
        }

        [Route("orders")]
        [HttpPatch]
        public async Task<IActionResult> EditOrder([FromBody] OrderEntity[] oldAndEditedItem)
        {
            var exemplar = await repository.GetAsync<Order>(true, x => x.Id == oldAndEditedItem[0].Id);
            mapper.Map(oldAndEditedItem[1], exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }
        #endregion

        #region ModuleInstance
        [Route("module-instances")]
        [HttpGet]
        public async Task<IActionResult> GetModuleInstances()
        {
            var items = await repository.GetRangeAsync<ModuleInstance>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<ModuleInstanceEntity>(x));
            return Ok(itemsEntities);
        }
        [Route("module-instances")]
        [HttpDelete]
        public async Task<IActionResult> DeleteModuleInstance(ModuleInstanceEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<ModuleInstance>(true, x => x.Id == deleteItem.Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("module-instances")]
        [HttpPost]
        public async Task<IActionResult> AddModuleInstance([FromBody] ModuleInstanceEntity addItem)
        {
            var newItem = mapper.Map<ModuleInstance>(addItem);
            var exemplar = await repository.AddAsync(newItem);
            return Ok(mapper.Map<ModuleInstanceEntity>(exemplar));
        }

        [Route("module-instances")]
        [HttpPatch]
        public async Task<IActionResult> EditModuleInstance([FromBody] ModuleInstanceEntity[] oldAndEditedItem)
        {
            var exemplar = await repository.GetAsync<ModuleInstance>(true, x => x.Id == oldAndEditedItem[0].Id);
            mapper.Map(oldAndEditedItem[1], exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }
        #endregion

        #region SystemInstance
        [Route("system-instances")]
        [HttpGet]
        public async Task<IActionResult> GetSystemInstances()
        {
            var items = await repository.GetRangeAsync<SystemInstance>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<SystemInstanceEntity>(x));
            return Ok(itemsEntities);
        }
        [Route("system-instances")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSystemInstance(SystemInstanceEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<SystemInstance>(true, x => x.Id == deleteItem.Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("system-instances")]
        [HttpPost]
        public async Task<IActionResult> AddSystemInstance([FromBody] SystemInstanceEntity addItem)
        {
            var newItem = mapper.Map<SystemInstance>(addItem);
            var exemplar = await repository.AddAsync(newItem);
            return Ok(mapper.Map<SystemInstanceEntity>(exemplar));
        }

        [Route("system-instances")]
        [HttpPatch]
        public async Task<IActionResult> EditSystemInstance([FromBody] SystemInstanceEntity[] oldAndEditedItem)
        {
            var exemplar = await repository.GetAsync<SystemInstance>(true, x => x.Id == oldAndEditedItem[0].Id);
            mapper.Map(oldAndEditedItem[1], exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }
        #endregion

        #region Grows
        [Route("grows")]
        [HttpGet]
        public async Task<IActionResult> GetGrows()
        {
            var items = await repository.GetRangeAsync<Grow>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<GrowEntity>(x));
            return Ok(itemsEntities);
        }
        [Route("grows")]
        [HttpDelete]
        public async Task<IActionResult> DeleteGrow(GrowEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<Grow>(true, x => x.Id == deleteItem.Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("grows")]
        [HttpPost]
        public async Task<IActionResult> AddGrow([FromBody] GrowEntity addItem)
        {
            var newItem = mapper.Map<Grow>(addItem);
            var exemplar = await repository.AddAsync(newItem);
            return Ok(mapper.Map<GrowEntity>(exemplar));
        }

        [Route("grows")]
        [HttpPatch]
        public async Task<IActionResult> EditGrow([FromBody] GrowEntity[] oldAndEditedItem)
        {
            var exemplar = await repository.GetAsync<Grow>(true, x => x.Id == oldAndEditedItem[0].Id);
            mapper.Map(oldAndEditedItem[1], exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }
        #endregion

        #region Schedules
        [Route("schedules")]
        [HttpGet]
        public async Task<IActionResult> GetSchedules()
        {
            var items = await repository.GetRangeAsync<Schedule>(false, x => true);
            var itemsEntities = items.Select(x => mapper.Map<ScheduleEntity>(x));
            return Ok(itemsEntities);
        }
        [Route("schedules")]
        [HttpDelete]
        public async Task<IActionResult> DeleteSchedule(ScheduleEntity deleteItem)
        {
            var exemplar = await repository.GetAsync<Schedule>(true, x => x.Id == deleteItem.Id);
            await repository.DeleteAsync(exemplar);
            return Ok();
        }

        [Route("schedules")]
        [HttpPost]
        public async Task<IActionResult> AddSchedule([FromBody] ScheduleEntity addItem)
        {
            var newItem = mapper.Map<Schedule>(addItem);
            var exemplar = await repository.AddAsync(newItem);
            return Ok(mapper.Map<ScheduleEntity>(exemplar));
        }

        [Route("schedules")]
        [HttpPatch]
        public async Task<IActionResult> EditSchedule([FromBody] ScheduleEntity[] oldAndEditedItem)
        {
            var exemplar = await repository.GetAsync<Schedule>(true, x => x.Id == oldAndEditedItem[0].Id);
            mapper.Map(oldAndEditedItem[1], exemplar);
            await repository.UpdateAsync(exemplar);
            return Ok();
        }
        #endregion
    }
}
