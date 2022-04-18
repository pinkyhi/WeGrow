using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeGrow.DAL.Entities;
using WeGrow.DAL.Interfaces;
using WeGrow.Models.Entities;

namespace WeGrow.Controllers
{
    [Route("admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdminController : ControllerBase
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;

        public AdminController(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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
    }
}
