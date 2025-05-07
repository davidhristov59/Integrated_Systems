using System.ComponentModel.DataAnnotations;

namespace EShopApplication.Domain.DomainModels;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
}