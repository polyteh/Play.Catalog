using Play.Catalog.Service.Repository.Models;
using static Play.Catalog.Service.Models.Dtos;

namespace Play.Catalog.Service.Extensions
{
    public static class Extensions
    {
        public static ItemDto AsDto(this Item item){
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}