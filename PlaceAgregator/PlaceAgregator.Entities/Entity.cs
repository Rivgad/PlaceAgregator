using System.Text.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlaceAgregator.Entities
{
    /// <summary>
    /// Abstract class <see cref="Entity"/> with Id
    /// </summary>
    public abstract class Entity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
    }
}
