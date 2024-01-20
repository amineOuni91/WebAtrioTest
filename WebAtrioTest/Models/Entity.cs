using System.ComponentModel.DataAnnotations;

namespace WebAtrioTest.Models
{
    public class Entity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}