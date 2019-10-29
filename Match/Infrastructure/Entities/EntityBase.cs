using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Match.Infrastructure
{
    public class EntityBase //:EntityBase, IEntityBase, IValidatableObject
    {
        //public virtual int Id { get; set; }

        //public virtual DateTime? CreateTime { get; set; }

        //public virtual DateTime? UpdateTime { get; set; }

        //public virtual int? WriteId { get; set; }

        //public virtual string WriteIp { get; set; }

        //protected EntityBase()
        //{
        //    CreateTime = DateTime.Now;
        //}

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (this.BirthYear < System.DateTime.Now.Year - 72)
        //    {
        //        yield return new ValidationResult("必須在70歲以內", new string[] { "BirthYear" });
        //    }

        //    if (this.BirthYear > System.DateTime.Now.Year - 15)
        //    {
        //        yield return new ValidationResult("必須在15歲以上", new string[] { "BirthYear" });
        //    }

        //}
    }
}
